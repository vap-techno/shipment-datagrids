using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentDataGrids.Lib.Interfaces;

namespace ShipmentDataGrids.Tests.Models
{
    class ShipmentDto : IShipmentDto
    {

        public ShipmentDto()
        {

        }

        public ShipmentDto(IShipment shipment)
        {
            Map(shipment);
        }

        private void Map(IShipment shipment)
        {
            PostName = shipment.PostName;
            TimeBegin = shipment.TimeBegin;
            SetPoint = shipment.SetPoint;
            ResultMain = shipment.ResultMain;
            ResultSecondary = shipment.ResultSecondary;

            switch (shipment.UnitMain)
            {
                case "кг":
                    UnitMain = 0;
                    break;

                case "л":
                    UnitMain = 1;
                    break;

                case "см":
                    UnitMain = 2;
                    break;

                default:
                    UnitMain = 0;
                    break;

            }

            switch (shipment.UnitSecondary)
            {
                case "кг":
                    UnitSecondary = 0;
                    break;

                case "л":
                    UnitSecondary = 1;
                    break;

                case "см":
                    UnitSecondary = 2;
                    break;

                default:
                    UnitSecondary = 0;
                    break;

            }

            Density = shipment.Density;
            Temperature = shipment.Temperature;
            ProductName = shipment.ProductName;
            TankName = shipment.TankName;

            switch(shipment.FinalStatus)
            {
                case "Завершена":
                    FinalStatus = 1;
                    break;

                case "Остановлена":
                    FinalStatus = 2;
                    break;

                default:
                    FinalStatus = 0;
                    break;
            }


        }

        public string PostName { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime TimeEnd { get; set; }
        public double SetPoint { get; set; }
        public double ResultMain { get; set; }
        public double ResultSecondary { get; set; }
        public int UnitMain { get; set; }

        public int UnitSecondary { get; set; }
        public double Density { get; set; }
        public double Temperature { get; set; }
        public string ProductName { get; set; }
        public string TankName { get; set; }
        public int FinalStatus { get; set; }
    }
}
