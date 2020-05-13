using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDataGrids.Lib.Interfaces
{
    interface IConfig
    {
        /// <summary>
        /// Имя базы данных
        /// </summary>
        string DbName { get; set; }

        /// <summary>
        /// Наименование СУБД
        /// </summary>
        string DbType { get; set; }

    }
}
