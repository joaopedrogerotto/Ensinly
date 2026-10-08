using Ensinly.Server.DAO.Interfaces;
using Microsoft.Data.SqlClient;

namespace Ensinly.Server.DAO {
    public class DAOSQLServer : IDAODatabase{
        private string _strConexao { get; set; }
        public DAOSQLServer(IConfiguration configuration) {
            _strConexao = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<SqlConnection> OpenConnection() {
            var conn = new SqlConnection(_strConexao);
            await conn.OpenAsync();
            return conn;
        }

        public void CloseConnection(SqlConnection connection) {
            connection.Close();
        }
    }
}
