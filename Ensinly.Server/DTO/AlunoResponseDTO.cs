using Ensinly.Server.Enums;

namespace Ensinly.Server.DTO {
    public class AlunoResponseDTO {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Observacao { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NivelAprendizado { get; set; }
        public bool Status { get; set; }
    }
}
