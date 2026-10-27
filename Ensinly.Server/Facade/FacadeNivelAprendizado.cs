using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade {
    public class FacadeNivelAprendizado : IFacadeNivelAprendizado {
        private readonly IDAONivelAprendizado _daoNivelAprendizado;

        public FacadeNivelAprendizado(IDAONivelAprendizado daoNivelAprendizado) {
            _daoNivelAprendizado = daoNivelAprendizado;
        }

        public async Task<List<NivelAprendizado>> SelecionarTodos() => await _daoNivelAprendizado.SelectAll();
    }
}
