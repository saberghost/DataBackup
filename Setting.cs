using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DataBackup
{
    public class Setting
    {
        /// <summary>
        /// 数据库IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 数据库密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 数据库服务名
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 数据库版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 数据库泵目录
        /// </summary>
        public string Dir { get; set; }
        /// <summary>
        /// 保留天数
        /// </summary>
        public string Diff { get; set; }
        /// <summary>
        /// 额外备份目录
        /// </summary>
        public string ExpDir { get; set; }
        public Setting()
        {
            IP = ConfigurationManager.AppSettings["数据库IP"];
            Uid = ConfigurationManager.AppSettings["数据库用户名"];
            Pwd = ConfigurationManager.AppSettings["数据库密码"];
            Server = ConfigurationManager.AppSettings["数据库服务名"];
            Version = ConfigurationManager.AppSettings["数据库版本"];
            Dir = ConfigurationManager.AppSettings["数据库泵目录"];
            Diff = ConfigurationManager.AppSettings["保留天数"];
            ExpDir = ConfigurationManager.AppSettings["额外备份目录"];
        }
    }
}
