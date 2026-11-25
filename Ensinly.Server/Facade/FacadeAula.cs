using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Exceptions;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Mapper;
using Ensinly.Server.Models;
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

        public async Task<List<AulaResponseDTO>> SelecionarAulasDoProfessor(int idUsuarioProfessor) {
            var professor = await _daoProfessor.SelectProfessorByIdUsuario(idUsuarioProfessor);

            if(professor == null) {
                throw new EntityNotFoundException("Professor não encontrado");
            }

            return await _daoAula.SelectAulasByProfessor(professor);
        }
    }
}
