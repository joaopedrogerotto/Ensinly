using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.Mapper {
    public class AulaMapper {
        public static Aula ToEntidade(AulaRequestDTO dto) => new Aula {
            Aluno = new Aluno { Id = dto.idAluno },
            DataHoraInicio = dto.dataHoraInicio,
            DataHoraFim = dto.dataHoraFim,
            Observacao = dto.observacao
        };
    }
}
