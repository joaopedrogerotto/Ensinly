using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeAula {
        public Task<bool> AgendarAula(AulaRequestDTO aulaRequestDTO, int idUsuarioProfessor);
        public Task<List<AulaResponseDTO>> SelecionarAulasDoProfessor(int idUsuarioProfessor);
    }
}
