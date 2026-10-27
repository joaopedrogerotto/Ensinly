using Ensinly.Server.Models;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeNivelAprendizado {
        public Task<List<NivelAprendizado>> SelecionarTodos();
    }
}
