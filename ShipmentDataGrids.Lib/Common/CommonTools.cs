using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Data.Odbc;
using Newtonsoft.Json;
using ShipmentDataGrids.Lib.Interfaces;

namespace ShipmentDataGrids.Lib.Common
{
    public static class CommonTools
    {

        #region Fields

            // Базовая строка запроса
            const string _sqlAll = @"SELECT  
            shipment.id as 'Id',
            timestamp as 'Ts',
            post_name as 'PostName',
            shipment.time_begin as 'TimeBegin',
            shipment.time_end as 'TimeEnd',
            shipment.set_point as 'SetPoint',
            shipment.result_weight as 'ResultWeight',
            shipment.result_volume as 'ResultVolume',
            unit.name as 'Unit',
            shipment.density as 'Density',
            shipment.temperature as 'Temperature',
            shipment.product_name as 'Product',
            shipment.tank_name as 'TankName',
            final_status.name as 'FinalStatus'
            FROM shipment
            INNER JOIN unit ON shipment.unit_id = unit.id
            INNER JOIN final_status ON shipment.final_status_id = final_status.id
            ORDER BY shipment.time_begin DESC";

        #endregion


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

                    conString = "DRIVER={MySQL ODBC 8.0 Unicode Driver};SERVER=localhost;DATABASE=ShipmentDb;UID=asn_user;PASSWORD=asn_user;OPTION=3";
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
        /// Возвращает экземляр соединения с БД IDbConnection
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IDbConnection GetDbConnection(IConfig config)
        {

            IDbConnection dbConnection = null;

            switch (config.DbType)
            {
                case "SqlExpress":

                    dbConnection = new SqlConnection(GetConnectionString(config));

                    break;

                case "MySql":
                    dbConnection = new OdbcConnection(GetConnectionString(config));
                    break;

                case "PostgreSql":
                    // TODO: VAP; Доделать
                    break;

                case "Sqlite":
                    // TODO: VAP; Доделать
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
                    cfg = (Config)serializer.Deserialize(file, typeof(Config));
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
