using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ensinly.Server.DAO {
    public class DAONivelAprendizado : IDAONivelAprendizado {
        private readonly IDAODatabase _database;

        public DAONivelAprendizado(IDAODatabase database) {
            _database = database;
        }

        public async Task<List<NivelAprendizado>> SelectAll() {
            var list = new List<NivelAprendizado>();

            try {
                using (var conn = await _database.OpenConnection()) {
                    using (var cmd = new SqlCommand("PR_S_TODOS_NIVEIS_APRENDIZADO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync()) {
                            while (await reader.ReadAsync()) {
                                list.Add(new NivelAprendizado {
                                    Id = reader.GetInt32(reader.GetOrdinal("NVA_ID")),
                                    Nivel = reader.GetString(reader.GetOrdinal("NVA_NIVEL_APRENDIZADO"))
                                });
                            }
                        }
                    }
                }
                return list;
            } catch (Exception ex) {
                throw new Exception($"Erro ao selecionar todos os níveis de aprendizado: {ex.Message}");
            }
        }
    }
}
