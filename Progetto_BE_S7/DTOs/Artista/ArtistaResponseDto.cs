using Progetto_BE_S7.DTOs.Evento;
namespace Progetto_BE_S7.DTOs.Artista

{
    public class ArtistaResponseDto
    {
        public required int ArtistaId { get; set; }
        public required string Nome { get; set; }
        public required string Genere { get; set; }
        public required string Biografia { get; set; }

        public List<EventoResponseDto>? Eventi { get; set; }

    }
}
