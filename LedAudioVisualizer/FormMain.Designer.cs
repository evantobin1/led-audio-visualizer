namespace LedAudioVisualizer
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ListViewItem listViewItem5 = new ListViewItem("Test1");
            ListViewItem listViewItem6 = new ListViewItem("Test2");
            buttonRefreshApps = new Button();
            listViewApplications = new ListView();
            btnSelectApp = new Button();
            pictureBoxAppIcon = new PictureBox();
            labelAppName = new Label();
            labelAppWindowTitle = new Label();
            groupBoxSelectedApp = new GroupBox();
            labelAppProcessId = new Label();
            btnCancel = new Button();
            groupBoxAudioAnalyzer = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            formsPlotFilterbank = new ScottPlot.WinForms.FormsPlot();
            formsPlotVisualization = new ScottPlot.WinForms.FormsPlot();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAppIcon).BeginInit();
            groupBoxSelectedApp.SuspendLayout();
            groupBoxAudioAnalyzer.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonRefreshApps
            // 
            buttonRefreshApps.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRefreshApps.FlatStyle = FlatStyle.Flat;
            buttonRefreshApps.Location = new Point(580, 14);
            buttonRefreshApps.Name = "buttonRefreshApps";
            buttonRefreshApps.Size = new Size(104, 35);
            buttonRefreshApps.TabIndex = 1;
            buttonRefreshApps.Text = "Refresh Apps";
            buttonRefreshApps.UseVisualStyleBackColor = true;
            buttonRefreshApps.Click += buttonRefreshApps_Click;
            // 
            // listViewApplications
            // 
            listViewApplications.BackColor = Color.FromArgb(67, 73, 83);
            listViewApplications.Dock = DockStyle.Fill;
            listViewApplications.ForeColor = Color.FromArgb(245, 246, 250);
            listViewApplications.Items.AddRange(new ListViewItem[] { listViewItem5, listViewItem6 });
            listViewApplications.Location = new Point(3, 19);
            listViewApplications.Name = "listViewApplications";
            listViewApplications.Size = new Size(691, 531);
            listViewApplications.TabIndex = 2;
            listViewApplications.UseCompatibleStateImageBehavior = false;
            listViewApplications.View = View.Tile;
            // 
            // btnSelectApp
            // 
            btnSelectApp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSelectApp.FlatStyle = FlatStyle.Flat;
            btnSelectApp.Location = new Point(580, 98);
            btnSelectApp.Name = "btnSelectApp";
            btnSelectApp.Size = new Size(104, 35);
            btnSelectApp.TabIndex = 3;
            btnSelectApp.Text = "Listen To Audio";
            btnSelectApp.UseVisualStyleBackColor = true;
            btnSelectApp.Click += btnSelectApp_Click;
            // 
            // pictureBoxAppIcon
            // 
            pictureBoxAppIcon.Location = new Point(6, 22);
            pictureBoxAppIcon.Name = "pictureBoxAppIcon";
            pictureBoxAppIcon.Size = new Size(100, 50);
            pictureBoxAppIcon.TabIndex = 4;
            pictureBoxAppIcon.TabStop = false;
            // 
            // labelAppName
            // 
            labelAppName.AutoSize = true;
            labelAppName.Location = new Point(6, 75);
            labelAppName.Name = "labelAppName";
            labelAppName.Size = new Size(38, 15);
            labelAppName.TabIndex = 5;
            labelAppName.Text = "label1";
            // 
            // labelAppWindowTitle
            // 
            labelAppWindowTitle.AutoSize = true;
            labelAppWindowTitle.Location = new Point(6, 90);
            labelAppWindowTitle.Name = "labelAppWindowTitle";
            labelAppWindowTitle.Size = new Size(38, 15);
            labelAppWindowTitle.TabIndex = 6;
            labelAppWindowTitle.Text = "label2";
            // 
            // groupBoxSelectedApp
            // 
            groupBoxSelectedApp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxSelectedApp.Controls.Add(labelAppProcessId);
            groupBoxSelectedApp.Controls.Add(pictureBoxAppIcon);
            groupBoxSelectedApp.Controls.Add(labelAppWindowTitle);
            groupBoxSelectedApp.Controls.Add(labelAppName);
            groupBoxSelectedApp.Location = new Point(6, 3);
            groupBoxSelectedApp.Name = "groupBoxSelectedApp";
            groupBoxSelectedApp.Size = new Size(568, 133);
            groupBoxSelectedApp.TabIndex = 7;
            groupBoxSelectedApp.TabStop = false;
            // 
            // labelAppProcessId
            // 
            labelAppProcessId.AutoSize = true;
            labelAppProcessId.Location = new Point(6, 105);
            labelAppProcessId.Name = "labelAppProcessId";
            labelAppProcessId.Size = new Size(38, 15);
            labelAppProcessId.TabIndex = 7;
            labelAppProcessId.Text = "label3";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Enabled = false;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(580, 55);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(104, 35);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // groupBoxAudioAnalyzer
            // 
            groupBoxAudioAnalyzer.Controls.Add(tableLayoutPanel4);
            groupBoxAudioAnalyzer.Dock = DockStyle.Fill;
            groupBoxAudioAnalyzer.ForeColor = Color.FromArgb(245, 246, 250);
            groupBoxAudioAnalyzer.Location = new Point(3, 3);
            groupBoxAudioAnalyzer.Name = "groupBoxAudioAnalyzer";
            groupBoxAudioAnalyzer.Size = new Size(698, 534);
            groupBoxAudioAnalyzer.TabIndex = 8;
            groupBoxAudioAnalyzer.TabStop = false;
            groupBoxAudioAnalyzer.Text = "Audio Analyzer";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(formsPlotFilterbank, 0, 0);
            tableLayoutPanel4.Controls.Add(formsPlotVisualization, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(692, 512);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // formsPlotFilterbank
            // 
            formsPlotFilterbank.DisplayScale = 1F;
            formsPlotFilterbank.Dock = DockStyle.Fill;
            formsPlotFilterbank.Location = new Point(3, 3);
            formsPlotFilterbank.Name = "formsPlotFilterbank";
            formsPlotFilterbank.Size = new Size(686, 250);
            formsPlotFilterbank.TabIndex = 0;
            // 
            // formsPlotVisualization
            // 
            formsPlotVisualization.DisplayScale = 1F;
            formsPlotVisualization.Dock = DockStyle.Fill;
            formsPlotVisualization.Location = new Point(3, 259);
            formsPlotVisualization.Name = "formsPlotVisualization";
            formsPlotVisualization.Size = new Size(686, 250);
            formsPlotVisualization.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(listViewApplications);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.ForeColor = Color.FromArgb(245, 246, 250);
            groupBox2.Location = new Point(3, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(697, 553);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "App Selection";
            // 
            // groupBox1
            // 
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.ForeColor = Color.FromArgb(245, 246, 250);
            groupBox1.Location = new Point(3, 543);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(698, 159);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Data Transfer";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1419, 711);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(panel1, 0, 1);
            tableLayoutPanel3.Controls.Add(groupBox2, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 79.29078F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 20.709219F));
            tableLayoutPanel3.Size = new Size(703, 705);
            tableLayoutPanel3.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSelectApp);
            panel1.Controls.Add(groupBoxSelectedApp);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(buttonRefreshApps);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 562);
            panel1.Name = "panel1";
            panel1.Size = new Size(697, 140);
            panel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(groupBoxAudioAnalyzer, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(712, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 76.72131F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 23.2786884F));
            tableLayoutPanel2.Size = new Size(704, 705);
            tableLayoutPanel2.TabIndex = 13;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(47, 54, 64);
            ClientSize = new Size(1419, 711);
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.FromArgb(245, 246, 250);
            Name = "FormMain";
            Text = "Audio Visualizer";
            ((System.ComponentModel.ISupportInitialize)pictureBoxAppIcon).EndInit();
            groupBoxSelectedApp.ResumeLayout(false);
            groupBoxSelectedApp.PerformLayout();
            groupBoxAudioAnalyzer.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button buttonRefreshApps;
        private ListView listViewApplications;
        private Button btnSelectApp;
        private PictureBox pictureBoxAppIcon;
        private Label labelAppName;
        private Label labelAppWindowTitle;
        private GroupBox groupBoxSelectedApp;
        private Label labelAppProcessId;
        private GroupBox groupBoxAudioAnalyzer;
        private GroupBox groupBox2;
        private ScottPlot.WinForms.FormsPlot formsPlotFilterbank;
        private GroupBox groupBox1;
        private ScottPlot.WinForms.FormsPlot formsPlotVisualization;
        private Button btnCancel;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel4;
    }
}