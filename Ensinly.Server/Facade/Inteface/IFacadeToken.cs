using Ensinly.Server.Models;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeToken {
        public Task<Usuario> ValidarToken(string Token);
    }
}
