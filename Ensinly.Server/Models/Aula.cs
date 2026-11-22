namespace Ensinly.Server.Models {
    public class Aula {
        public Aluno Aluno { get; set; } 
        public Professor Professor { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string? Observacao { get; set; }
    }
}
