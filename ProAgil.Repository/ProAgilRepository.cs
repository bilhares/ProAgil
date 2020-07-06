using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        //GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //EVENTOS
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(p => p.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }
            query = query.AsNoTracking()
                .OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(p => p.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }
            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento).Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();

        }

        public async Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(p => p.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }
            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento).Where(c => c.EventoID == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        //PALESTRANTES
        public async Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestranteEventos).ThenInclude(p => p.Evento);

            }

            query = query.OrderBy(p => p.Nome).Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetPalestranteAsyncByName(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);


            if (includeEventos)
            {
                query = query.Include(p => p.PalestranteEventos).ThenInclude(p => p.Evento);

            }

            query = query.AsNoTracking().OrderBy(p => p.Nome).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }


    }
}
