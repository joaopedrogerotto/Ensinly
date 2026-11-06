using System.Security.Cryptography;

namespace Ensinly.Server.Service {
    public class ConviteService {
        private readonly IConfiguration _configuration;

        public ConviteService(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string GenerateToken() {
            byte[] tokenBytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(tokenBytes).Replace("=", "").Replace("/", "_").Replace("+", "-");
        }

        public string GenerateConviteLink(string token) {
            string baseUrl = _configuration["UrlsFrontend:BaseUrl"];
            string rotaConvite = _configuration["UrlsFrontend:RotaConvite"];
            return $"{baseUrl}{rotaConvite}?token={token}";
        }
    }
}
