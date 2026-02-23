using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace vesalius_m.Utils
{
    public class DefaultConnection
    {
        private readonly string connectionString;

        public DefaultConnection(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection") ?? "";
        }

        public IDbConnection CreateConnection()
        {
            var con = new OracleConnection(connectionString);
            con.Open();
            return con;
        }
    }
}
