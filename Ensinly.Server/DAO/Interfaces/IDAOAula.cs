using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOAula {
        public Task<bool> InsertAula(Aula Aula);
        public Task<List<AulaResponseDTO>> SelectAulasByProfessor(Professor Professor);

    }
}
