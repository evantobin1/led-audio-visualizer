﻿namespace LedAudioVisualizer
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
            ListViewItem listViewItem27 = new ListViewItem("Test1");
            ListViewItem listViewItem28 = new ListViewItem("Test2");
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
            tableLayoutPanel6 = new TableLayoutPanel();
            formsPlotVisualization = new ScottPlot.WinForms.FormsPlot();
            panel3 = new Panel();
            numericUpDownLedCount = new NumericUpDown();
            label2 = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            panel2 = new Panel();
            tableLayoutPanel7 = new TableLayoutPanel();
            label7 = new Label();
            label6 = new Label();
            numericUpDown_BlueMin = new NumericUpDown();
            numericUpDown_BlueMax = new NumericUpDown();
            numericUpDown_GreenMin = new NumericUpDown();
            numericUpDown_GreenMax = new NumericUpDown();
            numericUpDown_RedMin = new NumericUpDown();
            numericUpDown_RedMax = new NumericUpDown();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            label5 = new Label();
            formsPlotFilterbank = new ScottPlot.WinForms.FormsPlot();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            groupBox3 = new GroupBox();
            radioButton4 = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAppIcon).BeginInit();
            groupBoxSelectedApp.SuspendLayout();
            groupBoxAudioAnalyzer.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLedCount).BeginInit();
            tableLayoutPanel5.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_BlueMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_BlueMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_GreenMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_GreenMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_RedMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_RedMax).BeginInit();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox3.SuspendLayout();
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
            listViewApplications.Items.AddRange(new ListViewItem[] { listViewItem27, listViewItem28 });
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
            tableLayoutPanel4.Controls.Add(tableLayoutPanel6, 0, 1);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(692, 512);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.5306129F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.46939F));
            tableLayoutPanel6.Controls.Add(formsPlotVisualization, 1, 0);
            tableLayoutPanel6.Controls.Add(panel3, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(3, 259);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(686, 250);
            tableLayoutPanel6.TabIndex = 0;
            // 
            // formsPlotVisualization
            // 
            formsPlotVisualization.DisplayScale = 1F;
            formsPlotVisualization.Dock = DockStyle.Fill;
            formsPlotVisualization.Location = new Point(185, 3);
            formsPlotVisualization.Name = "formsPlotVisualization";
            formsPlotVisualization.Size = new Size(498, 244);
            formsPlotVisualization.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(numericUpDownLedCount);
            panel3.Controls.Add(label2);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(182, 250);
            panel3.TabIndex = 2;
            // 
            // numericUpDownLedCount
            // 
            numericUpDownLedCount.Anchor = AnchorStyles.Top;
            numericUpDownLedCount.Location = new Point(63, 21);
            numericUpDownLedCount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownLedCount.Name = "numericUpDownLedCount";
            numericUpDownLedCount.Size = new Size(54, 23);
            numericUpDownLedCount.TabIndex = 3;
            numericUpDownLedCount.Value = new decimal(new int[] { 600, 0, 0, 0 });
            numericUpDownLedCount.ValueChanged += numericUpDownLedCount_ValueChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(59, 3);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 2;
            label2.Text = "LED Count";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.5306129F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.46939F));
            tableLayoutPanel5.Controls.Add(panel2, 0, 0);
            tableLayoutPanel5.Controls.Add(formsPlotFilterbank, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(686, 250);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(tableLayoutPanel7);
            panel2.Controls.Add(label5);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(182, 250);
            panel2.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel7.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel7.ColumnCount = 3;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Controls.Add(label7, 2, 0);
            tableLayoutPanel7.Controls.Add(label6, 1, 0);
            tableLayoutPanel7.Controls.Add(numericUpDown_BlueMin, 1, 3);
            tableLayoutPanel7.Controls.Add(numericUpDown_BlueMax, 2, 3);
            tableLayoutPanel7.Controls.Add(numericUpDown_GreenMin, 1, 2);
            tableLayoutPanel7.Controls.Add(numericUpDown_GreenMax, 2, 2);
            tableLayoutPanel7.Controls.Add(numericUpDown_RedMin, 1, 1);
            tableLayoutPanel7.Controls.Add(numericUpDown_RedMax, 2, 1);
            tableLayoutPanel7.Controls.Add(label4, 0, 3);
            tableLayoutPanel7.Controls.Add(label3, 0, 2);
            tableLayoutPanel7.Controls.Add(label1, 0, 1);
            tableLayoutPanel7.Location = new Point(3, 28);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 4;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel7.Size = new Size(174, 107);
            tableLayoutPanel7.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(113, 1);
            label7.Name = "label7";
            label7.Size = new Size(57, 18);
            label7.TabIndex = 11;
            label7.Text = "Max";
            label7.TextAlign = ContentAlignment.BottomCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(50, 1);
            label6.Name = "label6";
            label6.Size = new Size(56, 18);
            label6.TabIndex = 11;
            label6.Text = "Min";
            label6.TextAlign = ContentAlignment.BottomCenter;
            // 
            // numericUpDown_BlueMin
            // 
            numericUpDown_BlueMin.Dock = DockStyle.Fill;
            numericUpDown_BlueMin.Location = new Point(50, 81);
            numericUpDown_BlueMin.Maximum = new decimal(new int[] { 19999, 0, 0, 0 });
            numericUpDown_BlueMin.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            numericUpDown_BlueMin.Name = "numericUpDown_BlueMin";
            numericUpDown_BlueMin.Size = new Size(56, 23);
            numericUpDown_BlueMin.TabIndex = 8;
            numericUpDown_BlueMin.Value = new decimal(new int[] { 2001, 0, 0, 0 });
            numericUpDown_BlueMin.ValueChanged += numericUpDown_BlueMin_ValueChanged;
            // 
            // numericUpDown_BlueMax
            // 
            numericUpDown_BlueMax.Dock = DockStyle.Fill;
            numericUpDown_BlueMax.Location = new Point(113, 81);
            numericUpDown_BlueMax.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericUpDown_BlueMax.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown_BlueMax.Name = "numericUpDown_BlueMax";
            numericUpDown_BlueMax.Size = new Size(57, 23);
            numericUpDown_BlueMax.TabIndex = 13;
            numericUpDown_BlueMax.Value = new decimal(new int[] { 20000, 0, 0, 0 });
            numericUpDown_BlueMax.ValueChanged += numericUpDown_BlueMax_ValueChanged;
            // 
            // numericUpDown_GreenMin
            // 
            numericUpDown_GreenMin.Dock = DockStyle.Fill;
            numericUpDown_GreenMin.Location = new Point(50, 52);
            numericUpDown_GreenMin.Maximum = new decimal(new int[] { 19997, 0, 0, 0 });
            numericUpDown_GreenMin.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDown_GreenMin.Name = "numericUpDown_GreenMin";
            numericUpDown_GreenMin.Size = new Size(56, 23);
            numericUpDown_GreenMin.TabIndex = 7;
            numericUpDown_GreenMin.Value = new decimal(new int[] { 201, 0, 0, 0 });
            numericUpDown_GreenMin.ValueChanged += numericUpDown_GreenMin_ValueChanged;
            // 
            // numericUpDown_GreenMax
            // 
            numericUpDown_GreenMax.Dock = DockStyle.Fill;
            numericUpDown_GreenMax.Location = new Point(113, 52);
            numericUpDown_GreenMax.Maximum = new decimal(new int[] { 19998, 0, 0, 0 });
            numericUpDown_GreenMax.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numericUpDown_GreenMax.Name = "numericUpDown_GreenMax";
            numericUpDown_GreenMax.Size = new Size(57, 23);
            numericUpDown_GreenMax.TabIndex = 12;
            numericUpDown_GreenMax.Value = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDown_GreenMax.ValueChanged += numericUpDown_GreenMax_ValueChanged;
            // 
            // numericUpDown_RedMin
            // 
            numericUpDown_RedMin.Dock = DockStyle.Fill;
            numericUpDown_RedMin.Location = new Point(50, 23);
            numericUpDown_RedMin.Maximum = new decimal(new int[] { 19995, 0, 0, 0 });
            numericUpDown_RedMin.Name = "numericUpDown_RedMin";
            numericUpDown_RedMin.Size = new Size(56, 23);
            numericUpDown_RedMin.TabIndex = 6;
            numericUpDown_RedMin.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDown_RedMin.ValueChanged += numericUpDown_RedMin_ValueChanged;
            // 
            // numericUpDown_RedMax
            // 
            numericUpDown_RedMax.Dock = DockStyle.Fill;
            numericUpDown_RedMax.Location = new Point(113, 23);
            numericUpDown_RedMax.Maximum = new decimal(new int[] { 19996, 0, 0, 0 });
            numericUpDown_RedMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_RedMax.Name = "numericUpDown_RedMax";
            numericUpDown_RedMax.Size = new Size(57, 23);
            numericUpDown_RedMax.TabIndex = 11;
            numericUpDown_RedMax.Value = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDown_RedMax.ValueChanged += numericUpDown_RedMax_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(4, 78);
            label4.Name = "label4";
            label4.Size = new Size(39, 28);
            label4.TabIndex = 5;
            label4.Text = "Blue";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(4, 49);
            label3.Name = "label3";
            label3.Size = new Size(39, 28);
            label3.TabIndex = 3;
            label3.Text = "Green";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(4, 20);
            label1.Name = "label1";
            label1.Size = new Size(39, 28);
            label1.TabIndex = 1;
            label1.Text = "Red";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(42, 6);
            label5.Name = "label5";
            label5.Size = new Size(97, 15);
            label5.TabIndex = 9;
            label5.Text = "Frequency Bands";
            // 
            // formsPlotFilterbank
            // 
            formsPlotFilterbank.DisplayScale = 1F;
            formsPlotFilterbank.Dock = DockStyle.Fill;
            formsPlotFilterbank.Location = new Point(185, 3);
            formsPlotFilterbank.Name = "formsPlotFilterbank";
            formsPlotFilterbank.Size = new Size(498, 244);
            formsPlotFilterbank.TabIndex = 0;
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
            groupBox1.Controls.Add(groupBox3);
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
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(6, 22);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(51, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "12Hz";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Checked = true;
            radioButton2.Location = new Point(6, 47);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(51, 19);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "24Hz";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(6, 72);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(51, 19);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "48Hz";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(radioButton4);
            groupBox3.Controls.Add(radioButton1);
            groupBox3.Controls.Add(radioButton3);
            groupBox3.Controls.Add(radioButton2);
            groupBox3.ForeColor = Color.FromArgb(245, 246, 250);
            groupBox3.Location = new Point(6, 24);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(142, 128);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Data Transmission Rate";
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(6, 95);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(51, 19);
            radioButton4.TabIndex = 3;
            radioButton4.TabStop = true;
            radioButton4.Text = "72Hz";
            radioButton4.UseVisualStyleBackColor = true;
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
            tableLayoutPanel6.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLedCount).EndInit();
            tableLayoutPanel5.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_BlueMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_BlueMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_GreenMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_GreenMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_RedMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_RedMax).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
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
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private Panel panel3;
        private Label label2;
        private NumericUpDown numericUpDownLedCount;
        private Panel panel2;
        private NumericUpDown numericUpDown_BlueMin;
        private NumericUpDown numericUpDown_GreenMin;
        private NumericUpDown numericUpDown_RedMin;
        private Label label4;
        private Label label3;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label5;
        private NumericUpDown numericUpDown_GreenMax;
        private NumericUpDown numericUpDown_RedMax;
        private Label label7;
        private Label label6;
        private NumericUpDown numericUpDown_BlueMax;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private GroupBox groupBox3;
        private RadioButton radioButton4;
    }
}