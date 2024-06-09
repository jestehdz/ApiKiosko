using ApiKiosko.Models;

namespace ApiKiosko.Interfaces
{
    public interface IProductos
    {
        Task<List<Productos>> GetProductos();
        Task<Productos> GetProductosById(int Id);
        Task<bool> DeleteProducto(int Id);
        Task<Productos> CreateProductos(Productos producto);
        Task<Productos> UpdateProductos(int id,Productos producto);
    }
}
