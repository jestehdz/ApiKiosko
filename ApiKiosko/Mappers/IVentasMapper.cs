using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace ApiKiosko.Mappers
{
    public class IVentasMapper : IVentas
    {
        private readonly AppDbContext _context;

        public IVentasMapper(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderHed>> GetVentas()
        {
            List<OrderHed> Ventas = await _context.OrderHed.ToListAsync();
            return Ventas;
        }

        public async Task<OrderHed> GetVentasById(int Id)
        {
            OrderHed Ventas = await _context.OrderHed.FindAsync(Id);
            if (Ventas != null)
            {
                return Ventas;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<OrderDtl>> GetOrderDtlByID_Ov(int ID_ov)
        {
            var orderdtl = await _context.orderDtl.Where(o => o.ID_OrderHed == ID_ov).ToListAsync();
            if (orderdtl != null)
            {
                List<OrderDtl> order = new List<OrderDtl>() ;
                order = orderdtl;
                return order;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteVentas(int Id)
        {

            OrderHed ventas = await _context.OrderHed.FindAsync(Id);
            if (ventas == null)
            {
                return false;
            }
            _context.OrderHed.Remove(ventas);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<String> CreateVenta(OrderHed ventas)
        {

            var clienteNombre =  _context.Clientes.Where(s =>  s.Id == ventas.id_cliente).FirstOrDefault();
            //Productos productosnombre = await _context.Productos.FindAsync(ventas.id_producto);
            ventas.cliente = clienteNombre.Nombres;
            ventas.nit = clienteNombre.Num_Doc;
            ventas.direccion = clienteNombre.Direccion;
            ventas.invoice = false;
            ventas.fecha = DateTime.Now;
            //ventas.producto = productosnombre.Producto;
            //ventas.Valor = productosnombre.Precio * ventas.cantidad;
            OrderHed Ventas = ventas;
            //bool Venta = venta(ventas.cantidad, ventas.id_producto,"C");
            
                _context.OrderHed.Add(Ventas);
                await _context.SaveChangesAsync();
                //return Ventas;
            
            return "Se creo satisfactoriamente el pedido.";
        }
        public async Task<OrderHed> UpdateVenta(int id, OrderHed ventas)
        {
            if (id != ventas.Id)
            {
                ventas.Id = id;
            }
            Clientes clienteNombre = await _context.Clientes.FindAsync(ventas.id_cliente);
           ventas.cliente = clienteNombre.Nombres + " "+clienteNombre.Apellidos;
            
                _context.OrderHed.Update(ventas);
                await _context.SaveChangesAsync();
                return ventas;
           
        }
        public async Task<OrderDtl> UpdateLinea(OrderDtl orderDtl)
        {
            Productos productos = _context.Productos.Where(a => a.Id == orderDtl.Id_Producto).FirstOrDefault();
            orderDtl.Nom_Prod = productos.Producto;
            orderDtl.Valor = productos.Precio * orderDtl.Cantidad;
            orderDtl.Impuestos = (productos.Precio * orderDtl.Cantidad) * 0.190000M;
            _context.orderDtl.Update(orderDtl);
            await _context.SaveChangesAsync();
            return orderDtl;
        }
        public async Task<string> deleteorderline(OrderDtl orderDtl)
        {
            _context.orderDtl.Remove(orderDtl);
            await _context.SaveChangesAsync();
            return $"Se elimino satisfactoriamente la linea N° {orderDtl.OrderLine} del pedido N° {orderDtl.ID_OrderHed}";
        }
        public async Task<String> venta(OrderDtl orderDtl)
        {
            var linea = _context.orderDtl.Where(x => x.ID_OrderHed == orderDtl.ID_OrderHed).Select(s => s.OrderLine).DefaultIfEmpty().Max();
            var producto = _context.Productos.Where(x => x.Id == orderDtl.Id_Producto).FirstOrDefault();
            
            decimal numlinea = Convert.ToDecimal(linea);

            orderDtl.OrderLine = (int)numlinea + 1;
            orderDtl.Nom_Prod = producto.Producto == null ? "" : producto.Producto;
            orderDtl.Valor = producto.Precio * orderDtl.Cantidad;
            orderDtl.Impuestos = (producto.Precio * orderDtl.Cantidad) * 0.19000M ;

            OrderDtl dtl = orderDtl;
            _context.orderDtl.Add(dtl);
            await _context.SaveChangesAsync();

            return $"Se creo satisfactoria la linea N°{orderDtl.OrderLine}  del Pedido N° {orderDtl.ID_OrderHed}.";
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
