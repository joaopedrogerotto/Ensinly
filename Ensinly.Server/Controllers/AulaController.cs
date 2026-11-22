using Ensinly.Server.DTO;
using Ensinly.Server.Exceptions;
using Ensinly.Server.Facade;
using Ensinly.Server.Facade.Inteface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AulaController : ControllerBase{
        private readonly IFacadeAula _facadeAula;

        public AulaController(IFacadeAula facadeAula) {
            _facadeAula = facadeAula;
        }

        [HttpPost]
        public async Task<IActionResult> PostAula([FromBody] AulaRequestDTO aulaRequestDTO) {
            try {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario);
                
                var result = await _facadeAula.AgendarAula(aulaRequestDTO, idUsuario);

                if (!result) {
                    return BadRequest("Falha ao agendar a aula.");
                }
                return Ok("Aula agendada com sucesso");
            } catch (ClassAlreadyRegisteredException ex) {
                return Conflict(new { message = ex.Message });
            } catch (InvalidDateException ex) {
                return BadRequest(ex.Message);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
