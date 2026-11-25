using Ensinly.Server.Models;

namespace Ensinly.Server.DTO {
    public class AulaResponseDTO {
        public AlunoResponseAulaDTO Aluno { get; set; }
        public Professor Professor { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Status { get; set; }
        public string? Observacao { get; set; }
    }
}
