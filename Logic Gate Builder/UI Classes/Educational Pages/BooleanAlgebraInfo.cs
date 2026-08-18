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
    public partial class BooleanAlgebraInfo : UserControl
    {
        public BooleanAlgebraInfo()
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
                Text = "Boolean Algebra",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };


            Label text1 = new Label
            {
                Text = "Boolean algebra is a mathematical system used to represent and simplify logical expressions. Boolean algebra also operates on binary, to represent the output of a particular circuit." +
                "\n\n Here are the major laws you must remember for your exam:" +
                "\n\n - Identity Law: A·1 = A and A + 0 = A"+
                "\n - Null (Dominance) Law: A·0 = 0 and A + 1 = 1"+
                 "\n - Inverse Law: A·A′ = 0 and A + A′ = 1" +
                "\n - Commutative Law: A·B = B·A and A + B = B + A" +
                "\n - Distributive Law: A + (B·C) = (A + B)·(A + C)" +
                "\n - Associative Law: (A·B)·C = A·(B·C) and (A + B) + C = A + (B + C)" +
                "\n - Absorption Law: A·(A + B) = A and A + (A·B) = A" +
                "\n - Double Complement Law: (A′)′ = A" +
                "\n - De Morgan’s Laws: ¬(A + B) = ¬A·¬B and ¬(A·B) = ¬A + ¬B"
                +"\n\n There is also an order of precedence that you must remember. Here it is, starting with the highest priority first:"+
                "\n\n 1. Brackets ()" +
                "\n 2. NOT (¬)" +
                "\n 3. XOR (⊕)" +
                "\n 4. AND (·)" +
                "\n 5. OR (+)"+
                "\n\n Personally, I find that approaching boolean algebra problems like simplification problems in mathematics makes this topic significantly easier as all the same rules apply." +
                " The only law that doesn't seem like standard maths problems is De Morgan's law which you will just have to memorise. This law is however, closely related to the contrapositive in mathematics."+
                "\n\n When you have come to grips with the laws try this question:",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            PictureBox exampleQuestion = new PictureBox
            {
                Image = Image.FromFile("R/BooleanAlgebraExample.png"), 
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
                VideoForm videoForm = new VideoForm("BooleanAlgebraWorkThrough.mp4", "R");
                videoForm.Show();
            };
            Controls.Add(openVideoButton);
            Controls.Add(exampleQuestion);
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
