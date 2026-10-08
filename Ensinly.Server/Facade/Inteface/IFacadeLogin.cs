using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeLogin {
        public Task<Usuario> ValidarLogin(LoginDTO login);
    }
}
