/*
 * LEDJeb MAX7219 Display
 * 
 * BASED ON Dmitry Brant's 7-Segment display
 *    http://www.codeproject.com/Articles/37800/Seven-segment-LED-Control-for-NET
 *    me@dmitrybrant.com
 *    http://dmitrybrant.com
 * 
 * Author: heaton84 (as in, added MAX7219 logic)
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
    public partial class MAX7219Display : UserControl
    {
        public MAX7219Display()
        {
            InitializeComponent();

            this.SuspendLayout();
            this.Name = "SevenSegmentArray";
            this.Size = new System.Drawing.Size(100, 25);
            this.Resize += new System.EventHandler(this.SevenSegmentArray_Resize);
            this.ResumeLayout(false);

            this.TabStop = false;
            elementPadding = new Padding(4, 4, 4, 4);
            RecreateSegments(8);
        }

        #region 7-Segment

        /// <summary>
        /// Array of segment controls that are currently children of this control.
        /// </summary>
        private SevenSegment[] segments = null;

        /// <summary>
        /// Change the number of elements in our LED array. This destroys
        /// the previous elements, and creates new ones in their place, applying
        /// all the current options to the new ones.
        /// </summary>
        /// <param name="count">Number of elements to create.</param>
        private void RecreateSegments(int count)
        {
            if (segments != null)
                for (int i = 0; i < segments.Length; i++) { segments[i].Parent = null; segments[i].Dispose(); }

            if (count <= 0) return;
            segments = new SevenSegment[count];

            for (int i = 0; i < count; i++)
            {
                segments[i] = new SevenSegment();
                segments[i].Parent = this;
                segments[i].Top = 0;
                segments[i].Height = this.Height;
                segments[i].Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                segments[i].Visible = true;
            }

            ResizeSegments();
            UpdateSegments();
            this.Value = theValue;
        }

        /// <summary>
        /// Align the elements of the array to fit neatly within the
        /// width of the parent control.
        /// </summary>
        private void ResizeSegments()
        {
            int segWidth = this.Width / segments.Length;
            for (int i = 0; i < segments.Length; i++)
            {
                segments[i].Left = this.Width * (segments.Length - 1 - i) / segments.Length;
                segments[i].Width = segWidth;
            }
        }

        /// <summary>
        /// Update the properties of each element with the properties
        /// we have stored.
        /// </summary>
        private void UpdateSegments()
        {
            for (int i = 0; i < segments.Length; i++)
            {
                segments[i].ColorBackground = colorBackground;
                segments[i].ColorDark = colorDark;
                segments[i].ColorLight = colorLight;
                segments[i].ElementWidth = elementWidth;
                segments[i].ItalicFactor = italicFactor;
                segments[i].DecimalShow = showDot;
                segments[i].Padding = elementPadding;
            }
        }

        private void SevenSegmentArray_Resize(object sender, EventArgs e) { ResizeSegments(); }

        protected override void OnPaintBackground(PaintEventArgs e) { e.Graphics.Clear(colorBackground); }

        private int elementWidth = 8;
        private float italicFactor = 0.0F;
        private Color colorBackground = Color.DarkGray;
        private Color colorDark = Color.DimGray;
        private Color colorLight = Color.Red;
        private bool showDot = true;
        private Padding elementPadding;

        /// <summary>
        /// Background color of the LED array.
        /// </summary>
        public Color ColorBackground { get { return colorBackground; } set { colorBackground = value; UpdateSegments(); } }
        /// <summary>
        /// Color of inactive LED segments.
        /// </summary>
        public Color ColorDark { get { return colorDark; } set { colorDark = value; UpdateSegments(); } }
        /// <summary>
        /// Color of active LED segments.
        /// </summary>
        public Color ColorLight { get { return colorLight; } set { colorLight = value; UpdateSegments(); } }

        /// <summary>
        /// Width of LED segments.
        /// </summary>
        public int ElementWidth { get { return elementWidth; } set { elementWidth = value; UpdateSegments(); } }
        /// <summary>
        /// Shear coefficient for italicizing the displays. Try a value like -0.1.
        /// </summary>
        public float ItalicFactor { get { return italicFactor; } set { italicFactor = value; UpdateSegments(); } }
        /// <summary>
        /// Specifies if the decimal point LED is displayed.
        /// </summary>
        public bool DecimalShow { get { return showDot; } set { showDot = value; UpdateSegments(); } }

        /// <summary>
        /// Number of seven-segment elements in this array.
        /// </summary>
        public int ArrayCount { get { return segments.Length; } set { if ((value > 0) && (value <= 100)) RecreateSegments(value); } }
        /// <summary>
        /// Padding that applies to each seven-segment element in the array.
        /// Tweak these numbers to get the perfect appearance for the array of your size.
        /// </summary>
        public Padding ElementPadding { get { return elementPadding; } set { elementPadding = value; UpdateSegments(); } }


        private string theValue = null;
        /// <summary>
        /// The value to be displayed on the LED array. This can contain numbers,
        /// certain letters, and decimal points.
        /// </summary>
        public string Value
        {
            get { return theValue; }
            set
            {
                theValue = value;
                //for (int i = 0; i < segments.Length; i++) { segments[i].CustomPattern = 0; segments[i].DecimalOn = false; }
                if (theValue != null)
                {
                    int segmentIndex = 0;
                    for (int i = theValue.Length - 1; i >= 0; i--)
                    {
                        if (segmentIndex >= segments.Length) break;
                        if (theValue[i] == '.') segments[segmentIndex].DecimalOn = true;
                        else segments[segmentIndex++].Value = theValue[i].ToString();
                    }
                }
            }
        }

        #endregion

        #region MAX7219

        protected MAX7219Display m_NextInChain;
        private byte m_LowByte = 0;
        private byte m_HighByte = 0;

        private byte[] m_Registers = new byte[16];
        private char[] m_BCODE = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', 'E', 'H', 'L', 'P', ' ' };

        private System.Text.Encoding m_Encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");


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
            byte[] ReadoutBytes = new byte[ScanLimit * 2];
            int BytesDisplayed = 0;

            if (m_Registers[0x0C] == 0)
            {
                // In shutdown mode, blank display
                for (int i = 1; i <= ScanLimit; i++)
                    ReadoutBytes[i - 1] = (byte)' ';

                BytesDisplayed = 8;
            }
            else if (m_Registers[0x0F] == 1)
            {
                // Not in shutdown, and in test mode
                for (int i = 1; i <= ScanLimit; i++)
                    ReadoutBytes[i - 1] = (byte)'8';

                BytesDisplayed = 8;
            }
            else
            {
                // Not in shutdown, not in test mode

                for (int i = 1; i <= ScanLimit; i++)
                {
                    ReadoutBytes[BytesDisplayed++] = (byte)m_BCODE[m_Registers[i] & 0x0f];

                    if ((m_Registers[i] & 0x80) != 0)
                        ReadoutBytes[BytesDisplayed++] = (byte)'.';
                }

            }

            this.Value = m_Encoding.GetString(ReadoutBytes, 0, BytesDisplayed);
        }


        #endregion
    }
}
