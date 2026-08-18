using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.UI_Classes
{
    public class ViewPanel:Panel
    {
        private Point centreCoordinates;
        public ViewPanel() { 
            this.Scroll += OnScroll;
            centreCoordinates= new Point(750, 400);
        }
        public void OnScroll(object sender, EventArgs e) {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                int scrollX = -AutoScrollPosition.X;
                int scrollY = -AutoScrollPosition.Y;
                centreCoordinates = new Point(scrollX + ClientSize.Width / 2,
                    scrollY + ClientSize.Height / 2);


            }
        }
        public Point getCentreCoordinates() { 
            return centreCoordinates;
        }
    }
}
