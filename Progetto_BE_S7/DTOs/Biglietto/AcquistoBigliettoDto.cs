namespace Progetto_BE_S7.DTOs.Biglietto
{
    public class AcquistoBigliettoDto
    {
        public required int EventoId { get; set; }

        public required int Quantita { get; set; } = 1;

    }
}
