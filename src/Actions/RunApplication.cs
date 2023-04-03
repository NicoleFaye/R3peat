using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System.Diagnostics;

namespace R3peat
{
    public class RunApplication : Action
    {
        public string ApplicationPath { get; set; }
        public string Arguments { get; set; }

        public RunApplication(string name, string applicationPath, string arguments = "")
        {
            Name = name;
            ApplicationPath = applicationPath;
            Arguments = arguments;
        }

        public override void Run()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ApplicationPath,
                Arguments = Arguments,
                UseShellExecute = true,
            };

            Process.Start(startInfo);
        }
    }

}
