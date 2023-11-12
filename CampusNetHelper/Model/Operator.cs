using System.Diagnostics;

namespace CampusNetHelper.Model;

public class Operator
{
    public static bool Login(string account,string password)
    {
        Process p = new Process();
        p.StartInfo.FileName = "rasdial";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.Arguments = $"CampusNet {account} {password}";
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();
        var strOutput = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        p.Close();
        return strOutput.Contains("完成") || strOutput.Contains("successfully");
    }
    public static bool LogOut()
    {
        Process p = new Process();
        p.StartInfo.FileName = "rasdial";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.Arguments = $"CampusNet /disconnect";
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();
        var strOutput = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        p.Close();
        return strOutput.Contains("完成") || strOutput.Contains("successfully");
    }
}