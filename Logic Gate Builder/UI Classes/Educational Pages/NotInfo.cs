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
    public partial class NotInfo : UserControl
    {
        public NotInfo()
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
                Text = "NOT Gate",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            Label text1 = new Label
            {
                Text = "A NOT gate simply reverses the value of the input. If the input is 1, the output is 0. If the input is 0, then the output is 1.",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            PictureBox gateImage = new PictureBox
            {
                Image = Image.FromFile("R/NOTInfo.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Height = 200,
                Margin = new Padding(10)
            };
            Label text2 = new Label
            {
                Text = "The boolean algebra notation for a NOT gate is '¬'. For example:" +
             "\n\n ¬A",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };

            texts.add(text2);
            Controls.Add(text2);
            texts.add(text1);
            Controls.Add(gateImage);
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
