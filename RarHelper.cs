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
        /// <summary>
        /// 压缩指定文件
        /// </summary>
        /// <param name="SourcePath">源文件路径</param>
        /// <param name="TargetPath">目标文件路径</param>
        public static void Compress(string SourcePath, string TargetPath)
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
                sb.Append($" {SourcePath}");
                sb.Append($" {TargetPath}");
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
