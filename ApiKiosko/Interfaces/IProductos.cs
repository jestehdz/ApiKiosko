using ApiKiosko.Models;

namespace ApiKiosko.Interfaces
{
    public interface IProductos
    {
        Task<List<Productos>> GetProductos(string id);
        Task<Productos> GetProductosById(int Id);
        Task<List<Productos>> DeleteProducto(List<Productos> productos);
        Task<List<Productos>> CreateProductos(List<Productos> producto);
        Task<List<Productos>> UpdateProductos(List<Productos> producto);
    }
}
