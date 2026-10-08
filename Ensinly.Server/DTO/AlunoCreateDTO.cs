using Ensinly.Server.Enums;

namespace Ensinly.Server.DTO {
    public class AlunoCreateDTO : UsuarioCreateDTO{
        public string? Observacao { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public int IdNivelAprendizado { get; set; }
    }
}
