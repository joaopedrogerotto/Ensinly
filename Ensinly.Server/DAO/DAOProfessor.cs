using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ensinly.Server.DAO {
    public class DAOProfessor : IDAOProfessor {
        private readonly IDAODatabase _database;

        public DAOProfessor(IDAODatabase database) {
            _database = database;
        }

        public async Task<bool> InsertProfessor(ProfessorCreateDTO ProfessorCreateDTO) {
            try {
                using (var conn = await _database.OpenConnection()) {
                    using (var cmd = new SqlCommand("PR_I_PROFESSOR", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOME", ProfessorCreateDTO.Nome);
                        cmd.Parameters.AddWithValue("@EMAIL", ProfessorCreateDTO.Email);
                        cmd.Parameters.AddWithValue("@SENHA", ProfessorCreateDTO.Senha);
                        cmd.Parameters.AddWithValue("@DT_CADASTRO", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DESCRICAO", ProfessorCreateDTO.Descricao);
                        cmd.Parameters.AddWithValue("@ARRAY_MATERIAS", ConvertForDataTable(ProfessorCreateDTO.IdMaterias));

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            } catch (Exception ex) {
                throw new Exception($"Erro ao inserir professor: {ex.Message}");
            }
        }

        private DataTable ConvertForDataTable (List<int> IdMaterias) {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            
            foreach(var id in IdMaterias) {
                table.Rows.Add(id);
            }

            return table;
        }
    }
}
