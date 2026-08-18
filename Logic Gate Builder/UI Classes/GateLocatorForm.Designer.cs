namespace Logic_Gate_Builder.UI_Classes
{
    partial class GateLocatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBox1 = new ComboBox();
            label1 = new Label();
            InfoLabel = new Label();
            FindButton = new Button();
            LastGateButton = new Button();
            NextGateButton = new Button();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "AND", "NAND", "OR", "NOR", "XOR", "NXOR", "NOT", "SWITCH", "LAMP" });
            comboBox1.Location = new Point(12, 56);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(282, 33);
            comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(270, 25);
            label1.TabIndex = 1;
            label1.Text = "What gate are you searching for:";
            // 
            // InfoLabel
            // 
            InfoLabel.AutoSize = true;
            InfoLabel.Location = new Point(12, 104);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(59, 25);
            InfoLabel.TabIndex = 2;
            InfoLabel.Text = "label2";
            // 
            // FindButton
            // 
            FindButton.Location = new Point(316, 55);
            FindButton.Name = "FindButton";
            FindButton.Size = new Size(55, 34);
            FindButton.TabIndex = 3;
            FindButton.Text = "Find";
            FindButton.UseVisualStyleBackColor = true;
            FindButton.Click += FindButton_Click;
            // 
            // LastGateButton
            // 
            LastGateButton.Location = new Point(12, 146);
            LastGateButton.Name = "LastGateButton";
            LastGateButton.Size = new Size(112, 34);
            LastGateButton.TabIndex = 4;
            LastGateButton.Text = "Back";
            LastGateButton.UseVisualStyleBackColor = true;
            LastGateButton.Click += LastGateButton_Click;
            // 
            // NextGateButton
            // 
            NextGateButton.Location = new Point(130, 146);
            NextGateButton.Name = "NextGateButton";
            NextGateButton.Size = new Size(112, 34);
            NextGateButton.TabIndex = 5;
            NextGateButton.Text = "Next";
            NextGateButton.UseVisualStyleBackColor = true;
            NextGateButton.Click += NextGateButton_Click;
            // 
            // GateLocatorForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 204);
            Controls.Add(NextGateButton);
            Controls.Add(LastGateButton);
            Controls.Add(FindButton);
            Controls.Add(InfoLabel);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GateLocatorForm";
            Text = "Find";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Label label1;
        private Label InfoLabel;
        private Button FindButton;
        private Button LastGateButton;
        private Button NextGateButton;
    }
}