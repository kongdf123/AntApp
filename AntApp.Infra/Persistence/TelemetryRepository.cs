using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntApp.Infra.Persistence
{
    public class TelemetryRepository
    {
        private readonly IDbConnectionFactory _factory;

        public TelemetryRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task SaveTelemetryAsync(Domain.Entities.Telemetry telemetry)
        {
            using (var db = _factory.CreateConnection())
            {
                string sql = @"INSERT INTO telemetry(timestamp, temperature, pressure)
                       VALUES(@Timestamp, @Temperature, @Pressure)";

                await db.ExecuteAsync(sql,telemetry);
            }
        }

        public async Task ExecuteInTransaction(Func<IDbConnection, IDbTransaction, Task> action)
        {
            using var conn = _factory.CreateConnection();
            conn.Open();

            using var tran = conn.BeginTransaction();

            try
            {
                await action(conn, tran);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
