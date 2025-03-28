
using Progetto_BE_S7.DTOs.Artista;

namespace Progetto_BE_S7.DTOs.Evento
{
    public class SingleEventoRequestDto
    {
        public required int EventoId { get; set; }
        public required string Titolo { get; set; }
        public required DateTime Data { get; set; }
        public required string Luogo { get; set; }

        public required SingleArtistaDto Artista { get; set; }
    }
}
