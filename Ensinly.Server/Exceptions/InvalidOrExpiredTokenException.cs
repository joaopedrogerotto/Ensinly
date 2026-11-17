namespace Ensinly.Server.Exceptions {
    public class InvalidOrExpiredTokenException : Exception {
        public InvalidOrExpiredTokenException(string message) : base(message) {
        }
    }
}
