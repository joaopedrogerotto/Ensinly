using Ensinly.Server.DAO.Interfaces;
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
    }
}
