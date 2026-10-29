using Ensinly.Server.Models;

namespace Ensinly.Server.DAO.Interfaces {
    public interface IDAOMateria {
        public Task<List<Materia>> SelectAll();
    }
}
