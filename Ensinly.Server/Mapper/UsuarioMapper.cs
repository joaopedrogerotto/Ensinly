using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.Mapper {
    public class UsuarioMapper {
        public static Usuario ToEntidade(UsuarioCreateDTO dto) {
            return new Usuario {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };
        }
    }
}
