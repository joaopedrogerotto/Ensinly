using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOAluno {
        public Task<bool> InsertAluno(Aluno aluno);
        public Task<AlunoResponseDTO> SelectByEmail(string email);
        public Task<Aluno> SelectAlunoByIdUsuario(int idUsuario);
        public Task<Aluno> SelectByIdAluno(int idAluno);
    }
}
