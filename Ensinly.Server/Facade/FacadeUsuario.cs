using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade {
    public class FacadeUsuario : IFacadeUsuario {
        private readonly IDAOUsuario _daoUsuario;
        public FacadeUsuario(IDAOUsuario daoUsuario) { 
            _daoUsuario = daoUsuario;
        }
        public Task<bool> CadastrarUsuario(Usuario usuario) => _daoUsuario.InsertUsuario(usuario);
    }
}
