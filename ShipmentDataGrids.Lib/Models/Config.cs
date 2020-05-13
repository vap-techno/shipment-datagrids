using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentDataGrids.Lib.Interfaces;

namespace ShipmentDataGrids.Lib
{
    class Config: IConfig
    {
        /// <summary>
        /// Имя базы данных
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// Наименование СУБД
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
