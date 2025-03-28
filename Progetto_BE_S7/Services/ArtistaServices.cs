using Microsoft.EntityFrameworkCore;
using Progetto_BE_S7.Data;
using Progetto_BE_S7.DTOs.Artista;
using Progetto_BE_S7.DTOs.Evento;
using Progetto_BE_S7.Models;

namespace Progetto_BE_S7.Services
{
    public class ArtistaServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ArtistaServices> _logger;
        public ArtistaServices(ApplicationDbContext context, ILogger<ArtistaServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Create(ArtistaCreateRequestDto artistaCreateRequest)
        {
            try
            {
                var artista = new Artista()
                {
                    Nome = artistaCreateRequest.Nome,
                    Genere = artistaCreateRequest.Genere,
                    Biografia = artistaCreateRequest.Biografia,
                };
                _context.Artisti.Add(artista);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(int id , ArtistaUpdateRequestDto artistaUpdateRequest)
        {
            try
            {
                var artista = await _context.Artisti.FirstOrDefaultAsync(p=> p.ArtistaId == id);
                if (artista == null) return false;

                artista.Nome = artistaUpdateRequest.Nome;
                artista.Genere = artistaUpdateRequest.Genere;
                artista.Biografia = artistaUpdateRequest.Biografia;
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var artista = await _context.Artisti.FirstOrDefaultAsync(p => p.ArtistaId == id);
                if (artista == null) return false;
                _context.Artisti.Remove(artista);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<ArtistaResponseDto>> GetAllArtists()
        {
            List<ArtistaResponseDto>? ListaArtisti = null;
            var data = await _context.Artisti.Include(p=> p.Eventi).ToListAsync();
            if (data == null) return ListaArtisti;

            ListaArtisti = data.Select(p => new ArtistaResponseDto()
            {
                ArtistaId = p.ArtistaId,
                Nome = p.Nome,
                Genere = p.Genere,
                Biografia = p.Biografia,
                Eventi = p.Eventi.Select(e => new EventoResponseDto()
                {
                    Titolo = e.Titolo,
                    Data = e.Data,
                    Luogo = e.Luogo,
                }).ToList(),
            }).ToList();
            return ListaArtisti;
        }

        public async Task<SingleArtistResponseDto> GetArtist(int id)
        {
            var artista = await _context.Artisti.FirstOrDefaultAsync(p=> p.ArtistaId == id);
            if (artista == null) return null;
            var result = new SingleArtistResponseDto()
            {
                ArtistaId = artista.ArtistaId,
                Nome = artista.Nome,
                Genere = artista.Genere,
                Biografia = artista.Biografia,
            };
            return result;
        }


    }
}
