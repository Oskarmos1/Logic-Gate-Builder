namespace Logic_Gate_Builder.UI_Classes
{
    partial class GateComp
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GateName = new Label();
            isSelectedPanel = new Panel();
            SuspendLayout();
            // 
            // GateName
            // 
            GateName.AutoSize = true;
            GateName.Location = new Point(49, 61);
            GateName.Name = "GateName";
            GateName.Size = new Size(49, 25);
            GateName.TabIndex = 0;
            GateName.Text = "label";
            GateName.MouseDown += GateName_MouseMove;
            GateName.MouseMove += GateName_MouseMove;
            GateName.MouseUp += GateName_MouseUp;
            // 
            // isSelectedPanel
            // 
            isSelectedPanel.Location = new Point(65, 13);
            isSelectedPanel.Name = "isSelectedPanel";
            isSelectedPanel.Size = new Size(18, 18);
            isSelectedPanel.TabIndex = 1;
            // 
            // GateComp
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(isSelectedPanel);
            Controls.Add(GateName);
            Name = "GateComp";
            MouseClick += GateComp_MouseClick;
            MouseDown += GateName_MouseDown;
            MouseMove += GateName_MouseMove;
            MouseUp += GateName_MouseUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label GateName;
        private Panel panelInput1;
        private Panel panelInput2;
        private Panel panelOutput;
        private Panel isSelectedPanel;
    }
}
