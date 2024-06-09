using ApiKiosko.Models;

namespace ApiKiosko.Interfaces
{
    public interface IClientes 
    {
        Task<List<Clientes>> GetClientes();
        Task<Clientes> GetClienteById(int Id);
        Task<bool> DeleteCliente(int Id);
        Task<Clientes> CreateClientes(Clientes producto);
        Task<Clientes> UpdateClientes(int id, Clientes producto);
    }
}
