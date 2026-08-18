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

namespace Logic_Gate_Builder.UI_Classes.Educational_Pages
{
    public partial class RandomQuestion : UserControl
    {
        public RandomQuestion()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            Random rnd = new Random();
            int marks = rnd.Next(1, 6);
            int qN = -1;
            switch (marks) {
                case 1:
                    qN = rnd.Next(1, 6);
                    break;
                case 2:
                    qN = rnd.Next(1, 4);
                    break;
                case 3:
                    qN = rnd.Next(1, 10);
                    break;
                case 4:
                    qN = rnd.Next(1, 7);
                    break;
                case 5:
                    qN = rnd.Next(1, 3);
                    break;
            }
            string videoPath = "R/Q/" + marks.ToString() + "M/" + qN.ToString();
            string mainPath = videoPath + "/";
            string qPath = mainPath + "Q.png";
            string aPath = mainPath + "A.png";
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                ColumnCount = 1,
                RowCount = 3,
                AutoScroll = true
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); 
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); 
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); 
            PictureBox questionImage = new PictureBox
            {

                Image = Image.FromFile(qPath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = Image.FromFile(aPath).Width,
                Height = Image.FromFile(aPath).Height,
                Margin = new Padding(10)
            };
            scaleImage(ref questionImage, this.Width);
            PictureBox answerImage = new PictureBox
            {

                Image = Image.FromFile(aPath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = Image.FromFile(aPath).Width,
                Height = Image.FromFile(aPath).Height,
                Margin = new Padding(10),
                Visible = false
            };
            scaleImage(ref answerImage, this.Width);
            Button showAnswerButton = new Button
            {
                Text = "Show Answer",
                Width = 200,
                Height = 40
            };
            showAnswerButton.Click += (sender, e) =>
            {
                if (answerImage.Visible == true)
                {
                    answerImage.Visible = false;
                }
                else {
                    answerImage.Visible = true;
                }
            };
            layout.Controls.Add(questionImage, 0, 0);
            layout.Controls.Add(showAnswerButton, 0, 1);
            layout.Controls.Add(answerImage, 0, 2);
            
            if (marks >= 4) {
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                Button openVideoButton = new Button();
                openVideoButton.Text = "Video workthrough";
                openVideoButton.Height = 40;
                openVideoButton.Width = 200;
                openVideoButton.MouseClick += (sender, e) =>
                {
                    VideoForm videoForm = new VideoForm("WS.mp4", videoPath);
                    videoForm.Show();
                };
                layout.Controls.Add(openVideoButton, 0 ,3);
            }
            Button newRandomQButton = new Button();
            newRandomQButton.Text = "New question";
            newRandomQButton.Height = 40;
            newRandomQButton.Width = 200;
            newRandomQButton.MouseClick += (sender, e) =>
            {
                EducationForm educationForm = this.FindForm() as EducationForm;
                educationForm.loadPage(new RandomQuestion());
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.Controls.Add(newRandomQButton, 0, 4);


            Controls.Add(layout);
        }

        public void scaleImage(ref PictureBox image, int screenWidth) {
            double scaleFactor = 0;
            while (image.Image.Width * scaleFactor < screenWidth) {
                scaleFactor += 0.1;
            }
            if (scaleFactor > 1)
            {
                image.Width = image.Image.Width * Convert.ToInt16(Math.Floor(scaleFactor));
                image.Height = image.Image.Height * Convert.ToInt16(Math.Floor(scaleFactor));
            }
            else {
                image.Width = image.Image.Width * Convert.ToInt16(Math.Ceiling(scaleFactor));
                image.Height = image.Image.Height * Convert.ToInt16(Math.Ceiling(scaleFactor));
            }

            
        }
    }
}
