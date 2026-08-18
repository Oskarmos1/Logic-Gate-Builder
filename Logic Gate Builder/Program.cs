using Logic_Gate_Builder.Functionality_Classes;

namespace Logic_Gate_Builder
{
    public static class Program
    {
        static public MainForm mainForm;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                MessageBox.Show("BACKGROUND EXCEPTION FIRED!\n" + e.ExceptionObject.ToString());
            };


            try
            {
                if (args.Length > 0)
                {
                    if (args[0] == "--show-toast")
                    {
                        if (args[2] == "B")
                        {
                            NotificationsManager.sendEmailNotification(args[1]);
                            NotificationsManager.displayNotification();
                        }
                        else if (args[2] == "E")
                        {
                            NotificationsManager.sendEmailNotification(args[1]);
                        }
                        else if (args[2] == "C")
                        {
                            NotificationsManager.displayNotification();
                        }

                    }
                    if (File.Exists(args[0]) == true)
                    {
                        mainForm = new MainForm(args[0]);
                    }

                }
                else
                {
                    mainForm = new MainForm(null);
                }
                Application.Run(mainForm);
            }
            catch (Exception ex) {
                MessageBox.Show("An error has occured:" + "\n" + ex);
            }
            }
          
    }
    //Screen dimensions should be 1600x900
}