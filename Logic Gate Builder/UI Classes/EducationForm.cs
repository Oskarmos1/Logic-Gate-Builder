using Logic_Gate_Builder.UI_Classes.Educational_Pages;
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
using AxWMPLib;
using WMPLib;

namespace Logic_Gate_Builder.UI_Classes
{
    public partial class EducationForm : Form
    {
        public EducationForm()
        {
            InitializeComponent();
            loadPage(new Information_Menu());
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(EducationForm));
            toolStrip1 = new ToolStrip();
            notesMenu = new ToolStripDropDownButton();
            logicGatesToolStripMenuItem = new ToolStripMenuItem();
            aNDToolStripMenuItem = new ToolStripMenuItem();
            oRToolStripMenuItem = new ToolStripMenuItem();
            nOTToolStripMenuItem = new ToolStripMenuItem();
            nANDToolStripMenuItem = new ToolStripMenuItem();
            nORToolStripMenuItem = new ToolStripMenuItem();
            xORToolStripMenuItem = new ToolStripMenuItem();
            nXORToolStripMenuItem = new ToolStripMenuItem();
            truthTablesToolStripMenuItem = new ToolStripMenuItem();
            booleanAlgebraToolStripMenuItem = new ToolStripMenuItem();
            questionsMenu = new ToolStripDropDownButton();
            randomQuestionToolStripMenuItem = new ToolStripMenuItem();
            mockExamToolStripMenuItem = new ToolStripMenuItem();
            contentPanel = new Panel();
            this.notificationsMenu = new ToolStripButton();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.Left;
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { notesMenu, questionsMenu, this.notificationsMenu });
            toolStrip1.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(118, 544);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // notesMenu
            // 
            notesMenu.DropDownItems.AddRange(new ToolStripItem[] { logicGatesToolStripMenuItem, truthTablesToolStripMenuItem, booleanAlgebraToolStripMenuItem });
            notesMenu.Name = "notesMenu";
            notesMenu.Size = new Size(113, 29);
            notesMenu.Text = "Notes";
            // 
            // logicGatesToolStripMenuItem
            // 
            logicGatesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aNDToolStripMenuItem, oRToolStripMenuItem, nOTToolStripMenuItem, nANDToolStripMenuItem, nORToolStripMenuItem, xORToolStripMenuItem, nXORToolStripMenuItem });
            logicGatesToolStripMenuItem.Name = "logicGatesToolStripMenuItem";
            logicGatesToolStripMenuItem.Size = new Size(245, 34);
            logicGatesToolStripMenuItem.Text = "Logic Gates";
            // 
            // aNDToolStripMenuItem
            // 
            aNDToolStripMenuItem.Name = "aNDToolStripMenuItem";
            aNDToolStripMenuItem.Size = new Size(165, 34);
            aNDToolStripMenuItem.Text = "AND";
            aNDToolStripMenuItem.Click += aNDToolStripMenuItem_Click;
            // 
            // oRToolStripMenuItem
            // 
            oRToolStripMenuItem.Name = "oRToolStripMenuItem";
            oRToolStripMenuItem.Size = new Size(165, 34);
            oRToolStripMenuItem.Text = "OR";
            oRToolStripMenuItem.Click += oRToolStripMenuItem_Click;
            // 
            // nOTToolStripMenuItem
            // 
            nOTToolStripMenuItem.Name = "nOTToolStripMenuItem";
            nOTToolStripMenuItem.Size = new Size(165, 34);
            nOTToolStripMenuItem.Text = "NOT";
            nOTToolStripMenuItem.Click += nOTToolStripMenuItem_Click;
            // 
            // nANDToolStripMenuItem
            // 
            nANDToolStripMenuItem.Name = "nANDToolStripMenuItem";
            nANDToolStripMenuItem.Size = new Size(165, 34);
            nANDToolStripMenuItem.Text = "NAND";
            nANDToolStripMenuItem.Click += nANDToolStripMenuItem_Click;
            // 
            // nORToolStripMenuItem
            // 
            nORToolStripMenuItem.Name = "nORToolStripMenuItem";
            nORToolStripMenuItem.Size = new Size(165, 34);
            nORToolStripMenuItem.Text = "NOR";
            nORToolStripMenuItem.Click += nORToolStripMenuItem_Click;
            // 
            // xORToolStripMenuItem
            // 
            xORToolStripMenuItem.Name = "xORToolStripMenuItem";
            xORToolStripMenuItem.Size = new Size(165, 34);
            xORToolStripMenuItem.Text = "XOR";
            xORToolStripMenuItem.Click += xORToolStripMenuItem_Click;
            // 
            // nXORToolStripMenuItem
            // 
            nXORToolStripMenuItem.Name = "nXORToolStripMenuItem";
            nXORToolStripMenuItem.Size = new Size(165, 34);
            nXORToolStripMenuItem.Text = "NXOR";
            nXORToolStripMenuItem.Click += nXORToolStripMenuItem_Click;
            // 
            // truthTablesToolStripMenuItem
            // 
            truthTablesToolStripMenuItem.Name = "truthTablesToolStripMenuItem";
            truthTablesToolStripMenuItem.Size = new Size(245, 34);
            truthTablesToolStripMenuItem.Text = "Truth Tables";
            truthTablesToolStripMenuItem.Click += truthTablesToolStripMenuItem_Click;
            // 
            // booleanAlgebraToolStripMenuItem
            // 
            booleanAlgebraToolStripMenuItem.Name = "booleanAlgebraToolStripMenuItem";
            booleanAlgebraToolStripMenuItem.Size = new Size(245, 34);
            booleanAlgebraToolStripMenuItem.Text = "Boolean Algebra";
            booleanAlgebraToolStripMenuItem.Click += booleanAlgebraToolStripMenuItem_Click;
            // 
            // questionsMenu
            // 
            questionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            questionsMenu.DropDownItems.AddRange(new ToolStripItem[] { randomQuestionToolStripMenuItem, mockExamToolStripMenuItem });
            questionsMenu.Image = (Image)resources.GetObject("questionsMenu.Image");
            questionsMenu.ImageTransparentColor = Color.Magenta;
            questionsMenu.Name = "questionsMenu";
            questionsMenu.Size = new Size(113, 29);
            questionsMenu.Text = "Questions";
            // 
            // randomQuestionToolStripMenuItem
            // 
            randomQuestionToolStripMenuItem.Name = "randomQuestionToolStripMenuItem";
            randomQuestionToolStripMenuItem.Size = new Size(270, 34);
            randomQuestionToolStripMenuItem.Text = "Random Question";
            randomQuestionToolStripMenuItem.Click += randomQuestionToolStripMenuItem_Click;
            // 
            // mockExamToolStripMenuItem
            // 
            mockExamToolStripMenuItem.Name = "mockExamToolStripMenuItem";
            mockExamToolStripMenuItem.Size = new Size(270, 34);
            mockExamToolStripMenuItem.Text = "Mock Exam";
            mockExamToolStripMenuItem.Click += mockExamToolStripMenuItem_Click;
            // 
            // contentPanel
            // 
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(118, 0);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(560, 544);
            contentPanel.TabIndex = 1;
            // 
            // notificationsMenu
            // 
            this.notificationsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.notificationsMenu.Image = (Image)resources.GetObject("notificationsMenu.Image");
            this.notificationsMenu.ImageTransparentColor = Color.Magenta;
            this.notificationsMenu.Name = "notificationsMenu";
            this.notificationsMenu.Size = new Size(113, 29);
            this.notificationsMenu.Text = "Notifications";
            this.notificationsMenu.Click += this.notificationsMenu_Click;
            // 
            // EducationForm
            // 
            ClientSize = new Size(678, 544);
            Controls.Add(contentPanel);
            Controls.Add(toolStrip1);
            MaximumSize = new Size(1300, 800);
            MinimumSize = new Size(600, 600);
            Name = "EducationForm";
            Text = "Education Mode";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();


        }

        public void loadPage(UserControl page)
        {
            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(page);
        }

        private void aNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new AndInfo());
        }

        private void oRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new ORInfo());
        }

        private void nOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new NotInfo());
        }

        private void nANDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new NANDInfo());
        }

        private void nORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new NorInfo());
        }

        private void xORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new XORInfo());
        }

        private void nXORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new XnorInfo());
        }

        private void truthTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new TruthTableInfo());
        }

        private void booleanAlgebraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new BooleanAlgebraInfo());
        }

        private void randomQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new RandomQuestion());
        }

        private void mockExamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPage(new ExamSetup());
        }

        private void notificationsMenu_Click(object sender, EventArgs e)
        {
            loadPage(new NotificationManager());
        }
    }
}
