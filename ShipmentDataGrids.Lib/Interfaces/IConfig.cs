﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDataGrids.Lib.Interfaces
{
    public interface IConfig
    {
        /// <summary>
        /// Адрес сервера
        /// </summary>
        string Server { get; set; }

        /// <summary>
        /// Имя базы данных
        /// </summary>
        string DbName { get; set; }

        /// <summary>
        /// Наименование СУБД
        /// </summary>
        string DbType { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Указвает, какие колонки следует отобразить в таблице
        /// </summary>
        Columns Columns { get; set; }

        /// <summary>
        /// Путь к шаблону ТТН
        /// </summary>
        string TemplatePath { get; set; }

    }
}
