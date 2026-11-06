using Ensinly.Server.DAO;
using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Mapper;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade {
    public class FacadeAluno : IFacadeAluno {
        private readonly IDAOAluno _daoAluno;
        private readonly IDAOProfessor _daoProfessor;
        private readonly IDAOConvite _daoConvite;

        public FacadeAluno(IDAOAluno daoAluno, IDAOProfessor daoProfessor, IDAOConvite daoConvite) {
            _daoAluno = daoAluno;
            _daoProfessor = daoProfessor;
            _daoConvite = daoConvite;
        }


        public async Task<bool> CadastrarAluno(AlunoCreateDTO alunoCreateDTO) {
            var aluno = AlunoMapper.ToEntidade(alunoCreateDTO);
            return await _daoAluno.InsertAluno(aluno);
        }

        public async Task<AlunoResponseDTO> SelecionarPorEmail(string email) => await _daoAluno.SelectByEmail(email);

        public async Task<AlunoVinculoResponseDTO> SelecionarAlunoParaVinculo(AlunoEmailRequestDTO alunoEmailRequestDTO) {
            var aluno = await _daoAluno.SelectByEmail(alunoEmailRequestDTO.email);
            var professor = await _daoProfessor.SelectProfessorByIdUsuario(alunoEmailRequestDTO.idProfessorUsuario);
            var vinculoStatus = await _daoConvite.SelectVinculoAlunoProfessor(aluno.Id, professor.Id);


            return AlunoMapper.ToVinculoDTO(aluno, vinculoStatus);
        }
    }
}
