using Ensinly.Server.DTO;
using Ensinly.Server.Enums;
using Ensinly.Server.Models;

namespace Ensinly.Server.Mapper {
    public class AlunoMapper {
        public static Aluno ToEntidade(AlunoCreateDTO dto) {
            return new Aluno {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                Observacao = dto.Observacao,
                Telefone = dto.Telefone,
                DataNascimento = dto.DataNascimento,
                NivelAprendizado = (NivelAprendizadoEnum)dto.IdNivelAprendizado
            };
        }
    }
}
