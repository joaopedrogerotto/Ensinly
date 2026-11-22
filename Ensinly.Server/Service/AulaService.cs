using Ensinly.Server.Exceptions;
using Ensinly.Server.Models;

namespace Ensinly.Server.Service {
    public class AulaService {
        public void ValidateDate(Aula Aula) {
            if(Aula.DataHoraInicio >= Aula.DataHoraFim) {
                throw new InvalidDateException("Data de início maior que a data de fim");
            }
        }
    }
}
