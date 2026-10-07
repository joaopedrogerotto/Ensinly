using Ensinly.Server.Models;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeUsuario {
        public Task<bool> CadastrarUsuario(Usuario usuario);
    }
}
