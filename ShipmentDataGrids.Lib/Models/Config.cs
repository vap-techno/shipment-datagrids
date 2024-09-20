using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentDataGrids.Lib.Interfaces;

namespace ShipmentDataGrids.Lib
{
    public class Config: IConfig
    {

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string Server { get; set; }

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

        /// <summary>
        /// Указвает, какие колонки следует отобразить в таблице
        /// </summary>
        public Columns Columns { get; set; }

        /// <summary>
        /// Путь к шаблону ТТН
        /// </summary>
        public string TemplatePath { get; set; }

    }

    /// <summary>
    /// Указывает какие колонки отобразить в таблице
    /// </summary>
    public struct Columns
    {
        public bool Id;
        public bool Ts;
        public bool PostName;
        public bool TimeBegin;
        public bool TimeEnd;
        public bool SetPoint;
        public bool ResultMain;
        public bool UnitMain;
        public bool ResultSecondary;
        public bool UnitSecondary;
        public bool Density;
        public bool Temperature;
        public bool ProductName;
        public bool TankName;
        public bool FinalStatus;
    }
}
