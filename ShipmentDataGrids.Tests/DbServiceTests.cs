using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ShipmentDataGrids.Lib.Models;
using ShipmentDataGrids.Lib.Interfaces;


namespace ShipmentDataGrids.Tests
{
    [TestFixture]
    public class DbServiceTests
    {
        [Test]
        public void Sqlite_reading()
        {

        }

        private void InsertEntity(IShipment entity)
        {

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
