using Microsoft.Win32;
using Rimot.Shared.Utilities;
using System;
using System.Diagnostics;
using System.IO;

namespace Rimot.Agent.Services
{
    public class Uninstaller
    {
        public void UninstallAgent()
        {
            if (EnvironmentHelper.IsWindows)
            {
                Process.Start("cmd.exe", "/c sc delete Rimot_Service");

                var view = Environment.Is64BitOperatingSystem ?
                    "/reg:64" :
                    "/reg:32";

                Process.Start("cmd.exe", @$"/c REG DELETE HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Rimot /f {view}");

                var currentDir = Path.GetDirectoryName(typeof(Uninstaller).Assembly.Location);
                Process.Start("cmd.exe", $"/c timeout 5 & rd /s /q \"{currentDir}\"");
            }
            else if (EnvironmentHelper.IsLinux)
            {
                Process.Start("sudo", "systemctl stop rimot-agent").WaitForExit();
                Directory.Delete("/usr/local/bin/Rimot", true);
                File.Delete("/etc/systemd/system/rimot-agent.service");
                Process.Start("sudo", "systemctl daemon-reload").WaitForExit();
            }
            Environment.Exit(0);
        }
    }
}
