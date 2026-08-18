using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic_Gate_Builder.UI_Classes
{
    public partial class GateLocatorForm : Form
    {
        private MyList<GateComp> searchingList = new MyList<GateComp>();
        private int searchIndex = 0;
        private MainForm mainFormReference;
        public GateLocatorForm(MainForm mFR)
        {
            InitializeComponent();
            InfoLabel.Text = "Item " + 0.ToString() + " out of " + searchingList.getLength().ToString() + ".";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            mainFormReference = mFR;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            searchingList = new MyList<GateComp>();
            string selectedGateType = comboBox1.Text;
            MyList<GateComp> currentGateComps = new MyList<GateComp>();
            foreach (Control ctrl in mainFormReference.getCanvasPanel().Controls)
            {
                if (ctrl is GateComp gateComp)
                {
                    if (gateComp.getGate().getGateType() == selectedGateType)
                    {
                        searchingList.add(gateComp);
                    }
                }
            }
            if (searchingList.getLength() > 0)
            {
                InfoLabel.Text = "Item " + (searchIndex + 1).ToString() + " out of " + searchingList.getLength().ToString() + ".";
                this.Invalidate();
                moveScreen();
            }
            else {
                MessageBox.Show("No gates found.");
            }

        }
        private void moveScreen()
        {
            if (searchingList.getLength() != 0)
            {
                GateComp locatedGate = searchingList.getItem(searchIndex);

                Point gatePosInCanvas = mainFormReference.getCanvasPanel().PointToClient(locatedGate.PointToScreen(Point.Empty));
                int targetScrollX = gatePosInCanvas.X - (mainFormReference.getViewPanel().ClientSize.Width / 2) + (locatedGate.Width / 2);
                int targetScrollY = gatePosInCanvas.Y - (mainFormReference.getViewPanel().ClientSize.Height / 2) + (locatedGate.Height / 2);
                mainFormReference.getViewPanel().AutoScrollPosition = new Point(
                    Math.Max(0, targetScrollX),
                    Math.Max(0, targetScrollY)
                );
            }
        }
        private void NextGateButton_Click(object sender, EventArgs e)
        {
            if (searchingList.getLength() > 0) {
                searchIndex = (searchIndex + 1) % searchingList.getLength();
                InfoLabel.Text = "Item " + (searchIndex + 1).ToString() + " out of " + searchingList.getLength().ToString() + ".";
                moveScreen();
            }

        }
        private void LastGateButton_Click(object sender, EventArgs e)
        {
            if (searchingList.getLength() > 0) {
                if (searchIndex > 0)
                {
                    searchIndex--;
                }
                else
                {
                    searchIndex = searchingList.getLength() - 1;
                }
                InfoLabel.Text = "Item " + (searchIndex + 1).ToString() + " out of " + searchingList.getLength().ToString() + ".";
                moveScreen();
            }

        }
    }
}
