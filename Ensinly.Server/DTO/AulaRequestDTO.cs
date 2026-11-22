namespace Ensinly.Server.DTO {
    public class AulaRequestDTO {
        public int idAluno { get; set; }
        public DateTime dataHoraInicio { get; set; }
        public DateTime dataHoraFim { get; set; }
        public string? observacao { get; set; }
    }
}
