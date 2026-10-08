using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOLogin {
        public Task<Usuario> SelectLogin(LoginDTO login);
    }
}
