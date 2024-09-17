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
        private ImageList appImageList = new ImageList(); // ImageList to store application icons
        private string selectedProcessId; // To store the selected Process ID

        private bool isStreaming = false; // To track if audio is currently being streamed
        private AudioProcessor _audioProcessor; // Audio processor

        private double[] _plotData; // Array to hold the Y-values for plotting
        private int _plotMaxPoints = 10000; // Number of points to display in the plot

        public FormMain()
        {
            InitializeComponent();
            InitializeListView();
            LoadRunningApplications();

            // Initially disable the Select button
            btnSelectApp.Enabled = false;

            // Subscribe to the SelectedIndexChanged event for the ListView
            listViewApplications.SelectedIndexChanged += ListViewApplications_SelectedIndexChanged;

            // Set default label text
            SetNoAppSelectedState();

            // Initialize the ScottPlot control and audio processor
            InitializePlot();
            _audioProcessor = new AudioProcessor();
            _audioProcessor.AudioDataAvailable += OnAudioDataAvailable;

            // Handle form closed event
            this.FormClosed += FormMain_FormClosed;
        }

        private void InitializePlot()
        {
            // Set up the array to hold Y-values for the plot
            _plotData = new double[_plotMaxPoints];

            // Add a ScottPlot signal plot to the formsPlot
            formsPlotFilterbank.Plot.Clear();
            formsPlotFilterbank.Plot.Add.Scatter(Enumerable.Range(0, _plotMaxPoints).Select(i => (double)i).ToArray(), _plotData);

            // Set plot axis limits and labels
            formsPlotFilterbank.Plot.Axes.SetLimits(bottom: -32768, top: 32767); // Y-limits for 16-bit PCM range
            formsPlotFilterbank.Plot.Title("Real-Time Audio Visualization");
            formsPlotFilterbank.Plot.YLabel("Amplitude");
            formsPlotFilterbank.Plot.XLabel("Time");

            formsPlotFilterbank.Refresh();
        }

        private void OnAudioDataAvailable(object sender, float[] audioData)
        {
            // Update the plot with new audio data
            UpdatePlot(audioData);
        }

        // Update plot safely from the background thread
        private void UpdatePlot(float[] audioData)
        {
            // Check if the current thread is the UI thread
            if (formsPlotFilterbank.InvokeRequired)
            {
                // If we're on a background thread, marshal the update back to the UI thread
                formsPlotFilterbank.Invoke(new Action(() => UpdatePlot(audioData)));
            }
            else
            {
                // Convert float[] to double[] and update _plotData for ScottPlot
                for (int i = 0; i < Math.Min(audioData.Length, _plotMaxPoints); i++)
                {
                    _plotData[i] = audioData[i]; // ScottPlot uses double[] for plotting
                }

                // Redraw the plot with updated data
                formsPlotFilterbank.Refresh();
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

    }
}
