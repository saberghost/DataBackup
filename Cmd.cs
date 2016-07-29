using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBackup
{
    class Cmd
    {
        public static void CmdExecute(string commonText)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(commonText);
            p.StandardInput.WriteLine("exit");
            p.WaitForExit();
            p.Close();
        }
    }
}
