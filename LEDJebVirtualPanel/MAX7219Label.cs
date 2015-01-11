/*
 * LEDJeb MAX7219 Label
 * 
 * Author: heaton84
 * Date:   1/10/2015
 * Brief:  Emulates a MAX7219 controller per http://datasheets.maximintegrated.com/en/ds/MAX7219-MAX7221.pdf
 *         NOTE: Only BCODE mode is supported, as I don't yet have a way to render individual segments.
 * 
 * Usage:
 * 
 * 1. Specify next device in chain via NextInChain property (equivialant to wiring DOUT to DIN)
 * 
 * 2. Clock data in "root" display device via ClockData(), device will automatically shift bytes to next
 *    display as data is loaded (one writes from the "outmost" display in to the "root")
 *    
 * 3. Load data via Load() method (akin to pulling LOAD line high briefly)
 *    Note that this will pull each chained Load() line low.
 * 
 * TODO List:
 * 
 * 1. Support intensity
 * 2. Support non-BCODE mode
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LEDJebVirtualPanel
{
    public partial class MAX7219Label : Label
    {
        private MAX7219Label m_NextInChain = null;

        private byte m_LowByte = 0;
        private byte m_HighByte = 0;

        private byte[] m_Registers = new byte[16];
        private char[] m_BCODE = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', 'E', 'H', 'L', 'P', ' ' };

        private System.Text.Encoding m_Encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

        public MAX7219Label NextInChain
        {
            get
            {
                return m_NextInChain;
            }

            set
            {
                if (value == this)
                    throw new System.InvalidOperationException("Cannot chain display to self");
                else
                    m_NextInChain = value;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                base.BackColor = Color.FromArgb(base.ForeColor.R / 8, base.ForeColor.G / 8, base.ForeColor.B / 8);
            }
        }

        public MAX7219Label()
        {
            InitializeComponent();

            this.Font = new Font("7 Segment", this.Font.Size);
        }

        public void ClockData(byte data)
        {
            // data -> m_LowByte -> m_HighByte -> next display

            byte carry = m_HighByte;

            m_HighByte = m_LowByte;
            m_LowByte = data;

            if (m_NextInChain != null)
                m_NextInChain.ClockData(carry);
        }

        public void Load()
        {
            int reg = (int)(m_HighByte & 0x0f);

            m_Registers[reg] = m_LowByte;

            UpdateDisplay();

            if (m_NextInChain != null)
                m_NextInChain.Load();
        }

        private void UpdateDisplay()
        {
            int ScanLimit = (int)m_Registers[0x0B];
            byte[] ReadoutBytes = new byte[ScanLimit];

            if (m_Registers[0x0C] == 0)
            {
                // In shutdown mode, blank display
                for (int i = 1; i <= ScanLimit; i++)
                    ReadoutBytes[i - 1] = (byte)' ';
            }
            else if (m_Registers[0x0F] == 1)
            {
                // Not in shutdown, and in test mode
                for (int i = 1; i <= ScanLimit; i++)
                    ReadoutBytes[i - 1] = (byte)'8';
            }
            else
            {
                // Not in shutdown, not in test mode

                for (int i = 1; i <= ScanLimit; i++)
                    ReadoutBytes[i - 1] = (byte)m_BCODE[m_Registers[i] & 0x0f];

            }

            this.Text = m_Encoding.GetString(ReadoutBytes);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Overlay our decimal point(s) as needed

            SizeF charSize = MeasureStringAccurately(e.Graphics, "88", this.Font);
            int decimalSize = 4;
            Brush decimalBrush = new SolidBrush(this.ForeColor);

            for (int c = 1; c <= 8; c++)
            {
                if ((m_Registers[c] & 0x80) != 0)
                {
                    // Decimal point has been set
                    // Fiddling is as needed for size 36pt font
                    // It's hokey and needs to be done better!

                    Rectangle rDecimalPt = new Rectangle((int)(c * charSize.Width / 2) + 5, (int)(charSize.Height * 0.9 - decimalSize * 2), decimalSize, decimalSize);
                    e.Graphics.FillEllipse(decimalBrush, rDecimalPt);
                }
            }

            decimalBrush.Dispose();
        }

        protected SizeF MeasureStringAccurately(Graphics graphics, string text, Font font)
        {
            System.Drawing.StringFormat format  = new System.Drawing.StringFormat ();
            System.Drawing.RectangleF   rect    = new System.Drawing.RectangleF(0, 0, 1000, 1000);
            System.Drawing.CharacterRange[] ranges  = { new System.Drawing.CharacterRange(0, text.Length) };
            System.Drawing.Region[]         regions = new System.Drawing.Region[1];

            format.SetMeasurableCharacterRanges (ranges);

            regions = graphics.MeasureCharacterRanges (text, font, rect, format);
            rect    = regions[0].GetBounds (graphics);

            return new SizeF(rect.Right - rect.Left, rect.Bottom - rect.Top);
        }
    }
}
