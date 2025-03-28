using System.ComponentModel.DataAnnotations;

namespace Progetto_BE_S7.DTOs.Artista
{
    public class ArtistaUpdateRequestDto
    {
        public required string Nome { get; set; }
        public required string Genere { get; set; }
        public required string Biografia { get; set; }

    }
}
