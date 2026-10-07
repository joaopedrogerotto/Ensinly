using Ensinly.Server.DAO.Interfaces;
using Microsoft.Data.SqlClient;

namespace Ensinly.Server.DAO {
    public class DAOSQLServer : IDAODatabase{
        private string _strConexao { get; set; }
        public DAOSQLServer(IConfiguration configuration) {
            _strConexao = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection OpenConnection() {
            var conn = new SqlConnection(_strConexao);
            conn.Open();
            return conn;
        }

        public void CloseConnection(SqlConnection connection) {
            connection.Close();
        }
    }
}
