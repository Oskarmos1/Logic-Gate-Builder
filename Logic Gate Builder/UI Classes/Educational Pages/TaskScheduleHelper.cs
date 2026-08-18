using System.Diagnostics;
using System.Globalization;

public static class TaskSchedulerHelper
{
    private static void RunSchtasks(string arguments, out string stdErr)
    {
        stdErr = "";
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false
            });
            
        }
        catch (Exception ex)
        {
            stdErr = ex.Message;
            Debug.WriteLine(ex.ToString());
        }
    }
    public static string? ScheduleTask(string choice, string email, DateTime examDate, string exePath, string argumentForExe, string frequency, string? daysOfWeek = null)
    {
        string taskName = "LogicGateReminder_" + Guid.NewGuid().ToString("N");
        string time = "12:00";
        DateTime startDate = DateTime.Now.Date;

        // Ensure the exam date is after today
        if (startDate >= examDate) {
            examDate = startDate.AddDays(1);
        }

        var culture = CultureInfo.CurrentCulture;
        string dateFormat = culture.DateTimeFormat.ShortDatePattern;

        dateFormat = dateFormat.Replace("-", "/");

        string startDateStr = startDate.ToString(dateFormat);
        string endDateStr = examDate.ToString(dateFormat);

        string args = "";
        string dotnetPath = "C:\\Program Files\\dotnet\\dotnet.exe";
        switch (frequency.ToUpperInvariant())
        {
            
            case "DAILY":
                args =
                    $"/Create /SC DAILY /TN \"{taskName}\" " +
                    $"/TR \"\\\"{dotnetPath}\\\" \\\"{exePath}\\\" --show-toast \\\"{email}\\\" \\\"{choice}\\\"\" " +
                    $"/ST {time} /F";
                break;

            case "WEEKLY":
                string dArg = string.IsNullOrWhiteSpace(daysOfWeek) ? "" : $"/D {daysOfWeek}";
                args =
                    $"/Create /SC WEEKLY /TN \"{taskName}\" " +
                    $"/TR \"\\\"{dotnetPath}\\\" \\\"{exePath}\\\" --show-toast \\\"{email}\\\" \\\"{choice}\\\"\" " +
                    $"/ST {time} /F";
                break;

            case "MONTHLY":
                args =
                    $"/Create /SC MONTHLY /TN \"{taskName}\" " +
                    $"/TR \"\\\"{dotnetPath}\\\" \\\"{exePath}\\\" --show-toast \\\"{email}\\\" \\\"{choice}\\\"\" " +
                    $"/ST {time} /F";
                break;

            default:
                Debug.WriteLine("Invalid frequency: " + frequency);
                return null;
        }
        //----------------------
        /*
        time = DateTime.Now.AddMinutes(1).ToString("HH:mm");
        string dotnetPath = "C:\\Program Files\\dotnet\\dotnet.exe";
        args =
            $"/Create /SC ONCE /TN \"{taskName}\" " +
            $"/TR \"\\\"{dotnetPath}\\\" \\\"{exePath}\\\" --show-toast \\\"{email}\\\" \\\"{choice}\\\"\" " +
            $"/ST {time} /F";
        */
        //----------------------
        Debug.WriteLine("SCHTASKS ARGS: " + args);
        RunSchtasks(args, out var err);
        return taskName;
    }
    public static void CancelTask(string taskName)
    {
        string args = $"/Delete /TN \"{taskName}\" /F";
        RunSchtasks(args, out var err);
    }
}
