using CSCore.CoreAudioAPI;
using LedAudioVisualizer.Visualizers;
using System.Diagnostics;

namespace LedAudioVisualizer
{
    public partial class FormMain : Form
    {
        private readonly ImageList appImageList = new();    // ImageList to store application icons
        private string selectedProcessId = "";              // To store the selected Process ID
        private bool isStreaming = false;                   // To track if audio is currently being streamed

        private uint _numLeds = 300;
        private readonly int _transmissionRate = 48;
        private int _delayMs = 0;
        private DateTime _lastTransmissionTime = DateTime.Now;
        private readonly DataTransmission _dataTransmission;

        private AudioListener _audioListener;
        private readonly IAudioVisualizer _audioVisualizer;

        private double[] _filterBankData;                   // Array to hold the Y-values for plotting
        private double[] _redColorData;                     // Array to hold the Y-values for plotting
        private double[] _greenColorData;                   // Array to hold the Y-values for plotting
        private double[] _blueColorData;                    // Array to hold the Y-values for plotting
        private uint _redPowerPercent = 100;
        private uint _greenPowerPercent = 100;
        private uint _bluePowerPercent = 100;
        private readonly int _filterBankPlotMaxPoints = 24000;


        private float _overallPeak = 1.0f;                  // Track the overall peak (initialize to a reasonable value)
        private const float _peakDecayFactor = 0.995f;      // Slow decay over time
        private const int _windowSizeInSeconds = 5;         // Time window to track peak
        private readonly Queue<float> _peakHistory = new(); // Track recent peaks for moving average

        private readonly Dictionary<string, Type> VisualizationTypes = new()
        {
            { "Spectrum", typeof(SpectrumAudioVisualizer) },
        };


        public FormMain()
        {
            InitializeComponent();

            // Initialize UI
            InitializeUI();
            LoadRunningApplications();
            btnSelectApp.Enabled = false; // Initially disable the Select button
            groupBoxSelectedApp.Visible = false;

            _dataTransmission = new DataTransmission("192.168.0.150", 7777); // Customize to your liking. TODO expose to the main form, allow for customization
            _dataTransmission.Connect();
            _dataTransmission.DelayMs = (int)numericUpDownDelay.Value;

            _audioVisualizer = new SpectrumAudioVisualizer();

            ((SpectrumAudioVisualizer)_audioVisualizer).redMinFreq = (int)numericUpDown_RedMin.Value;
            ((SpectrumAudioVisualizer)_audioVisualizer).redMaxFreq = (int)numericUpDown_RedMax.Value;
            ((SpectrumAudioVisualizer)_audioVisualizer).greenMinFreq = (int)numericUpDown_GreenMin.Value;
            ((SpectrumAudioVisualizer)_audioVisualizer).greenMaxFreq = (int)numericUpDown_GreenMax.Value;
            ((SpectrumAudioVisualizer)_audioVisualizer).blueMinFreq = (int)numericUpDown_BlueMin.Value;
            ((SpectrumAudioVisualizer)_audioVisualizer).blueMaxFreq = (int)numericUpDown_BlueMax.Value;

            _filterBankData = new double[_numLeds];
            _redColorData = new double[_numLeds];
            _greenColorData = new double[_numLeds];
            _blueColorData = new double[_numLeds];

            // Handle form closed event
            FormClosed += FormMain_FormClosed;
        }

        private void InitializeUI()
        {
            // Initialize all variables retreived from the UI
            _numLeds = (uint)numericUpDownLedCount.Value;
            _redPowerPercent = (uint)numericUpDown_RedPower.Value;
            _greenPowerPercent = (uint)numericUpDown_GreenPower.Value;
            _bluePowerPercent = (uint)numericUpDown_BluePower.Value;

            // Set up columns for the ListView
            listViewApplications.Items.Clear();
            listViewApplications.Columns.Clear();
            listViewApplications.Columns.Add("Process Name", 150, System.Windows.Forms.HorizontalAlignment.Left);
            listViewApplications.Columns.Add("Window Title", 300, System.Windows.Forms.HorizontalAlignment.Left);
            listViewApplications.Columns.Add("Process ID", 100, System.Windows.Forms.HorizontalAlignment.Left);

            // Set ListView properties
            listViewApplications.View = View.Details;
            listViewApplications.FullRowSelect = true;
            listViewApplications.GridLines = true;

            // Set up ImageList for icons
            appImageList.ImageSize = new Size(32, 32); // Set the size of the icons
            listViewApplications.SmallImageList = appImageList; // Assign the ImageList to the ListView

            // Add all of the visualization types
            ComboBoxVisualziationTypes.Items.AddRange(VisualizationTypes.Keys.ToArray());
            if (ComboBoxVisualziationTypes.Items.Count > 0)
            {
                ComboBoxVisualziationTypes.SelectedIndex = 0;
            }

            // Frequency analysis graph
            formsPlotFilterbank.Plot.Title("Frequency Spectrum");
            formsPlotFilterbank.Plot.YLabel("Amplitude");
            formsPlotFilterbank.Plot.XLabel("Frequency (Hz)");
            _filterBankData = new double[_filterBankPlotMaxPoints];
            formsPlotFilterbank.Plot.Add.Signal(_filterBankData);
            AutoScaleFrequencyPlot();
            formsPlotFilterbank.Refresh();

            // Color visualization graph
            formsPlotVisualization.Plot.Title("Color Visualization");
            formsPlotVisualization.Plot.YLabel("Brightness");
            formsPlotVisualization.Plot.XLabel("LED");
            _redColorData = new double[_numLeds];
            _greenColorData = new double[_numLeds];
            _blueColorData = new double[_numLeds];
            formsPlotVisualization.Plot.Add.Signal(_redColorData, color: ScottPlot.Color.FromColor(Color.Red));
            formsPlotVisualization.Plot.Add.Signal(_greenColorData, color: ScottPlot.Color.FromColor(Color.Green));
            formsPlotVisualization.Plot.Add.Signal(_blueColorData, color: ScottPlot.Color.FromColor(Color.Blue));
            AutoScaleColorPlot();
            formsPlotVisualization.Refresh();
        }

        public void AutoScaleFrequencyPlot()
        {
            // Add 10% to each side
            formsPlotFilterbank.Plot.Axes
                .SetLimits(0 - 0.1 * _filterBankPlotMaxPoints,
                _filterBankPlotMaxPoints + 0.1 * _filterBankPlotMaxPoints,
                -0.10,
                1.10
            );
        }

        public void AutoScaleColorPlot()
        {
            // Add 10% to each side
            formsPlotVisualization.Plot.Axes
                .SetLimits(0 - 0.1 * _numLeds,
                _numLeds + 0.1 * _numLeds,
                -0.10,
                1.10
            );
        }

        private void OnAudioDataAvailable(object? sender, float[] audioData)
        {
            if (formsPlotFilterbank.InvokeRequired)
            {
                formsPlotFilterbank.Invoke(new Action(() => OnAudioDataAvailable(null, audioData)));
                return;
            }

            // Process rise, decay, and normalization for the filter bank
            ProcessRiseAndDecay(audioData);

            // Get the Visualization Data from the frequency spectrum
            (double[] red, double[] green, double[] blue) = _audioVisualizer.VisualizeAudio(audioData, _numLeds, _audioListener.SampleRate);

            Color[] colors = new Color[_redColorData.Length];
            for (int i = 0; i < _redColorData.Length && i < red.Length; i++)
            {
                _redColorData[i] = red[i] * _redPowerPercent / 100.0;
                _greenColorData[i] = green[i] * _greenPowerPercent / 100.0;
                _blueColorData[i] = blue[i] * _bluePowerPercent / 100.0;

                colors[i] = Color.FromArgb(0xFF, (byte)(_redColorData[i] * 0xFF), (byte)(_greenColorData[i] * 0xFF), (byte)(_blueColorData[i] * 0xFF));
            }

            _ = TrySendDataAsync(colors);
            formsPlotFilterbank.Refresh();
            formsPlotVisualization.Refresh();
        }

        // Separate function for handling rise, decay, peak tracking, and normalization
        private void ProcessRiseAndDecay(float[] audioData)
        {
            const float decayFactor = 0.99f;   // Slow decay factor
            const float riseFactor = 0.95f;    // Fast rise factor

            // Track the current peak and update peak history
            float currentPeak = audioData.Max();
            _peakHistory.Enqueue(currentPeak);

            int maxHistorySize = (int)(_windowSizeInSeconds * _audioListener.SampleRate / audioData.Length);
            if (_peakHistory.Count > maxHistorySize)
                _peakHistory.Dequeue();

            // Calculate rolling peak and overall peak with decay
            float rollingPeak = _peakHistory.Max();
            _overallPeak = rollingPeak > _overallPeak ? rollingPeak : _overallPeak * _peakDecayFactor;

            // Apply rise/decay and normalize the data points
            for (int i = 0; i < Math.Min(audioData.Length, _filterBankPlotMaxPoints); i++)
            {
                float newDataPoint = audioData[i];

                // Apply rise or decay based on new data point
                if (newDataPoint > audioData[i])
                {
                    // Quick rise
                    audioData[i] = audioData[i] * (1 - riseFactor) + newDataPoint * riseFactor;
                }
                else
                {
                    // Slow decay
                    audioData[i] = audioData[i] * decayFactor;
                }

                // Normalize by the overall peak and update filter bank data
                _filterBankData[i] = audioData[i] / _overallPeak;
                audioData[i] = (float)_filterBankData[i]; // Copy back normalized value
            }
        }

        private async Task TrySendDataAsync(Color[] colors)
        {
            double interval = 1000.0 / _transmissionRate;
        
            double timeElapsedSinceLastPacket = (DateTime.Now - _lastTransmissionTime).TotalMilliseconds;

            
            if (timeElapsedSinceLastPacket >= interval)
            {
                if (_delayMs > 0)
                {
                    await Task.Delay(_delayMs); 
                }
                _dataTransmission.SendData(colors);
                _lastTransmissionTime = DateTime.Now;
            }
        }

        private void LoadRunningApplications()
        {
            listViewApplications.Items.Clear();
            appImageList.Images.Clear();

            var processes = Process.GetProcesses().Where(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle)).ToList();

            foreach (var process in processes)
            {
                try
                {
                    Icon appIcon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);

                    // Add the icon to the ImageList, use the process ID as the key
                    appImageList.Images.Add(process.Id.ToString(), appIcon);

                    var listItem = new ListViewItem(new string[]
                    {
                        process.ProcessName,        // Process name
                        process.MainWindowTitle,    // Window title
                        process.Id.ToString()       // Process ID
                    });

                    listItem.ImageKey = process.Id.ToString();
                    listViewApplications.Items.Add(listItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not retrieve icon for process {process.ProcessName}: {ex.Message}");
                }
            }

            if (!string.IsNullOrEmpty(selectedProcessId))
            {
                foreach (ListViewItem item in listViewApplications.Items)
                {
                    if (item.SubItems[2].Text == selectedProcessId)
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                        break;
                    }
                }
            }
        }

        private void DisplaySelectedAppDetails()
        {
            if (listViewApplications.SelectedItems.Count > 0)
            {
                groupBoxSelectedApp.Visible = true;
                var selectedItem = listViewApplications.SelectedItems[0];

                string processName = selectedItem.SubItems[0].Text;
                string windowTitle = selectedItem.SubItems[1].Text;
                int processId = int.Parse(selectedItem.SubItems[2].Text);

                labelAppName.Text = $"Process Name: {processName}";
                labelAppWindowTitle.Text = $"Window Title: {windowTitle}";
                labelAppProcessId.Text = $"Process ID: {processId}";

                if (appImageList.Images.ContainsKey(processId.ToString()))
                {
                    pictureBoxAppIcon.Image = appImageList.Images[processId.ToString()];
                }
                else
                {
                    pictureBoxAppIcon.Image = null;
                }
            }
        }


        #region UI Events

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Stop audio capture and exit the application
            _audioListener.StopAudioCapture();
            Application.Exit();
        }

        private void ListViewApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewApplications.SelectedItems.Count > 0)
            {
                if (!isStreaming)
                {
                    btnSelectApp.Enabled = true;
                }
                selectedProcessId = listViewApplications.SelectedItems[0].SubItems[2].Text;
            }
            else
            {
                btnSelectApp.Enabled = false;
            }
        }

        private void ButtonRefreshApps_Click(object sender, EventArgs e)
        {
            LoadRunningApplications();
            btnSelectApp.Enabled = false;
        }

        private void ButttonSelectApp_Click(object sender, EventArgs e)
        {
            if (!isStreaming && listViewApplications.SelectedItems.Count > 0)
            {
                DisplaySelectedAppDetails();
                using (MMDeviceEnumerator mmDeviceEnumerator = new())
                {
                    MMDevice _audioDevice = mmDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                    _audioListener = new(_audioDevice, OnAudioDataAvailable);
                }
                _audioListener.StopAudioCapture();
                _audioListener.StartAudioCapture();
                isStreaming = true;

                // Disable the "Select" button and enable the "Cancel" button
                btnSelectApp.Enabled = false;
                btnCancel.Enabled = true;
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (isStreaming)
            {
                _audioListener.StopAudioCapture();
                isStreaming = false;
                btnCancel.Enabled = false;
                btnSelectApp.Enabled = true;
            }
        }

        private void numericUpDownLedCount_ValueChanged(object sender, EventArgs e)
        {
            _numLeds = (uint)numericUpDownLedCount.Value;
            formsPlotVisualization.Plot.Clear();
            _redColorData = new double[_numLeds];
            _greenColorData = new double[_numLeds];
            _blueColorData = new double[_numLeds];
            formsPlotVisualization.Plot.Add.Signal(_redColorData, color: ScottPlot.Color.FromColor(Color.Red));
            formsPlotVisualization.Plot.Add.Signal(_greenColorData, color: ScottPlot.Color.FromColor(Color.Green));
            formsPlotVisualization.Plot.Add.Signal(_blueColorData, color: ScottPlot.Color.FromColor(Color.Blue));
            AutoScaleColorPlot();
            formsPlotVisualization.Refresh();
        }

        private void numericUpDown_RedMin_ValueChanged(object sender, EventArgs e)
        {
            // Ensure RedMax is greater than or equal to RedMin
            if (numericUpDown_RedMin.Value >= numericUpDown_RedMax.Value)
            {
                numericUpDown_RedMax.Value = numericUpDown_RedMin.Value + 1;
            }

            ((SpectrumAudioVisualizer)_audioVisualizer).redMinFreq = (int)numericUpDown_RedMin.Value;
        }

        private void numericUpDown_RedMax_ValueChanged(object sender, EventArgs e)
        {
            // Ensure RedMax is greater than or equal to RedMin
            if (numericUpDown_RedMax.Value <= numericUpDown_RedMin.Value)
            {
                numericUpDown_RedMin.Value = numericUpDown_RedMax.Value - 1;
            }

            // Ensure GreenMin is greater than or equal to RedMax
            if (numericUpDown_GreenMin.Value <= numericUpDown_RedMax.Value)
            {
                numericUpDown_GreenMin.Value = numericUpDown_RedMax.Value + 1;
            }

            ((SpectrumAudioVisualizer)_audioVisualizer).redMaxFreq = (int)numericUpDown_RedMax.Value;
        }

        private void numericUpDown_GreenMin_ValueChanged(object sender, EventArgs e)
        {
            // Ensure GreenMin is greater than or equal to RedMax
            if (numericUpDown_GreenMin.Value <= numericUpDown_RedMax.Value)
            {
                numericUpDown_GreenMin.Value = numericUpDown_RedMax.Value + 1;
            }

            ((SpectrumAudioVisualizer)_audioVisualizer).greenMinFreq = (int)numericUpDown_GreenMin.Value;
        }

        private void numericUpDown_GreenMax_ValueChanged(object sender, EventArgs e)
        {
            // Ensure GreenMax is greater than or equal to GreenMin
            if (numericUpDown_GreenMax.Value <= numericUpDown_GreenMin.Value)
            {
                numericUpDown_GreenMin.Value = numericUpDown_GreenMax.Value - 1;
            }

            // Ensure BlueMin is greater than or equal to GreenMax
            if (numericUpDown_BlueMin.Value <= numericUpDown_GreenMax.Value)
            {
                numericUpDown_BlueMin.Value = numericUpDown_GreenMax.Value + 1;
            }

            ((SpectrumAudioVisualizer)_audioVisualizer).greenMaxFreq = (int)numericUpDown_GreenMax.Value;
        }

        private void numericUpDown_BlueMin_ValueChanged(object sender, EventArgs e)
        {
            // Ensure BlueMin is greater than or equal to GreenMax
            if (numericUpDown_BlueMin.Value <= numericUpDown_GreenMax.Value)
            {
                numericUpDown_BlueMin.Value = numericUpDown_GreenMax.Value + 1;
            }

            ((SpectrumAudioVisualizer)_audioVisualizer).blueMinFreq = (int)numericUpDown_BlueMin.Value;
        }

        private void numericUpDown_BlueMax_ValueChanged(object sender, EventArgs e)
        {
            // Ensure BlueMax is greater than or equal to BlueMin
            if (numericUpDown_BlueMax.Value <= numericUpDown_BlueMin.Value)
            {
                numericUpDown_BlueMin.Value = numericUpDown_BlueMax.Value - 1;
            }

            ((SpectrumAudioVisualizer)_audioVisualizer).blueMaxFreq = (int)numericUpDown_BlueMax.Value;
        }

        private void radioButtonDataTransmissionRate_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                _dataTransmission.TransmissionRate = byte.Parse(((RadioButton)sender).Text.Substring(0, 2));
            }
        }


        private void numericUpDown_RedPower_ValueChanged(object sender, EventArgs e)
        {
            _redPowerPercent = (uint)numericUpDown_RedPower.Value;
        }

        private void numericUpDown_GreenPower_ValueChanged(object sender, EventArgs e)
        {
            _greenPowerPercent = (uint)numericUpDown_GreenPower.Value;
        }

        private void numericUpDown_BluePower_ValueChanged(object sender, EventArgs e)
        {
            _bluePowerPercent = (uint)numericUpDown_BluePower.Value;
        }

        private void numericUpDownDelay_ValueChanged(object sender, EventArgs e)
        {
            _delayMs = (int)numericUpDownDelay.Value;
        }

        #endregion
    }
}
