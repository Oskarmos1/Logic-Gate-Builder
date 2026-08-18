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
    public partial class ExamReviewPage : UserControl
    {
        public ExamReviewPage(MyList<QA> theExam)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                AutoScroll = true
            };
            for (int i = 0; i < theExam.getLength(); i++)
            {
                QA questionInfo = theExam.getItem(i);
                PictureBox qImage = questionInfo.getQuestion();
                PictureBox aImage = questionInfo.getAnswer();
                VideoForm wsVideo = questionInfo.getVideo();
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                Label text1 = new Label
                {
                    Text = "Q" + (i + 1).ToString() + ")",
                    Font = new Font("Segoe UI", 10),
                    Dock = DockStyle.Top,
                    Padding = new Padding(12),
                    AutoSize = true,
                    MaximumSize = new Size(this.Width, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black
                };
                qImage.Visible = false;
                aImage.Visible = false;
                Button showQuestionButton = new Button
                {
                    Text = "Show Question",
                    Width = 200,
                    Height = 40
                };
                showQuestionButton.Click += (sender, e) =>
                {
                    if (qImage.Visible == true)
                    {
                        qImage.Visible = false;
                    }
                    else
                    {
                        qImage.Visible = true;
                    }
                };
                Button showAnswerButton = new Button
                {
                    Text = "Show Answer",
                    Width = 200,
                    Height = 40
                };
                showAnswerButton.Click += (sender, e) =>
                {
                    if (aImage.Visible == true)
                    {
                        aImage.Visible = false;
                    }
                    else
                    {
                        aImage.Visible = true;
                    }
                };
                layout.Controls.Add(text1, 0, 3 * i);
                layout.Controls.Add(showQuestionButton, 0, 3 * i + 1);
                layout.Controls.Add(showAnswerButton, 1, 3 * i + 1);
                layout.Controls.Add(qImage, 0, 3 * i + 2);
                layout.Controls.Add(aImage, 1, 3 * i + 2);
                if (wsVideo != null) { 
                    Button showWSButton = new Button
                    {
                        Text = "Show Worked Solution",
                        Width = 200,
                        Height = 40
                    };
                    showWSButton.Click += (sender, e) =>
                    {
                        VideoForm copy = new VideoForm(wsVideo.getVideoFileName(), wsVideo.getBinPath());
                        copy.Show();
                    };
                    layout.Controls.Add(showWSButton, 2, 3 * i + 1);
                }
            }
            this.Controls.Add(layout);

        }
    }
}
