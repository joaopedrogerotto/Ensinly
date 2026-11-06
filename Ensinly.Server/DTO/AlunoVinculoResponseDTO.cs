namespace Ensinly.Server.DTO {
    public class AlunoVinculoResponseDTO {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Observacao { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NivelAprendizado { get; set; }
        public bool Status { get; set; }
        public string VinculoStatus { get; set; }
    }
}
