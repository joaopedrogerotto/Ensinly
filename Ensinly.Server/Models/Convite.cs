namespace Ensinly.Server.Models {
    public class Convite {
        public int Id { get; set; }
        public string TokenHash { get; set; } = string.Empty;
        public Professor Professor { get; set; } = new Professor();
        public DateTime DataCriacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public DateTime DataAceite { get; set; }
        public Aluno Aluno { get; set; } = new Aluno();
    }
}
