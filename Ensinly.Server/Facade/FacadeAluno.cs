using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Mapper;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade {
    public class FacadeAluno : IFacadeAluno {
        private readonly IDAOAluno _daoAluno;

        public FacadeAluno(IDAOAluno daoAluno) {
            _daoAluno = daoAluno;
        }

        public async Task<bool> CadastrarAluno(AlunoCreateDTO alunoCreateDTO) {
            var aluno = AlunoMapper.ToEntidade(alunoCreateDTO);
            return await _daoAluno.InsertAluno(aluno);
        }
    }
}
