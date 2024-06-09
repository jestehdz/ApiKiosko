using ApiKiosko.Context;
using ApiKiosko.Interfaces;

namespace ApiKiosko.Mappers
{
    public class IVentasMapper : IVentas
    {
        private readonly AppDbContext _context;
        public IVentasMapper(AppDbContext context)
        {
            _context = context;
        }
    }
}
