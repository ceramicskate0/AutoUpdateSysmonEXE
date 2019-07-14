# AutoUpdateSysmonEXE

How to use:
- Compile and run your own. 
- Run AutoUpdateSysmonEXE.exe as a scheduled task (as LOCAL ADMIN) per sysmon guidance.

- Flagged by AV? Here why:
"            Wclient.DownloadFile(@"https://raw.githubusercontent.com/ceramicskate0/sysmon-config/master/sysmonconfig-export.xml",    @"C:\Windows\sysmonconfig-export.xml");

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
            "
            
            
In short its because im downloading a file to C:\Windows by default (sysmon default install location) and the starting a program that calls it.
