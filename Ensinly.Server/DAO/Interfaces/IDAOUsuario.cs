using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOUsuario {
        public Task<bool> InsertUsuario (Usuario usuario);
    }
}
