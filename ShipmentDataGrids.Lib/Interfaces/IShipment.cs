using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDataGrids.Lib.Interfaces
{
    public interface IShipment
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        [System.ComponentModel.DisplayName("ID")]
        int Id { get; set; }

        /// <summary>
        /// Метка времени
        /// </summary>
        [System.ComponentModel.DisplayName("TS")]
        DateTime Ts { get; set; }

        /// <summary>
        /// Номер поста налива
        /// </summary>
        [System.ComponentModel.DisplayName("Пост")]
        string PostName { get; set; }

        /// <summary>
        /// Дата начала налива
        /// </summary>
        [System.ComponentModel.DisplayName("Время начала")]
        DateTime TimeBegin { get; set; }

        /// <summary>
        /// Дата окончания налива
        /// </summary>
        [System.ComponentModel.DisplayName("Время окончания")]
        DateTime TimeEnd { get; set; }

        /// <summary>
        /// Уставка
        /// </summary>
        [System.ComponentModel.DisplayName("Задание")]
        double SetPoint { get; set; }

        /// <summary>
        /// Отгруженное значение по массе
        /// </summary>
        [System.ComponentModel.DisplayName("Отгружено (масса)")]
        double ResultWeight { get; set; }

        /// <summary>
        /// Отгруженное значение по объему
        /// </summary>
        [System.ComponentModel.DisplayName("Отгружено (объем)")]
        double ResultVolume { get; set; }

        /// <summary>
        /// Ед. измерения (тип измерения)
        /// </summary>
        [System.ComponentModel.DisplayName("Тип отгрузки")]
        string Unit { get; set; }

        /// <summary>
        /// Плотность продукта
        /// </summary>
        [System.ComponentModel.DisplayName("Плотность")]
        double Density { get; set; }

        /// <summary>
        /// Температура продутка
        /// </summary>
        [System.ComponentModel.DisplayName("Температура")]
        double Temperature { get; set; }

        /// <summary>
        /// Наименование продукта
        /// </summary>
        [System.ComponentModel.DisplayName("Продукт")]
        string ProductName { get; set; }

        /// <summary>
        /// Наименование цистерны
        /// </summary>
        [System.ComponentModel.DisplayName("Цистерна")]
        string TankName { get; set; }

        /// <summary>
        /// Статус налива
        /// </summary>
        [System.ComponentModel.DisplayName("Статус")]
        string FinalStatus { get; set; }

    }
}
