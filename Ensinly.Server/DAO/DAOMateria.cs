using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ensinly.Server.DAO {
    public class DAOMateria : IDAOMateria {
        private readonly IDAODatabase _database;

        public DAOMateria(IDAODatabase database) {
            _database = database;
        }

        public async Task<List<Materia>> SelectAll() {
            var list = new List<Materia>();

            try {
                using (var conn = await _database.OpenConnection()) {
                    using (var cmd = new SqlCommand("PR_S_TODAS_MATERIAS", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync()) {
                            while (await reader.ReadAsync()) {
                                list.Add(new Materia {
                                    Id = reader.GetInt32("MAT_ID"),
                                    Nome = reader.GetString("MAT_NOME")
                                });
                            }
                        }
                    }
                }
                return list;
            } catch (Exception ex) {
                throw new Exception($"Erro ao buscar todas as matérias: {ex.Message}");
            }
        }
    }
}
