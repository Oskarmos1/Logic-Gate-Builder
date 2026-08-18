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
    public partial class NotificationComponent : UserControl
    {
        public NotificationComponent(NotificationPref noti)
        {
            InitializeComponent();
            MyList<Label> texts = new MyList<Label>();
            SetupCustomContent(ref texts, noti);
            this.Resize += (s, e) => adjustLabelWidth(ref texts);
            
        }
        private void SetupCustomContent(ref MyList<Label> texts, NotificationPref noti)
        {
            this.Height = 200;
            BorderStyle = BorderStyle.FixedSingle; 

             
            Dock = DockStyle.Fill;
            string txt = "";
            if (noti.getEmailsWanted() == true)
            {
                 txt = "Name: " + noti.getExamName() +
                "\nDate:" + noti.getExamDate().ToString() +
                "\nEmails:" + noti.getEmailsWanted().ToString() +
                "\nEmail address:" + noti.getEmailAddress().ToString()+
                "\nComputer:" + noti.getComputerNotWanted().ToString() +
                "\nFrequency:" + noti.getFreq().ToString();
            }
            else {
                txt = "Name: " + noti.getExamName() +
                 "\nDate:" + noti.getExamDate().ToString() +
                 "\nEmails:" + noti.getEmailsWanted().ToString() +
                 "\nComputer:" + noti.getComputerNotWanted().ToString() +
                 "\nFrequency:" + noti.getFreq().ToString();
            }

                Label text = new Label
                {
                    Text = txt,
                    Font = new Font("Segoe UI", 10),
                    Dock = DockStyle.Top,
                    Padding = new Padding(12),
                    AutoSize = true,
                    MaximumSize = new Size(this.Width, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black
                };
            texts.add(text);
            this.Controls.Add(text);
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

