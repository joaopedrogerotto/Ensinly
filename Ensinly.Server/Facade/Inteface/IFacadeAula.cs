using Ensinly.Server.DTO;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeAula {
        public Task<bool> AgendarAula(AulaRequestDTO aulaRequestDTO, int idUsuarioProfessor);
    }
}
