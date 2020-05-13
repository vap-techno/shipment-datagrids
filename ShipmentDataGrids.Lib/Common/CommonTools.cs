using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using ShipmentDataGrids.Lib.Interfaces;

namespace ShipmentDataGrids.Lib.Common
{
    static class CommonTools
    {
        /// <summary>
        /// Возвращает строку подключения
        /// </summary>
        /// <param name="config"> Конфигурация </param>
        /// <returns></returns>
        public static string GetConnectionString(IConfig config)
        {

            string conString = null;

            switch (config.DbType)
            {
                case "SqlExpress":
                    
                    if (config.UserName == null)
                    {
                        conString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ShipmentDB;Integrated Security=True";
                    } 
                    else
                    {
                        conString = $"Provider=SQLOLEDB.1;Persist Security Info=False;User ID={config.UserName}; Password={config.Password};Initial Catalog={config.DbName};Data Source=.\\SQLEXPRESS";
                    }

                    break;

                case "MySql":

                    conString = $"Server = localhost; Database = {config.DbName}; Uid = {config.UserName}; Pwd = {config.Password};";
                    break;

                case "PostgreSql":
                    // TODO: VAP; Вставить строку
                    conString = null;
                    break;

                case "Sqlite":
                    // TODO: VAP; Вставить строку
                    conString = null;
                    break;
            default:
                    break;
            }

            return conString;
        }

        /// <summary>
        /// Считывает конфигурацию из файла
        /// </summary>
        /// <param name="filename"> JSON файл с конфигурацией </param>
        /// <returns></returns>
        public static IConfig GetConfig(string filename)
        {
            IConfig cfg = null;

                using (StreamReader file = File.OpenText(filename))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    cfg = (IConfig)serializer.Deserialize(file, typeof(IConfig));
                }

            return cfg;
        }
        
    }
}
