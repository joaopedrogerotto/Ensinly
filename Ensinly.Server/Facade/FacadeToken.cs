using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade {
    public class FacadeToken : IFacadeToken {
        private readonly IDAOToken _daoToken;

        public FacadeToken(IDAOToken daoToken) {
            _daoToken = daoToken;
        }

        public async Task<Usuario> ValidarToken(string Token) => await _daoToken.ValidateToken(Token);
    }
}
