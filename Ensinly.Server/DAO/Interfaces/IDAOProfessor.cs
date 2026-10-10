using Ensinly.Server.DTO;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOProfessor {
        public Task<bool> InsertProfessor(ProfessorCreateDTO ProfessorCreateDTO);
    }
}
