using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDataGrids.Tests.Models
{
    interface IShipmentDto
    {

        /// <summary>
        /// Номер поста налива
        /// </summary>
        string PostName { get; set; }

        /// <summary>
        /// Дата начала налива
        /// </summary>
        DateTime TimeBegin { get; set; }

        /// <summary>
        /// Дата окончания налива
        /// </summary>
        DateTime TimeEnd { get; set; }

        /// <summary>
        /// Уставка
        /// </summary>
        double SetPoint { get; set; }

        /// <summary>
        /// Отгруженное значение по массе
        /// </summary>
        double ResultMain { get; set; }

        /// <summary>
        /// Отгруженное значение по объему
        /// </summary>
        double ResultSecondary { get; set; }

        /// <summary>
        /// Ед. измерения (тип измерения)
        /// </summary>
        int UnitMain { get; set; }

        /// <summary>
        /// Ед. измерения (тип измерения)
        /// </summary>
        int UnitSecondary { get; set; }

        /// <summary>
        /// Плотность продукта
        /// </summary>
        double Density { get; set; }

        /// <summary>
        /// Температура продутка
        /// </summary>
        double Temperature { get; set; }

        /// <summary>
        /// Наименование продукта
        /// </summary>
        string ProductName { get; set; }

        /// <summary>
        /// Наименование цистерны
        /// </summary>
        string TankName { get; set; }

        /// <summary>
        /// Статус налива
        /// </summary>
        int FinalStatus { get; set; }
    }
}
