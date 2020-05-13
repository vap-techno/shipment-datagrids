using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentDataGrids.Lib.Interfaces;


namespace ShipmentDataGrids.Lib.Models
{
    class Shipment: IShipment
    {
        
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер поста налива
        /// </summary>
        public string PostName { get; set; }
       
        /// <summary>
        /// Метка времени
        /// </summary>
        public DateTime Ts { get; set; }

        /// <summary>
        /// Дата начала налива
        /// </summary>
        public DateTime TimeBegin { get; set; }

        /// <summary>
        /// Дата окончания налива
        /// </summary>
        public DateTime TimeEnd { get; set; }

        /// <summary>
        /// Уставка
        /// </summary>
        public double SetPoint { get; set; }

        /// <summary>
        /// Отгруженное значение по массе
        /// </summary>
        public double ResultWeight { get; set; }

        /// <summary>
        /// Отгруженное значение по объему
        /// </summary>
        public double ResultVolume { get; set; }

        /// <summary>
        /// Ед. измерения (тип измерения)
        /// </summary>
        public int Unit { get; set; }

        /// <summary>
        /// Плотность продукта
        /// </summary>
        public double Density { get; set; }

        /// <summary>
        /// Температура продутка
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Наименование цистерны
        /// </summary>
        public string TankName { get; set; }

        /// <summary>
        /// Статус налива
        /// </summary>
        public int FinalStatus { get; set; }


    }
}
