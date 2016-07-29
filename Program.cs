using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            string ip = ConfigurationManager.AppSettings["数据库IP"];
            string uid = ConfigurationManager.AppSettings["数据库用户名"];
            string pwd = ConfigurationManager.AppSettings["数据库密码"];
            string server = ConfigurationManager.AppSettings["数据库服务名"];
            string version = ConfigurationManager.AppSettings["数据库版本"];
            string dir = ConfigurationManager.AppSettings["数据库泵目录"];
            string isZip = ConfigurationManager.AppSettings["是否压缩"];
            string epName = $"{uid}.{DateTime.Now.ToString("yyyy.MM.dd.HH.mm")}".ToUpper();
            string temp = DateTime.Now.ToString("yyyy.MM.dd.HH.mm");
            string commonText = $"expdp {uid}/{pwd}@{server} directory={dir} dumpfile={epName}.dmp logfile={epName}.log";
            if (!string.IsNullOrEmpty(version))
            {
                commonText += $" version={version}";
            }
            Console.WriteLine(commonText);
            string path = string.Empty;
            string connStr = $"Data Source={ip}/{server};User ID={uid};Password={pwd}";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select directory_path from dba_directories where directory_name='{dir.ToUpper()}'";
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
                    path = cmd.ExecuteScalar().ToString();
                    if (!path.EndsWith("\\"))
                    {
                        path = path + "\\";
                    }
                    path = $"{path}{epName}";
                }
            }
            Cmd.CmdExecute(commonText);
            if (isZip == "是")
            {
                RarHelper.Rar(path + ".rar", path + ".*");
            }
            Console.WriteLine("数据库导出成功");
            Thread.Sleep(3000);
        }
    }
}
