namespace Ensinly.Server.Models {
    public class Professor {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
