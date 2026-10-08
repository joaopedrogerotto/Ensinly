using Microsoft.Data.SqlClient;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAODatabase {
        public Task<SqlConnection> OpenConnection();
        public void CloseConnection(SqlConnection connection);
    }
}
