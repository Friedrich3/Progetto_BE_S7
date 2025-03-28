using Progetto_BE_S7.DTOs.Evento;
using Progetto_BE_S7.Models.Auth;

namespace Progetto_BE_S7.DTOs.Biglietto
{
    public class SingleBigliettoRequestDto
    {
        public int BigliettoId { get; set; }
        public required DateTime DataAcquisto { get; set; }
        public required EventoFromTicketRequestDto Evento { get; set; }
        public string UserEmail { get; set; }
    }
}
