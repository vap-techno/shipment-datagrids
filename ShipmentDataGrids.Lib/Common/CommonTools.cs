using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using Newtonsoft.Json;
using ShipmentDataGrids.Lib.Interfaces;
using ShipmentDataGrids.Lib.Models;

namespace ShipmentDataGrids.Lib.Common
{
    public static class CommonTools
    {

        #region Fields

            // Базовая строка запроса
            const string _sqlAll = @"SELECT shipment.id as 'Id',
		        timestamp as 'Ts',
		        post_name as 'PostName',
		        shipment.time_begin as 'TimeBegin',
		        shipment.time_end as 'TimeEnd',
		        shipment.set_point as 'SetPoint',
		        shipment.result_main as 'ResultMain',
		        u1.name as 'UnitMain',
		        shipment.result_secondary as 'ResultSecondary',
		        u2.name as 'UnitSecondary',
		        shipment.density as 'Density',
		        shipment.temperature as 'Temperature',
		        shipment.product_name as 'ProductName',
		        shipment.tank_name as 'TankName',
		        final_status.name as 'FinalStatus'
		        FROM shipment
		        LEFT JOIN final_status ON shipment.final_status_id = final_status.id
		        LEFT JOIN unit u1 ON shipment.unit_main_id = u1.id
		        LEFT JOIN unit u2 ON shipment.unit_secondary_id = u2.id
		        ORDER BY shipment.time_begin DESC;";

        #endregion


        /// <summary>
        /// Возвращает строку подключения
        /// </summary>
        /// <param name="config"> Конфигурация </param>
        /// <returns></returns>
        public static string GetConnectionString(IConfig config)
        {

            string conString = null;
            string serverAddress = null;

            switch (config.DbType.ToLower())
            {
                case "sqlexpress":
                    
                    if (config.UserName == null)
                    {
                        conString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ShipmentDB;Integrated Security=True";
                    }
                    else
                    {
                        serverAddress = config.Server ?? ".\\SQLEXPRESS";
                        conString = $"Data Source={serverAddress};Persist Security Info=False;User ID={config.UserName}; Password={config.Password};Initial Catalog={config.DbName}";
                    }

                    break;

                case "mysql":

                    serverAddress = config.Server ?? "localhost";
                    conString = $"DRIVER={{MySQL ODBC 8.0 Unicode Driver}};SERVER={serverAddress};DATABASE={config.DbName};UID={config.UserName};PASSWORD={config.Password};OPTION=3";
                    break;

                case "postgresql":
                    // TODO: VAP; Вставить строку
                    conString = null;
                    break;

                case "sqlite":
                    conString = $"DRIVER={{SQLite3 ODBC Driver}};Database={config.DbName}";
                    break;
            default:
                    break;
            }

            return conString;
        }

        /// <summary>
        /// Возвращает экземляр соединения с БД IDbConnection
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IDbConnection GetDbConnection(IConfig config)
        {

            IDbConnection dbConnection = null;

            switch (config.DbType.ToLower())
            {
                case "sqlexpress":

                    dbConnection = new SqlConnection(GetConnectionString(config));

                    break;

                case "mysql":
                    dbConnection = new OdbcConnection(GetConnectionString(config));
                    break;

                case "postgresql":
                    // TODO: VAP; Доделать
                    break;

                case "sqlite":
                    dbConnection = new OdbcConnection(GetConnectionString(config));
                    break;
                default:
                    break;
            }

            return dbConnection;
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
                    var cfgDto = (ConfigDto)serializer.Deserialize(file, typeof(ConfigDto));
                    if (cfgDto != null) cfg = cfgDto.Config;
                }

            return cfg;
        }


        /// <summary>
        /// Строка запроса, возвращающая все записи и поля
        /// </summary>
        public static string SqlQueryAll { 
            get 
            {
                return _sqlAll;
            } 
        }
        


    }
}
