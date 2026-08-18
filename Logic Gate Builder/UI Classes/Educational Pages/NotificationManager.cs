using Logic_Gate_Builder.Functionality_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Logic_Gate_Builder.UI_Classes.Educational_Pages
{
    public partial class NotificationManager : UserControl
    {
        private TableLayoutPanel layout;
        public NotificationManager()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            
            string fileName = "Notifications.txt";
            MyList<NotificationPref> notifications = new MyList<NotificationPref>();
            if (File.Exists(fileName) == true)
            {
                StreamReader sr = new StreamReader(fileName);
                string? newName = null;
                DateTime? date = null;
                bool? email = null;
                string? address = null;
                bool? computer = null;
                int? freq = null;
                string taskName = "";
                while (sr.Peek() > -1)
                {
                    string line = sr.ReadLine();
                    string first2 = line[0].ToString() + line[1].ToString();
                    string remaining = line.Substring(2);

                    switch (first2)
                    {
                        case "NE":
                            Debug.WriteLine(newName);
                            Debug.WriteLine(date.ToString());
                            Debug.WriteLine(email);
                            Debug.WriteLine(computer);
                            Debug.WriteLine(freq);
                            if (FileHandling.isNullOrWhiteSpace(newName) == false && date != null && email != null && computer != null && freq != null)
                            {
                                NotificationPref pref = new NotificationPref(newName, date.Value, email.Value, address, computer.Value, freq.Value);
                                pref.setTaskName(taskName);                             
                                notifications.add(pref);
                            }
                            break;
                        case "N:":
                            newName = remaining;
                            break;
                        case "D:":
                            date = DateTime.ParseExact(remaining, "yyyy/MM/dd", null);
                            break;
                        case "E:":
                            string yN = remaining;
                            if (yN == "T")
                            {
                                email = true;
                            }
                            else
                            {
                                email = false;
                            }
                            break;
                        case "A:":
                            if (FileHandling.isNullOrWhiteSpace(remaining) == true)
                            {
                                address = null;
                            }
                            else
                            {
                                address = remaining;
                            }
                            break;
                        case "C:":
                            string yN1 = remaining;
                            if (yN1 == "T")
                            {
                                computer = true;
                            }
                            else
                            {
                                computer = false;
                            }
                            break;
                        case "F:":
                            try
                            {
                                freq = int.Parse(remaining);
                            }
                            catch (Exception ex)
                            {
                                throw new IOException("Unrecognised input. Please check the file is correct.");
                            }
                            break;
                        case "T:":
                            taskName = remaining;
                            break;
                        default:
                            throw new IOException("Unrecognised input. Please check the file is correct.");
                            break;
                    }
                }
            }
            else {
                File.Create(fileName);
            }
            layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                AutoScroll = true
            };

            rebuildLayout(notifications);
            this.Controls.Add(layout);
        }

        private void rebuildLayout(MyList<NotificationPref> notifications) {
            
            layout.ColumnCount = 2;
            layout.SuspendLayout();
            layout.Controls.Clear();
            layout.RowStyles.Clear();
            layout.ColumnStyles.Clear();
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            for (int i = 0; i < notifications.getLength(); i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layout.Controls.Add(new NotificationComponent(notifications.getItem(i)), 0, i);
                Button deleteButton = new Button();
                deleteButton.Tag = i;
                deleteButton.Text = "Delete notification";
                deleteButton.Dock = DockStyle.Fill;
                deleteButton.Click += (sender, e) =>
                {
                    Button clickedButton = (Button)sender;
                    int thePos = (int)clickedButton.Tag;

                    NotificationPref notif = notifications.getItem(thePos) as NotificationPref;
                    if (FileHandling.isNullOrWhiteSpace(notif.getTaskName()) == false)
                    {
                        TaskSchedulerHelper.CancelTask(notif.getTaskName());
                    }
                    notifications.removeAt(thePos);
                    updateSavedFile(notifications);
                    rebuildLayout(notifications);
                };
                layout.Controls.Add(deleteButton, 1, i);
            }
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Button addNewNotificationButton = new Button();
            addNewNotificationButton.Dock = DockStyle.Top;
            addNewNotificationButton.Text = "Add new notification";
            addNewNotificationButton.Height = 50;
            addNewNotificationButton.Click += (sender, e) => {
                MyList<string> currentNotificationNames = new MyList<string>();
                for (int i = 0; i < notifications.getLength(); i++)
                {
                    currentNotificationNames.add(notifications.getItem(i).getExamName());
                }
                NotificationPref newNotification = null;
                NewNotificationForm nNF = new NewNotificationForm(currentNotificationNames);

                if (nNF.ShowDialog() == DialogResult.OK)
                {
                    newNotification = nNF.getNotificationPref();
                    nNF.Close();
                }
                if (newNotification != null)
                {
                    string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string argument = "--show-toast";
                    string freqToken; 
                    int freq = newNotification.getFreq();
                    if (freq >= 1 && freq <= 3)
                    {
                        freqToken = "DAILY";
                    }
                    else if (freq >= 3 && freq <= 10)
                    {
                        freqToken = "WEEKLY";
                    }
                    else {
                        freqToken = "MONTHLY";
                    }
                    string choice = "";
                    if (newNotification.getEmailsWanted() == true && newNotification.getComputerNotWanted() == true)
                    {
                        choice = "B";
                    }
                    else if (newNotification.getEmailsWanted() == false && newNotification.getComputerNotWanted() == true)
                    {
                        choice = "C";
                    }
                    else {
                        choice = "E";
                    }
                        string? taskName = TaskSchedulerHelper.ScheduleTask(choice, newNotification.getEmailAddress(), newNotification.getExamDate(), exePath, argument, freqToken, null);
                    if (taskName != null)
                    {
                        newNotification.setTaskName(taskName);

                    }
                    else
                    {
                        throw
                        new Exception("Notification scheduling failed.");
                    }
                    notifications.add(newNotification);
                    rebuildLayout(notifications);
                    updateSavedFile(notifications);
                }
            };
            layout.Controls.Add(addNewNotificationButton);
            layout.AutoScroll = true;
            layout.ResumeLayout();
        }

        private void updateSavedFile(MyList<NotificationPref> notifications)
        {
            string fileName = "Notifications.txt";
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.WriteLine("NEW EXAM");
                for (int i = 0; i < notifications.getLength(); i++)
                {
                    NotificationPref nP = notifications.getItem(i);
                    sw.WriteLine("N:" + nP.getExamName());
                    DateTime date = nP.getExamDate();
                    string monthString = "";
                    string dayString = "";
                    if (date.Month > 9)
                    {
                        monthString = date.Month.ToString();
                    }
                    else
                    {
                        monthString = "0" + date.Month.ToString();
                    }

                    if (date.Day > 9)
                    {
                        dayString = date.Day.ToString();
                    }
                    else
                    {
                        dayString = "0" + date.Day.ToString();
                    }
                    string formattedDate = date.Year.ToString() + "/" + monthString + "/" + dayString;
                    sw.WriteLine("D:" + formattedDate);
                    if (nP.getEmailsWanted() == true)
                    {
                        sw.WriteLine("E:T");
                        sw.WriteLine("A:" + nP.getEmailAddress());
                    }
                    else
                    {
                        sw.WriteLine("E:F");
                        sw.WriteLine("A:");
                    }
                    if (nP.getComputerNotWanted() == true)
                    {
                        sw.WriteLine("C:T");
                    }
                    else
                    {
                        sw.WriteLine("C:F");
                    }
                    sw.WriteLine("F:" + nP.getFreq().ToString());
                    if (nP.getTaskName() != null)
                    {
                        sw.WriteLine("T:" + nP.getTaskName().ToString());
                    }
                    else
                    {
                        sw.WriteLine("T:");
                    }
                    sw.WriteLine("NEW EXAM");
                }
            }
        }
        
    }
}
