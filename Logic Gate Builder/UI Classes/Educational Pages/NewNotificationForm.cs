using Logic_Gate_Builder.Functionality_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic_Gate_Builder.UI_Classes.Educational_Pages
{
    public partial class NewNotificationForm : Form
    {
        private NotificationPref newNotification;
        public NewNotificationForm(MyList<string> unavailableNames)
        {
            InitializeComponent();
            MyList<Label> texts = new MyList<Label>();
            SetupCustomContent(ref texts, unavailableNames);
            this.Resize += (s, e) => adjustLabelWidth(ref texts);
        }

        private void SetupCustomContent(ref MyList<Label> texts, MyList<string> unavailableNames)
        {
            this.Text = "New Notification";
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            Label title = new Label
            {
                Text = "New Exam Notification",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            Label text1 = new Label
            {
                Text = "What is the name of your exam:",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            string name = "";
            bool nameValid = false;
            TextBox newNameBox = new TextBox();
            newNameBox.Dock = DockStyle.Top;
            newNameBox.BackColor = Color.Red;
            newNameBox.TextChanged += (sender, args) =>
            {

                string text = newNameBox.Text;
                if (unavailableNames.doesContain(text) || text == "")
                {
                    nameValid = false;
                }
                else
                {
                    nameValid = true;
                }

                if (nameValid == true)
                {
                    name = text;
                    newNameBox.BackColor = Color.Green;
                }
                else
                {
                    name = "";
                    newNameBox.BackColor = Color.Red;
                }
            };
            Label text2 = new Label
            {
                Text = "When is the exam:",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            Panel datePanel = new Panel
            {

                Padding = new Padding(2),
                BackColor = Color.Red,
                Dock = DockStyle.Top

            };
            datePanel.Height = 50;
            DateTime date = DateTime.Now;
            bool dateValid = false;
            DateTimePicker datePicker = new DateTimePicker
            {

                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy/MM/dd",
                Value = DateTime.Now,
                Dock = DockStyle.Fill
            };
            datePicker.ValueChanged += (sender, e) =>
            {
                if (datePicker.Value > DateTime.Now)
                {
                    date = datePicker.Value;
                    datePanel.BackColor = Color.Green;
                    dateValid = true;
                }
                else
                {
                    date = DateTime.Now;
                    datePanel.BackColor = Color.Red;
                    dateValid = false;
                }
            };
            datePanel.Controls.Add(datePicker);
            Label text3 = new Label
            {
                Text = "Do you want email notifications:",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            bool emailsWanted = false;
            CheckBox emailCheckBox = new CheckBox();
            emailCheckBox.Dock = DockStyle.Top;
            emailCheckBox.AutoSize = true;
            emailCheckBox.Padding = new Padding(12);
            emailCheckBox.CheckedChanged += (sender, e) =>
            {
                if (emailsWanted == false)
                {
                    emailsWanted = true;
                }
                else
                {
                    emailsWanted = false;
                }
            };
            Label text4 = new Label
            {
                Text = "What is your email address:",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            string emailAddress = "";
            bool addressValid = false;
            TextBox newAddressBox = new TextBox();
            newAddressBox.Dock = DockStyle.Top;
            newAddressBox.BackColor = Color.Red;
            newAddressBox.TextChanged += (sender, args) =>
            {

                string text = newAddressBox.Text;
                if (text == "" || isValidEmail(text) == false)
                {
                    addressValid = false;
                }
                else
                {
                    addressValid = true;
                }

                if (addressValid == true)
                {
                    emailAddress = text;
                    newAddressBox.BackColor = Color.Green;
                }
                else
                {
                    emailAddress = "";
                    newAddressBox.BackColor = Color.Red;
                }
            };
            Label text5 = new Label
            {
                Text = "Do you want desktop notifications:",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            bool computerWanted = false;
            CheckBox computerCheckBox = new CheckBox();
            computerCheckBox.Dock = DockStyle.Top;
            computerCheckBox.AutoSize = true;
            computerCheckBox.Padding = new Padding(12);
            computerCheckBox.CheckedChanged += (sender, e) =>
            {
                if (computerWanted == false)
                {
                    computerWanted = true;
                }
                else
                {
                    computerWanted = false;
                }
            };
            //------------------------
            Label text6 = new Label
            {
                Text = "How often do you want these revision notifications (every how many days):",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Top,
                Padding = new Padding(12),
                AutoSize = true,
                MaximumSize = new Size(this.Width, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            bool freqValid = false;
            int freq = -1;
            TextBox freqBox = new TextBox();
            freqBox.Dock = DockStyle.Top;
            freqBox.BackColor = Color.Red;
            freqBox.TextChanged += (sender, args) => {
                string text = freqBox.Text;
                for (int i = 0; i < text.Length; i++)
                {
                    if (BinaryFunctions.isNumeric(text[i]) == false)
                    {
                        freqValid = false;
                        break;
                    }
                }
                try
                {
                    freq = int.Parse(text);
                    if (freq <= 0)
                    {
                        freqValid = false;
                    }
                    else
                    {
                        freqValid = true;
                    }
                }
                catch (Exception e)
                {
                    freqValid = false;
                }


                if (freqValid == true)
                {
                    freqBox.BackColor = Color.Green;
                }
                else
                {
                    freq = -1;
                    freqBox.BackColor = Color.Red;
                }
            };
            Button createNewNotificationButton = new Button();
            createNewNotificationButton.Text = "Create new notification";
            createNewNotificationButton.Dock = DockStyle.Top;
            createNewNotificationButton.Height = 50;
            createNewNotificationButton.MouseClick += (sender, e) =>
            {
                if (nameValid == true && dateValid == true && freqValid == true) {
                    if (computerWanted == true || emailsWanted == true) {
                        if (emailsWanted == true)
                        {
                            if (addressValid == true)
                            {
                                newNotification = new NotificationPref(name, date, emailsWanted, emailAddress, computerWanted, freq);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
                        else
                        {
                            newNotification = new NotificationPref(name, date, emailsWanted, null, computerWanted, freq);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            };
            this.Controls.Add(createNewNotificationButton);
                this.Controls.Add(freqBox);
            texts.add(text6);
            this.Controls.Add(text6);
            this.Controls.Add(computerCheckBox);
            texts.add(text5);
            this.Controls.Add(text5);
            this.Controls.Add(newAddressBox);
            texts.add(text4);
            this.Controls.Add(text4);
            this.Controls.Add(emailCheckBox);
            texts.add(text3);
            this.Controls.Add(text3);
            this.Controls.Add(datePanel);
            texts.add(text2);
            this.Controls.Add(text2);
            this.Controls.Add(newNameBox);
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


        public bool isValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public NotificationPref getNotificationPref() {
            return this.newNotification;
        }
    }
}
