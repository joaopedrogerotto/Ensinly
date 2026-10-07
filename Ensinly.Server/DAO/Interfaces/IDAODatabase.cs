using Microsoft.Data.SqlClient;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAODatabase {
        public SqlConnection OpenConnection();
        public void CloseConnection(SqlConnection connection);
    }
}
