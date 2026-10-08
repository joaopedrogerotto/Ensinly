using Ensinly.Server.DAO.Interfaces;
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

            } catch (Exception ex) {
                throw new Exception($"Erro ao inserir aluno: {ex.Message}", ex);
            }
        }
    }
}
