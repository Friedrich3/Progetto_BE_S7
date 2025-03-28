namespace Progetto_BE_S7.DTOs.Artista
{
    public class SingleArtistResponseDto
    {
        public required int ArtistaId { get; set; }
        public required string Nome { get; set; }
        public required string Genere { get; set; }
        public required string Biografia { get; set; }
    }
}
