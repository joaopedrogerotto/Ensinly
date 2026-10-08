using Ensinly.Server.Enums;

namespace Ensinly.Server.Models {
    public class Aluno : Usuario{
        public string? Observacao { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public NivelAprendizadoEnum NivelAprendizado { get; set; }
    }
}
