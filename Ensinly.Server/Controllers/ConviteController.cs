using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ConviteController : ControllerBase {
        private readonly IFacadeAluno _facadeAluno;
        private readonly IFacadeConvite _facadeConvite;

        public ConviteController(IFacadeAluno facadeAluno, IFacadeConvite facadeConvite) {
            _facadeAluno = facadeAluno;
            _facadeConvite = facadeConvite;
        }


        [HttpPost("aluno-professor-vinculo")]
        public async Task<IActionResult> PostEmailConvite([FromBody] ConviteRequestDTO conviteRequestDto) {
            try {
                if(await _facadeConvite.VerificaVinculo(conviteRequestDto)) {
                    return BadRequest(new { message = "O aluno já está vinculado a este professor." });
                }

                var convite = await _facadeConvite.CriarConvite(conviteRequestDto);
                return Ok();
            }catch(Exception ex) {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
