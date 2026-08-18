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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Logic_Gate_Builder.UI_Classes.Educational_Pages
{
    public partial class Exam : UserControl
    {
        public Exam(MyList<QA> theExam, int totalMinutes, ref ProgressBarForm pBF)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            int timeLeft = totalMinutes * 60;
            Label timerLabel = new Label
            {
                Text = formatT(timeLeft),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Height = 50,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pBF.add10Percentage();
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Start();
            t.Interval = 1000;
            t.Tick += (s, e) => {
                if (timeLeft > 0)
                {
                    timeLeft--;
                    timerLabel.Text = formatT(timeLeft);
                }
                else
                {
                    try
                    {
                        EducationForm educationForm = this.FindForm() as EducationForm;
                        educationForm.loadPage(new ExamReviewPage(theExam));
                        t.Stop();
                    }
                    catch { }

                }
            };
            
           
            Controls.Add(timerLabel);
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                ColumnCount = 1,
                RowCount = 3,
                AutoScroll = true
            };

            pBF.add10Percentage();
            layout.SuspendLayout();
            Label fillerText = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                
            };
            
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.Controls.Add(fillerText);
            for (int i = 0; i < theExam.getLength(); i++) {
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                Label text1 = new Label
                {
                    Text = "Q"+(i+1).ToString()+")",
                    Font = new Font("Segoe UI", 10),
                    Dock = DockStyle.Top,
                    Padding = new Padding(12),
                    AutoSize = true,
                    MaximumSize = new Size(this.Width, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black,
                };
                layout.Controls.Add(text1, 0, 2*i+1);
                PictureBox pictureBox1 = theExam.getItem(i).getQuestion();
                layout.Controls.Add(pictureBox1, 0, 2*i+2);
            }
            pBF.add10Percentage();
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Button endExamButton = new Button
            {
                Text = "End exam",
                Width = 200,
                Height = 40
            };
            pBF.add10Percentage();
            endExamButton.Click += (s, e) =>
            {
                EducationForm educationForm = this.FindForm() as EducationForm;
                educationForm.loadPage(new ExamReviewPage(theExam));
            };
            layout.Controls.Add(endExamButton);
            layout.ResumeLayout();

            this.Controls.Add(layout);
            foreach (Control c in layout.Controls)
            {
                c.TabStop = false;
            }
            pBF.add10Percentage();

        }

        private string formatT(int seconds) {
            int hours = 0;
            int minutes = 0;
            while (seconds / 3600 >= 1) {
                seconds = seconds - 3600;
                hours++;
            }while (seconds / 60 >= 1) { 
                seconds = seconds- 60;
                minutes++;
            }
            string hoursStr = "";
            if (hours > 9)
            {
                hoursStr = hours.ToString();
            }
            else {
                hoursStr = "0" + hours.ToString();
            }
            string minutesStr = "";
            if (minutes > 9)
            {
                minutesStr = minutes.ToString();
            }
            else
            {
                minutesStr = "0" + minutes.ToString();
            }
            string secondsStr = "";
            if (seconds > 9)
            {
                secondsStr = seconds.ToString();
            }
            else { 
                secondsStr= "0" + seconds.ToString();
            }
            string niceT = hoursStr + ":" + minutesStr + ":" + secondsStr;
            return niceT;
        }

    }
}
