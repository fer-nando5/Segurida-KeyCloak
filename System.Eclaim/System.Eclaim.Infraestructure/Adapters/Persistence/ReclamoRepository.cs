using Microsoft.EntityFrameworkCore;
using System.Eclaim.Domain.Dpo;
using System.Eclaim.Domain.Entities;
using System.Eclaim.Domain.OutPort.Persistence;
using System.Eclaim.Infraestructure.Configurations.Context;

namespace System.Eclaim.Infraestructure.Adapters.Persistence
{
    public class ReclamoRepository : IReclamoRepository
    {
        private readonly IdentityDbContext _context;
        public ReclamoRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateAsync(Reclamo reclamo)
        {
            await _context.AddAsync(reclamo);
            await _context.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<ReclamoResponseDpo?> FindByCodeAsync(string numeroReclamo)
        {
            var reclamo = (
                from a in await _context.Reclamos
                    .Where(a => a.NumeroReclamo == numeroReclamo)
                    .ToListAsync()
                //join b in await _context.Users.ToListAsync()
                //    on a.UsuarioRegistro.ToString() equals b.Id
                select new ReclamoResponseDpo
                {
                    Id = a.Id,
                    NumeroReclamo = a.NumeroReclamo,
                    Descripcion = a.Descripcion,
                    FechaReclamo = a.FechaReclamo,
                    UsuarioRegistro = "Fernando Huamani"
                }
            ).FirstOrDefault();

            return reclamo;
        }

        public async Task<int> MaximoCorrelativoAsync()
        {
            string fecha = DateTime.Now.ToString("yyyyMMdd");
            
            var reclamosFiltrados = await _context.Reclamos
                .Where(r => EF.Functions.Like(r.NumeroReclamo, "%" + fecha + "%"))
                .ToListAsync(); 

            int maxNumero = reclamosFiltrados
                .Select(r => {
                    var lastThree = r.NumeroReclamo.Length >= 3
                        ? r.NumeroReclamo.Substring(r.NumeroReclamo.Length - 3)
                        : r.NumeroReclamo;
                    return int.TryParse(lastThree, out int num) ? num : 0;
                })
                .DefaultIfEmpty(0)
                .Max();

            return maxNumero;
        }
    }
}
