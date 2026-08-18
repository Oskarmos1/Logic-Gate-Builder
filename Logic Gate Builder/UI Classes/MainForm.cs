using Logic_Gate_Builder.Functionality_Classes;
using Logic_Gate_Builder.Logic_Gate_Classes;
using Logic_Gate_Builder.UI_Classes;
using Logic_Gate_Builder.UI_Classes.Command_Classes;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
//1600,900
namespace Logic_Gate_Builder
{
    public partial class MainForm : Form
    {
        private MyList<IGate> gatesList;
        private int componentCount;
        private MyList<CustomGate> savedCustomGates = new MyList<CustomGate>();
        private GateComp currentSourceComp;
        private string currentlySavedGate;
        private ContextMenuStrip formContextMenu;
        private Stack<ICommand> undoStack = new Stack<ICommand>();
        private Stack<ICommand> redoStack = new Stack<ICommand>();
        private MyList<GateComp> gateCompStates = new MyList<GateComp>();
        private GateComp selectedGate = null;
        private ViewPanel viewPanel = new ViewPanel();
        private CanvasPanel canvasPanel = new CanvasPanel();
        private string preLoadedFile;
        private int currentFormatUsed = 1;
        public MainForm(string pLF)
        {
            preLoadedFile = pLF;
            this.Load += MainForm_Load;
            InitializeComponent();
            formContextMenu = new ContextMenuStrip();
            setupContextMenu();
            this.KeyPreview = true;
            gatesList = new MyList<IGate>();
            componentCount = 0;
            this.DoubleBuffered = true;
            panelSetup();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (preLoadedFile != null)
            {
                try
                {
                    MyList<IGate> loadedComponents;
                    switch (FileHandling.getFileEnding(preLoadedFile))
                    {
                        case ".txt":
                            loadedComponents = FileHandling.openLogicCircuit(preLoadedFile);
                            break;
                        case ".json":
                            loadedComponents = FileHandling.importFromJson(preLoadedFile);
                            break;
                        case ".xml":
                            loadedComponents = FileHandling.importFromXML(preLoadedFile);
                            break;
                        case ".csv":
                            loadedComponents = FileHandling.importFromCSV(preLoadedFile);
                            break;
                        default:
                            MessageBox.Show("Unrecognised file type.");
                            return;
                    }
                    spawnComponents(loadedComponents);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured when loading the file:" + ex);
                }
            }




        }
        private void panelSetup()
        {
            viewPanel.Dock = DockStyle.Fill;
            viewPanel.BackColor = Color.Gray;
            viewPanel.AutoScroll = true;
            canvasPanel.Size = new Size(5000, 5000);
            canvasPanel.BackColor = Color.White;
            canvasPanel.MouseClick += Form1_MouseClick;
            canvasPanel.MouseMove += Form1_MouseMove;
            viewPanel.Controls.Add(canvasPanel);

            this.Controls.Add(viewPanel);
        }
        private void setupContextMenu()
        {
            ToolStripMenuItem undo = new ToolStripMenuItem("Undo");
            undo.Click += Undo_Click;
            formContextMenu.Items.Add(undo);
            ToolStripMenuItem redo = new ToolStripMenuItem("Redo");
            redo.Click += Redo_Click;
            formContextMenu.Items.Add(redo);
            ToolStripMenuItem pasteItem = new ToolStripMenuItem("Paste");
            pasteItem.Click += Paste_Click;
            formContextMenu.Items.Add(pasteItem);

        }
        public void beginConnection(GateComp sC, Point sP)
        {
            if (canvasPanel.getIsDrawing() == false)
            {
                currentSourceComp = sC;
                canvasPanel.setCurrentSourcePoint(sP);
                canvasPanel.setIsDrawing(true);
            }
        }
        public void endConnection(GateComp tC, Point tP, int inputNum)
        {
            if (canvasPanel.getIsDrawing() == true)
            {
                if (tC != currentSourceComp)
                {
                    dynamic sourceGate = currentSourceComp.getGate();
                    dynamic targetGate = tC.getGate();
                    try
                    {
                        MyList<InputNode> targetGateInputs = targetGate.getInputs();
                        InputNode inputNode = targetGateInputs.getItem(inputNum - 1);
                        if (inputNode.getPreviousOutputGate() != null)
                        {
                            string getPrevGate = inputNode.getPreviousOutputGate();
                            MyList<Connection> connections = canvasPanel.getConnectionList();
                            for (int i = 0; i < connections.getLength(); i++)
                            {
                                Connection c = connections.getItem(i);
                                if (c.getTargetG().getGate().getName() == targetGate.getName() && c.getSourceG().getGate().getName() == inputNode.getPreviousOutputGate() && c.getTargetP() == tP)
                                {
                                    if (targetGate.getGateType() == "LAMP")
                                    {
                                        targetGate.breakInput();
                                    }
                                    else if (targetGate.getGateType() == "CUSTOM")
                                    {
                                        targetGate.breakInput(inputNum - 1);
                                    }
                                    else
                                    {
                                        targetGate.breakInput(inputNum);
                                    }
                                    dynamic sourceGateToBeBroken = c.getSourceG().getGate();
                                    OutputNode brokenOutput = sourceGateToBeBroken.getOutput();
                                    brokenOutput.oneInputBreakConnection(inputNode);
                                    connections.removeAt(i);

                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                    targetGate.connectToInput(sourceGate.getOutput(), inputNum);
                    Connection newC = new Connection(currentSourceComp, tC, canvasPanel.getCurrentSourcePoint(), tP);
                    canvasPanel.getConnectionList().add(newC);
                    undoStack.push(new AddConnection(newC, canvasPanel.getConnectionList().getLength() - 1));
                    canvasPanel.setCurrentSourcePoint(new Point());
                    currentSourceComp = null;
                    canvasPanel.setIsDrawing(false);
                    canvasPanel.Invalidate();
                }
            }
        }
        public void deleteGate(GateComp dG)
        {
            string deletedGateName = dG.getGate().getName();
            for (int i = 0; i < canvasPanel.getConnectionList().getLength(); i++)
            {
                Connection connection = canvasPanel.getConnectionList().getItem(i);
                if (connection.getSourceG() == dG)
                {
                    dynamic gateToBeModified = connection.getTargetG().getGate();
                    gateToBeModified.deleteInputConnectionFromPreviousGateName(deletedGateName);
                }
            }
            for (int i = 0; i < undoStack.getLength(); i++)
            {
                if (undoStack.peek(i).getCommandType() == "ADDC")
                {
                    AddConnection connectionControl = undoStack.peek(i) as AddConnection;
                    Connection connection = connectionControl.getCurrentConnection();
                    if (connection.getSourceG() == dG || connection.getTargetG() == dG)
                    {
                        undoStack.removeAt(i);
                        i--;
                    }
                }
            }
            for (int i = 0; i < redoStack.getLength(); i++)
            {
                if (redoStack.peek(i).getCommandType() == "ADDC")
                {
                    AddConnection connectionControl = redoStack.peek(i) as AddConnection;
                    Connection connection = connectionControl.getCurrentConnection();
                    if (connection.getSourceG() == dG || connection.getTargetG() == dG)
                    {
                        redoStack.removeAt(i);
                        i--;
                    }
                }
            }
            for (int i = 0; i < gatesList.getLength(); i++)
            {
                if (gatesList.getItem(i).getName() == deletedGateName)
                {
                    gatesList.removeAt(i);
                    break;
                }
            }
            //------
            MyList<int> removeIndexes = new MyList<int>();
            for (int i = 0; i < canvasPanel.getConnectionList().getLength(); i++)
            {
                Connection connection = canvasPanel.getConnectionList().getItem(i);

                if (connection.getSourceG() == dG || connection.getTargetG() == dG)
                {
                    removeIndexes.add(i);
                }
            }
            Stack<int> tempStack = new Stack<int>();
            for (int i = 0; i < removeIndexes.getLength(); i++)
            {
                tempStack.push(removeIndexes.getItem(i));
            }
            MyList<int> reversedRemoveIndexes = new MyList<int>();
            while (tempStack.isEmpty() == false)
            {
                reversedRemoveIndexes.add(tempStack.pop());
            }


            for (int i = 0; i < reversedRemoveIndexes.getLength(); i++)
            {
                canvasPanel.getConnectionList().removeAt(reversedRemoveIndexes.getItem(i));
            }

            canvasPanel.Invalidate();

        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            canvasPanel.setMousePos(e.Location);
            if (canvasPanel.getIsDrawing() == true)
            {
                canvasPanel.Invalidate();
            }
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Control clicked = canvasPanel.GetChildAtPoint(e.Location);
            if (e.Button == MouseButtons.Right)
            {
                formContextMenu.Show(canvasPanel, e.Location);
            }
            else
            {
                if (clicked == null)
                {
                    if (canvasPanel.getIsDrawing() == true)
                    {
                        canvasPanel.setCurrentSourcePoint(new Point());
                        currentSourceComp = null;
                        canvasPanel.setIsDrawing(false);
                        canvasPanel.Invalidate();
                    }
                    deselectAllgates();
                }
            }

        }
        public void updateConnections(MyList<dynamic> info)
        {
            GateComp movedGate = info.getItem(0);
            for (int i = 1; i < info.getLength(); i++)
            {
                Point[] pointT = info.getItem(i);
                for (int j = 0; j < canvasPanel.getConnectionList().getLength(); j++)
                {
                    Connection connection = canvasPanel.getConnectionList().getItem(j);
                    if (connection.getSourceG() == movedGate || connection.getTargetG() == movedGate)
                    {
                        if (connection.getSourceP() == pointT[0])
                        {
                            connection.setSourceP(pointT[1]);
                        }
                        if (connection.getTargetP() == pointT[0])
                        {
                            connection.setTargetP(pointT[1]);
                        }
                    }
                }
            }
            canvasPanel.Invalidate();
        }
        private Point getCentre(Point viewPanelCentre, GateComp newG)
        {
            Point result = new Point();
            result.X = viewPanelCentre.X - (newG.Width / 2);
            result.Y = viewPanelCentre.Y - (newG.Height / 2);
            return result;
        }
        private void ANDSpawnButton_Click(object sender, EventArgs e)
        {
            AND and = new AND(componentCount);
            gatesList.add(and);
            GateComp newG = new GateComp(and, gateCompStates.getLength(), currentFormatUsed);
            Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
            newG.Location = centre;
            canvasPanel.Controls.Add(newG);
            componentCount++;
            gateCompStates.add(newG);
            AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
            undoStack.push(newAdd);
        }
        private void NANDSpawnButton_Click(object sender, EventArgs e)
        {
            NAND nand = new NAND(componentCount);
            gatesList.add(nand);
            GateComp newG = new GateComp(nand, gateCompStates.getLength(), currentFormatUsed);
            Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
            newG.Location = centre;
            canvasPanel.Controls.Add(newG);
            componentCount++;
            gateCompStates.add(newG);
            AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
            undoStack.push(newAdd);
        }
        private void NORSpawnButton_Click(object sender, EventArgs e)
        {
            NOR nor = new NOR(componentCount);
            gatesList.add(nor);
            GateComp newG = new GateComp(nor, gateCompStates.getLength(), currentFormatUsed);
            Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
            newG.Location = centre;
            canvasPanel.Controls.Add(newG);
            componentCount++;
            gateCompStates.add(newG);
            AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
            undoStack.push(newAdd);
        }
        private void NOTSpawnButton_Click(object sender, EventArgs e)
        {
            NOT not = new NOT(componentCount);
            gatesList.add(not);
            GateComp newG = new GateComp(not, gateCompStates.getLength(), currentFormatUsed);
            Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
            newG.Location = centre;
            canvasPanel.Controls.Add(newG);
            componentCount++;
            gateCompStates.add(newG);
            AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
            undoStack.push(newAdd);
        }
        private void ORSpawnButton_Click(object sender, EventArgs e)
        {
            OR or = new OR(componentCount);
            gatesList.add(or);
            GateComp newG = new GateComp(or, gateCompStates.getLength(), currentFormatUsed);
            Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
            newG.Location = centre;
            canvasPanel.Controls.Add(newG);
            componentCount++;
            gateCompStates.add(newG);
            AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
            undoStack.push(newAdd);
        }
        private void XORSpawnButton_Click(object sender, EventArgs e)
        {
            XOR xor = new XOR(componentCount);
            gatesList.add(xor);
            GateComp newG = new GateComp(xor, gateCompStates.getLength(), currentFormatUsed);
            Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
            newG.Location = centre;
            canvasPanel.Controls.Add(newG);
            componentCount++;
            gateCompStates.add(newG);
            AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
            undoStack.push(newAdd);
        }
        private void nXORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NXOR nxor = new NXOR(componentCount);
            gatesList.add(nxor);
            GateComp newG = new GateComp(nxor, gateCompStates.getLength(), currentFormatUsed);
            Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
            newG.Location = centre;
            canvasPanel.Controls.Add(newG);
            componentCount++;
            gateCompStates.add(newG);
            AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
            undoStack.push(newAdd);
        }
        private void SWITCHSpawnButton_Click(object sender, EventArgs e)
        {
            Switch switch1 = new Switch(componentCount);
            gatesList.add(switch1);
            GateComp newG = new GateComp(switch1, gateCompStates.getLength(), currentFormatUsed);
            Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
            newG.Location = centre;
            canvasPanel.Controls.Add(newG);
            componentCount++;
            gateCompStates.add(newG);
            AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
            undoStack.push(newAdd);
        }
        private void LAMPSpawnButton_Click(object sender, EventArgs e)
        {
            bool canBeAdded = true;
            for (int i = 0; i < gatesList.getLength(); i++)
            {
                IGate c = gatesList.getItem(i);
                if (c.getGateType() == "LAMP")
                {
                    canBeAdded = false;
                    break;
                }
            }
            if (canBeAdded == true)
            {
                Lamp lamp1 = new Lamp(componentCount);
                gatesList.add(lamp1);
                GateComp newG = new GateComp(lamp1, gateCompStates.getLength(), currentFormatUsed);
                Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
                newG.Location = centre;
                canvasPanel.Controls.Add(newG);
                componentCount++;
                gateCompStates.add(newG);
                AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
                undoStack.push(newAdd);
            }
        }
        public void copyGate(string gateType)
        {
            currentlySavedGate = gateType;
        }
        private void Paste_Click(object sender, EventArgs e)
        {
            if (currentlySavedGate != null)
            {
                IGate newG = null;
                switch (currentlySavedGate)
                {
                    case "AND":
                        newG = new AND(componentCount);
                        break;
                    case "LAMP":
                        newG = new Lamp(componentCount);
                        break;
                    case "NAND":
                        newG = new NAND(componentCount);
                        break;
                    case "NOR":
                        newG = new NOR(componentCount);
                        break;
                    case "NOT":
                        newG = new NOT(componentCount);
                        break;
                    case "OR":
                        newG = new OR(componentCount);
                        break;
                    case "SWITCH":
                        newG = new Switch(componentCount);
                        break;
                    case "XOR":
                        newG = new XOR(componentCount);
                        break;
                }
                if (newG != null)
                {
                    gatesList.add(newG);
                    GateComp newGate = new GateComp(newG, gateCompStates.getLength(), currentFormatUsed);
                    newGate.Location = canvasPanel.getMousePos();
                    canvasPanel.Controls.Add(newGate);
                    componentCount++;
                    gateCompStates.add(newGate);
                    AddGate newAdd = new AddGate(newGate, gateCompStates.getLength() - 1);
                    undoStack.push(newAdd);
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save Logic Circuit";
            saveFileDialog1.DefaultExt = "txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedPath = saveFileDialog1.FileName;
                    FileHandling.saveLogicCircuit(gatesList, selectedPath);
                    MessageBox.Show("File Saved.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save circuit:\n" + ex.Message, "Error");
                }
            }
        }
        private void jsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            saveFileDialog1.Title = "Export Logic Circuit";
            saveFileDialog1.DefaultExt = "json";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedPath = saveFileDialog1.FileName;
                    FileHandling.exportToJson(gatesList, selectedPath);
                    MessageBox.Show("File Exported Correctly.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to export circuit:\n" + ex.Message, "Error");
                }
            }
        }
        private void xmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog1.Title = "Export Logic Circuit";
            saveFileDialog1.DefaultExt = "xml";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedPath = saveFileDialog1.FileName;
                    FileHandling.exportToXML(gatesList, selectedPath);
                    MessageBox.Show("File Exported Correctly.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to export circuit:\n" + ex.Message, "Error");
                }
            }
        }
        private void csvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            saveFileDialog1.Title = "Export Logic Circuit";
            saveFileDialog1.DefaultExt = "csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedPath = saveFileDialog1.FileName;
                    FileHandling.exportToCSV(gatesList, selectedPath);
                    MessageBox.Show("File Exported Correctly.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to export circuit:\n" + ex.Message, "Error");
                }
            }
        }
        private void openFileInNewForm(string filePath)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath, $"\"{filePath}\"");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file:\n" + ex.Message);
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.Title = "Open Logic Circuit";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                openFileInNewForm(filePath);
            }
        }
        private void spawnComponents(MyList<IGate> loadedComponents)
        {
            MyList<GateComp> switchList = new MyList<GateComp>();
            MyList<GateComp> lampList = new MyList<GateComp>();
            MyList<GateComp> generalList = new MyList<GateComp> { };
            for (int i = 0; i < loadedComponents.getLength(); i++)
            {
                IGate newG = loadedComponents.getItem(i);
                if (newG.getGateType() == "CUSTOM")
                {
                    CustomGate newCustom = newG as CustomGate;
                    this.saveCustomGate(newCustom);
                }
                gatesList.add(newG);
                GateComp newGate = new GateComp(newG, gateCompStates.getLength(), currentFormatUsed);
                newGate.Location = new Point(100, 100);
                if (newG.getGateType() == "SWITCH")
                {
                    switchList.add(newGate);

                }
                else if (newG.getGateType() == "LAMP")
                {
                    lampList.add(newGate);
                }
                else
                {

                    generalList.add(newGate);
                }
                componentCount++;
                gateCompStates.add(newGate);
                AddGate newAdd = new AddGate(newGate, gateCompStates.getLength() - 1);
                undoStack.push(newAdd);
            }
            int switchJump = (900 / switchList.getLength()) / 2;
            int switchVerticalCoordinates = 0;
            for (int i = 0; i < switchList.getLength(); i++)
            {
                switchVerticalCoordinates += switchJump;
                switchList.getItem(i).Location = new Point(267, switchVerticalCoordinates);
                canvasPanel.Controls.Add(switchList.getItem(i));
            }
            int generalJump = (900 / generalList.getLength()) / 2;
            int generalVerticalCoordinates = 0;
            for (int i = 0; i < generalList.getLength(); i++)
            {
                generalVerticalCoordinates += generalJump;
                generalList.getItem(i).Location = new Point(800, generalVerticalCoordinates);
                canvasPanel.Controls.Add(generalList.getItem(i));
            }
            int lampJump = (900 / lampList.getLength()) / 2;
            int lampVerticalCoordinates = 0;
            for (int i = 0; i < lampList.getLength(); i++)
            {
                lampVerticalCoordinates += lampJump;
                lampList.getItem(i).Location = new Point(1333, lampVerticalCoordinates);
                canvasPanel.Controls.Add(lampList.getItem(i));
            }
            for (int i = 0; i < loadedComponents.getLength(); i++)
            {
                IGate currentGate = loadedComponents.getItem(i);
                GateComp sourceGateComp = null;
                foreach (Control control in canvasPanel.Controls)
                {
                    if (control.Name == currentGate.getName())
                    {
                        sourceGateComp = control as GateComp;
                        break;
                    }
                }
                if (currentGate.getGateType() != "LAMP")
                {
                    dynamic cG = currentGate as dynamic;
                    OutputNode outputN = cG.getOutput();
                    MyList<InputNode> nextInputs = outputN.getInputList();
                    MyList<string> nextOwners = new MyList<string>();
                    for (int j = 0; j < nextInputs.getLength(); j++)
                    {
                        nextOwners.add(nextInputs.getItem(j).getOwnerGate());
                    }
                    MyList<GateComp> targetGateComps = new MyList<GateComp>();
                    foreach (Control control in canvasPanel.Controls)
                    {
                        for (int j = 0; j < nextOwners.getLength(); j++)
                        {
                            if (control.Name == nextOwners.getItem(j))
                            {
                                targetGateComps.add(control as GateComp);
                            }
                        }
                    }
                    for (int j = 0; j < targetGateComps.getLength(); j++)
                    {
                        if (sourceGateComp != null)
                        {
                            Connection c = new Connection(sourceGateComp, targetGateComps.getItem(j), sourceGateComp.getOutputButtonPoint(), targetGateComps.getItem(j).getInputButtonPoint());
                            canvasPanel.getConnectionList().add(c);
                            AddConnection newConnection = new AddConnection(c, canvasPanel.getConnectionList().getLength() - 1);
                        }

                    }
                }
            }
            canvasPanel.Invalidate();
        }
        private void jsonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.Title = "Import Logic Circuit";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                openFileInNewForm(filePath);
            }

        }
        private void xmlToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.Title = "Import Logic Circuit";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                openFileInNewForm(filePath);
            }
        }
        private void csvToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.Title = "Import Logic Circuit";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                openFileInNewForm(filePath);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void runCircuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MyList<IGate> sortedComponents = GraphAlgorithms.khansAlgorithm(gatesList);
                MyList<Lamp> lampList = new MyList<Lamp>();
                for (int i = 0; i < sortedComponents.getLength(); i++)
                {
                    sortedComponents.getItem(i).execute();
                }
                for (int i = 0; i < sortedComponents.getLength(); i++)
                {
                    if (sortedComponents.getItem(i).getGateType() == "LAMP")
                    {
                        Lamp x = sortedComponents.getItem(i) as Lamp;
                    }
                    else if (sortedComponents.getItem(i).getGateType() == "SWITCH")
                    {
                        Switch x = sortedComponents.getItem(i) as Switch;
                    }
                    else if (sortedComponents.getItem(i).getGateType() == "CUSTOM")
                    {
                        CustomGate x = sortedComponents.getItem(i) as CustomGate;
                    }
                    else
                    {

                        Gate x = sortedComponents.getItem(i) as Gate;
                    }

                }
                for (int i = 0; i < sortedComponents.getLength(); i++)
                {

                    if (sortedComponents.getItem(i).getGateType() == "LAMP")
                    {
                        lampList.add(sortedComponents.getItem(i) as Lamp);
                    }
                }
                foreach (Control ctrl in canvasPanel.Controls)
                {
                    for (int i = 0; i < lampList.getLength(); i++)
                    {
                        Debug.WriteLine(sortedComponents.getItem(i).getName());
                        Debug.WriteLine(ctrl.Name);
                        if (ctrl.Name == lampList.getItem(i).getName())
                        {
                            GateComp g = ctrl as GateComp;

                            g.updateLampColour(lampList.getItem(i).getOutput());
                        }
                    }

                }
                for (int j = 0; j < gatesList.getLength(); j++)
                {
                    gatesList.getItem(j).resetGate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex);
            }
        }
        private void generateTruthTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MyList<MyList<string>> truthTable = LogicGateFunctions.generateTruthTable(gatesList);
                TruthTableForm tableWindow = new TruthTableForm(truthTable, ref componentCount, this);
                tableWindow.Show();
                resetAllSwitches();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex);
            }

        }
        public void saveCustomGate(CustomGate c)
        {
            savedCustomGates.add(c);
            ToolStripMenuItem newGateButton = new ToolStripMenuItem(c.getName());
            newGateButton.Click += customGateSpawnButton_Click;
            customToolStripMenuItem.DropDownItems.Add(newGateButton);
        }
        private void customGateSpawnButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem btn = sender as ToolStripMenuItem;
            CustomGate theGate = null;
            string buttonText = btn.Text;
            for (int i = 0; i < savedCustomGates.getLength(); i++)
            {
                if (savedCustomGates.getItem(i).getName() == buttonText)
                {
                    theGate = savedCustomGates.getItem(i);
                    break;
                }
            }
            if (theGate != null)
            {
                gatesList.add(theGate);
                GateComp newG = new GateComp(theGate, gateCompStates.getLength(), currentFormatUsed );
                Point centre = getCentre(viewPanel.getCentreCoordinates(), newG);
                newG.Location = centre;
                canvasPanel.Controls.Add(newG);
                componentCount++;
                gateCompStates.add(newG);
                AddGate newAdd = new AddGate(newG, gateCompStates.getLength() - 1);
                undoStack.push(newAdd);
            }
        }
        private void sumOfProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string SOP = LogicGateFunctions.generateUnsimplifiedBooleanExpression(gatesList);
                MessageBox.Show(SOP);
                resetAllSwitches();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex);
            }

        }
        private void simplifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string simplifiedExpression = LogicGateFunctions.generateSimplifiedBooleanExpression(gatesList);
                MessageBox.Show(simplifiedExpression);
                resetAllSwitches();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex);
            }

        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
        }
        private void Undo_Click(object sender, EventArgs e)
        {
            if (undoStack.isEmpty() == false)
            {
                ICommand command = undoStack.pop();
                try
                {
                    command.undo(ref componentCount);
                    redoStack.push(command);
                }
                catch { }



            }
        }
        private void Redo_Click(object sender, EventArgs e)
        {
            if (redoStack.isEmpty() == false)
            {
                ICommand command = redoStack.pop();
                try
                {
                    command.redo(ref componentCount);
                    undoStack.push(command);
                }
                catch
                {

                }

            }
        }
        public void addComponentFromOutsideForm(GateComp gateComp)
        {
            if (canvasPanel.InvokeRequired)
            {
                Action<GateComp> addGateAction = new Action<GateComp>(addComponentFromOutsideForm);
                canvasPanel.Invoke(addGateAction, new object[] { gateComp });
                return;
            }
            gatesList.add(gateComp.getGate());
            componentCount++;
            gateCompStates.add(gateComp);
            canvasPanel.Controls.Add(gateComp);
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                if (e.KeyCode == Keys.Z && e.Shift == false)
                {
                    Undo_Click(sender, e);
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Z && e.Shift == true)
                {
                    Redo_Click(sender, e);
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.S)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
                else if (e.KeyCode == Keys.R)
                {
                    runCircuitToolStripMenuItem_Click(sender, e);
                }
                else if (e.KeyCode == Keys.C)
                {
                    if (selectedGate != null)
                    {
                        copyGate(selectedGate.getGate().getGateType());
                    }
                }
                else if (e.KeyCode == Keys.X)
                {
                    if (selectedGate != null)
                    {
                        selectedGate.CutGate_Click(sender, e);
                    }
                }
                else if (e.KeyCode == Keys.V)
                {
                    Paste_Click(sender, e);
                }
                else if (e.KeyCode == Keys.O)
                {
                    openToolStripMenuItem_Click(sender, e);
                }
                else if (e.KeyCode == Keys.N)
                {
                    newToolStripMenuItem_Click(sender, e);
                }
                else if (e.KeyCode == Keys.W)
                {
                    exitToolStripMenuItem_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F)
                {
                    findToolStripMenuItem_Click(sender, e);
                }
            }
            else
            {
                if (e.KeyCode == Keys.F5)
                {
                    runCircuitToolStripMenuItem_Click(sender, e);
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (selectedGate != null)
                    {
                        selectedGate.DeleteGate_Click(sender, e);
                    }
                }
            }
        }
        public void deselectAllGatesApartFrom(GateComp ignoreGateComp)
        {
            foreach (Control ctrl in canvasPanel.Controls)
            {
                if (ctrl is GateComp gate)
                {
                    if (gate != ignoreGateComp && gate.getIsSelected() == true)
                    {
                        gate.deselectSelf();
                    }
                }
            }
            selectedGate = ignoreGateComp;
        }
        public void deselectAllgates()
        {
            foreach (Control ctrl in canvasPanel.Controls)
            {
                if (ctrl is GateComp gate)
                {
                    if (gate.getIsSelected() == true)
                    {
                        gate.deselectSelf();
                    }
                }
            }
            selectedGate = null;
        }
        public void increaseComponentCount()
        {
            componentCount++;
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedGate != null)
            {
                selectedGate.CutGate_Click(sender, e);
            }
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedGate != null)
            {
                copyGate(selectedGate.getGate().getGateType());
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedGate != null)
            {
                selectedGate.DeleteGate_Click(sender, e);
            }
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GateLocatorForm gateLocatorForm = new GateLocatorForm(this);
            gateLocatorForm.Show();
        }
        private void circuitCostCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitCostCalc circuitCostCalcForm = new CircuitCostCalc(this);
            circuitCostCalcForm.Show();
        }
        private void resetAllSwitches()
        {
            foreach (Control control in canvasPanel.Controls)
            {
                if (control is GateComp gateComp)
                {
                    if (gateComp.getGate().getGateType() == "SWITCH")
                    {
                        gateComp.resetSwitchGateState();
                    }
                }
            }
        }
        private void educationModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EducationForm educationForm = new EducationForm();
            educationForm.Show();
        }

        private void standardFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control control in canvasPanel.Controls)
            {
                if (control is GateComp gateComp)
                {
                    gateComp.setBackgroundImage(1);
                }
            }
            currentFormatUsed = 1;
        }

        private void nANDFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control control in canvasPanel.Controls)
            {
                if (control is GateComp gateComp)
                {
                    Debug.WriteLine("I happened");
                    gateComp.setBackgroundImage(2);
                }
            }
            currentFormatUsed = 2;
        }

        private void nORFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control control in canvasPanel.Controls)
            {
                if (control is GateComp gateComp)
                {
                    gateComp.setBackgroundImage(3);
                }
            }
            currentFormatUsed = 3;
        }
    }
}
