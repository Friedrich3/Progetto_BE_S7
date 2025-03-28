using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Progetto_BE_S7.Data;
using Progetto_BE_S7.DTOs.Artista;
using Progetto_BE_S7.DTOs.Biglietto;
using Progetto_BE_S7.DTOs.Evento;
using Progetto_BE_S7.Models;

namespace Progetto_BE_S7.Services
{
    public class BigliettoServices
    {
        private readonly ApplicationDbContext _context;
        public BigliettoServices (ApplicationDbContext context)
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

        public async Task<List<SingleBigliettoRequestDto>> GetAllTickets()
        {
            List<SingleBigliettoRequestDto>? ListaTickets = null;
            var data = await _context.Biglietti.Include(p=> p.User).Include(p=>p.Evento).ThenInclude(p=>p.Artista).ToListAsync();
            if (data == null) return ListaTickets;

            ListaTickets = data.Select(item => new SingleBigliettoRequestDto()
            {
                BigliettoId = item.BigliettoId,
                DataAcquisto = item.DataAcquisto,
                UserEmail = item.User.Email,
                Evento = new EventoFromTicketRequestDto()
                {
                    EventoId = item.Evento.EventoId,
                    Titolo = item.Evento.Titolo,
                    Data = item.Evento.Data,
                    Luogo = item.Evento.Luogo,
                    Artista = new SingleArtistaDto()
                    {
                        ArtistaId = item.Evento.Artista.ArtistaId,
                        Nome = item.Evento.Artista.Nome,
                        Genere = item.Evento.Artista.Genere,
                    }
                }
            }).ToList();

            return ListaTickets;
        }


        public async Task<bool> BuyNew( AcquistoBigliettoDto acquistoBigliettoDto , string userEmail)
        {
            try
            {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(p => p.Email == userEmail);
            if (user == null) {  return false; }
            var evento = await _context.Eventi.FirstOrDefaultAsync(p => p.EventoId == acquistoBigliettoDto.EventoId);
            if (evento == null) { return false;}

            var biglietto = new Biglietto()
            {
                DataAcquisto = DateTime.Now,
                EventoId = evento.EventoId,
                UserId = user.Id,
            };
            _context.Biglietti.Add(biglietto);
            evento.numeroBiglietti -= acquistoBigliettoDto.Quantita;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<SingleBigliettoResponseDto>> UserTickets(string userEmail)
        {
            List<SingleBigliettoResponseDto>? ListaTicket = null;

            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(p => p.Email == userEmail);
            var data = await _context.Biglietti.Include(p=>p.Evento).ThenInclude(p=>p.Artista).Where(p=> p.UserId == user.Id).ToListAsync();
            if (user == null || data == null) return ListaTicket;
            ListaTicket = data.Select(item=> new SingleBigliettoResponseDto()
            {
                BigliettoId = item.BigliettoId,
                DataAcquisto = item.DataAcquisto,
                Evento = new EventoBigliettoDto()
                {
                    EventoId = item.Evento.EventoId,
                    Titolo = item.Evento.Titolo,
                    Data = item.Evento.Data,
                    Luogo = item.Evento.Luogo,
                    Artista = new ArtistaBigliettoDto()
                    {
                        ArtistaId = item.Evento.Artista.ArtistaId,
                        Nome = item.Evento.Artista.Nome,
                    }
                }
            }).ToList();
            return ListaTicket;
        }
    }
}
