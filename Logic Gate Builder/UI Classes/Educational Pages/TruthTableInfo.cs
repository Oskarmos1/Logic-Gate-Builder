using AxWMPLib;
using WMPLib;
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
    public partial class TruthTableInfo : UserControl
    {
        public TruthTableInfo()
        {
            InitializeComponent();
            MyList<Label> texts = new MyList<Label>();
            MyList<AxWindowsMediaPlayer> videos = new MyList<AxWindowsMediaPlayer>();
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
                Text = "Truth Tables",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };


            Label text1 = new Label
            {
                Text = "A truth table is a chart which is used to show all possible outputs of a circuit and their respective inputs. If you click onto any logic gate info in this program, you will see the gate's truth table. " +
                "Truth tables are important because they allow us to better understand the way a logic circuit behaves. They are also useful in algorithms such as the Quine–McCluskey algorithm which was used in this program to simplify complex circuits." +
                "\n\nFor example, here is the truth table for the boolean expression: (SWITCH1·¬SWITCH2)+(SWITCHO·¬SWITCH2)",
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
                Image = Image.FromFile("R/TruthTableExample.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Height = 200,
                Margin = new Padding(10)
            };
            Button openVideoButton = new Button();
            openVideoButton.Text = "Watch the video breakdown!";
            openVideoButton.Dock = DockStyle.Top;
            openVideoButton.Height = 50;
            openVideoButton.MouseClick += (sender, e) =>
            {
                VideoForm videoForm = new VideoForm("TruthTableWorkThrough.mp4", "R");
                videoForm.Show();
            };
            Controls.Add(openVideoButton);
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
