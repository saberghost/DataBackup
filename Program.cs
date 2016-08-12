using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            Setting set = new Setting();
            string epName = $"{set.Uid}.{DateTime.Now.ToString("yyyy.MM.dd.HH.mm")}".ToUpper();
            string commonText = $"expdp {set.Uid}/{set.Pwd}@{set.Server} directory={set.Dir} dumpfile={epName}.dmp logfile={epName}.log";
            if (!string.IsNullOrEmpty(set.Version))
            {
                commonText += $" version={set.Version}";
            }
            Console.WriteLine(commonText);
            string dirPath = string.Empty;
            string filePath = string.Empty;
            string connStr = $"Data Source={set.IP}/{set.Server};User ID={set.Uid};Password={set.Pwd}";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select directory_path from dba_directories where directory_name='{set.Dir.ToUpper()}'";
                    try
                    {
                        cmd.Connection.Open();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        return;
                    }
                    dirPath = cmd.ExecuteScalar().ToString();
                    if (!dirPath.EndsWith("\\"))
                    {
                        dirPath += "\\";
                    }
                    filePath = $"{dirPath}{epName}";
                }
            }
            DeleteFile(set, dirPath);
            Cmd.CmdExecute(commonText);
            RarHelper.Compress(filePath + ".rar", filePath + ".*");
            if (!string.IsNullOrEmpty(set.ExpDir))
            {
                if (!set.ExpDir.EndsWith("\\"))
                {
                    set.ExpDir += "\\";
                }
                DeleteFile(set, set.ExpDir);
                File.Copy(filePath + ".rar", set.ExpDir + epName + ".rar");
            }
            Console.WriteLine("数据库导出成功");
            Thread.Sleep(3000);
        }

        private static void DeleteFile(Setting set, string dirPath)
        {
            string[] files = Directory.GetFiles(dirPath, $"{set.Uid.ToUpper()}*.rar");
            foreach (string file in files)
            {
                try
                {
                    string fileName = Path.GetFileName(file);
                    string timeStr = fileName.Substring(fileName.IndexOf('.') + 1, 16);
                    var time = DateTime.ParseExact(timeStr, "yyyy.MM.dd.HH.mm", System.Globalization.CultureInfo.InvariantCulture);
                    TimeSpan span = DateTime.Now.Subtract(time);
                    int dayDiff = span.Days + 1;
                    if (dayDiff > 7)
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
