using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LEDJebVirtualPanel
{
    public class PanelLayoutItem
    {
        private Control cDisplay = null;
        private Label cLabel = null;

        public double X;
        public double Y;

        private double m_DisplayWidthToHeightRatio = 1;
        private System.Drawing.Size m_OrigionalSize;

        public PanelLayoutItem()
        {

        }

        public PanelLayoutItem(Control p_DisplayObj, Control p_LabelObj, double p_X, double p_Y, string p_Name)
        {
            Bind(p_DisplayObj, p_LabelObj);

            X = p_X / 100D;
            Y = p_Y / 100D;

            if (cLabel != null)
                cLabel.Text = p_Name;
        }

        public void SetOrigionalSize(System.Drawing.Size p_OrigionalContainerSize)
        {
            m_OrigionalSize = p_OrigionalContainerSize;
        }

        public void Bind(Control p_DisplayObj, Control p_LabelObj)
        {
            cDisplay = p_DisplayObj;
            cLabel = p_LabelObj as Label;

            if (cLabel != null)
            {
                cLabel.AutoSize = false;
                cLabel.Width = cDisplay.Width;
                cLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }

            if (p_DisplayObj != null)
                m_DisplayWidthToHeightRatio = p_DisplayObj.Width / p_DisplayObj.Height;
        }

        public void PerformLayout()
        {
            if (cDisplay != null)
            {
                Control container = cDisplay;

                while (container.Parent != null && container.Parent != container)
                    container = container.Parent;

                PerformLayout(container.Size);
            }
        }

        public void PerformLayout(System.Drawing.Size p_ContainerSize)
        {
            cDisplay.Location = new System.Drawing.Point((int)(X * p_ContainerSize.Width), (int)(Y * p_ContainerSize.Height));
            cLabel.Location = new System.Drawing.Point(cDisplay.Location.X, cDisplay.Location.Y - cLabel.Height - 5);

            // Constrain size to X direction

            
        }
    }
}
