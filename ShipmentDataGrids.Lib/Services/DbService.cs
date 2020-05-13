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
        public IEnumerable<IShipment> GetShipments() => _dbConnection.Query<IShipment>(_sqlQuery)
                                                        .ToList();
        #endregion


    }
}
