using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKiosko.Mappers
{
    public class IVentasMapper : IVentas
    {
        private readonly AppDbContext _context;

        public IVentasMapper(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ventas>> GetVentas()
        {
            List<Ventas> Ventas = await _context.Ventas.ToListAsync();
            return Ventas;
        }

        public async Task<Ventas> GetVentasById(int Id)
        {
            Ventas Ventas = await _context.Ventas.FindAsync(Id);
            if (Ventas != null)
            {
                return Ventas;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteVentas(int Id)
        {

            Ventas ventas = await _context.Ventas.FindAsync(Id);
            if (ventas == null)
            {
                return false;
            }
            _context.Ventas.Remove(ventas);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Ventas> CreateVenta(Ventas ventas)
        {

            Clientes clienteNombre = await _context.Clientes.FindAsync(ventas.id_cliente);
            Productos productosnombre = await _context.Productos.FindAsync(ventas.id_producto);
            ventas.cliente = clienteNombre.Nombres;
            ventas.producto = productosnombre.Producto;
            ventas.Valor = productosnombre.Precio * ventas.cantidad;
            Ventas Ventas = ventas;
            bool Venta = venta(ventas.cantidad, ventas.id_producto,"C");
            if (Venta)
            {
                _context.Ventas.Add(Ventas);
                await _context.SaveChangesAsync();
                //return Ventas;
            }
            return Ventas;
        }
        public async Task<Ventas> UpdateVenta(int id, Ventas ventas)
        {
            if (id != ventas.Id)
            {
                ventas.Id = id;
            }
            Clientes clienteNombre = await _context.Clientes.FindAsync(ventas.id_cliente);
            Productos productosnombre = await _context.Productos.FindAsync(ventas.id_producto);
            ventas.cliente = clienteNombre.Nombres + " "+clienteNombre.Apellidos;
            ventas.producto = productosnombre.Producto;
            ventas.Valor = productosnombre.Precio * ventas.cantidad;
            bool Venta = venta(ventas.cantidad, ventas.id_producto,"U");
            if (Venta)
            {
                _context.Ventas.Update(ventas);
                await _context.SaveChangesAsync();
                return ventas;
            }//Productos productos_ = //appDb.Productos.Update(producto);
            return null;

        }
        public bool venta(decimal cantidad, int id_product, string Accion)
        {
            Productos producto = _context.Productos.Where(x => x.Id == id_product).FirstOrDefault();
            if (producto == null)
            {
                return false;
            }
            else
            {
                if (producto.Inv_Act >= cantidad)
                {
                    if (Accion == "C")
                    {
                        RestarInv(cantidad, id_product);
                        return true;
                    }
                    else if (Accion == "U")
                    {
                        RestaurarInv(cantidad, id_product);
                        RestarInv(cantidad, id_product);
                        return true;
                    }
                }
            }
            return true;
        }
        public void RestarInv(decimal cantidad, int id_product)
        {
            Productos productos = _context.Productos.Where(x => x.Id == id_product).FirstOrDefault();
            if (productos != null)
            {
                productos.Inv_Act = productos.Inv_Act - cantidad;
                _context.Productos.Update(productos);
                _context.SaveChangesAsync();
            }
        }

        public void RestaurarInv(decimal cantidad, int id_product)
        {
            Productos productos = _context.Productos.Where( x=> x.Id == id_product).FirstOrDefault();
            if(productos != null)
            {
                productos.Inv_Act = productos.Inv_Act + cantidad;
                _context.Productos.Update(productos);
                _context.SaveChangesAsync();
            }
        }
    }
}
