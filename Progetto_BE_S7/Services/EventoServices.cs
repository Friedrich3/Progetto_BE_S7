using Microsoft.EntityFrameworkCore;
using Progetto_BE_S7.Data;
using Progetto_BE_S7.DTOs.Evento;
using Progetto_BE_S7.DTOs.Artista;
using Progetto_BE_S7.Models;

namespace Progetto_BE_S7.Services
{
    public class EventoServices
    {
        private readonly ApplicationDbContext _context;
        public EventoServices(ApplicationDbContext context)
        {
            _context = context;
        }
        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateNew(CreateEventoRequestDto createEventoRequest)
        {
            try
            {
                var evento = new Evento()
                {
                    Titolo = createEventoRequest.Titolo,
                    Data = createEventoRequest.Data,
                    Luogo = createEventoRequest.Luogo,
                    numeroBiglietti = createEventoRequest.numeroBiglietti,
                    ArtistaId = createEventoRequest.ArtistaId
                };
                _context.Eventi.Add(evento);
                return await SaveAsync();

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Update(int id, UpdateEventoRequestDto updateEventoRequest)
        {
            try
            {
                var evento = await _context.Eventi.FirstOrDefaultAsync(p => p.EventoId == id);
                if (evento == null) return false;

                evento.Titolo = updateEventoRequest.Titolo;
                evento.Data = updateEventoRequest.Data;
                evento.Luogo = updateEventoRequest.Luogo;
                evento.numeroBiglietti = updateEventoRequest.numeroBiglietti;
                //evento.ArtistaId = updateEventoRequest.ArtistaId;
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var evento = await _context.Eventi.FirstOrDefaultAsync(p=> p.EventoId == id);
                if (evento == null) return false;
                _context.Eventi.Remove(evento);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<SingleEventoRequestDto>> GetallEvents()
        {
            List<SingleEventoRequestDto>? ListaEventi = null;
            var data = await _context.Eventi.Include(p=> p.Artista).ToListAsync();
            if(data == null) return ListaEventi;
            ListaEventi = data.Select(ev => new SingleEventoRequestDto()
            {
                EventoId = ev.EventoId,
                Titolo = ev.Titolo,
                Data = ev.Data,
                Luogo = ev.Luogo,
                numeroBiglietti = ev.numeroBiglietti,
                Artista = new SingleArtistaDto()
                {
                    ArtistaId = ev.Artista.ArtistaId,
                    Nome = ev.Artista.Nome,
                    Genere = ev.Artista.Genere,
                }
            }).ToList();
            return ListaEventi;
        }

        public async Task<SingleEventoResponseDto> GetEvent(int id)
        {
            var evento = await _context.Eventi.FirstOrDefaultAsync(p => p.EventoId == id);
            if (evento == null) return null;
            var result = new SingleEventoResponseDto()
            {
                EventoId = evento.EventoId,
                Titolo = evento.Titolo,
                Data = evento.Data,
                Luogo = evento.Luogo,
                numeroBiglietti= evento.numeroBiglietti,
                ArtistaId = evento.ArtistaId,
            };
            return result;
        }






    }
}
