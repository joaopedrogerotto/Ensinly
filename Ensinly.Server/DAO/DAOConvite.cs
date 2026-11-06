using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ensinly.Server.DAO {
    public class DAOConvite : IDAOConvite {
        private readonly IDAODatabase _database;

        public DAOConvite(IDAODatabase database) {
            _database = database;
        }

        public async Task<bool> InsertConvite(Convite convite) {
            try {
                using (var conn = await _database.OpenConnection()) {
                    using (var cmd = new SqlCommand("PR_I_CONVITE_ALUNO_PROFESSOR_VINCULO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TOKEN", convite.TokenHash);
                        cmd.Parameters.AddWithValue("@ID_PROFESSOR", convite.Professor.Id);
                        cmd.Parameters.AddWithValue("@ID_ALUNO", convite.Aluno.Id);
                        cmd.Parameters.AddWithValue("@DT_EXPERICAO", convite.DataExpiracao);
                        cmd.Parameters.AddWithValue("@DT_CRIACAO", convite.DataCriacao);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            } catch (Exception ex) {
                throw new Exception($"Erro ao inserir convite: {ex.Message}", ex);
            }           
        }

        public async Task<string> SelectVinculoAlunoProfessor(int idAluno, int idProfessor) {
            string vinculo = "";

            using (var conn = await _database.OpenConnection()) {
                using (var cmd = new SqlCommand("PR_S_VERIFICA_VINCULO", conn)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_ALUNO", idAluno);
                    cmd.Parameters.AddWithValue("@ID_PROFESSOR", idProfessor);
                    vinculo = await cmd.ExecuteScalarAsync() as string;
                }
            }

            return vinculo;
        }
    }
}
