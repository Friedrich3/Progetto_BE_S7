using System.ComponentModel.DataAnnotations;

namespace Progetto_BE_S7.DTOs.Artista
{
    public class ArtistaCreateRequestDto
    {
        public required string Nome { get; set; }
        public required string Genere { get; set; }
        public required string Biografia { get; set; }

    }
}
