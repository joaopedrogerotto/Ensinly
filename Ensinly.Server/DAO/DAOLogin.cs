using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ensinly.Server.DAO {
    public class DAOLogin : IDAOLogin {
        private readonly IDAODatabase _database;

        public DAOLogin(IDAODatabase database) {
            _database = database;
        }

        public async Task<Usuario> SelectLogin(LoginDTO login) {
            var usuario = new Usuario();
            using (var conn = await _database.OpenConnection()) {
                using (var cmd = new SqlCommand("PR_S_VALIDAR_LOGIN", conn)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMAIL", login.Email);
                    cmd.Parameters.AddWithValue("@SENHA", login.Senha);

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        while(await reader.ReadAsync()) {
                            usuario.Id = reader.GetInt32(reader.GetOrdinal("USU_ID"));
                            usuario.Nome = reader.GetString(reader.GetOrdinal("USU_NOME"));
                            usuario.Email = reader.GetString(reader.GetOrdinal("USU_EMAIL"));
                            usuario.TipoUsuario = reader.GetString(reader.GetOrdinal("TIPO_USUARIO"));
                        }   
                    }
                }
                return usuario;
            }
            throw new InvalidOperationException("Email e/ou senha inválido");
        }
    }
}
