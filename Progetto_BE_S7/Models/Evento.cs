using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Progetto_BE_S7.Models
{
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }
        [Required]
        public required string Titolo { get; set; }
        [Required]
        public required DateTime Data {  get; set; }
        [Required]
        public required string Luogo { get; set; }
        [Required]
        [Range(1,999)]
        public required int numeroBiglietti { get; set; }


        [Required]
        public int ArtistaId { get; set; }
        [ForeignKey(nameof(ArtistaId))]
        public Artista Artista {  get; set; }

        public ICollection<Biglietto> Biglietti { get; set; }
        
    }
}
