using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeAluno {
        public Task<bool> CadastrarAluno(AlunoCreateDTO alunoCreateDTO);
    }
}
