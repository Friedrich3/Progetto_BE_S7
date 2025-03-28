using System.ComponentModel.DataAnnotations;

namespace Progetto_BE_S7.DTOs.Evento
{
    public class CreateEventoRequestDto
    {
        public required string Titolo { get; set; }
        public required DateTime Data { get; set; }
        public required string Luogo { get; set; }
        public required int ArtistaId { get; set; }
    }
}
