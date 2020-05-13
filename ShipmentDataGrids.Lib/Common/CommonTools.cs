using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using ShipmentDataGrids.Lib.Interfaces;

namespace ShipmentDataGrids.Lib.Common
{
    static class CommonTools
    {
        public static string GetConnectionString(IConfig config)
        {

            var conString;

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

                    conString = ""
                    
                    break;
            

            default:
                    break;
            }
        }
    }
}
