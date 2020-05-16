using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ShipmentDataGrids.Lib.Models;
using ShipmentDataGrids.Lib.Interfaces;
using ShipmentDataGrids.Lib.Common;
using ShipmentDataGrids.Lib.Services;
using ShipmentDataGrids.Lib;
using ShipmentDataGrids.Tests.Models;
using System.Data.Odbc;

namespace ShipmentDataGrids.Tests
{
    [TestFixture]
    public class DbServiceTests
    {
        [Test]
        public void Mysql_reading()
        {

            // Arrange
            //string sqlQueryIns = "INSERT INTO shipment(post_name, timestamp, time_begin, time_end, set_point, result_weight, result_volume, unit_id, temperature, density, product_name, tank_name, final_status_id) " +
            //    "VALUES(@PostName, NOW(), @TimeBegin, @TimeEnd, @SetPoint, @ResultWeight, @ResultVolume, @Unit, @Temperature, @Density, @ProductName, @TankName, @FinalSatus)";

            var cfg = new Config()
            {
                DbName = "ShipmentDb",
                DbType = "Mysql",
                UserName = "asn_user",
                Password = "asn_user"
            };

            // Act
            var dbService = new DbService(cfg);
            var lst = dbService.GetShipments();

            // Drop values


            // Assert
            Assert.IsTrue(lst.Count > 0);
        }



        private List<IShipment> GenerateTestEntities(int qnt = 100)
        {
            List<IShipment> lst = new List<IShipment>();

            for (int i = 0; i < qnt; i++)
            {
                IShipment s = new Shipment();

                s.Id = i;
                s.Ts = DateTime.Now.AddHours(-i);
                s.PostName = "Пост 1";
                s.TimeBegin = s.Ts.AddMinutes(-10);
                s.TimeEnd = s.Ts;
                s.SetPoint = 1000 + 0.1 * i;
                s.ResultWeight = s.SetPoint + (new Random()).NextDouble();
                s.ResultVolume = s.SetPoint + (new Random()).NextDouble();

            }

            return lst;
        }
    }
}

