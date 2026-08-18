using Logic_Gate_Builder.Logic_Gate_Classes;
using Logic_Gate_Builder.UI_Classes.Command_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic_Gate_Builder.UI_Classes
{
    public partial class GateComp : UserControl
    {
        private IGate controllerComp;
        private bool isDragging;
        private MyList<Button> inputButtons = new MyList<Button>();
        private MyList<bool> inputButtonsUsed = new MyList<bool>();
        private Button outputButton;
        private int referenceToGateCompStates;
        private bool isSelected;
        private Panel outputPanel;
        private int savedSwitchState;
        private int currentGateFormat;
        public GateComp(IGate cC, int rTGCS, int currentFormat)
        {
            currentGateFormat = currentFormat;
            InitializeComponent();
            setupContextMenu();

            isSelectedPanel.BackColor = Color.Red;
            isSelected = false;
            referenceToGateCompStates = rTGCS;
            controllerComp = cC;
            this.Name = cC.getName();
            isDragging = false;
            string gateType = controllerComp.getGateType();
            int inputCount = -1;
            switch (gateType)
            {
                case "AND":
                    inputCount = 2;
                    break;
                case "NAND":
                    inputCount = 2;
                    break;
                case "NOR":
                    inputCount = 2;
                    break;
                case "OR":
                    inputCount = 2;
                    break;
                case "NXOR":
                    inputCount = 2;
                    break;
                case "XOR":
                    inputCount = 2;
                    break;
                case "NOT":
                case "LAMP":
                    inputCount = 1;
                    break;
                case "SWITCH":
                    inputCount = 0;
                    savedSwitchState = 0;
                    break;
                case "CUSTOM":
                    CustomGate g = controllerComp as CustomGate;
                    inputCount = g.getNumberOfInputs();
                    break;

            }
            setBackgroundImage(currentFormat);
            if (gateType != "SWITCH")
            {
                if (inputCount != 1)
                {
                    int difference = 150 / inputCount;
                    int runningTotal = 0;
                    for (int i = 0; i < inputCount; i++)
                    {
                        Button inputButton = new Button();
                        inputButton.Size = new Size(25, 25);
                        inputButton.Location = new Point(0, runningTotal + 20);
                        runningTotal += difference;
                        inputButton.Click += InputButton_Click;
                        inputButtons.add(inputButton);
                        this.Controls.Add(inputButton);
                    }
                }
                else
                {
                    Button inputButton = new Button();
                    inputButton.Size = new Size(25, 25);
                    inputButton.Location = new Point(0, 62);
                    inputButton.Click += InputButton_Click;
                    inputButtons.add(inputButton);
                    this.Controls.Add(inputButton);
                }

            }
            else
            {
                CheckBox switchBox = new CheckBox();
                switchBox.Size = new Size(25, 25);
                switchBox.Location = new Point(0, 62);
                switchBox.MouseClick += ToggleSwitch_Click;
                this.Controls.Add(switchBox);
            }
            if (gateType != "LAMP")
            {
                outputButton = new Button();
                outputButton.Size = new Size(25, 25);
                outputButton.Location = new Point(this.Width - 20, (this.Height / 2) - 15);
                outputButton.Click += OutputButton_Click;
                this.Controls.Add(outputButton);

            }
            else
            {
                outputPanel = new Panel();
                outputPanel.Size = new Size(40, 40);
                outputPanel.Location = new Point(
                    (this.Width - outputPanel.Width)/2,             
                    (this.Height - outputPanel.Height) / 2      
                );
                outputPanel.BackColor = Color.Black;
                this.Controls.Add(outputPanel); 
                outputPanel.BringToFront();
            }
            for (int i = 0; i < inputButtons.getLength(); i++)
            {
                inputButtonsUsed.add(false);
            }

        }

        public void setBackgroundImage(int num) {
            string imagePath = "R/G/" + this.getGate().getGateType() + "/F" + num.ToString() + ".png";

            try
            {
                PictureBox gateImage = new PictureBox
                {
                    Image = Image.FromFile(imagePath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill,
                    Enabled = false
                };
                this.BackgroundImage = Image.FromFile(imagePath);
                this.BackgroundImageLayout = ImageLayout.Zoom;


                if (controllerComp.getGateType() != "CUSTOM")
                {
                    GateName.Visible = false;
                }
                else
                {
                    string newStr = "";
                    for (int i = 0; i < controllerComp.getName().Length; i++)
                    {
                        if (i >= 6)
                        {
                            newStr += newStr + controllerComp.getName()[i];
                        }
                    }
                    GateName.Text = newStr;
                }

            }
            catch (Exception ex)
            {
                if (controllerComp.getGateType() == "LAMP")
                {
                    GateName.Visible = false;

                }
                else if (controllerComp.getGateType() == "SWITCH")
                {
                    string initialString = controllerComp.getName();
                    string updatedString = "S";
                    for (int i = 0; i < initialString.Length; i++)
                    {
                        if (i >= 6)
                        {
                            updatedString += initialString[i];
                        }
                    }
                    GateName.Text = updatedString;
                }
                else
                {
                    GateName.Text = controllerComp.getName();
                }
            }

        }
        private void GateName_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            selectedSelf();
        }
        private void GateName_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        private void GateName_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == true)
            {
                
                    MainForm mainForm = (MainForm)this.FindForm();
                    Panel canvas = mainForm.getCanvasPanel();
                    MyList<Point> oldPositions = new MyList<Point>();

                    for (int i = 0; i < inputButtons.getLength(); i++)
                    {
                        Button inputButton = inputButtons.getItem(i);
                        Point oldInputFormPos = mainForm.getCanvasPanel().PointToClient(inputButton.PointToScreen(Point.Empty));
                        oldPositions.add(oldInputFormPos);
                    }
                    if (outputButton != null)
                    {
                        Point oldOutputFormPos = mainForm.getCanvasPanel().PointToClient(outputButton.PointToScreen(Point.Empty));
                        oldPositions.add(oldOutputFormPos);
                    }


                    Point newLocation = this.Parent.PointToClient(Cursor.Position);
                    this.Location = new Point(newLocation.X - 75, newLocation.Y - 75);
                    MyList<Point> newPositions = new MyList<Point>();
                    for (int i = 0; i < inputButtons.getLength(); i++)
                    {
                        Button inputButton = inputButtons.getItem(i);
                        Point newInputFormPos = mainForm.getCanvasPanel().PointToClient(inputButton.PointToScreen(Point.Empty));
                        newPositions.add(newInputFormPos);
                    }
                    if (outputButton != null)
                    {
                        Point newOutputFormPos = mainForm.getCanvasPanel().PointToClient(outputButton.PointToScreen(Point.Empty));
                        newPositions.add(newOutputFormPos);
                    }
                    MyList<dynamic> passingInfo = new MyList<dynamic>();
                    passingInfo.add(this);
                    for (int i = 0; i < oldPositions.getLength(); i++)
                    {
                        Point[] buttonTran = new Point[] { oldPositions.getItem(i), newPositions.getItem(i) };
                        passingInfo.add(buttonTran);
                    }
                    mainForm.updateConnections(passingInfo);
                

            }
        }
        public IGate getGate()
        {
            return controllerComp;
        }
        private void InputButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.FindForm();
            Button button = (Button)sender;
            int position = -1;
            for (int i = 0; i < inputButtons.getLength(); i++)
            {
                if ((Button)sender == inputButtons.getItem(i))
                {
                    position = i+1;
                }
            }
            Panel canvas = mainForm.getCanvasPanel();
            Point panelPos = canvas.PointToClient(button.PointToScreen(Point.Empty));
            mainForm.endConnection(this, panelPos, position);
            selectedSelf();
        }
        private void OutputButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.FindForm();
            Button button = (Button)sender;
            Panel canvas = mainForm.getCanvasPanel();
            Point panelPos = canvas.PointToClient(button.PointToScreen(Point.Empty));
            mainForm.beginConnection(this, panelPos);
            selectedSelf();
        }
        private void ToggleSwitch_Click(object sender, EventArgs e)
        {
            Switch gate = controllerComp as Switch;
            if (gate.getOutput().getVal() == 0)
            {
                gate.getOutput().setVal(1);
                savedSwitchState = 1;
            }
            else
            {
                gate.getOutput().setVal(0);
                savedSwitchState = 0;
            }
            selectedSelf();
        }
        public void updateLampColour(int output)
        {
            if (output == 0)
            {
                outputPanel.BackColor = Color.Black;
            }
            else
            {
                outputPanel.BackColor = Color.Yellow;
            }
        }
        private void setupContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy");
            copyItem.Click += CopyGate_Click;
            menu.Items.Add(copyItem);
            ToolStripMenuItem cutItem = new ToolStripMenuItem("Cut");
            cutItem.Click += CutGate_Click;
            menu.Items.Add(cutItem);
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
            deleteItem.Click += DeleteGate_Click;
            menu.Items.Add(deleteItem);
            this.ContextMenuStrip = menu;
        }
        public void DeleteGate_Click(object sender, EventArgs e)
        {
            if (controllerComp.getGateType() != "SWITCH")
            {
                dynamic gate = controllerComp as dynamic;
                gate.breakAllInputs();
            }
            MainForm mainForm = (MainForm)this.FindForm();
            DeleteGate newAction = new DeleteGate(this, referenceToGateCompStates);
            mainForm.getUndoStack().push(newAction);
            mainForm.deleteGate(this);
            Control parent = this.Parent;
            parent.Controls.Remove(this);
            this.Dispose();

        }
        public void deleteSelfFromOutside()
        {
            if (controllerComp.getGateType() != "SWITCH")
            {
                dynamic gate = controllerComp as dynamic;
                gate.breakAllInputs();
            }
            MainForm mainForm = (MainForm)this.FindForm();
            mainForm.deleteGate(this);
            Control parent = this.Parent;
            parent.Controls.Remove(this);
            this.Dispose();
        }
        private void CopyGate_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.FindForm();
            mainForm.copyGate(controllerComp.getGateType());
        }
        public void CutGate_Click(object sender, EventArgs e)
        {
            CopyGate_Click(sender, e);
            DeleteGate_Click(sender, e);

        }
        public Point getOutputButtonPoint()
        {
            if (controllerComp.getGateType() != "LAMP")
            {
                Point screenPoint = outputButton.PointToScreen(Point.Empty);
                Point formPoint = this.FindForm().PointToClient(screenPoint);
                return formPoint;
            }
            else
            {
                Point screenPoint = outputPanel.PointToScreen(Point.Empty);
                Point formPoint = this.FindForm().PointToClient(screenPoint);
                return formPoint;
            }

        }
        public Point getInputButtonPoint()
        {
            int nextFreeInput = -1;
            for (int i = 0; i < inputButtonsUsed.getLength(); i++)
            {
                if (inputButtonsUsed.getItem(i) == false)
                {
                    inputButtonsUsed.setVal(i, true);
                    nextFreeInput = i;
                    break;
                }
            }
            if (nextFreeInput != -1)
            {
                Point screenPoint = inputButtons.getItem(nextFreeInput).PointToScreen(Point.Empty);
                Point formPoint = this.FindForm().PointToClient(screenPoint);
                return formPoint;
            }
            else
            {
                throw new Exception("No more inputs are available.");
            }
        }
        public GateComp ExportState()
        {
            try
            {
                GateComp returnGC = new GateComp(this.controllerComp.exportComponent(), referenceToGateCompStates, currentGateFormat);
                MainForm mainForm = (MainForm)this.FindForm();
                if (mainForm != null) {
                    mainForm.increaseComponentCount();
                }
                returnGC.Location = this.Location;
                return returnGC;
            }
            catch  (Exception e){
                throw new Exception("An error occured.", e);
            }
        }
        public bool getIsSelected()
        {
            return isSelected;
        }
        private void GateComp_MouseClick(object sender, MouseEventArgs e)
        {
            selectedSelf();
        }
        private void selectedSelf() {
            isSelected = true;
            isSelectedPanel.BackColor = Color.Green;
            MainForm mainForm = (MainForm)this.FindForm();
            mainForm.deselectAllGatesApartFrom(this);
        }
        public void deselectSelf() {
            isSelected = false;
            isSelectedPanel.BackColor = Color.Red;
        }

        public void resetSwitchGateState() {
            Switch gate = controllerComp as Switch;

            if (savedSwitchState == 0)
            {
                gate.getOutput().setVal(0);   
            }
            else {
                gate.getOutput().setVal(1);
            }
        }
    }
}
