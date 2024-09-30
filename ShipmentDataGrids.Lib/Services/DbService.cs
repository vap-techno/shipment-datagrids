using Dapper;
using ShipmentDataGrids.Lib.Common;
using ShipmentDataGrids.Lib.Interfaces;
using ShipmentDataGrids.Lib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ShipmentDataGrids.Lib.Services
{
    public class DbService
    {
        #region Fields
        private readonly string _sqlQuery;
        private readonly IConfig _config;

        #endregion

        #region Constructors
        public DbService(IConfig cfg)
        {
            _sqlQuery = CommonTools.SqlQueryAll;
            _config = cfg;
        }

        #endregion

         #region Methods
        /// <summary>
        /// Возвращает все отгрузки
        /// </summary>
        /// <returns></returns>
        public List<IShipment> GetShipments() 
        {

            using (var connection = CommonTools.GetDbConnection(_config))
            {
                var lst = connection.QueryAsync<Shipment>(_sqlQuery).Result.ToList();
                return new List<IShipment>(lst.Cast<IShipment>());
            }
        }

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

        #region Async Methods
        /// <summary>
        /// Возвращает все отгрузки
        /// </summary>
        /// <returns></returns>
        public async Task<List<IShipment>> GetShipmentsAsync()
        {

            using(var connection = CommonTools.GetDbConnection(_config)) 
            { 
                var lst = await connection.QueryAsync<Shipment>(_sqlQuery);
                return new List<IShipment>(lst.Cast<IShipment>());
            }
        }

        /// <summary>
        /// Возвращает отгрузки за сутки
        /// </summary>
        /// <returns></returns>
        public async Task<List<IShipment>> GetShipmentsLastDayAsync()
        {

            var lst = await GetShipmentsAsync();
            var res = from s in lst
                      where s.TimeBegin > DateTime.Now.AddDays(-1)
                      select s;

            return res.ToList();
        }

        /// <summary>
        /// Возвращает отгрузки за месяц
        /// </summary>
        /// <returns></returns>
        public async Task<List<IShipment>> GetShipmentsLastMonthAsync()
        {
            var lst = await GetShipmentsAsync();
            var res = from s in lst
                      where s.TimeBegin > DateTime.Now.AddMonths(-1)
                      select s;

            return res.ToList();
        }

        /// <summary>
        /// Возвращает отгрузки за неделю
        /// </summary>
        /// <returns></returns>
        public async Task<List<IShipment>> GetShipmentsLastWeekAsync()
        {
            var lst = await GetShipmentsAsync();
            var res = from s in lst
                      where s.TimeBegin > DateTime.Now.AddDays(-7)
                      select s;

            return res.ToList();
        }

        /// <summary>
        /// Возвращает отгрузки за год
        /// </summary>
        /// <returns></returns>
        public async Task<List<IShipment>> GetShipmentsLastYearAsync()
        {

            var lst = await GetShipmentsAsync();

            var res = (from s in lst
                       where s.TimeBegin > DateTime.Now.AddYears(-1)
                       select s);

            return res.ToList();
        }

        /// <summary>
        /// Возвращает отгрузки за определенный период
        /// </summary>
        /// <param name="begin"> Начало выборки </param>
        /// <param name="end"> Конец выборки </param>
        /// <returns></returns>
        public async Task<List<IShipment>> GetShipmentsInRangeAsync(DateTime begin, DateTime end)
        {
            var lst = await GetShipmentsAsync();
            var res = from s in lst
                      where s.TimeBegin >= begin && s.TimeBegin <= end
                      select s;

            return res.ToList();
        }

        #endregion



    }
}
