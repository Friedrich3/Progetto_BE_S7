using Progetto_BE_S7.DTOs.Evento;
using Progetto_BE_S7.Models.Auth;

namespace Progetto_BE_S7.DTOs.Biglietto
{
    public class SingleBigliettoResponseDto
    {
        public required int BigliettoId { get; set; }
        public required DateTime DataAcquisto { get; set; }


        public required EventoBigliettoDto Evento { get; set; }



    }
}
