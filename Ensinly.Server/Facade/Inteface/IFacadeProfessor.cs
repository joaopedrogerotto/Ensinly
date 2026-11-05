using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeProfessor {
        public Task<bool> CadastrarProfessor(ProfessorCreateDTO ProfessorCreateDTO);
        public Task<List<AlunoResponseDTO>> SelecionaAlunosPorProfessor(int idUsuario);
        public Task<int> SelecionaQuantidadeDeAlunosPorProfessor(int idUsuario);
    }
}
