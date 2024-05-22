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
        /// <summary>
        /// Проверяем подключение к MySQL
        /// </summary>
        [Test]
        public void Mysql_reading()
        {

            // Arrange
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

            // Assert
            Assert.That(lst.Count > 0, Is.True);
        }

        /// <summary>
        /// Проверяем подключение к MSSQL
        /// </summary>
        [Test]
        public void Mssql_reading()
        {

            // Arrange
            var cfg = new Config()
            {
                Server = "\\.SQLEXPRESS",
                DbName = "ShipmentDb",
                DbType = "mssql",
                UserName = "asn_user",
                Password = "asn_user"
            };

            // Act
            var dbService = new DbService(cfg);
            var lst = dbService.GetShipments();

            // Assert
            Assert.That(lst.Count > 0, Is.True);
        }

    }
}

