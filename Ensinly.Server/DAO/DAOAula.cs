using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Exceptions;
using Ensinly.Server.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ensinly.Server.DAO {
    public class DAOAula : IDAOAula {
        private readonly IDAODatabase _database;

        public DAOAula(IDAODatabase database) {
            _database = database;
        }

        public async Task<bool> InsertAula(Aula Aula) {
            try {
                using (var conn = await _database.OpenConnection()) {
                    using (var cmd = new SqlCommand("PR_I_AULA", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_ALUNO", Aula.Aluno.Id);
                        cmd.Parameters.AddWithValue("@ID_PROF", Aula.Professor.Id);
                        cmd.Parameters.AddWithValue("@DT_HR_INICIO", Aula.DataHoraInicio);
                        cmd.Parameters.AddWithValue("@DT_HR_FIM", Aula.DataHoraFim);

                        if (!string.IsNullOrEmpty(Aula.Observacao)) {
                            cmd.Parameters.AddWithValue("@OBSERVACAO", Aula.Observacao);
                        }

                        await cmd.ExecuteNonQueryAsync();

                        return true;
                    }
                }
            }catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627) {
                throw new ClassAlreadyRegisteredException("O aluno já possui uma aula cadastrada nesse horário.");
            
            }catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AulaResponseDTO>> SelectAulasByProfessor(Professor Professor) {
            var aulas = new List<AulaResponseDTO>();

            using (var conn = await _database.OpenConnection()) {
                using (var cmd = new SqlCommand("PR_S_AULAS_POR_PROFESSOR", conn)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_PROF", Professor.Id);
                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            aulas.Add(
                                new AulaResponseDTO {
                                    Aluno = new AlunoResponseAulaDTO {
                                        Id = reader.GetInt32(reader.GetOrdinal("ID_ALUNO")),
                                        Nome = reader.GetString(reader.GetOrdinal("NOME"))
                                    },
                                    Status = reader.GetString(reader.GetOrdinal("STATUS")),
                                    Observacao = reader.IsDBNull(reader.GetOrdinal("OBSERVACAO")) ? string.Empty : reader.GetString(reader.GetOrdinal("OBSERVACAO")),
                                    DataHoraInicio = reader.GetDateTime(reader.GetOrdinal("DT_HR_INICIO")),
                                    DataHoraFim = reader.GetDateTime(reader.GetOrdinal("DT_HR_FIM")),
                                    Professor = Professor
                                }
                            );
                        }
                    }
                }
            }
            return aulas;
        }
    }
}
