using AxWMPLib;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Resources;
using System.Security.Policy;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Logic_Gate_Builder.UI_Classes.Educational_Pages
{
    public partial class Information_Menu : UserControl
    {
        public Information_Menu()
        {
            InitializeComponent();
            MyList<Label> texts = new MyList<Label>();
            SetupCustomContent(ref texts);
            this.Resize += (s, e) => adjustLabelWidth(ref texts);
        }

        private void SetupCustomContent(ref MyList<Label> texts)
        {
            // Optional for debugging:
            Dock = DockStyle.Fill;
            AutoScroll = true;
            Label title = new Label
            {
                Text = "Welcome!",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };


            Label text1 = new Label
            {
                Text = "In the educational mode you have access to: \n" +
                "\n - Educational Notes" +
                "\n - Educational Videos" +
                "\n - Mock Practice Questions" +
                "\n - Revision Notifications",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            texts.add(text1);
            Controls.Add(text1);
            Controls.Add(title);
            
        }


        private void adjustLabelWidth(ref MyList<Label> texts)
        {
            for (int i = 0; i < texts.getLength(); i++)
            {
                Label text = texts.getItem(i);
                text.MaximumSize = new Size(this.Width - 40, 0);
                text.Refresh();
            }
        }
    }
}
