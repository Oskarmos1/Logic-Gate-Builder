using Logic_Gate_Builder.Functionality_Classes;
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
    public partial class ExamSetup : UserControl
    {
        public ExamSetup()
        {
            InitializeComponent();
            MyList<Label> texts = new MyList<Label>();
            SetupCustomContent(ref texts);
            this.Resize += (s, e) => adjustLabelWidth(ref texts);
        }

        private void SetupCustomContent(ref MyList<Label> texts) {
            this.Dock = DockStyle.Fill;
            Label title = new Label
            {
                Text = "Exam",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            Label text1 = new Label
            {
                Text = "How many marks do you want the exam to be (Max 30):",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            int totalMarks = -1;
            TextBox markAmountBox = new TextBox();
            markAmountBox.Dock = DockStyle.Top;
            markAmountBox.BackColor = Color.Red;
            markAmountBox.TextChanged += (sender, args) => {
                bool isValid = true;
                string text = markAmountBox.Text;
                for (int i = 0; i < text.Length; i++)
                {
                    if (BinaryFunctions.isNumeric(text[i]) == false)
                    {
                        isValid = false;
                        break;
                    }
                }
                int marks = -1;
                try
                {
                    marks = int.Parse(text);
                }
                catch (Exception e)
                {
                    isValid = false;
                }
                if (marks <= 0 || marks >= 31)
                {
                    isValid = false;
                }

                if (isValid == true)
                {
                    totalMarks = marks;
                    markAmountBox.BackColor = Color.Green;
                }
                else
                {
                    totalMarks = -1;
                    markAmountBox.BackColor = Color.Red;
                }
            };

            Label text2 = new Label
            {
                Text = "How many long do you want the exam to be (minutes):",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            int totalMinutes = -1;
            TextBox totalTimeBox = new TextBox();
            totalTimeBox.Dock = DockStyle.Top;
            totalTimeBox.BackColor = Color.Red;
            totalTimeBox.TextChanged += (sender, args) => {
                bool isValid = true;
                string text = totalTimeBox.Text;
                for (int i = 0; i < text.Length; i++)
                {
                    if (BinaryFunctions.isNumeric(text[i]) == false)
                    {
                        isValid = false;
                        break;
                    }
                }
                int mins = -1;
                try
                {
                    mins = int.Parse(text);
                }
                catch (Exception e)
                {
                    isValid = false;
                }
                if (mins <= 0 || mins >= 5940)
                {
                    isValid = false;
                }

                if (isValid == true)
                {
                    totalMinutes = mins;
                    totalTimeBox.BackColor = Color.Green;
                }
                else
                {
                    totalMinutes = -1;
                    totalTimeBox.BackColor = Color.Red;
                }
            };
            Button generateExam = new Button();
            generateExam.Text = "Generate Exam.";
            generateExam.Dock = DockStyle.Top;
            generateExam.Height = 50;
            generateExam.MouseClick += (sender, e) =>
            {
               
                if (totalMinutes != -1 && totalMarks != -1) {
                    ProgressBarForm pBF = new ProgressBarForm();
                    pBF.Show();
                    MyList<QA> theExam = new MyList<QA>();
                    MyList<int> M1 = new MyList<int>();
                    for (int i = 0; i < 5; i++) {
                        M1.add(i + 1);
                    }
                    MyList<int> M2 = new MyList<int>();
                    for (int i = 0; i < 3; i++)
                    {
                        M2.add(i + 1);
                    }
                    MyList<int> M3 = new MyList<int>();
                    for (int i = 0; i < 9; i++)
                    {
                        M3.add(i + 1);
                    }
                    MyList<int> M4 = new MyList<int>();
                    for (int i = 0; i < 6; i++)
                    {
                        M4.add(i + 1);
                    }
                    MyList<int> M5 = new MyList<int>();
                    for (int i = 0; i < 2; i++)
                    {
                        M5.add(i + 1);
                    }
                    pBF.add10Percentage();
                    Random rnd = new Random();
                    pBF.add10Percentage();
                    //Course Adjustement
                    while (totalMarks > 6) { 
                        int m = rnd.Next(3, 6);
                        int qN = -1;
                        int listLength = -1;
                        switch (m) {
                            case 3:
                                listLength = M3.getLength();
                                if (listLength > 0) {
                                    int index = rnd.Next(0, listLength);
                                    qN = M3.getItem(index);
                                    M3.removeAt(index);
                                    totalMarks -= m;
                                }
                                break;
                            case 4:
                                listLength = M4.getLength();
                                if (listLength > 0)
                                {
                                    int index = rnd.Next(0, listLength);
                                    qN = M4.getItem(index);
                                    M4.removeAt(index);
                                    totalMarks -= m;
                                }
                                break;
                            case 5:
                                listLength = M5.getLength();
                                if (listLength > 0)
                                {
                                    int index = rnd.Next(0, listLength);
                                    qN = M5.getItem(index);
                                    M5.removeAt(index);
                                    totalMarks -= m;
                                }
                                break;

                        }

                        if (qN != -1) {
                            theExam.add(new QA(m, qN, this.Width));
                        }

                    }
                    pBF.add10Percentage();
                    pBF.add10Percentage();
                    //Fine Adjustement
                    while (totalMarks > 0) {
                        int m = rnd.Next(1,3);
                        int qN = -1;
                        int listLength = -1;
                        if (totalMarks - m >= 0) {
                            switch (m)
                            {
                                case 1:
                                    listLength = M1.getLength();
                                    if (listLength > 0)
                                    {
                                        int index = rnd.Next(0, listLength);
                                        qN = M1.getItem(index);
                                        M1.removeAt(index);
                                        totalMarks -= m;
                                    }
                                    break;
                                case 2:
                                    listLength = M2.getLength();
                                    if (listLength > 0)
                                    {
                                        int index = rnd.Next(0, listLength);
                                        qN = M2.getItem(index);
                                        M2.removeAt(index);
                                        totalMarks -= m;
                                    }
                                    break;
                            }
                        }
                        if (qN != -1) {

                            theExam.add(new QA(m, qN, this.Width));

                        }

                    }
                    pBF.add10Percentage();

                    theExam.randomiseList();
                    EducationForm educationForm = this.FindForm() as EducationForm;
                    educationForm.loadPage(new Exam(theExam, totalMinutes, ref pBF));
                }

            };
            Controls.Add(generateExam);
            texts.add(text2);
            this.Controls.Add(totalTimeBox);
            this.Controls.Add(text2);
            this.Controls.Add(markAmountBox);
            texts.add(text1);
            this.Controls.Add(text1);
            this.Controls.Add(title);

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
