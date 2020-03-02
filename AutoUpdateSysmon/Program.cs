using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoUpdateSysmon
{
    class Program
    {
        private static WebClient Wclient = new WebClient();//.net webclient to pull down central config file

        static void Main(string[] args)
        {
            try
            {
                if (args.Length <= 0)
                {
                    RunUpdate();
                }
                else
                {
                    if (args[0] == "-h" || args[0] == "h" || args[0] == "help" || args[0] == "-help" || args[0] == "/h" || args[0] == "/help")
                    {
                        Console.WriteLine(@"Args must be in this fomat:

                                            AutoUpdateSysmon.exe 'URL of Sysmon Config' 'Host Computer File Path of Sysmon xml file'

                                            Example: ./AutoUpdateSysmon.exe 'https://raw.githubusercontent.com/ceramicskate0/sysmon-config/master/sysmonconfig-export.xml' 'C:\Windows\sysmonconfig-export.xml'");
                    }
                    else
                    {
                        RunUpdate(args[0], args[1]);
                        Console.WriteLine("[*] Update Status: Complete no issues.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[!] App ERROR: Error: " + e.Message.ToString());
                try
                {
                    RunUpdate();
                }
                catch(Exception r)
                {
                    Console.WriteLine("[!] Update Status: " + r.Message.ToString());
                    Environment.Exit(1);
                }
            }
            Thread.Sleep(3000);
        }
        private static void RunUpdate(string URL= @"https://raw.githubusercontent.com/ceramicskate0/sysmon-config/master/sysmonconfig-export.xml", string HostLocation= @"C:\Windows\sysmonconfig-export.xml")
        {
            Wclient.DownloadFile(URL, HostLocation);
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
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine("[*] Process Output:" + output);
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
