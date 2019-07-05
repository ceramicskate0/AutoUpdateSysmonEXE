using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdateSysmon
{
    class Program
    {
        private static WebClient Wclient = new WebClient();//.net webclient to pull down central config file

        static void Main(string[] args)
        {
            Wclient.DownloadFile(@"https://raw.githubusercontent.com/ceramicskate0/sysmon-config/master/sysmonconfig-export.xml", @"C:\Windows\sysmonconfig-export.xml");
            ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Windows\Sysmon.exe", @"-c C:\Windows\sysmonconfig-export.xml");
            startInfo.WorkingDirectory = Path.GetDirectoryName(@"C:\Windows\");
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.LoadUserProfile = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }
    }
    internal class CustomWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.UseDefaultCredentials = true;
            w.Timeout = 1000;
            return w;
        }
    }
}
