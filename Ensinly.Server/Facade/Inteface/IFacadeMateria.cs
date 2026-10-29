using Ensinly.Server.Models;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeMateria {
        public Task<List<Materia>> SelecionaTodas();
    }
}
