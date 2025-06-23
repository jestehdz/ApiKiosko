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
        public async Task<List<Clientes>> GetClientes(string id)
        {
            List<Clientes> Cliente = new List<Clientes>();
            
            var client = await _context.Clientes.ToListAsync();
            if(!string.IsNullOrEmpty(id))
            {
                int ids = Convert.ToInt32(id);
                var clientes = client.Where(x => x.Id == ids).ToList();
                Cliente = clientes;
                return Cliente;
            }
            Cliente = client;
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

        public async Task<List<Clientes>> DeleteCliente(List<Clientes> Id)
        {
            List<Clientes> clientes = new List<Clientes>();
            foreach(var item in Id)
            {
                var Cliente = _context.Clientes.Where(i => i.Id == item.Id).FirstOrDefault();
                if (Cliente != null)
                {
                    _context.Clientes.Remove(Cliente);
                    clientes.Add(Cliente);
                }
                
            }            
            await _context.SaveChangesAsync();
            return clientes;
        }
        public async Task<List<Clientes>> CreateClientes(List<Clientes> Cliente)
        {
            List<Clientes> clientes = new List<Clientes>();
            foreach(var item in Cliente)
            {
                Clientes Cliente_ = item;
                _context.Clientes.Add(Cliente_);
                clientes.Add(Cliente_);
            }
            
            await _context.SaveChangesAsync();
            return clientes;
        }
        public async Task<List<Clientes>> UpdateClientes( List<Clientes> Cliente)
        {
            List<Clientes> clientes = new List<Clientes>();
            foreach(var item in Cliente)
            {
                var existingclient = _context.Clientes.Local.FirstOrDefault(p => p.Id == item.Id);
                if (existingclient != null)
                {
                    _context.Entry(existingclient).CurrentValues.SetValues(item);
                }
                else
                {
                    _context.Update(item);
                }
                clientes.Add(item);
            }
            await _context.SaveChangesAsync();
            return clientes;

        }
    }
}
