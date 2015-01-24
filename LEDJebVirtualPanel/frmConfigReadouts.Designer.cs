namespace LEDJebVirtualPanel
{
    partial class frmConfigReadouts
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudContrast = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDefaults = new System.Windows.Forms.Button();
            this.nudSegmentSpacing = new System.Windows.Forms.NumericUpDown();
            this.nudSegmentThickness = new System.Windows.Forms.NumericUpDown();
            this.nudDigitSpacing = new System.Windows.Forms.NumericUpDown();
            this.nudShear = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dspDemo = new LEDJebVirtualPanel.MAX7219Display();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSegmentSpacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSegmentThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDigitSpacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShear)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudContrast);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnDefaults);
            this.groupBox1.Controls.Add(this.nudSegmentSpacing);
            this.groupBox1.Controls.Add(this.nudSegmentThickness);
            this.groupBox1.Controls.Add(this.nudDigitSpacing);
            this.groupBox1.Controls.Add(this.nudShear);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 154);
            this.groupBox1.TabIndex = 99;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Segment Details";
            // 
            // nudContrast
            // 
            this.nudContrast.Location = new System.Drawing.Point(297, 94);
            this.nudContrast.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudContrast.Name = "nudContrast";
            this.nudContrast.Size = new System.Drawing.Size(64, 20);
            this.nudContrast.TabIndex = 10;
            this.nudContrast.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudContrast.ValueChanged += new System.EventHandler(this.nudContrast_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(236, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Contrast:";
            // 
            // btnDefaults
            // 
            this.btnDefaults.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDefaults.FlatAppearance.BorderSize = 2;
            this.btnDefaults.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnDefaults.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnDefaults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaults.Location = new System.Drawing.Point(277, 36);
            this.btnDefaults.Name = "btnDefaults";
            this.btnDefaults.Size = new System.Drawing.Size(134, 35);
            this.btnDefaults.TabIndex = 8;
            this.btnDefaults.Text = "Load Defaults";
            this.btnDefaults.UseVisualStyleBackColor = true;
            this.btnDefaults.Click += new System.EventHandler(this.btnDefaults_Click);
            // 
            // nudSegmentSpacing
            // 
            this.nudSegmentSpacing.DecimalPlaces = 1;
            this.nudSegmentSpacing.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudSegmentSpacing.Location = new System.Drawing.Point(145, 124);
            this.nudSegmentSpacing.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudSegmentSpacing.Name = "nudSegmentSpacing";
            this.nudSegmentSpacing.Size = new System.Drawing.Size(64, 20);
            this.nudSegmentSpacing.TabIndex = 7;
            this.nudSegmentSpacing.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSegmentSpacing.ValueChanged += new System.EventHandler(this.nudSegmentSpacing_ValueChanged);
            // 
            // nudSegmentThickness
            // 
            this.nudSegmentThickness.DecimalPlaces = 1;
            this.nudSegmentThickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudSegmentThickness.Location = new System.Drawing.Point(145, 94);
            this.nudSegmentThickness.Maximum = new decimal(new int[] {
            105,
            0,
            0,
            65536});
            this.nudSegmentThickness.Name = "nudSegmentThickness";
            this.nudSegmentThickness.Size = new System.Drawing.Size(64, 20);
            this.nudSegmentThickness.TabIndex = 6;
            this.nudSegmentThickness.Value = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.nudSegmentThickness.ValueChanged += new System.EventHandler(this.nudSegmentThickness_ValueChanged);
            // 
            // nudDigitSpacing
            // 
            this.nudDigitSpacing.DecimalPlaces = 1;
            this.nudDigitSpacing.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudDigitSpacing.Location = new System.Drawing.Point(145, 56);
            this.nudDigitSpacing.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudDigitSpacing.Name = "nudDigitSpacing";
            this.nudDigitSpacing.Size = new System.Drawing.Size(64, 20);
            this.nudDigitSpacing.TabIndex = 5;
            this.nudDigitSpacing.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudDigitSpacing.ValueChanged += new System.EventHandler(this.nudDigitSpacing_ValueChanged);
            // 
            // nudShear
            // 
            this.nudShear.DecimalPlaces = 1;
            this.nudShear.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudShear.Location = new System.Drawing.Point(145, 26);
            this.nudShear.Maximum = new decimal(new int[] {
            350,
            0,
            0,
            65536});
            this.nudShear.Name = "nudShear";
            this.nudShear.Size = new System.Drawing.Size(64, 20);
            this.nudShear.TabIndex = 4;
            this.nudShear.Value = new decimal(new int[] {
            40,
            0,
            0,
            65536});
            this.nudShear.ValueChanged += new System.EventHandler(this.nudShear_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Segment Spacing:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Segment Thickness:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Digit Spacing:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Digit Shear (Italic):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Full Preview:";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(309, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 35);
            this.button1.TabIndex = 9;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(157, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 35);
            this.button2.TabIndex = 100;
            this.button2.Text = "Save && Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 271);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 101;
            this.label6.Text = "Saving includes colors";
            // 
            // dspDemo
            // 
            this.dspDemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
            this.dspDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dspDemo.DigitShear = 4M;
            this.dspDemo.DigitSpacing = 7M;
            this.dspDemo.ForeColor = System.Drawing.Color.Lime;
            this.dspDemo.Location = new System.Drawing.Point(215, 12);
            this.dspDemo.Name = "dspDemo";
            this.dspDemo.NextInChain = null;
            this.dspDemo.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.dspDemo.SegmentPadding = 1M;
            this.dspDemo.SegmentWidth = 2.8M;
            this.dspDemo.Size = new System.Drawing.Size(228, 49);
            this.dspDemo.TabIndex = 98;
            this.dspDemo.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(340, 13);
            this.label7.TabIndex = 102;
            this.label7.Text = "To set display colors, double-click on each display on the main window";
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(455, 307);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dspDemo);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmConfig";
            this.Text = "Config";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSegmentSpacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSegmentThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDigitSpacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MAX7219Display dspDemo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudShear;
        private System.Windows.Forms.NumericUpDown nudSegmentSpacing;
        private System.Windows.Forms.NumericUpDown nudSegmentThickness;
        private System.Windows.Forms.NumericUpDown nudDigitSpacing;
        private System.Windows.Forms.Button btnDefaults;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudContrast;
        private System.Windows.Forms.Label label8;
    }
}