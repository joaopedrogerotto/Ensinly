using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Exceptions;
using Ensinly.Server.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ensinly.Server.DAO {
    public class DAOToken : IDAOToken {
        private readonly IDAODatabase _database;

        public DAOToken(IDAODatabase database) {
            _database = database;
        }

        public async Task<Usuario> ValidateToken(string Token) {
            var usuario = new Usuario();

            using (var conn = await _database.OpenConnection()) {
                try {
                    using (var cmd = new SqlCommand("PR_S_VALIDA_TOKEN", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TOKEN_HASH", Token);

                        using (var reader = await cmd.ExecuteReaderAsync()) {
                            if (await reader.ReadAsync()) {
                                usuario.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                                usuario.Nome = reader.GetString(reader.GetOrdinal("NOME"));
                                usuario.Email = reader.GetString(reader.GetOrdinal("EMAIL"));
                                usuario.TipoUsuario = reader.GetString(reader.GetOrdinal("TIPO_USUARIO"));
                            } else {
                                throw new InvalidOrExpiredTokenException("Token inválido ou expirado.");
                            }
                        }
                    }
                    return usuario;

                } catch (Exception ex) {
                    throw new Exception($"Erro ao validar token: {ex.Message}");
                }
            }
        }
    }
}
