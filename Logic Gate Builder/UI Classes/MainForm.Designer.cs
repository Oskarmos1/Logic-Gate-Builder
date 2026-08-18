namespace Logic_Gate_Builder
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            menuStrip1 = new MenuStrip();
            fIleToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            jsonToolStripMenuItem1 = new ToolStripMenuItem();
            xmlToolStripMenuItem1 = new ToolStripMenuItem();
            csvToolStripMenuItem1 = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            jsonToolStripMenuItem = new ToolStripMenuItem();
            xmlToolStripMenuItem = new ToolStripMenuItem();
            csvToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            findToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            insertGateToolStripMenuItem = new ToolStripMenuItem();
            ANDSpawnButton = new ToolStripMenuItem();
            NANDSpawnButton = new ToolStripMenuItem();
            ORSpawnButton = new ToolStripMenuItem();
            NORSpawnButton = new ToolStripMenuItem();
            XORSpawnButton = new ToolStripMenuItem();
            NXORSpawnButton = new ToolStripMenuItem();
            NOTSpawnButton = new ToolStripMenuItem();
            SWITCHSpawnButton = new ToolStripMenuItem();
            LAMPSpawnButton = new ToolStripMenuItem();
            customToolStripMenuItem = new ToolStripMenuItem();
            logicFunctionsToolStripMenuItem = new ToolStripMenuItem();
            runCircuitToolStripMenuItem = new ToolStripMenuItem();
            generateTruthTableToolStripMenuItem = new ToolStripMenuItem();
            generateExpressionToolStripMenuItem = new ToolStripMenuItem();
            sumOfProductsToolStripMenuItem = new ToolStripMenuItem();
            simplifiedToolStripMenuItem = new ToolStripMenuItem();
            engineerToolsToolStripMenuItem = new ToolStripMenuItem();
            circuitCostCalculatorToolStripMenuItem = new ToolStripMenuItem();
            formatModifierToolStripMenuItem = new ToolStripMenuItem();
            standardFormatToolStripMenuItem = new ToolStripMenuItem();
            nANDFormatToolStripMenuItem = new ToolStripMenuItem();
            nORFormatToolStripMenuItem = new ToolStripMenuItem();
            educationModeToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fIleToolStripMenuItem, editToolStripMenuItem, insertGateToolStripMenuItem, logicFunctionsToolStripMenuItem, engineerToolsToolStripMenuItem, educationModeToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1578, 33);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            fIleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, saveToolStripMenuItem, openToolStripMenuItem, importToolStripMenuItem, exportToolStripMenuItem, exitToolStripMenuItem });
            fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            fIleToolStripMenuItem.Size = new Size(54, 29);
            fIleToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(169, 34);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(169, 34);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(169, 34);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { jsonToolStripMenuItem1, xmlToolStripMenuItem1, csvToolStripMenuItem1 });
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(169, 34);
            importToolStripMenuItem.Text = "Import";
            // 
            // jsonToolStripMenuItem1
            // 
            jsonToolStripMenuItem1.Name = "jsonToolStripMenuItem1";
            jsonToolStripMenuItem1.Size = new Size(151, 34);
            jsonToolStripMenuItem1.Text = ".json";
            jsonToolStripMenuItem1.Click += jsonToolStripMenuItem1_Click;
            // 
            // xmlToolStripMenuItem1
            // 
            xmlToolStripMenuItem1.Name = "xmlToolStripMenuItem1";
            xmlToolStripMenuItem1.Size = new Size(151, 34);
            xmlToolStripMenuItem1.Text = ".xml";
            xmlToolStripMenuItem1.Click += xmlToolStripMenuItem1_Click;
            // 
            // csvToolStripMenuItem1
            // 
            csvToolStripMenuItem1.Name = "csvToolStripMenuItem1";
            csvToolStripMenuItem1.Size = new Size(151, 34);
            csvToolStripMenuItem1.Text = ".csv";
            csvToolStripMenuItem1.Click += csvToolStripMenuItem1_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { jsonToolStripMenuItem, xmlToolStripMenuItem, csvToolStripMenuItem });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(169, 34);
            exportToolStripMenuItem.Text = "Export";
            // 
            // jsonToolStripMenuItem
            // 
            jsonToolStripMenuItem.Name = "jsonToolStripMenuItem";
            jsonToolStripMenuItem.Size = new Size(151, 34);
            jsonToolStripMenuItem.Text = ".json";
            jsonToolStripMenuItem.Click += jsonToolStripMenuItem_Click;
            // 
            // xmlToolStripMenuItem
            // 
            xmlToolStripMenuItem.Name = "xmlToolStripMenuItem";
            xmlToolStripMenuItem.Size = new Size(151, 34);
            xmlToolStripMenuItem.Text = ".xml";
            xmlToolStripMenuItem.Click += xmlToolStripMenuItem_Click;
            // 
            // csvToolStripMenuItem
            // 
            csvToolStripMenuItem.Name = "csvToolStripMenuItem";
            csvToolStripMenuItem.Size = new Size(151, 34);
            csvToolStripMenuItem.Text = ".csv";
            csvToolStripMenuItem.Click += csvToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(169, 34);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, findToolStripMenuItem, deleteToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(58, 29);
            editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(164, 34);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += Undo_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(164, 34);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += Redo_Click;
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(164, 34);
            cutToolStripMenuItem.Text = "Cut";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(164, 34);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(164, 34);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += Paste_Click;
            // 
            // findToolStripMenuItem
            // 
            findToolStripMenuItem.Name = "findToolStripMenuItem";
            findToolStripMenuItem.Size = new Size(164, 34);
            findToolStripMenuItem.Text = "Find";
            findToolStripMenuItem.Click += findToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(164, 34);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // insertGateToolStripMenuItem
            // 
            insertGateToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ANDSpawnButton, NANDSpawnButton, ORSpawnButton, NORSpawnButton, XORSpawnButton, NXORSpawnButton, NOTSpawnButton, SWITCHSpawnButton, LAMPSpawnButton, customToolStripMenuItem });
            insertGateToolStripMenuItem.Name = "insertGateToolStripMenuItem";
            insertGateToolStripMenuItem.Size = new Size(113, 29);
            insertGateToolStripMenuItem.Text = "Insert Gate";
            // 
            // ANDSpawnButton
            // 
            ANDSpawnButton.Name = "ANDSpawnButton";
            ANDSpawnButton.Size = new Size(176, 34);
            ANDSpawnButton.Text = "AND";
            ANDSpawnButton.Click += ANDSpawnButton_Click;
            // 
            // NANDSpawnButton
            // 
            NANDSpawnButton.Name = "NANDSpawnButton";
            NANDSpawnButton.Size = new Size(176, 34);
            NANDSpawnButton.Text = "NAND";
            NANDSpawnButton.Click += NANDSpawnButton_Click;
            // 
            // ORSpawnButton
            // 
            ORSpawnButton.Name = "ORSpawnButton";
            ORSpawnButton.Size = new Size(176, 34);
            ORSpawnButton.Text = "OR";
            ORSpawnButton.Click += ORSpawnButton_Click;
            // 
            // NORSpawnButton
            // 
            NORSpawnButton.Name = "NORSpawnButton";
            NORSpawnButton.Size = new Size(176, 34);
            NORSpawnButton.Text = "NOR";
            NORSpawnButton.Click += NORSpawnButton_Click;
            // 
            // XORSpawnButton
            // 
            XORSpawnButton.Name = "XORSpawnButton";
            XORSpawnButton.Size = new Size(176, 34);
            XORSpawnButton.Text = "XOR";
            XORSpawnButton.Click += XORSpawnButton_Click;
            // 
            // NXORSpawnButton
            // 
            NXORSpawnButton.Name = "NXORSpawnButton";
            NXORSpawnButton.Size = new Size(176, 34);
            NXORSpawnButton.Text = "NXOR";
            NXORSpawnButton.Click += nXORToolStripMenuItem_Click;
            // 
            // NOTSpawnButton
            // 
            NOTSpawnButton.Name = "NOTSpawnButton";
            NOTSpawnButton.Size = new Size(176, 34);
            NOTSpawnButton.Text = "NOT";
            NOTSpawnButton.Click += NOTSpawnButton_Click;
            // 
            // SWITCHSpawnButton
            // 
            SWITCHSpawnButton.Name = "SWITCHSpawnButton";
            SWITCHSpawnButton.Size = new Size(176, 34);
            SWITCHSpawnButton.Text = "Switch";
            SWITCHSpawnButton.Click += SWITCHSpawnButton_Click;
            // 
            // LAMPSpawnButton
            // 
            LAMPSpawnButton.Name = "LAMPSpawnButton";
            LAMPSpawnButton.Size = new Size(176, 34);
            LAMPSpawnButton.Text = "Lamp";
            LAMPSpawnButton.Click += LAMPSpawnButton_Click;
            // 
            // customToolStripMenuItem
            // 
            customToolStripMenuItem.Name = "customToolStripMenuItem";
            customToolStripMenuItem.Size = new Size(176, 34);
            customToolStripMenuItem.Text = "Custom";
            // 
            // logicFunctionsToolStripMenuItem
            // 
            logicFunctionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { runCircuitToolStripMenuItem, generateTruthTableToolStripMenuItem, generateExpressionToolStripMenuItem });
            logicFunctionsToolStripMenuItem.Name = "logicFunctionsToolStripMenuItem";
            logicFunctionsToolStripMenuItem.Size = new Size(151, 29);
            logicFunctionsToolStripMenuItem.Text = "Logic Functions";
            // 
            // runCircuitToolStripMenuItem
            // 
            runCircuitToolStripMenuItem.Name = "runCircuitToolStripMenuItem";
            runCircuitToolStripMenuItem.Size = new Size(273, 34);
            runCircuitToolStripMenuItem.Text = "Run Circuit";
            runCircuitToolStripMenuItem.Click += runCircuitToolStripMenuItem_Click;
            // 
            // generateTruthTableToolStripMenuItem
            // 
            generateTruthTableToolStripMenuItem.Name = "generateTruthTableToolStripMenuItem";
            generateTruthTableToolStripMenuItem.Size = new Size(273, 34);
            generateTruthTableToolStripMenuItem.Text = "Generate Truth Table";
            generateTruthTableToolStripMenuItem.Click += generateTruthTableToolStripMenuItem_Click;
            // 
            // generateExpressionToolStripMenuItem
            // 
            generateExpressionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sumOfProductsToolStripMenuItem, simplifiedToolStripMenuItem });
            generateExpressionToolStripMenuItem.Name = "generateExpressionToolStripMenuItem";
            generateExpressionToolStripMenuItem.Size = new Size(273, 34);
            generateExpressionToolStripMenuItem.Text = "Generate Expression";
            // 
            // sumOfProductsToolStripMenuItem
            // 
            sumOfProductsToolStripMenuItem.Name = "sumOfProductsToolStripMenuItem";
            sumOfProductsToolStripMenuItem.Size = new Size(250, 34);
            sumOfProductsToolStripMenuItem.Text = "Sum Of Products";
            sumOfProductsToolStripMenuItem.Click += sumOfProductsToolStripMenuItem_Click;
            // 
            // simplifiedToolStripMenuItem
            // 
            simplifiedToolStripMenuItem.Name = "simplifiedToolStripMenuItem";
            simplifiedToolStripMenuItem.Size = new Size(250, 34);
            simplifiedToolStripMenuItem.Text = "Simplified";
            simplifiedToolStripMenuItem.Click += simplifiedToolStripMenuItem_Click;
            // 
            // engineerToolsToolStripMenuItem
            // 
            engineerToolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { circuitCostCalculatorToolStripMenuItem, formatModifierToolStripMenuItem });
            engineerToolsToolStripMenuItem.Name = "engineerToolsToolStripMenuItem";
            engineerToolsToolStripMenuItem.Size = new Size(142, 29);
            engineerToolsToolStripMenuItem.Text = "Engineer Tools";
            // 
            // circuitCostCalculatorToolStripMenuItem
            // 
            circuitCostCalculatorToolStripMenuItem.Name = "circuitCostCalculatorToolStripMenuItem";
            circuitCostCalculatorToolStripMenuItem.Size = new Size(287, 34);
            circuitCostCalculatorToolStripMenuItem.Text = "Circuit Cost Calculator";
            circuitCostCalculatorToolStripMenuItem.Click += circuitCostCalculatorToolStripMenuItem_Click;
            // 
            // formatModifierToolStripMenuItem
            // 
            formatModifierToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { standardFormatToolStripMenuItem, nANDFormatToolStripMenuItem, nORFormatToolStripMenuItem });
            formatModifierToolStripMenuItem.Name = "formatModifierToolStripMenuItem";
            formatModifierToolStripMenuItem.Size = new Size(287, 34);
            formatModifierToolStripMenuItem.Text = "Format Modifier";
            // 
            // standardFormatToolStripMenuItem
            // 
            standardFormatToolStripMenuItem.Name = "standardFormatToolStripMenuItem";
            standardFormatToolStripMenuItem.Size = new Size(270, 34);
            standardFormatToolStripMenuItem.Text = "Standard Format";
            standardFormatToolStripMenuItem.Click += standardFormatToolStripMenuItem_Click;
            // 
            // nANDFormatToolStripMenuItem
            // 
            nANDFormatToolStripMenuItem.Name = "nANDFormatToolStripMenuItem";
            nANDFormatToolStripMenuItem.Size = new Size(270, 34);
            nANDFormatToolStripMenuItem.Text = "NAND Format";
            nANDFormatToolStripMenuItem.Click += nANDFormatToolStripMenuItem_Click;
            // 
            // nORFormatToolStripMenuItem
            // 
            nORFormatToolStripMenuItem.Name = "nORFormatToolStripMenuItem";
            nORFormatToolStripMenuItem.Size = new Size(270, 34);
            nORFormatToolStripMenuItem.Text = "NOR Format";
            nORFormatToolStripMenuItem.Click += nORFormatToolStripMenuItem_Click;
            // 
            // educationModeToolStripMenuItem
            // 
            educationModeToolStripMenuItem.Name = "educationModeToolStripMenuItem";
            educationModeToolStripMenuItem.Size = new Size(158, 29);
            educationModeToolStripMenuItem.Text = "Education Mode";
            educationModeToolStripMenuItem.Click += educationModeToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1578, 844);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Logic Gate Builder";
            Click += Undo_Click;
            KeyDown += MainForm_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem insertGateToolStripMenuItem;
        private ToolStripMenuItem ANDSpawnButton;
        private ToolStripMenuItem NANDSpawnButton;
        private ToolStripMenuItem NORSpawnButton;
        private ToolStripMenuItem NOTSpawnButton;
        private ToolStripMenuItem ORSpawnButton;
        private ToolStripMenuItem XORSpawnButton;
        private ToolStripMenuItem SWITCHSpawnButton;
        private ToolStripMenuItem LAMPSpawnButton;
        private ToolStripMenuItem fIleToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem jsonToolStripMenuItem;
        private ToolStripMenuItem xmlToolStripMenuItem;
        private ToolStripMenuItem csvToolStripMenuItem;
        private ToolStripMenuItem jsonToolStripMenuItem1;
        private ToolStripMenuItem xmlToolStripMenuItem1;
        private ToolStripMenuItem csvToolStripMenuItem1;
        private ToolStripMenuItem logicFunctionsToolStripMenuItem;
        private ToolStripMenuItem runCircuitToolStripMenuItem;
        private ToolStripMenuItem generateTruthTableToolStripMenuItem;
        private ToolStripMenuItem customToolStripMenuItem;
        private ToolStripMenuItem generateExpressionToolStripMenuItem;
        private ToolStripMenuItem sumOfProductsToolStripMenuItem;
        private ToolStripMenuItem simplifiedToolStripMenuItem;
        private ToolStripMenuItem NXORSpawnButton;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem findToolStripMenuItem;
        private ToolStripMenuItem engineerToolsToolStripMenuItem;
        private ToolStripMenuItem circuitCostCalculatorToolStripMenuItem;
        private ToolStripMenuItem formatModifierToolStripMenuItem;
        private ToolStripMenuItem educationModeToolStripMenuItem;
        private ToolStripMenuItem standardFormatToolStripMenuItem;
        private ToolStripMenuItem nANDFormatToolStripMenuItem;
        private ToolStripMenuItem nORFormatToolStripMenuItem;
    }
}
