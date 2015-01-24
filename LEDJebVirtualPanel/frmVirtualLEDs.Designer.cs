namespace LEDJebVirtualPanel
{
    partial class frmVirtualLEDs
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVirtualLEDs));
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.header3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.header2 = new System.Windows.Forms.Label();
            this.header1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssGDILoad = new System.Windows.Forms.ToolStripProgressBar();
            this.tmrCheckData = new System.Windows.Forms.Timer(this.components);
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.display16 = new LEDJebVirtualPanel.MAX7219Display();
            this.display15 = new LEDJebVirtualPanel.MAX7219Display();
            this.display14 = new LEDJebVirtualPanel.MAX7219Display();
            this.display13 = new LEDJebVirtualPanel.MAX7219Display();
            this.display12 = new LEDJebVirtualPanel.MAX7219Display();
            this.display11 = new LEDJebVirtualPanel.MAX7219Display();
            this.display10 = new LEDJebVirtualPanel.MAX7219Display();
            this.display9 = new LEDJebVirtualPanel.MAX7219Display();
            this.display8 = new LEDJebVirtualPanel.MAX7219Display();
            this.display7 = new LEDJebVirtualPanel.MAX7219Display();
            this.display6 = new LEDJebVirtualPanel.MAX7219Display();
            this.display5 = new LEDJebVirtualPanel.MAX7219Display();
            this.display4 = new LEDJebVirtualPanel.MAX7219Display();
            this.display3 = new LEDJebVirtualPanel.MAX7219Display();
            this.display2 = new LEDJebVirtualPanel.MAX7219Display();
            this.display1 = new LEDJebVirtualPanel.MAX7219Display();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(316, 570);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 17);
            this.label16.TabIndex = 75;
            this.label16.Text = "MISSION TIME";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(82, 570);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 17);
            this.label15.TabIndex = 74;
            this.label15.Text = "VELOCITY";
            // 
            // header3
            // 
            this.header3.AutoSize = true;
            this.header3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header3.ForeColor = System.Drawing.Color.White;
            this.header3.Location = new System.Drawing.Point(190, 534);
            this.header3.Name = "header3";
            this.header3.Size = new System.Drawing.Size(114, 20);
            this.header3.TabIndex = 73;
            this.header3.Text = "FLIGHT DATA";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(809, 360);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(163, 17);
            this.label14.TabIndex = 70;
            this.label14.Text = "RELATIVE INCLINATION";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(567, 360);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(161, 17);
            this.label13.TabIndex = 69;
            this.label13.Text = "DISTANCE TO TARGET";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(567, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(145, 17);
            this.label11.TabIndex = 68;
            this.label11.Text = "RELATIVE VELOCITY";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(827, 255);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(145, 17);
            this.label12.TabIndex = 67;
            this.label12.Text = "TIME TO INTERCEPT";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(811, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 17);
            this.label10.TabIndex = 66;
            this.label10.Text = "BURN TIME REMAINING";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(834, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 17);
            this.label9.TabIndex = 65;
            this.label9.Text = "TIME TO NODE";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(567, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 17);
            this.label8.TabIndex = 64;
            this.label8.Text = "DELTA V REMAINING";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(79, 360);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 17);
            this.label7.TabIndex = 63;
            this.label7.Text = "INCLINATION";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(316, 255);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 17);
            this.label6.TabIndex = 62;
            this.label6.Text = "ORBITAL PERIOD";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(82, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 17);
            this.label5.TabIndex = 61;
            this.label5.Text = "ALTITUDE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(304, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 17);
            this.label4.TabIndex = 60;
            this.label4.Text = "TIME TO PERIAPSIS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(79, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 59;
            this.label3.Text = "PERIAPSIS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(306, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 58;
            this.label2.Text = "TIME TO APOAPSIS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(81, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 57;
            this.label1.Text = "APOAPSIS";
            // 
            // header2
            // 
            this.header2.AutoSize = true;
            this.header2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header2.ForeColor = System.Drawing.Color.White;
            this.header2.Location = new System.Drawing.Point(675, 10);
            this.header2.Name = "header2";
            this.header2.Size = new System.Drawing.Size(179, 20);
            this.header2.TabIndex = 56;
            this.header2.Text = "NODE / TARGET DATA";
            // 
            // header1
            // 
            this.header1.AutoSize = true;
            this.header1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header1.ForeColor = System.Drawing.Color.White;
            this.header1.Location = new System.Drawing.Point(179, 10);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(125, 20);
            this.header1.TabIndex = 55;
            this.header1.Text = "ORBITAL DATA";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStatus,
            this.toolStripSplitButton1,
            this.tssGDILoad});
            this.statusStrip1.Location = new System.Drawing.Point(0, 712);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 76;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssStatus
            // 
            this.tssStatus.BackColor = System.Drawing.SystemColors.Control;
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(909, 17);
            this.tssStatus.Spring = true;
            this.tssStatus.Text = "Waiting for in-game flight...";
            this.tssStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.testToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 20);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.configToolStripMenuItem.Text = "&Configure Readouts...";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.testToolStripMenuItem.Text = "&Test Displays";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // tssGDILoad
            // 
            this.tssGDILoad.Name = "tssGDILoad";
            this.tssGDILoad.Size = new System.Drawing.Size(50, 16);
            this.tssGDILoad.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.tssGDILoad.ToolTipText = "GDI Load (%)";
            // 
            // tmrCheckData
            // 
            this.tmrCheckData.Enabled = true;
            this.tmrCheckData.Interval = 50;
            this.tmrCheckData.Tick += new System.EventHandler(this.tmrCheckData_Tick);
            // 
            // display16
            // 
            this.display16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display16.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display16.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display16.ForeColor = System.Drawing.Color.Lime;
            this.display16.Location = new System.Drawing.Point(257, 590);
            this.display16.Name = "display16";
            this.display16.NextInChain = null;
            this.display16.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display16.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display16.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display16.Size = new System.Drawing.Size(228, 49);
            this.display16.TabIndex = 111;
            this.display16.TabStop = false;
            // 
            // display15
            // 
            this.display15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display15.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display15.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display15.ForeColor = System.Drawing.Color.Yellow;
            this.display15.Location = new System.Drawing.Point(3, 590);
            this.display15.Name = "display15";
            this.display15.NextInChain = this.display16;
            this.display15.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display15.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display15.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display15.Size = new System.Drawing.Size(228, 49);
            this.display15.TabIndex = 110;
            this.display15.TabStop = false;
            // 
            // display14
            // 
            this.display14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display14.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display14.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display14.ForeColor = System.Drawing.Color.Red;
            this.display14.Location = new System.Drawing.Point(775, 380);
            this.display14.Name = "display14";
            this.display14.NextInChain = this.display15;
            this.display14.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display14.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display14.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display14.Size = new System.Drawing.Size(228, 49);
            this.display14.TabIndex = 109;
            this.display14.TabStop = false;
            // 
            // display13
            // 
            this.display13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display13.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display13.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display13.ForeColor = System.Drawing.Color.Red;
            this.display13.Location = new System.Drawing.Point(521, 380);
            this.display13.Name = "display13";
            this.display13.NextInChain = this.display14;
            this.display13.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display13.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display13.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display13.Size = new System.Drawing.Size(228, 49);
            this.display13.TabIndex = 108;
            this.display13.TabStop = false;
            // 
            // display12
            // 
            this.display12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display12.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display12.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display12.ForeColor = System.Drawing.Color.Lime;
            this.display12.Location = new System.Drawing.Point(775, 275);
            this.display12.Name = "display12";
            this.display12.NextInChain = this.display13;
            this.display12.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display12.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display12.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display12.Size = new System.Drawing.Size(228, 49);
            this.display12.TabIndex = 107;
            this.display12.TabStop = false;
            // 
            // display11
            // 
            this.display11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display11.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display11.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display11.ForeColor = System.Drawing.Color.Yellow;
            this.display11.Location = new System.Drawing.Point(521, 275);
            this.display11.Name = "display11";
            this.display11.NextInChain = this.display12;
            this.display11.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display11.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display11.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display11.Size = new System.Drawing.Size(228, 49);
            this.display11.TabIndex = 106;
            this.display11.TabStop = false;
            // 
            // display10
            // 
            this.display10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display10.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display10.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display10.ForeColor = System.Drawing.Color.Lime;
            this.display10.Location = new System.Drawing.Point(775, 170);
            this.display10.Name = "display10";
            this.display10.NextInChain = this.display11;
            this.display10.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display10.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display10.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display10.Size = new System.Drawing.Size(228, 49);
            this.display10.TabIndex = 105;
            this.display10.TabStop = false;
            // 
            // display9
            // 
            this.display9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display9.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display9.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display9.ForeColor = System.Drawing.Color.Lime;
            this.display9.Location = new System.Drawing.Point(775, 65);
            this.display9.Name = "display9";
            this.display9.NextInChain = this.display10;
            this.display9.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display9.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display9.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display9.Size = new System.Drawing.Size(228, 49);
            this.display9.TabIndex = 104;
            this.display9.TabStop = false;
            // 
            // display8
            // 
            this.display8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display8.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display8.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display8.ForeColor = System.Drawing.Color.Yellow;
            this.display8.Location = new System.Drawing.Point(521, 65);
            this.display8.Name = "display8";
            this.display8.NextInChain = this.display9;
            this.display8.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display8.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display8.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display8.Size = new System.Drawing.Size(228, 49);
            this.display8.TabIndex = 103;
            this.display8.TabStop = false;
            // 
            // display7
            // 
            this.display7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display7.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display7.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display7.ForeColor = System.Drawing.Color.Red;
            this.display7.Location = new System.Drawing.Point(3, 380);
            this.display7.Name = "display7";
            this.display7.NextInChain = this.display8;
            this.display7.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display7.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display7.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display7.Size = new System.Drawing.Size(228, 49);
            this.display7.TabIndex = 102;
            this.display7.TabStop = false;
            // 
            // display6
            // 
            this.display6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display6.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display6.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display6.ForeColor = System.Drawing.Color.Lime;
            this.display6.Location = new System.Drawing.Point(257, 275);
            this.display6.Name = "display6";
            this.display6.NextInChain = this.display7;
            this.display6.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display6.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display6.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display6.Size = new System.Drawing.Size(228, 49);
            this.display6.TabIndex = 101;
            this.display6.TabStop = false;
            // 
            // display5
            // 
            this.display5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display5.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display5.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display5.ForeColor = System.Drawing.Color.Red;
            this.display5.Location = new System.Drawing.Point(3, 275);
            this.display5.Name = "display5";
            this.display5.NextInChain = this.display6;
            this.display5.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display5.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display5.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display5.Size = new System.Drawing.Size(228, 49);
            this.display5.TabIndex = 100;
            this.display5.TabStop = false;
            // 
            // display4
            // 
            this.display4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display4.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display4.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display4.ForeColor = System.Drawing.Color.Lime;
            this.display4.Location = new System.Drawing.Point(257, 170);
            this.display4.Name = "display4";
            this.display4.NextInChain = this.display5;
            this.display4.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display4.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display4.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display4.Size = new System.Drawing.Size(228, 49);
            this.display4.TabIndex = 99;
            this.display4.TabStop = false;
            // 
            // display3
            // 
            this.display3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display3.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display3.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display3.ForeColor = System.Drawing.Color.Red;
            this.display3.Location = new System.Drawing.Point(3, 170);
            this.display3.Name = "display3";
            this.display3.NextInChain = this.display4;
            this.display3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display3.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display3.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display3.Size = new System.Drawing.Size(228, 49);
            this.display3.TabIndex = 98;
            this.display3.TabStop = false;
            // 
            // display2
            // 
            this.display2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display2.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display2.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display2.ForeColor = System.Drawing.Color.Lime;
            this.display2.Location = new System.Drawing.Point(257, 65);
            this.display2.Name = "display2";
            this.display2.NextInChain = this.display3;
            this.display2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display2.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display2.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display2.Size = new System.Drawing.Size(228, 49);
            this.display2.TabIndex = 97;
            this.display2.TabStop = false;
            // 
            // display1
            // 
            this.display1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display1.DigitShear = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.display1.DigitSpacing = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.display1.ForeColor = System.Drawing.Color.Red;
            this.display1.Location = new System.Drawing.Point(3, 65);
            this.display1.Name = "display1";
            this.display1.NextInChain = this.display2;
            this.display1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.display1.SegmentPadding = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.display1.SegmentWidth = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.display1.Size = new System.Drawing.Size(228, 49);
            this.display1.TabIndex = 96;
            this.display1.TabStop = false;
            // 
            // frmVirtualLEDs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1008, 734);
            this.Controls.Add(this.display16);
            this.Controls.Add(this.display15);
            this.Controls.Add(this.display14);
            this.Controls.Add(this.display13);
            this.Controls.Add(this.display12);
            this.Controls.Add(this.display11);
            this.Controls.Add(this.display10);
            this.Controls.Add(this.display9);
            this.Controls.Add(this.display8);
            this.Controls.Add(this.display7);
            this.Controls.Add(this.display6);
            this.Controls.Add(this.display5);
            this.Controls.Add(this.display4);
            this.Controls.Add(this.display3);
            this.Controls.Add(this.display2);
            this.Controls.Add(this.display1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.header3);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.header2);
            this.Controls.Add(this.header1);
            this.Name = "frmVirtualLEDs";
            this.Text = "LEDJeb Flight Readout 0.25";
            this.Load += new System.EventHandler(this.frmVirtualLEDs_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVirtualLEDs_FormClosing);
            this.Resize += new System.EventHandler(this.frmVirtualLEDs_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label header3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label header2;
        private System.Windows.Forms.Label header1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus;
        private System.Windows.Forms.Timer tmrCheckData;
        private MAX7219Display display1;
        private MAX7219Display display2;
        private MAX7219Display display4;
        private MAX7219Display display3;
        private MAX7219Display display6;
        private MAX7219Display display5;
        private MAX7219Display display7;
        private MAX7219Display display8;
        private MAX7219Display display9;
        private MAX7219Display display10;
        private MAX7219Display display12;
        private MAX7219Display display11;
        private MAX7219Display display13;
        private MAX7219Display display14;
        private MAX7219Display display15;
        private MAX7219Display display16;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar tssGDILoad;
    }
}