using Logic_Gate_Builder.Logic_Gate_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic_Gate_Builder.UI_Classes
{
    public partial class TruthTableForm : Form
    {
        private MyList<MyList<string>> truthTable;
        private DataGridView dataGridView;
        private int compCount;
        private int numberOfInputs;
        private MainForm mainFormRef;
        private int numberOfOutputs;
        public TruthTableForm(MyList<MyList<string>> tT, ref int compCount, MainForm mFR)
        {
            InitializeComponent();
            Button generateCustomComponentButton = new Button();
            generateCustomComponentButton.Dock = DockStyle.Bottom;
            generateCustomComponentButton.Text = "Generate custom component";
            generateCustomComponentButton.Height = 50;
            generateCustomComponentButton.Click += CustomCompCreate_Click;
            this.Controls.Add(generateCustomComponentButton);
            mainFormRef = mFR;
            foreach (Control control in mFR.getCanvasPanel().Controls)
            {
                if (control is GateComp gateComp)
                {
                    if (gateComp.getGate().getGateType() == "LAMP")
                    {
                        numberOfOutputs++;
                    }
                }
            }
            dataGridView = new DataGridView() {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false, 
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,

            };
            truthTable = tT;
            MyList<string> headerRow = truthTable.getItem(0);
            numberOfInputs = headerRow.getLength() - 1;
            for (int i = 0; i < headerRow.getLength(); i++)
            {
                dataGridView.Columns.Add($"Col{i}", headerRow.getItem(i));
            }
            for (int i =1; i < truthTable.getLength();i++)
            {
                    MyList<string> row = truthTable.getItem(i);
                    dataGridView.Rows.Add(row.getList());
            }
            this.Controls.Add(dataGridView);


        }
        private void CustomCompCreate_Click(object sender, EventArgs e)
        {
            if (numberOfOutputs == 1)
            {
                try
                {
                    if (truthTable.getItem(0).getLength() <= 5)
                    {
                        CustomGate newGate = new CustomGate(compCount, numberOfInputs, truthTable);
                        mainFormRef.saveCustomGate(newGate);
                        MessageBox.Show("Custom gate created.");
                        this.Close();
                        this.Dispose();
                    }
                    else {
                        MessageBox.Show("Exceeded input limit for custom gate (max 4).");
                    }

                }
                catch (Exception er)
                {
                    MessageBox.Show("An error occured when creating the custom gate." + er);
                }
            }
            else {
                MessageBox.Show("Cannot create custom gate using more than 1 output.");
            }
        }
    }
}
