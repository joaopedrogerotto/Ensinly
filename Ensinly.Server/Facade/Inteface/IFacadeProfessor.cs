using Ensinly.Server.DTO;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeProfessor {
        public Task<bool> CadastrarProfessor(ProfessorCreateDTO ProfessorCreateDTO);
    }
}
