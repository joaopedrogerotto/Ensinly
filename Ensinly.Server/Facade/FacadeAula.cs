using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Mapper;
using Ensinly.Server.Service;

namespace Ensinly.Server.Facade {
    public class FacadeAula : IFacadeAula {
        private readonly IDAOAula _daoAula;
        private readonly IDAOProfessor _daoProfessor;
        private readonly AulaService _aulaService;

        public FacadeAula(IDAOAula daoAula, IDAOProfessor daoProfessor, AulaService aulaService) {
            _daoAula = daoAula;
            _daoProfessor = daoProfessor;
            _aulaService = aulaService;
        }

        public async Task<bool> AgendarAula(AulaRequestDTO aulaRequestDTO, int idUsuarioProfessor) {
            var aula = AulaMapper.ToEntidade(aulaRequestDTO);
            _aulaService.ValidateDate(aula);
            aula.Professor = await _daoProfessor.SelectProfessorByIdUsuario(idUsuarioProfessor);
            return await _daoAula.InsertAula(aula);
        }
    }
}
