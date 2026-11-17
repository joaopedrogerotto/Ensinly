using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOToken {
        public Task<Usuario> ValidateToken(string Token);
    }
}
