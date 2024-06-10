using ApiKiosko.Models;

namespace ApiKiosko.Interfaces
{
    public interface IVentas
    {
        Task<List<Ventas>> GetVentas();
        Task<Ventas> GetVentasById(int Id);
        Task<bool> DeleteVentas(int Id);
        Task<string> CreateVenta(Ventas ventas);
        Task<Ventas> UpdateVenta(int id, Ventas ventas);
    }
}
