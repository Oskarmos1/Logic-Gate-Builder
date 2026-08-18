using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic_Gate_Builder.UI_Classes.Educational_Pages
{
    public partial class ProgressBarForm : Form
    {
        private ProgressBar progressBar = new ProgressBar();
        public ProgressBarForm()
        {
            this.Dock = DockStyle.Fill;

            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Padding = new Padding(10);
            this.Text = "Generating Exam...";
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            InitializeComponent();
            this.Width = 500;
            this.Height = 100;
            progressBar.Show();
            progressBar.Height = 20;
            progressBar.Minimum = 0;
            progressBar.Dock = DockStyle.Top;
            progressBar.Style  = ProgressBarStyle.Continuous;
            progressBar.Maximum = 100;
            this.Controls.Add(progressBar);
        }

        public async void add10Percentage() {
            progressBar.Value = Math.Min(progressBar.Value + 10, 100);
            await Task.Delay(15);
            if (progressBar.Value >= 100)
            {
                await Task.Delay(200);
                this.Close();
            }
        }
    }
}
