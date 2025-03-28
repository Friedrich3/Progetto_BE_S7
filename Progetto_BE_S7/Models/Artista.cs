using System.ComponentModel.DataAnnotations;

namespace Progetto_BE_S7.Models
{
    public class Artista
    {
        [Key]
        public int ArtistaId { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Genere { get; set; }
        [Required]
        public required string Biografia { get; set; }
       

        public ICollection<Evento> Eventi { get; set; }
    }
}
