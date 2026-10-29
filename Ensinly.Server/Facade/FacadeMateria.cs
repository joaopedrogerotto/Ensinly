using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade {
    public class FacadeMateria : IFacadeMateria {
        private readonly IDAOMateria _daoMateria;

        public FacadeMateria(IDAOMateria daoMateria) {
            _daoMateria = daoMateria;
        }

        public async Task<List<Materia>> SelecionaTodas() => await _daoMateria.SelectAll();
    }
}
