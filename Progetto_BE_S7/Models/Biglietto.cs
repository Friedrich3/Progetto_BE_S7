using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Progetto_BE_S7.Models.Auth;

namespace Progetto_BE_S7.Models
{
    public class Biglietto
    {
        [Key]
        public int BigliettoId { get; set; }

        [Required]
        public DateTime DataAcquisto { get; set; }

        [Required]
        public int EventoId { get; set; }
        [ForeignKey(nameof(EventoId))]
        public Evento Evento { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

    }
}
