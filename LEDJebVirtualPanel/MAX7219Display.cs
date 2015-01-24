/*
 * LEDJeb MAX7219 Display
 * 
 * Author: heaton84
 * Date:   1/19/2015
 * Brief:  Emulates a MAX7219 controller per http://datasheets.maximintegrated.com/en/ds/MAX7219-MAX7221.pdf
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
using System.Drawing.Drawing2D;

namespace LEDJebVirtualPanel
{
    public partial class MAX7219Display : UserControl
    {
        public MAX7219Display()
        {
            InitializeComponent();

            this.SuspendLayout();
            this.Name = "max7219Display";
            this.Size = new System.Drawing.Size(228, 49);
            this.ResumeLayout(false);

            this.TabStop = false;
            this.DoubleBuffered = true;
            
            SetSegmentTemplate();
        }

        #region 7-Segment

        protected decimal m_DigitSpacing = 4.0M;
        protected decimal m_DigitShear = 7.0M;
        protected decimal m_SegmentWidth = 2.8M;
        protected decimal m_SegmentSpacing = 1M;

        public decimal DigitSpacing { get { return m_DigitSpacing; } set { m_DigitSpacing = value; SetSegmentTemplate(); RedrawControl(true); } }
        public decimal DigitShear { get { return m_DigitShear; } set { m_DigitShear = value; SetSegmentTemplate(); RedrawControl(true); } }
        public decimal SegmentWidth { get { return m_SegmentWidth; } set { m_SegmentWidth = value; SetSegmentTemplate(); RedrawControl(true); } }
        public decimal SegmentPadding { get { return m_SegmentSpacing; } set { m_SegmentSpacing = value; SetSegmentTemplate(); RedrawControl(true); } }
        public new Color ForeColor { get { return base.ForeColor; } set { base.ForeColor = value; m_SegmentBrush = new SolidBrush(value); RedrawControl(true); } }
        public new Color BackColor { get { return base.BackColor; } set { base.BackColor = value; m_BackgroundBrush = new SolidBrush(value); RedrawControl(true); } }

        protected PointF[][] m_SegmentTemplate;
        protected RectangleF m_DecimalTemplate;

        protected byte[] m_CodeBFont = { 0x7E, 0x30, 0x6D, 0x79, 0x33, 0x5B, 0x5F, 0x70, 0x7F, 0x7B, 0x01, 0x4F, 0x37, 0x0E, 0x67, 0x00 };

        protected Brush m_SegmentBrush = null;
        protected Brush m_BackgroundBrush = null;

        private void RedrawControl(bool p_ForceEverything)
        {
            this.Invalidate();
        }

        private byte GetDisplayPatternForDigit(int p_DigitNumber)
        {
            //
            // Bit  Segment
            //  0     G
            //  1     F
            //  2     E
            //  3     D
            //  4     C
            //  5     B
            //  6     A
            //  7     DP
            //

            if (p_DigitNumber < 0 || p_DigitNumber > 7)
                throw new InvalidOperationException("Bad digit number");


            if (this.DesignMode)
                return (byte)0xff; // All on in designer
            else
            {
                byte decodeSelectMask = (byte)(1 << p_DigitNumber);
                byte displayData = m_Registers[p_DigitNumber + 1];

                if (m_Registers[REG_SHUTDOWN] == 0)
                    return 0; // In shutdown mode

                if (p_DigitNumber + 1 > m_Registers[REG_SCAN_LIMIT])
                    return 0; // Obey scan limit

                if (m_Registers[REG_DISPLAY_TEST] == 1)
                    return 0xff; // In display test

                if ((m_Registers[REG_DECODE_MODE] & decodeSelectMask) != 0)
                    return (byte)(m_CodeBFont[displayData & 0x0F] | (displayData & 0x80));
                else
                    return displayData;
            }
        }

        private void SetSegmentTemplate()
        {
            m_SegmentTemplate = new PointF[7][];

            for (int i = 0; i < 7; i++)
                m_SegmentTemplate[i] = new PointF[6];

            // Follow natural order of register layout, G->A, then DP

            // These bounds describe the outer edge of the segments
            // Midlines will be towards center by 1*SegmentWidth
            int top = this.Padding.Top;
            int bottom = this.Height - this.Padding.Vertical - (this.BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle ? 2 : 0);
            int middle = (top + bottom) / 2;

            float fSegmentWidth = (float)m_SegmentWidth;
            float fSegmentSpacing = (float)m_SegmentSpacing;
            float width = (float)(this.Width - this.Padding.Horizontal - (m_DigitSpacing * 7)) / 8;

            float halfSegmentWidth = fSegmentWidth / 2;

            // All segments are drawn clockwise
            // G
            m_SegmentTemplate[0][0] = new PointF(halfSegmentWidth + fSegmentSpacing, middle);
            m_SegmentTemplate[0][1] = new PointF(fSegmentWidth + fSegmentSpacing, middle - halfSegmentWidth);
            m_SegmentTemplate[0][2] = new PointF(width - fSegmentWidth - fSegmentSpacing, middle - halfSegmentWidth);
            m_SegmentTemplate[0][3] = new PointF(width - halfSegmentWidth - fSegmentSpacing, middle);
            m_SegmentTemplate[0][4] = new PointF(width - fSegmentWidth - fSegmentSpacing, middle + halfSegmentWidth);
            m_SegmentTemplate[0][5] = new PointF(fSegmentWidth + fSegmentSpacing, middle + halfSegmentWidth);

            // F
            m_SegmentTemplate[1][0] = new PointF(halfSegmentWidth, top + halfSegmentWidth + fSegmentSpacing);
            m_SegmentTemplate[1][1] = new PointF(fSegmentWidth, top + fSegmentWidth + fSegmentSpacing);
            m_SegmentTemplate[1][2] = new PointF(fSegmentWidth, middle - halfSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[1][3] = new PointF(halfSegmentWidth, middle - fSegmentSpacing);
            m_SegmentTemplate[1][4] = new PointF(0, middle - halfSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[1][5] = new PointF(0, top + fSegmentWidth + fSegmentSpacing);

            // E
            m_SegmentTemplate[2][0] = new PointF(halfSegmentWidth, middle + fSegmentSpacing);
            m_SegmentTemplate[2][1] = new PointF(fSegmentWidth, middle + halfSegmentWidth + fSegmentSpacing);
            m_SegmentTemplate[2][2] = new PointF(fSegmentWidth, bottom - fSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[2][3] = new PointF(halfSegmentWidth, bottom - halfSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[2][4] = new PointF(0, bottom - fSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[2][5] = new PointF(0, middle + halfSegmentWidth + fSegmentSpacing);

            // D
            m_SegmentTemplate[3][0] = new PointF(halfSegmentWidth + fSegmentSpacing, bottom - halfSegmentWidth);
            m_SegmentTemplate[3][1] = new PointF(fSegmentWidth + fSegmentSpacing, bottom - fSegmentWidth);
            m_SegmentTemplate[3][2] = new PointF(width - fSegmentWidth - fSegmentSpacing, bottom - fSegmentWidth);
            m_SegmentTemplate[3][3] = new PointF(width - halfSegmentWidth - fSegmentSpacing, bottom - halfSegmentWidth);
            m_SegmentTemplate[3][4] = new PointF(width - fSegmentWidth - fSegmentSpacing, bottom);
            m_SegmentTemplate[3][5] = new PointF(fSegmentWidth + fSegmentSpacing, bottom);

            // C
            m_SegmentTemplate[4][0] = new PointF(width - halfSegmentWidth, middle + fSegmentSpacing);
            m_SegmentTemplate[4][1] = new PointF(width, middle + halfSegmentWidth + fSegmentSpacing);
            m_SegmentTemplate[4][2] = new PointF(width, bottom - fSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[4][3] = new PointF(width - halfSegmentWidth, bottom - halfSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[4][4] = new PointF(width - fSegmentWidth, bottom - fSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[4][5] = new PointF(width - fSegmentWidth, middle + halfSegmentWidth + fSegmentSpacing);

            // B
            m_SegmentTemplate[5][0] = new PointF(width - halfSegmentWidth, top + halfSegmentWidth + fSegmentSpacing);
            m_SegmentTemplate[5][1] = new PointF(width, top + fSegmentWidth + fSegmentSpacing);
            m_SegmentTemplate[5][2] = new PointF(width, middle - halfSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[5][3] = new PointF(width - halfSegmentWidth, middle - fSegmentSpacing);
            m_SegmentTemplate[5][4] = new PointF(width - fSegmentWidth, middle - halfSegmentWidth - fSegmentSpacing);
            m_SegmentTemplate[5][5] = new PointF(width - fSegmentWidth, top + fSegmentWidth + fSegmentSpacing);

            // A
            m_SegmentTemplate[6][0] = new PointF(halfSegmentWidth + fSegmentSpacing, top + halfSegmentWidth);
            m_SegmentTemplate[6][1] = new PointF(fSegmentWidth + fSegmentSpacing, top);
            m_SegmentTemplate[6][2] = new PointF(width - fSegmentWidth - fSegmentSpacing, top);
            m_SegmentTemplate[6][3] = new PointF(width - halfSegmentWidth - fSegmentSpacing, top + halfSegmentWidth);
            m_SegmentTemplate[6][4] = new PointF(width - fSegmentWidth - fSegmentSpacing, top + fSegmentWidth);
            m_SegmentTemplate[6][5] = new PointF(fSegmentWidth + fSegmentSpacing, top + fSegmentWidth);

            // Now apply shear to the template
            float shear_rads = (float)m_DigitShear * (3.1415927f / 180f);
            float slope = (float)Math.Sin(shear_rads);

            for (int seg=0;seg<7;seg++)
                for (int p=0;p<=5;p++)
                {
                    // |    *    *.X = sin(s) + *.Y
                    // |   /
                    // |  /
                    // |s/       s = shear
                    // |/

                    m_SegmentTemplate[seg][p].X += slope * (bottom - m_SegmentTemplate[seg][p].Y);
                }

            // Decimal point

            m_DecimalTemplate = new System.Drawing.RectangleF(width + (float)m_DigitSpacing / 2f - halfSegmentWidth, bottom - fSegmentWidth, fSegmentWidth, fSegmentWidth);
        }

        /// <summary>
        /// Draws a single digit and decimal point
        /// </summary>
        /// <param name="p_DigitNumber">Digit number (for drawing data from internal registers)</param>
        /// <param name="p_Xabs">Absolute X offset of left-hand side of digit</param>
        private void DrawDigit(int p_DigitNumber, Graphics g, byte p_DrawBits)
        {
            Matrix m = new System.Drawing.Drawing2D.Matrix();
            byte pattern = GetDisplayPatternForDigit(p_DigitNumber);
            float p_Xabs = ((float)p_DigitNumber * ((this.Width - (float)m_SegmentWidth) / 8f));

            m.Translate(p_Xabs, 0);

            g.Transform = m;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            for (int bit=0;bit<7;bit++)
            {
                if ((int)(p_DrawBits & (1 << bit)) != 0)
                {
                    // Segment should be lit

                    g.FillPolygon((pattern & (1 << bit)) != 0 ? m_SegmentBrush : m_BackgroundBrush, m_SegmentTemplate[bit]);
                }
            }

            if ((p_DrawBits & 0x80) != 0)
            {
                // Draw the decimal point

                g.FillEllipse(((pattern & 0x80) != 0) ? m_SegmentBrush : m_BackgroundBrush, m_DecimalTemplate);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { e.Graphics.Clear(this.BackColor); }

        protected override void OnPaint(PaintEventArgs e)
        {
            byte redrawEverythingMask = 0xff;
            
            base.OnPaint(e);

            for (int i = 0; i <= 7;i++)
                DrawDigit(i, e.Graphics, redrawEverythingMask);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            SetSegmentTemplate();
            RedrawControl(true);
        }

        #endregion

        #region MAX7219

        protected MAX7219Display m_NextInChain;
        private byte m_LowByte = 0;
        private byte m_HighByte = 0;

        private byte[] m_Registers = new byte[16];

        private System.Text.Encoding m_Encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

        public const int REG_NOOP = 0x00;
        public const int REG_DIGIT0 = 0x01;
        public const int REG_DIGIT1 = 0x02;
        public const int REG_DIGIT2 = 0x03;
        public const int REG_DIGIT3 = 0x04;
        public const int REG_DIGIT4 = 0x05;
        public const int REG_DIGIT5 = 0x06;
        public const int REG_DIGIT6 = 0x07;
        public const int REG_DIGIT7 = 0x08;
        public const int REG_DECODE_MODE = 0x09;
        public const int REG_INTENSITY = 0x0A;
        public const int REG_SCAN_LIMIT = 0x0B;
        public const int REG_SHUTDOWN = 0x0C;
        public const int REG_DISPLAY_TEST = 0x0F;

        public MAX7219Display NextInChain
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

        public void ClockData(byte data)
        {
            // data -> m_LowByte -> m_HighByte -> next display

            byte carry = m_HighByte;

            m_HighByte = m_LowByte;
            m_LowByte = data;

            if (m_NextInChain != null)
                m_NextInChain.ClockData(carry);
        }

        public void LoadData()
        {
            int reg = (int)(m_HighByte & 0x0f);
            byte lastData = m_Registers[reg];

            m_Registers[reg] = m_LowByte;

            if (reg >= REG_DIGIT1 && reg <= REG_DIGIT7)
            {
                // Compute only what has changed to preserve GDI calls
                byte diff = (byte)(lastData ^ m_LowByte);

                if (diff != 0)
                {
                    Graphics g = this.CreateGraphics();

                    DrawDigit(reg - REG_DIGIT1, g, diff);

                    g.Dispose();
                }
            }
            else
                this.RedrawControl(true); // Global register change

            if (m_NextInChain != null)
                m_NextInChain.LoadData();
        }



        #endregion
    }
}
