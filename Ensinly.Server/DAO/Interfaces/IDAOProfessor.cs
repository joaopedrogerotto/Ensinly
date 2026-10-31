using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOProfessor {
        public Task<bool> InsertProfessor(ProfessorCreateDTO ProfessorCreateDTO);
        public Task<List<AlunoResponseDTO>> SelectAlunosByProfessor(int idUsuario);
    }
}
