using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOAluno {
        public Task<bool> InsertAluno(Aluno aluno);
    }
}
