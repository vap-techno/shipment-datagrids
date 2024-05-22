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
using System.IO;


namespace ShipmentDataGrids.Tests
{
    [TestFixture]
    public class CommonToolsTests
    {
        /// <summary>
        /// Проверяем правильность генерации строки подлючения для mssql express
        /// </summary>
        [Test]
        public void GetConnectionString_sqlexpress()
        {
            // Arrange
            var cfg = new Config()
            {
                Server = "ASUTP_5//SQLEXPRESS",
                DbName = "ShipmentDb",
                DbType = "sqlexpress",
                UserName = "asn_user",
                Password = "asn_user"
            };

            // Act
            var conString = CommonTools.GetConnectionString(cfg);
            var expected = $"Data Source=ASUTP_5//SQLEXPRESS;Persist Security Info=False;User ID=asn_user; Password=asn_user;Initial Catalog=ShipmentDb";

            // Assert

            Assert.That(conString.ToLower(), Is.EqualTo(expected.ToLower()));
        }
        /// <summary>
        /// Проверяем правильность генерации строки подлючения для mysql
        /// </summary>
        [Test]
        public void GetConnectionString_mysql()
        {
            // Arrange
            var cfg = new Config()
            {
                Server = "localhost",
                DbName = "ShipmentDb",
                DbType = "mysql",
                UserName = "asn_user",
                Password = "asn_user"
            };

            // Act
            var conString = CommonTools.GetConnectionString(cfg);
            var expected = $"DRIVER={{MySQL ODBC 8.0 Unicode Driver}};SERVER=localhost;DATABASE=ShipmentDb;UID=asn_user;PASSWORD=asn_user;OPTION=3";

            // Assert
            Assert.That(conString.ToLower(), Is.EqualTo(expected.ToLower()));
        }

        /// <summary>
        /// Проверяем правильность генерации строки подлючения для sqlite
        /// </summary>
        [Test]
        public void GetConnectionString_sqlite()
        {
            // Arrange
            var cfg = new Config()
            {
                DbName = "ShipmentDb",
                DbType = "sqlite"
            };

            // Act
            var conString = CommonTools.GetConnectionString(cfg);
            var expected = $"DRIVER={{SQLite3 ODBC Driver}};Database=ShipmentDb";

            // Assert
            Assert.That(conString.ToLower(), Is.EqualTo(expected.ToLower()));
        }


        /// <summary>
        /// Проверяем правильность считывания конфигурации колонок Columns из файла
        /// </summary>
        [Test]
        public void GetConfig_getColumnsFromJsonFile()
        {

            // Arrange
            var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ConfigArm.json");

            var columns = new Columns()
            {
                Id = false,
                Ts = false,
                PostName = true,
                TimeBegin = true,
                TimeEnd = true,
                SetPoint = true,
                ResultMain = true,
                UnitMain = true,
                ResultSecondary = true,
                UnitSecondary = true,
                Density = true,
                Temperature = true,
                ProductName = true,
                TankName = true,
                FinalStatus = true
            };
            var expected = new Config()
            {
                Server = "ARM//SQLEXPRESS",
                DbName = "ShipmentDb",
                DbType = "sqlexpress",
                UserName = "asn_user",
                Password = "asn_user",
                Columns = columns
            };

            // Act
            var config = CommonTools.GetConfig(fileName);

            // Assert
            Assert.That(config.Columns, Is.EqualTo(expected.Columns));
        }

        /// <summary>
        /// Проверяем правильность считывания конфигурации подключения к БД из файла
        /// </summary>
        [Test]
        public void GetConfig_getConfigFromJsonFile()
        {

            // Arrange
            var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ConfigArm.json");

            var columns = new Columns()
            {
                Id = false,
                Ts = false,
                PostName = true,
                TimeBegin = true,
                TimeEnd = true,
                SetPoint = true,
                ResultMain = true,
                UnitMain = true,
                ResultSecondary = true,
                UnitSecondary = true,
                Density = true,
                Temperature = true,
                ProductName = true,
                TankName = true,
                FinalStatus = true
            };
            var expected = new Config()
            {
                Server = "ARM\\SQLEXPRESS",
                DbName = "ShipmentDb",
                DbType = "sqlexpress",
                UserName = "asn_user",
                Password = "asn_user",
                Columns = columns
            };

            // Act
            var config = CommonTools.GetConfig(fileName);
            var result = config.Server == expected.Server
                && config.DbName == expected.DbName
                && config.DbType == expected.DbType
                && config.UserName == expected.UserName
                && config.Password == expected.Password
                ;

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
