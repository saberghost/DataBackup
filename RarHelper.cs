using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DataBackup
{
    class RarHelper
    {
        public static void Rar(string OriPath, string destPath)
        {
            try
            {
                string exe = "Rar.exe";
                if (!File.Exists(exe))
                {
                    throw new ApplicationException("Can not find: " + exe);
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(" m");
                sb.Append(" -m5");
                sb.Append(" -ep1");
                sb.Append($" {OriPath}");
                sb.Append($" {destPath}");
                Process proc = new Process();
                proc.StartInfo.FileName = exe;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.Arguments = sb.ToString();
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
