using Dapper;
using ShipmentDataGrids.Lib.Common;
using ShipmentDataGrids.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDataGrids.Lib.Services
{
    class DbService
    {
        #region Fields
        private readonly IDbConnection _dbConnection;
        private readonly string _sqlQuery;

        #endregion

        #region Constructors
        public DbService(IConfig cfg)
        {
            // TODO: VAP; Создать dbConnection для каждой базы данных

            _sqlQuery = CommonTools.SqlQueryAll;
        }

        #endregion

        #region Methods
        public List<IShipment> GetShipments() => _dbConnection.Query<IShipment>(_sqlQuery)
                                                        .ToList();

        /// <summary>
        /// Возвращает отгрузки за сутки
        /// </summary>
        /// <returns></returns>
        public List<IShipment> GetShipmentsLastDay()
        {

            var res = from s in GetShipments()
                      where s.TimeBegin > DateTime.Now.AddDays(-1)
                      select s;

            return res.ToList();
        }

        /// <summary>
        /// Возвращает отгрузки за месяц
        /// </summary>
        /// <returns></returns>
        public List<IShipment> GetShipmentsLastMonth()
        {
            var res = from s in GetShipments()
                      where s.TimeBegin > DateTime.Now.AddMonths(-1)
                      select s;

            return res.ToList();
        }

        /// <summary>
        /// Возвращает отгрузки за неделю
        /// </summary>
        /// <returns></returns>
        public List<IShipment> GetShipmentsLastWeek()
        {
            var res = from s in GetShipments()
                      where s.TimeBegin > DateTime.Now.AddDays(-7)
                      select s;

            return res.ToList();
        }

        /// <summary>
        /// Возвращает отгрузки за год
        /// </summary>
        /// <returns></returns>
        public List<IShipment> GetShipmentsLastYear()
        {
            var res = from s in GetShipments()
                      where s.TimeBegin > DateTime.Now.AddYears(-1)
                      select s;

            return res.ToList();
        }

        /// <summary>
        /// Возвращает отгрузки за определенный период
        /// </summary>
        /// <param name="begin"> Начало выборки </param>
        /// <param name="end"> Конец выборки </param>
        /// <returns></returns>
        public List<IShipment> GetShipmentsInRange(DateTime begin, DateTime end)
        {
            var res = from s in GetShipments()
                      where s.TimeBegin >= begin && s.TimeBegin <= end
                      select s;

            return res.ToList();
        }

        #endregion

    }
}
