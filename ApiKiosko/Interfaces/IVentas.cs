using ApiKiosko.Models;

namespace ApiKiosko.Interfaces
{
    public interface IVentas
    {
        Task<List<OrderHed>> GetVentas();
        Task<OrderHed> GetVentasById(int Id);
        Task<List<OrderDtl>> GetOrderDtlByID_Ov(int ID_ov);
        Task<bool> DeleteVentas(int Id);
        Task<string> CreateVenta(OrderHed ventas);
        Task<OrderHed> UpdateVenta(int id, OrderHed ventas);
        Task<OrderDtl> UpdateLinea(OrderDtl orderDtl);
        Task<string> venta(OrderDtl orderDtl);
        Task<string> deleteorderline(OrderDtl orderDtl);
    }
}
