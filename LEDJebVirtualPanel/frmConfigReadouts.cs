using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LEDJebVirtualPanel
{
    public partial class frmConfigReadouts : Form
    {
        public frmConfigReadouts()
        {
            InitializeComponent();

        }

        private void nudShear_ValueChanged(object sender, EventArgs e)
        {
            dspDemo.DigitShear = nudShear.Value;
        }

        private void nudDigitSpacing_ValueChanged(object sender, EventArgs e)
        {
            dspDemo.DigitSpacing = nudDigitSpacing.Value;
        }

        private void nudSegmentThickness_ValueChanged(object sender, EventArgs e)
        {
            dspDemo.SegmentWidth = nudSegmentThickness.Value;
        }

        private void nudSegmentSpacing_ValueChanged(object sender, EventArgs e)
        {
            dspDemo.SegmentPadding = nudSegmentSpacing.Value;
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            // Set up MAX7219
            dspDemo.ClockData((byte)MAX7219Display.REG_SHUTDOWN);
            dspDemo.ClockData(0x01);
            dspDemo.LoadData();

            dspDemo.ClockData((byte)MAX7219Display.REG_SCAN_LIMIT);
            dspDemo.ClockData(0x08);
            dspDemo.LoadData();

            dspDemo.ClockData((byte)MAX7219Display.REG_DECODE_MODE);
            dspDemo.ClockData(0xFF);
            dspDemo.LoadData();

            dspDemo.ClockData((byte)MAX7219Display.REG_DISPLAY_TEST);
            dspDemo.ClockData(0x01);
            dspDemo.LoadData();

            dspDemo.ClockData((byte)MAX7219Display.REG_INTENSITY);
            dspDemo.ClockData(0x0F);
            dspDemo.LoadData();

        }

        private void btnDefaults_Click(object sender, EventArgs e)
        {
            nudShear.Value = 4;
            nudDigitSpacing.Value = 7;
            nudSegmentThickness.Value = 2.8M;
            nudSegmentSpacing.Value = 1;
            nudContrast.Value = 32;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public decimal Shear { get { return nudShear.Value; } set { nudShear.Value = value; } }
        public decimal DigitSpacing { get { return nudDigitSpacing.Value; } set { nudDigitSpacing.Value = value; } }
        public decimal SegmentThickness { get { return nudSegmentThickness.Value; } set { nudSegmentThickness.Value = value; } }
        public decimal SegmentSpacing { get { return nudSegmentSpacing.Value; } set { nudSegmentSpacing.Value = value; } }
        public decimal Contrast { get { return nudContrast.Value; } set { nudContrast.Value = value; } }

        private void nudContrast_ValueChanged(object sender, EventArgs e)
        {
            decimal c = nudContrast.Value / 255;

            dspDemo.BackColor = Color.FromArgb((int)(dspDemo.ForeColor.R * c), (int)(dspDemo.ForeColor.G * c), (int)(dspDemo.ForeColor.B * c));
        }
    }
}
