using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade {
    public class FacadeLogin : IFacadeLogin {
        private readonly IDAOLogin _daoLogin;

        public FacadeLogin(IDAOLogin daoLogin) {
            _daoLogin = daoLogin;
        }

        public async Task<Usuario> ValidarLogin(LoginDTO login) {
            return await _daoLogin.SelectLogin(login);
        }
    }
}
