using ScottPlot;

using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using ScottPlot.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedAudioVisualizer
{
    public partial class FormMain : Form
    {
        private ImageList appImageList = new ImageList();   // ImageList to store application icons
        private string selectedProcessId;                   // To store the selected Process ID

        private bool isStreaming = false;                   // To track if audio is currently being streamed
        private AudioProcessor _audioProcessor;             // Audio processor

        private double[] _filterBankData;                   // Array to hold the Y-values for plotting
        private double[] _redColorData;                     // Array to hold the Y-values for plotting
        private double[] _greenColorData;                   // Array to hold the Y-values for plotting
        private double[] _blueColorData;                    // Array to hold the Y-values for plotting
        private int _filterBankPlotMaxPoints = 20000;        // Human hearing is from 20Hz - 20kHz
        private int _colorDataPlotMaxPoints;                // Number of LEDS TODO change 

        // Refernces to the veritcal lines
        private ScottPlot.Plottables.VerticalLine redMinLine;
        private ScottPlot.Plottables.VerticalLine redMaxLine;
        private ScottPlot.Plottables.VerticalLine greenMinLine;
        private ScottPlot.Plottables.VerticalLine greenMaxLine;
        private ScottPlot.Plottables.VerticalLine blueMinLine;
        private ScottPlot.Plottables.VerticalLine blueMaxLine;

        private float overallPeak = 1.0f;
        private float decayFactor = 0.99f;  // Controls how fast the peak value decays
        private float riseFactor = 0.01f;   // Controls how fast the peak value increases
        private const float minPeakValue = 0.01f;  // To prevent division by zero


        public FormMain()
        {
            InitializeComponent();
            InitializeListView();
            LoadRunningApplications();
            _audioProcessor = new AudioProcessor();

            // Initially disable the Select button
            btnSelectApp.Enabled = false;

            // Subscribe to the SelectedIndexChanged event for the ListView
            listViewApplications.SelectedIndexChanged += ListViewApplications_SelectedIndexChanged;

            // Set default label text
            SetNoAppSelectedState();

            // Initialize the ScottPlot control and audio processor
            InitializeUI();
            _audioProcessor.AudioDataAvailable += OnAudioDataAvailable;
            _audioProcessor.ColorDataAvailable += OnColorDataAvailable;

            // Handle form closed event
            this.FormClosed += FormMain_FormClosed;
        }

        private void InitializeUI()
        {
            // Initialize NUM LEDS
            _colorDataPlotMaxPoints = (int)numericUpDownLedCount.Value;
            _audioProcessor.NumLeds = _colorDataPlotMaxPoints;


            // Set plot axis limits and labels
            formsPlotFilterbank.Plot.Title("Real-Time Audio Visualization");
            formsPlotFilterbank.Plot.YLabel("Amplitude");
            formsPlotFilterbank.Plot.XLabel("Frequency (Hz)");

            // Set up the array to hold Y-values for the plot
            _filterBankData = new double[_filterBankPlotMaxPoints];

            // Add a ScottPlot signal plot to the formsPlot
            formsPlotFilterbank.Plot.Add.Signal(_filterBankData);
            redMinLine = formsPlotFilterbank.Plot.Add.VerticalLine((double)numericUpDown_RedMin.Value, color: ScottPlot.Color.FromColor(System.Drawing.Color.Red));
            redMaxLine = formsPlotFilterbank.Plot.Add.VerticalLine((double)numericUpDown_RedMax.Value, color: ScottPlot.Color.FromColor(System.Drawing.Color.Red));
            greenMinLine = formsPlotFilterbank.Plot.Add.VerticalLine((double)numericUpDown_GreenMin.Value, color: ScottPlot.Color.FromColor(System.Drawing.Color.Green));
            greenMaxLine = formsPlotFilterbank.Plot.Add.VerticalLine((double)numericUpDown_GreenMax.Value, color: ScottPlot.Color.FromColor(System.Drawing.Color.Green));
            blueMinLine = formsPlotFilterbank.Plot.Add.VerticalLine((double)numericUpDown_BlueMin.Value, color: ScottPlot.Color.FromColor(System.Drawing.Color.Blue));
            blueMaxLine = formsPlotFilterbank.Plot.Add.VerticalLine((double)numericUpDown_BlueMax.Value, color: ScottPlot.Color.FromColor(System.Drawing.Color.Blue));

            formsPlotFilterbank.Refresh();



            // Set plot axis limits and labels
            formsPlotVisualization.Plot.Title("Color Visualization");
            formsPlotVisualization.Plot.YLabel("Brightness");
            formsPlotVisualization.Plot.XLabel("LED");

            // Set up the array to hold Y-values for the plot
            _redColorData = new double[_colorDataPlotMaxPoints];
            _greenColorData = new double[_colorDataPlotMaxPoints];
            _blueColorData = new double[_colorDataPlotMaxPoints];

            // Add a ScottPlot signal plot to the formsPlot
            formsPlotVisualization.Plot.Add.Signal(_redColorData, color: ScottPlot.Color.FromColor(System.Drawing.Color.Red));
            formsPlotVisualization.Plot.Add.Signal(_greenColorData, color: ScottPlot.Color.FromColor(System.Drawing.Color.Green));
            formsPlotVisualization.Plot.Add.Signal(_blueColorData, color: ScottPlot.Color.FromColor(System.Drawing.Color.Blue));

            formsPlotVisualization.Refresh();


        }


        private void OnAudioDataAvailable(object sender, float[] audioData)
        {
            // Update the plot with new audio data
            UpdateFilterBankPlot(audioData);
        }

        // Update plot safely from the background thread
        private void UpdateFilterBankPlot(float[] audioData)
        {
            // Check if the current thread is the UI thread
            if (formsPlotFilterbank.InvokeRequired)
            {
                // If we're on a background thread, marshal the update back to the UI thread
                formsPlotFilterbank.Invoke(new Action(() => UpdateFilterBankPlot(audioData)));
            }
            else
            {
                // Convert float[] to double[] and update _filterBankData for ScottPlot
                for (int i = 0; i < Math.Min(audioData.Length, _filterBankPlotMaxPoints); i++)
                {
                    _filterBankData[i] = audioData[i]; // ScottPlot uses double[] for plotting
                }

                // Redraw the plot with updated data
                formsPlotFilterbank.Refresh();
            }
        }

        private void OnColorDataAvailable(object sender, (float[], float[], float[]) audioData)
        {
            // Update the plot with new audio data
            UpdateColorPlot(audioData.Item1, audioData.Item2, audioData.Item3);
        }

        // Update plot safely from the background thread
        private void UpdateColorPlot(float[] red, float[] green, float[] blue)
        {
            // Check if the current thread is the UI thread
            if (formsPlotVisualization.InvokeRequired)
            {
                formsPlotVisualization.Invoke(new Action(() => UpdateColorPlot(red, green, blue)));
            }
            else
            {
                // Find the maximum value across all channels in the current update
                float maxCurrent = Math.Max(red.Max(), Math.Max(green.Max(), blue.Max()));

                // Update the overall peak with a combination of rising fast and decaying slow
                overallPeak = Math.Max(overallPeak * decayFactor, maxCurrent * riseFactor + overallPeak * (1 - riseFactor));

                // Prevent peak from going too low (avoid division by zero or extremely small values)
                overallPeak = Math.Max(overallPeak, minPeakValue);

                // Normalize all color data based on the overall peak
                for (int i = 0; i < _redColorData.Length && i < red.Length; i++)
                {
                    _redColorData[i] = red[i] / overallPeak;
                }

                for (int i = 0; i < _greenColorData.Length && i < green.Length; i++)
                {
                    _greenColorData[i] = green[i] / overallPeak;
                }

                for (int i = 0; i < _blueColorData.Length && i < blue.Length; i++)
                {
                    _blueColorData[i] = blue[i] / overallPeak;
                }

                // Redraw the plot with updated data
                formsPlotVisualization.Refresh();
            }
        }



        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Stop audio capture and exit the application
            _audioProcessor.StopAudioCapture();
            Application.Exit();
        }

        private void InitializeListView()
        {
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
        }

        private void LoadRunningApplications()
        {
            listViewApplications.Items.Clear(); // Clear the ListView
            appImageList.Images.Clear();        // Clear the ImageList

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
                        process.ProcessName,   // Process name
                        process.MainWindowTitle, // Window title
                        process.Id.ToString()   // Process ID
                    });

                    listItem.ImageKey = process.Id.ToString();
                    listViewApplications.Items.Add(listItem);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Could not retrieve icon for process {process.ProcessName}: {ex.Message}");
                }
            }

            RestorePreviousSelection();
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

        private void SetNoAppSelectedState()
        {
            groupBoxSelectedApp.Visible = false;
        }

        private void buttonRefreshApps_Click(object sender, EventArgs e)
        {
            InitializeListView();
            LoadRunningApplications();
            btnSelectApp.Enabled = false;
        }

        private void RestorePreviousSelection()
        {
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

        private void btnSelectApp_Click(object sender, EventArgs e)
        {
            if (!isStreaming && listViewApplications.SelectedItems.Count > 0)
            {
                DisplaySelectedAppDetails();
                StartAudioCaptureForApp();
                isStreaming = true;

                // Disable the "Select" button and enable the "Cancel" button
                btnSelectApp.Enabled = false;
                btnCancel.Enabled = true;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isStreaming)
            {
                _audioProcessor.StopAudioCapture();
                isStreaming = false;

                // Enable the "Select" button and disable the "Cancel" button
                btnCancel.Enabled = false;
                btnSelectApp.Enabled = true;
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

        private void StartAudioCaptureForApp()
        {
            _audioProcessor.StopAudioCapture();
            _audioProcessor.StartAudioCapture();
        }

        private void numericUpDownLedCount_ValueChanged(object sender, EventArgs e)
        {
            _audioProcessor.NumLeds = (int)numericUpDownLedCount.Value;
            formsPlotVisualization.Plot.Clear();
            _redColorData = new double[_audioProcessor.NumLeds];
            _greenColorData = new double[_audioProcessor.NumLeds];
            _blueColorData = new double[_audioProcessor.NumLeds];
            formsPlotVisualization.Plot.Add.Signal(_redColorData, color: ScottPlot.Color.FromColor(System.Drawing.Color.Red));
            formsPlotVisualization.Plot.Add.Signal(_greenColorData, color: ScottPlot.Color.FromColor(System.Drawing.Color.Green));
            formsPlotVisualization.Plot.Add.Signal(_blueColorData, color: ScottPlot.Color.FromColor(System.Drawing.Color.Blue));
        }

        private void numericUpDown_RedMin_ValueChanged(object sender, EventArgs e)
        {
            // Ensure RedMax is greater than or equal to RedMin
            if (numericUpDown_RedMin.Value >= numericUpDown_RedMax.Value)
            {
                numericUpDown_RedMax.Value = numericUpDown_RedMin.Value + 1;
            }

            // Update the redMin frequency in _audioProcessor
            _audioProcessor.redMinFreq = (int)numericUpDown_RedMin.Value;

            // Update the position of the redMinLine
            redMinLine.X = (double)numericUpDown_RedMin.Value;
            formsPlotFilterbank.Refresh();  // Refresh the plot to update the display
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

            // Update the redMax frequency in _audioProcessor
            _audioProcessor.redMaxFreq = (int)numericUpDown_RedMax.Value;

            // Update the position of the redMaxLine
            redMaxLine.X = (double)numericUpDown_RedMax.Value;
            formsPlotFilterbank.Refresh();  // Refresh the plot to update the display
        }

        private void numericUpDown_GreenMin_ValueChanged(object sender, EventArgs e)
        {
            // Ensure GreenMin is greater than or equal to RedMax
            if (numericUpDown_GreenMin.Value <= numericUpDown_RedMax.Value)
            {
                numericUpDown_GreenMin.Value = numericUpDown_RedMax.Value + 1;
            }

            // Update the greenMin frequency in _audioProcessor
            _audioProcessor.greenMinFreq = (int)numericUpDown_GreenMin.Value;

            // Update the position of the greenMinLine
            greenMinLine.X = (double)numericUpDown_GreenMin.Value;
            formsPlotFilterbank.Refresh();  // Refresh the plot to update the display
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

            // Update the greenMax frequency in _audioProcessor
            _audioProcessor.greenMaxFreq = (int)numericUpDown_GreenMax.Value;

            // Update the position of the greenMaxLine
            greenMaxLine.X = (double)numericUpDown_GreenMax.Value;
            formsPlotFilterbank.Refresh();  // Refresh the plot to update the display
        }

        private void numericUpDown_BlueMin_ValueChanged(object sender, EventArgs e)
        {
            // Ensure BlueMin is greater than or equal to GreenMax
            if (numericUpDown_BlueMin.Value <= numericUpDown_GreenMax.Value)
            {
                numericUpDown_BlueMin.Value = numericUpDown_GreenMax.Value + 1;
            }

            // Update the blueMin frequency in _audioProcessor
            _audioProcessor.blueMinFreq = (int)numericUpDown_BlueMin.Value;

            // Update the position of the blueMinLine
            blueMinLine.X = (double)numericUpDown_BlueMin.Value;
            formsPlotFilterbank.Refresh();  // Refresh the plot to update the display
        }

        private void numericUpDown_BlueMax_ValueChanged(object sender, EventArgs e)
        {
            // Ensure BlueMax is greater than or equal to BlueMin
            if (numericUpDown_BlueMax.Value <= numericUpDown_BlueMin.Value)
            {
                numericUpDown_BlueMin.Value = numericUpDown_BlueMax.Value - 1;
            }

            // Update the blueMax frequency in _audioProcessor
            _audioProcessor.blueMaxFreq = (int)numericUpDown_BlueMax.Value;

            // Update the position of the blueMaxLine
            blueMaxLine.X = (double)numericUpDown_BlueMax.Value;
            formsPlotFilterbank.Refresh();  // Refresh the plot to update the display
        }



    }
}
