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
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrCheckData = new System.Windows.Forms.Timer(this.components);
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
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(321, 584);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(103, 15);
            this.label31.TabIndex = 75;
            this.label31.Text = "MISSION TIME";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(86, 584);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(71, 15);
            this.label32.TabIndex = 74;
            this.label32.Text = "VELOCITY";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("OCR-A II", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.White;
            this.label33.Location = new System.Drawing.Point(184, 554);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(118, 17);
            this.label33.TabIndex = 73;
            this.label33.Text = "FLIGHT DATA";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.White;
            this.label30.Location = new System.Drawing.Point(809, 379);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(167, 15);
            this.label30.TabIndex = 70;
            this.label30.Text = "RELATIVE INCLINATION";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(559, 379);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(151, 15);
            this.label29.TabIndex = 69;
            this.label29.Text = "DISTANCE TO TARGET";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(565, 262);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(143, 15);
            this.label28.TabIndex = 68;
            this.label28.Text = "RELATIVE VELOCITY";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(819, 262);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(143, 15);
            this.label26.TabIndex = 67;
            this.label26.Text = "TIME TO INTERCEPT";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(811, 144);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(159, 15);
            this.label25.TabIndex = 66;
            this.label25.Text = "BURN TIME REMAINING";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(834, 40);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(103, 15);
            this.label22.TabIndex = 65;
            this.label22.Text = "TIME TO NODE";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(567, 40);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(143, 15);
            this.label21.TabIndex = 64;
            this.label21.Text = "DELTA V REMAINING";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(74, 379);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(95, 15);
            this.label27.TabIndex = 63;
            this.label27.Text = "INCLINATION";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(316, 262);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(119, 15);
            this.label24.TabIndex = 62;
            this.label24.Text = "ORBITAL PERIOD";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(81, 262);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(79, 15);
            this.label23.TabIndex = 61;
            this.label23.Text = "APLTITUDE";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(304, 144);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(143, 15);
            this.label19.TabIndex = 60;
            this.label19.Text = "TIME TO PERIAPSIS";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(79, 144);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 15);
            this.label20.TabIndex = 59;
            this.label20.Text = "PERIAPSIS";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(306, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(135, 15);
            this.label18.TabIndex = 58;
            this.label18.Text = "TIME TO APOAPSIS";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("OCR-A II", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(81, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 15);
            this.label17.TabIndex = 57;
            this.label17.Text = "APOAPSIS";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("OCR-A II", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(675, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(188, 17);
            this.label16.TabIndex = 56;
            this.label16.Text = "NODE / TARGET DATA";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("OCR-A II", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(179, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(128, 17);
            this.label15.TabIndex = 55;
            this.label15.Text = "ORBITAL DATA";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStatus});
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
            this.tssStatus.Size = new System.Drawing.Size(993, 17);
            this.tssStatus.Spring = true;
            this.tssStatus.Text = "Waiting for in-game flight...";
            this.tssStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tmrCheckData
            // 
            this.tmrCheckData.Enabled = true;
            this.tmrCheckData.Interval = 50;
            this.tmrCheckData.Tick += new System.EventHandler(this.tmrCheckData_Tick);
            // 
            // display16
            // 
            this.display16.ArrayCount = 8;
            this.display16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display16.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display16.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display16.ColorLight = System.Drawing.Color.Lime;
            this.display16.DecimalShow = true;
            this.display16.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display16.ElementWidth = 8;
            this.display16.ItalicFactor = -0.1F;
            this.display16.Location = new System.Drawing.Point(257, 607);
            this.display16.Name = "display16";
            this.display16.NextInChain = null;
            this.display16.Size = new System.Drawing.Size(228, 49);
            this.display16.TabIndex = 111;
            this.display16.TabStop = false;
            this.display16.Value = null;
            // 
            // display15
            // 
            this.display15.ArrayCount = 8;
            this.display15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display15.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display15.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display15.ColorLight = System.Drawing.Color.Yellow;
            this.display15.DecimalShow = true;
            this.display15.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display15.ElementWidth = 8;
            this.display15.ItalicFactor = -0.1F;
            this.display15.Location = new System.Drawing.Point(3, 607);
            this.display15.Name = "display15";
            this.display15.NextInChain = this.display16;
            this.display15.Size = new System.Drawing.Size(228, 49);
            this.display15.TabIndex = 110;
            this.display15.TabStop = false;
            this.display15.Value = null;
            // 
            // display14
            // 
            this.display14.ArrayCount = 8;
            this.display14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display14.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display14.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display14.ColorLight = System.Drawing.Color.Red;
            this.display14.DecimalShow = true;
            this.display14.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display14.ElementWidth = 8;
            this.display14.ItalicFactor = -0.1F;
            this.display14.Location = new System.Drawing.Point(775, 407);
            this.display14.Name = "display14";
            this.display14.NextInChain = this.display15;
            this.display14.Size = new System.Drawing.Size(228, 49);
            this.display14.TabIndex = 109;
            this.display14.TabStop = false;
            this.display14.Value = null;
            // 
            // display13
            // 
            this.display13.ArrayCount = 8;
            this.display13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display13.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display13.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display13.ColorLight = System.Drawing.Color.Red;
            this.display13.DecimalShow = true;
            this.display13.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display13.ElementWidth = 8;
            this.display13.ItalicFactor = -0.1F;
            this.display13.Location = new System.Drawing.Point(521, 407);
            this.display13.Name = "display13";
            this.display13.NextInChain = this.display14;
            this.display13.Size = new System.Drawing.Size(228, 49);
            this.display13.TabIndex = 108;
            this.display13.TabStop = false;
            this.display13.Value = null;
            // 
            // display12
            // 
            this.display12.ArrayCount = 8;
            this.display12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display12.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display12.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display12.ColorLight = System.Drawing.Color.Lime;
            this.display12.DecimalShow = true;
            this.display12.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display12.ElementWidth = 8;
            this.display12.ItalicFactor = -0.1F;
            this.display12.Location = new System.Drawing.Point(775, 288);
            this.display12.Name = "display12";
            this.display12.NextInChain = this.display13;
            this.display12.Size = new System.Drawing.Size(228, 49);
            this.display12.TabIndex = 107;
            this.display12.TabStop = false;
            this.display12.Value = null;
            // 
            // display11
            // 
            this.display11.ArrayCount = 8;
            this.display11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display11.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display11.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display11.ColorLight = System.Drawing.Color.Yellow;
            this.display11.DecimalShow = true;
            this.display11.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display11.ElementWidth = 8;
            this.display11.ItalicFactor = -0.1F;
            this.display11.Location = new System.Drawing.Point(521, 288);
            this.display11.Name = "display11";
            this.display11.NextInChain = this.display12;
            this.display11.Size = new System.Drawing.Size(228, 49);
            this.display11.TabIndex = 106;
            this.display11.TabStop = false;
            this.display11.Value = null;
            // 
            // display10
            // 
            this.display10.ArrayCount = 8;
            this.display10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display10.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display10.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display10.ColorLight = System.Drawing.Color.Lime;
            this.display10.DecimalShow = true;
            this.display10.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display10.ElementWidth = 8;
            this.display10.ItalicFactor = -0.1F;
            this.display10.Location = new System.Drawing.Point(775, 170);
            this.display10.Name = "display10";
            this.display10.NextInChain = this.display11;
            this.display10.Size = new System.Drawing.Size(228, 49);
            this.display10.TabIndex = 105;
            this.display10.TabStop = false;
            this.display10.Value = null;
            // 
            // display9
            // 
            this.display9.ArrayCount = 8;
            this.display9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display9.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display9.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display9.ColorLight = System.Drawing.Color.Lime;
            this.display9.DecimalShow = true;
            this.display9.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display9.ElementWidth = 8;
            this.display9.ItalicFactor = -0.1F;
            this.display9.Location = new System.Drawing.Point(775, 65);
            this.display9.Name = "display9";
            this.display9.NextInChain = this.display10;
            this.display9.Size = new System.Drawing.Size(228, 49);
            this.display9.TabIndex = 104;
            this.display9.TabStop = false;
            this.display9.Value = null;
            // 
            // display8
            // 
            this.display8.ArrayCount = 8;
            this.display8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display8.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display8.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display8.ColorLight = System.Drawing.Color.Yellow;
            this.display8.DecimalShow = true;
            this.display8.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display8.ElementWidth = 8;
            this.display8.ItalicFactor = -0.1F;
            this.display8.Location = new System.Drawing.Point(521, 65);
            this.display8.Name = "display8";
            this.display8.NextInChain = this.display9;
            this.display8.Size = new System.Drawing.Size(228, 49);
            this.display8.TabIndex = 103;
            this.display8.TabStop = false;
            this.display8.Value = null;
            // 
            // display7
            // 
            this.display7.ArrayCount = 8;
            this.display7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display7.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display7.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display7.ColorLight = System.Drawing.Color.Red;
            this.display7.DecimalShow = true;
            this.display7.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display7.ElementWidth = 8;
            this.display7.ItalicFactor = -0.1F;
            this.display7.Location = new System.Drawing.Point(3, 407);
            this.display7.Name = "display7";
            this.display7.NextInChain = this.display8;
            this.display7.Size = new System.Drawing.Size(228, 49);
            this.display7.TabIndex = 102;
            this.display7.TabStop = false;
            this.display7.Value = null;
            // 
            // display6
            // 
            this.display6.ArrayCount = 8;
            this.display6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display6.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display6.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display6.ColorLight = System.Drawing.Color.Lime;
            this.display6.DecimalShow = true;
            this.display6.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display6.ElementWidth = 8;
            this.display6.ItalicFactor = -0.1F;
            this.display6.Location = new System.Drawing.Point(257, 288);
            this.display6.Name = "display6";
            this.display6.NextInChain = this.display7;
            this.display6.Size = new System.Drawing.Size(228, 49);
            this.display6.TabIndex = 101;
            this.display6.TabStop = false;
            this.display6.Value = null;
            // 
            // display5
            // 
            this.display5.ArrayCount = 8;
            this.display5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display5.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display5.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display5.ColorLight = System.Drawing.Color.Red;
            this.display5.DecimalShow = true;
            this.display5.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display5.ElementWidth = 8;
            this.display5.ItalicFactor = -0.1F;
            this.display5.Location = new System.Drawing.Point(3, 288);
            this.display5.Name = "display5";
            this.display5.NextInChain = this.display6;
            this.display5.Size = new System.Drawing.Size(228, 49);
            this.display5.TabIndex = 100;
            this.display5.TabStop = false;
            this.display5.Value = null;
            // 
            // display4
            // 
            this.display4.ArrayCount = 8;
            this.display4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display4.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display4.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display4.ColorLight = System.Drawing.Color.Lime;
            this.display4.DecimalShow = true;
            this.display4.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display4.ElementWidth = 8;
            this.display4.ItalicFactor = -0.1F;
            this.display4.Location = new System.Drawing.Point(257, 170);
            this.display4.Name = "display4";
            this.display4.NextInChain = this.display5;
            this.display4.Size = new System.Drawing.Size(228, 49);
            this.display4.TabIndex = 99;
            this.display4.TabStop = false;
            this.display4.Value = null;
            // 
            // display3
            // 
            this.display3.ArrayCount = 8;
            this.display3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display3.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display3.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display3.ColorLight = System.Drawing.Color.Red;
            this.display3.DecimalShow = true;
            this.display3.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display3.ElementWidth = 8;
            this.display3.ItalicFactor = -0.1F;
            this.display3.Location = new System.Drawing.Point(3, 170);
            this.display3.Name = "display3";
            this.display3.NextInChain = this.display4;
            this.display3.Size = new System.Drawing.Size(228, 49);
            this.display3.TabIndex = 98;
            this.display3.TabStop = false;
            this.display3.Value = null;
            // 
            // display2
            // 
            this.display2.ArrayCount = 8;
            this.display2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display2.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display2.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.display2.ColorLight = System.Drawing.Color.Lime;
            this.display2.DecimalShow = true;
            this.display2.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display2.ElementWidth = 8;
            this.display2.ItalicFactor = -0.1F;
            this.display2.Location = new System.Drawing.Point(257, 65);
            this.display2.Name = "display2";
            this.display2.NextInChain = this.display3;
            this.display2.Size = new System.Drawing.Size(228, 49);
            this.display2.TabIndex = 97;
            this.display2.TabStop = false;
            this.display2.Value = null;
            // 
            // display1
            // 
            this.display1.ArrayCount = 8;
            this.display1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display1.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display1.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.display1.ColorLight = System.Drawing.Color.Red;
            this.display1.DecimalShow = true;
            this.display1.ElementPadding = new System.Windows.Forms.Padding(4);
            this.display1.ElementWidth = 8;
            this.display1.ItalicFactor = -0.1F;
            this.display1.Location = new System.Drawing.Point(3, 65);
            this.display1.Name = "display1";
            this.display1.NextInChain = this.display2;
            this.display1.Size = new System.Drawing.Size(228, 49);
            this.display1.TabIndex = 96;
            this.display1.TabStop = false;
            this.display1.Value = null;
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
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmVirtualLEDs";
            this.Text = "LEDJeb Client 0.2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVirtualLEDs_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
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
    }
}