using ApiKiosko.Models;

namespace ApiKiosko.Interfaces
{
    public interface IClientes 
    {
        Task<List<Clientes>> GetClientes(string id);
        Task<Clientes> GetClienteById(int Id);
        Task<List<Clientes>> DeleteCliente(List<Clientes> Id);
        Task<List<Clientes>> CreateClientes(List<Clientes> producto);
        Task<List<Clientes>> UpdateClientes(List<Clientes> producto);
    }
}
