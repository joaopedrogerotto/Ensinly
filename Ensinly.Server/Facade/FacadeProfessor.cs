using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;

namespace Ensinly.Server.Facade {
    public class FacadeProfessor : IFacadeProfessor {
        private readonly IDAOProfessor _daoProfessor;

        public FacadeProfessor(IDAOProfessor daoProfessor) {
            _daoProfessor = daoProfessor;
        }

        public async Task<bool> CadastrarProfessor(ProfessorCreateDTO ProfessorCreateDTO) =>await _daoProfessor.InsertProfessor(ProfessorCreateDTO);
    }
}
