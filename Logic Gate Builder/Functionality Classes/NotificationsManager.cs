using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.Functionality_Classes
{
    public class NotificationsManager
    {
        public static void displayNotification()
        {
            MessageBox.Show("It is time to revise logic circuits.");
            
        }
        public static void ScheduleNotificationInOneMinute(string email)
        {
            try
            {
                string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string time = DateTime.Now.AddMinutes(1).ToString("HH:mm");
                string taskName = "LogicGateReminder_" + Guid.NewGuid().ToString();
                string arguments =
                    $"/Create /SC ONCE /TN \"{taskName}\" /TR \"\\\"{exePath}\\\" --show-toast {email}\" /ST {time} /F";
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false
                });

                Console.WriteLine("Scheduled notification in 1 minute.");
            }
            catch (Exception e) {
                throw new Exception("An error occured when trying to schedule a notification: " + e.Message);
            }
            
        }
        public static void sendEmailNotification(string recipientMail)
        {
            try
            {
                string senderEmail = "logicgatebuilder3@gmail.com";
                string senderPassword = "sbcd kvpf weai scje";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Logic Gate Builder Revision",
                    Body = "You need to start revising.",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(recipientMail);
                smtpClient.Send(mailMessage);
            }
            catch (Exception e) {
                throw new Exception("An error occured when trying to send an email notification: " + e.Message);
            }

        }
    }
}
