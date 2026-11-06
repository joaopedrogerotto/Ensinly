using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOConvite {
        public Task<bool> InsertConvite(Convite convite);
        public Task<string> SelectVinculoAlunoProfessor(int idAluno, int idProfessor);
    }
}
