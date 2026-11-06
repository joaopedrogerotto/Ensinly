namespace Ensinly.Server.DTO {
    public class ConviteRequestDTO {
        public int IdProfessorUsuario { get; set; }
        public int IdAluno { get; set; }
        public string EmailDestinatario { get; set; } = string.Empty;
    }
}
