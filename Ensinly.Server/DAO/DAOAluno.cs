using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Enums;
using Ensinly.Server.Exceptions;
using Ensinly.Server.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ensinly.Server.DAO {
    public class DAOAluno : IDAOAluno {
        private readonly IDAODatabase _database;

        public DAOAluno(IDAODatabase database) {
            _database = database;
        }

        public async Task<bool> InsertAluno(Aluno aluno) {
            try {
                using (SqlConnection conn = await _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_I_ALUNO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOME", aluno.Nome);
                        cmd.Parameters.AddWithValue("@EMAIL", aluno.Email);
                        cmd.Parameters.AddWithValue("@SENHA", aluno.Senha);
                        cmd.Parameters.AddWithValue("@DT_CADASTRO", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DT_NASCIMENTO", aluno.DataNascimento);
                        cmd.Parameters.AddWithValue("@TELEFONE", aluno.Telefone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@OBSERVACAO", aluno.Observacao ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ID_NIVEL_APRENDIZADO", aluno.NivelAprendizado);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }

            }catch (SqlException sqlEx) when (sqlEx.Number == 2601 || sqlEx.Number == 2627) {
                throw new EntityAlreadyExistsException($"Aluno já existe: {sqlEx.Message}");
            } catch (Exception ex) {
                throw new Exception($"Erro ao inserir aluno: {ex.Message}", ex);
            }
        }

        public async Task<Aluno> SelectAlunoByIdUsuario(int idUsuario) {
            var aluno = new Aluno();

            try {
                using (var conn = await _database.OpenConnection()) {
                    using (var cmd = new SqlCommand("PR_S_ALUNO_POR_ID_USUARIO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_USU", idUsuario);
                        using (var reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                aluno.Id = reader.GetInt32(reader.GetOrdinal("ALU_ID"));
                                aluno.Nome = reader.GetString(reader.GetOrdinal("USU_NOME"));
                                aluno.Email = reader.GetString(reader.GetOrdinal("USU_EMAIL"));
                                aluno.Status = reader.GetBoolean(reader.GetOrdinal("USU_STATUS"));
                            } else {
                                throw new EntityNotFoundException($"Aluno com ID de usuário '{idUsuario}' não encontrado.");
                            }
                        }
                    }
                }
                return aluno;
            } catch (Exception ex) {
                throw new Exception($"Erro ao selecionar aluno por ID de usuário: {ex.Message}", ex);
            }
        }

        public async Task<AlunoResponseDTO> SelectByEmail(string email) {
            try {
                var alunoResponse = new AlunoResponseDTO();
                using (SqlConnection conn = await _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_ALUNO_POR_EMAIL", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EMAIL", email);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
                            if (await reader.ReadAsync()) {
                                alunoResponse.Id = reader.GetInt32(reader.GetOrdinal("ALU_ID"));
                                alunoResponse.Nome = reader.GetString(reader.GetOrdinal("USU_NOME"));
                                alunoResponse.Email = reader.GetString(reader.GetOrdinal("USU_EMAIL"));
                                alunoResponse.Observacao = reader.IsDBNull(reader.GetOrdinal("ALU_OBSERVACAO")) ? null : reader.GetString(reader.GetOrdinal("ALU_OBSERVACAO"));
                                alunoResponse.Telefone = reader.IsDBNull(reader.GetOrdinal("ALU_TELEFONE")) ? null : reader.GetString(reader.GetOrdinal("ALU_TELEFONE"));
                                alunoResponse.DataNascimento = reader.GetDateTime(reader.GetOrdinal("ALU_DT_NASCIMENTO"));
                                alunoResponse.NivelAprendizado = ((NivelAprendizadoEnum)reader.GetInt32(reader.GetOrdinal("NVA_ID"))).ToString();
                                alunoResponse.Status = reader.GetBoolean(reader.GetOrdinal("USU_STATUS"));
                            } else {
                                throw new EntityNotFoundException($"Aluno com email '{email}' não encontrado.");
                            }
                        }
                    }
                }
                return alunoResponse;
            } catch (Exception ex){
                throw new Exception($"Erro ao selecionar aluno por email: {ex.Message}", ex);
            }
        }

        public async Task<Aluno> SelectByIdAluno(int idAluno) {
            var aluno = new Aluno();

            try {
                using (var conn = await _database.OpenConnection()) {
                    using (var cmd = new SqlCommand("PR_S_ALUNO_POR_ID_ALUNO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_ALUNO", idAluno);
                        using (var reader = await cmd.ExecuteReaderAsync()) {
                            if (await reader.ReadAsync()) {
                                aluno.Id = reader.GetInt32(reader.GetOrdinal("ALU_ID"));
                                aluno.Nome = reader.GetString(reader.GetOrdinal("USU_NOME"));
                                aluno.Email = reader.GetString(reader.GetOrdinal("USU_EMAIL"));
                                aluno.Observacao = reader.IsDBNull(reader.GetOrdinal("ALU_OBSERVACAO")) ? null : reader.GetString(reader.GetOrdinal("ALU_OBSERVACAO"));
                                aluno.Telefone = reader.IsDBNull(reader.GetOrdinal("ALU_TELEFONE")) ? null : reader.GetString(reader.GetOrdinal("ALU_TELEFONE"));
                                aluno.DataNascimento = reader.GetDateTime(reader.GetOrdinal("ALU_DT_NASCIMENTO"));
                                aluno.NivelAprendizado = ((NivelAprendizadoEnum)reader.GetInt32(reader.GetOrdinal("NVA_ID")));
                                aluno.Status = reader.GetBoolean(reader.GetOrdinal("USU_STATUS"));
                            } else {
                                throw new EntityNotFoundException($"Aluno com id  '{idAluno}' não encontrado.");
                            }
                        }
                    }
                }
                return aluno;
            } catch (Exception ex) {
                throw new Exception($"Erro ao selecionar aluno por ID de usuário: {ex.Message}", ex);
            }
        }
    }
}
