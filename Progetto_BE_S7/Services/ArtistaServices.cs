﻿using Microsoft.EntityFrameworkCore;
using Progetto_BE_S7.Data;
using Progetto_BE_S7.DTOs.Artista;
using Progetto_BE_S7.DTOs.Evento;
using Progetto_BE_S7.Models;

namespace Progetto_BE_S7.Services
{
    public class ArtistaServices
    {
        private readonly ApplicationDbContext _context;
        public ArtistaServices(ApplicationDbContext context)
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
            catch
            {
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
            catch (Exception ex )
            {
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


    }
}
