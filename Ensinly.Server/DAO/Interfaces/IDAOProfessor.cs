using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOProfessor {
        public Task<bool> InsertProfessor(ProfessorCreateDTO ProfessorCreateDTO);
        public Task<List<AlunoResponseDTO>> SelectAlunosByProfessor(int idUsuario);
        public Task<int> SelectAmountOfAlunosByProfessor(int idUsuario);
        public Task<MaiorNivelAprendizadoPorProfessorDTO> SelectMaiorNivelAprendizadoPorProfessor(int IdUsuario);
        public Task<Professor> SelectProfessorByIdUsuario (int idUsuario);
    }
}
