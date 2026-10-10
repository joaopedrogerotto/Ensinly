namespace Ensinly.Server.DTO {
    public class ProfessorCreateDTO : UsuarioCreateDTO{
        public string Descricao { get; set; }
        public List<int> IdMaterias { get; set; } = new List<int>();
    }
}
