using Logic_Gate_Builder.Functionality_Classes;
using Logic_Gate_Builder.UI_Classes.Command_Classes;
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
    public partial class CircuitCostCalc : Form
    {
        private TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
        private MyList<CircuitCostCalcGateTInfo> gateSummary = new MyList<CircuitCostCalcGateTInfo>();
        private Label resultLabel = new Label();
        private Button executeButton = new Button();
        public CircuitCostCalc(MainForm mFR)
        {
            InitializeComponent();
            foreach (Control ctrl in mFR.getCanvasPanel().Controls)
            {
                if (ctrl is GateComp gateComp)
                {
                    bool exists = false;
                    int index = -1;
                    for (int i = 0; i < gateSummary.getLength(); i++) {
                        if (gateComp.getGate().getGateType() == gateSummary.getItem(i).getType()) {
                            exists = true;
                            index = i;
                            break;
                        }
                    }
                    if (exists == true)
                    {
                        gateSummary.getItem(index).setAmount(gateSummary.getItem(index).getAmount() + 1);
                    }
                    else
                    {
                        CircuitCostCalcGateTInfo newInfo = new CircuitCostCalcGateTInfo();
                        newInfo.setType(gateComp.getGate().getGateType());
                        newInfo.setAmount(1);
                        gateSummary.add(newInfo);
                    }
                }
            }
            this.Load += CostCalculatorForm_Load;
        }
        private void CostCalculatorForm_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(300, 200);
            this.MaximumSize = new Size(400, 800);
            this.AutoScroll = true;
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
            resultLabel = new Label();
            resultLabel.Location = new Point(10, 0); 
            resultLabel.AutoSize = true;
            resultLabel.Text = "Total Cost: 0";
            this.Controls.Add(resultLabel);
            executeButton = new Button();
            executeButton.Size = new Size(100, 35);
            executeButton.Text = "Calculate";
            executeButton.Location = new Point(10, 45);
            executeButton.Click += ExecuteButton_Click;
            this.Controls.Add(executeButton);

            int row = 0;
            if (gateSummary.getLength() > 0)
            {
                tableLayoutPanel = new TableLayoutPanel();
                tableLayoutPanel.Width = 300;
                tableLayoutPanel.Location = new Point(10, 90);
                tableLayoutPanel.ColumnCount = 3;
                tableLayoutPanel.RowCount = gateSummary.getLength() + 1;
                tableLayoutPanel.AutoSize = true;
                tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                tableLayoutPanel.Padding = new Padding(5);
                tableLayoutPanel.RowCount = gateSummary.getLength() + 1;
                Label componentLabel = new Label();
                componentLabel.Text = "Gate Type";
                Label priceLabel = new Label();
                priceLabel.Text = "Price";
                Label quantityLabel = new Label();
                quantityLabel.Text = "Quantity";

                tableLayoutPanel.Controls.Add(componentLabel, 0, 0);
                tableLayoutPanel.Controls.Add(priceLabel, 1, 0);
                tableLayoutPanel.Controls.Add(quantityLabel, 2, 0);
                for (int i = 0; i < gateSummary.getLength(); i++)
                {
                    CircuitCostCalcGateTInfo gInfo = gateSummary.getItem(i);
                    Label lblName = new Label() { Text = gInfo.getType(), AutoSize = true };
                    TextBox txtPrice = new TextBox() { Width = 80 };
                    txtPrice.Text = "0";
                    txtPrice.KeyPress += PriceBox_KeyPress1;

                    txtPrice.Leave += PriceBox_Leave;
                    Label lblCount = new Label() { Text = gInfo.getAmount().ToString(), AutoSize = true };

                    tableLayoutPanel.Controls.Add(lblName, 0, row + 1);
                    tableLayoutPanel.Controls.Add(txtPrice, 1, row + 1);
                    tableLayoutPanel.Controls.Add(lblCount, 2, row + 1);

                    row++;
                }
                this.Controls.Add(tableLayoutPanel);
                this.ClientSize = new Size(
                    tableLayoutPanel.Right + 5,
                    tableLayoutPanel.Bottom + 5
                );
            }
            else {
                Label label = new Label() { Text = "No gates placed", AutoSize = true, Location = new Point(0, 100)};
                this.Controls.Add(label);
                this.ClientSize = new Size(
                    label.Right + 5,
                    label.Bottom + 5
                );
            }


            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            //this.MinimizeBox = true;


        }
        private void PriceBox_KeyPress1(object sender, KeyPressEventArgs e) {
            bool isControl = char.IsControl(e.KeyChar);
            bool isDigit = char.IsDigit(e.KeyChar);
            bool isDot = e.KeyChar == '.';
            TextBox s = sender as TextBox;
            if (isDot == true && s.Text.Contains("."))
            {
                e.Handled = true;
            }
            else if (isDigit == true && BinaryFunctions.areAtLeast2DecimalPlaces(s.Text) == true)
            {
                e.Handled = true;
            }
            else if (!isControl && !isDigit && !isDot)
            {
                e.Handled = true;
            }
        }
        private void PriceBox_Leave(object sender, EventArgs e) {
            TextBox s = sender as TextBox;
            if (FileHandling.isNullOrWhiteSpace(s.Text) == true) {
                s.Text = "0";
            }
        }
        private void calculateTotalCost()
        {
            double totalCost = 0;
            for (int i = 1; i < tableLayoutPanel.RowCount; i++)
            {
                TextBox txtPrice = tableLayoutPanel.GetControlFromPosition(1, i) as TextBox;
                Label lblCount = tableLayoutPanel.GetControlFromPosition(2, i) as Label;
                totalCost = totalCost + Convert.ToDouble(txtPrice.Text)*Convert.ToDouble(lblCount.Text);
            }
            resultLabel.Text = "Total Cost: " + totalCost.ToString();
            this.Invalidate();
        }
        private void ExecuteButton_Click(object sender, EventArgs e) {
            calculateTotalCost();
        }
    }
}
