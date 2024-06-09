using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKiosko.Mappers
{
    public class IClientesMapper : IClientes
    {
        private readonly AppDbContext _context;
        public IClientesMapper(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Clientes>> GetClientes()
        {
            List<Clientes> Cliente = await _context.Clientes.ToListAsync();
            return Cliente;
        }

        public async Task<Clientes> GetClienteById(int Id)
        {
            Clientes Cliente = await _context.Clientes.FindAsync(Id);
            if (Cliente != null)
            {
                return Cliente;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteCliente(int Id)
        {

            Clientes Cliente = await _context.Clientes.FindAsync(Id);
            if (Cliente == null)
            {
                return false;
            }
            _context.Clientes.Remove(Cliente);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Clientes> CreateClientes(Clientes Cliente)
        {
            Clientes Cliente_ = Cliente;
            _context.Clientes.Add(Cliente_);
            await _context.SaveChangesAsync();
            return Cliente_;
        }
        public async Task<Clientes> UpdateClientes(int id, Clientes Cliente)
        {
            if (id != Cliente.Id)
            {
                Cliente.Id = id;
            }
            _context.Clientes.Update(Cliente);
            await _context.SaveChangesAsync();
            return Cliente;
            //Productos productos_ = //appDb.Productos.Update(producto);
            return null;

        }
    }
}
