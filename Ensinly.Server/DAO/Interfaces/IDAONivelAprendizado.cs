using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAONivelAprendizado {
        public Task<List<NivelAprendizado>> SelectAll();

    }
}
