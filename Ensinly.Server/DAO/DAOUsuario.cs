using Ensinly.Server.DAO.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ensinly.Server.DAO {
    public class DAOUsuario : IDAOUsuario {
        private readonly IDAODatabase _database;
        public DAOUsuario(IDAODatabase database) {
            _database = database;
        }
        public async Task<bool> InsertUsuario(Models.Usuario usuario) {
            try {
                using (var conn = _database.OpenConnection()) {
                    using (var command = new SqlCommand("PR_I_USUARIO", conn)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NOME", usuario.Nome);
                        command.Parameters.AddWithValue("@EMAIL", usuario.Email);
                        command.Parameters.AddWithValue("@SENHA", usuario.Senha);
                        command.Parameters.AddWithValue("@DT_CADASTRO", DateTime.Now);
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }

            } catch (Exception ex) {
                throw new Exception($"Erro ao inserir usuário: {ex.Message}");
            }
        }
    }
}
