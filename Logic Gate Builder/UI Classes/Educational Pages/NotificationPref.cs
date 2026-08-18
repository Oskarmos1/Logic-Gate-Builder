using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.UI_Classes.Educational_Pages
{


    public class NotificationPref
    {
        private string examName;
        private DateTime examDate;
        private bool emailsWanted;
        private string? emailAddress;
        private bool computerNotiWanted;
        private int freq;
        private string taskName;

        public NotificationPref(string examName, DateTime examDate, bool emailsWanted, string? emailAddress, bool computerNotWanted, int freq)
        {
            this.examName = examName;
            this.examDate = examDate;
            this.emailsWanted = emailsWanted;
            this.emailAddress = emailAddress;
            this.computerNotiWanted = computerNotWanted;
            this.freq = freq;
            this.taskName = null;
        }

        public string getExamName() {
            return examName;
        }

        public DateTime getExamDate() {
            return examDate;
        }

        public bool getEmailsWanted() { 
            return emailsWanted;
        }

        public string getEmailAddress()
        {
            return emailAddress;
        }

        public bool getComputerNotWanted() { 
            return computerNotiWanted; 
        }

        public int getFreq() { 
            return freq;
        }

        public string getTaskName() {
            return taskName;
        }

        public void setTaskName(string taskName) { 
            this.taskName = taskName;
        }


    }
}
