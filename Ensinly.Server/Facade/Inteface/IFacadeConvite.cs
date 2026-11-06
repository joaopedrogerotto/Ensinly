using Ensinly.Server.DTO;
using Ensinly.Server.Models;

namespace Ensinly.Server.Facade.Inteface {
    public interface IFacadeConvite {
        public Task<Convite> CriarConvite(ConviteRequestDTO conviteDto);
        public Task EnviarConvite(Convite convite);
        public Task<bool> VerificaVinculo(ConviteRequestDTO conviteDto);
    }
}
