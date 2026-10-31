using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Enums;
using Ensinly.Server.Models;
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

        public async Task<List<AlunoResponseDTO>> SelectAlunosByProfessor(int idUsuario) {
            var list = new List<AlunoResponseDTO>();

            try {
                using (var conn = await _database.OpenConnection()) {
                    using (var cmd = new SqlCommand("PR_S_ALUNOS_POR_PROF", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_USU", idUsuario);
                        using (var reader = await cmd.ExecuteReaderAsync()) {
                            while (await reader.ReadAsync()) {
                                var aluno = new AlunoResponseDTO {
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("ALU_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("USU_NOME")),
                                    Email = reader.GetString(reader.GetOrdinal("USU_EMAIL")),
                                    DataNascimento = reader.GetDateTime(reader.GetOrdinal("ALU_DT_NASCIMENTO")),
                                    Observacao = reader.IsDBNull(reader.GetOrdinal("ALU_OBSERVACAO")) ? null : reader.GetString(reader.GetOrdinal("ALU_OBSERVACAO")),
                                    Telefone = reader.IsDBNull(reader.GetOrdinal("ALU_TELEFONE")) ? null : reader.GetString(reader.GetOrdinal("ALU_TELEFONE")),
                                    NivelAprendizado = ((NivelAprendizadoEnum)reader.GetInt32(reader.GetOrdinal("NVA_ID"))).ToString(),
                                    Status = reader.GetBoolean(reader.GetOrdinal("USU_STATUS"))
                                };
                                list.Add(aluno);
                            }
                        }
                    }
                }
                return list;
            } catch (Exception ex) {
                throw new Exception($"Erro ao selecionar alunos por professor: {ex.Message}");
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
