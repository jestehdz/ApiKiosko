using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NuGet.Protocol;

namespace ApiKiosko.Mappers
{
    public class IInvoiceMapper: IInvoice
    {
        private readonly AppDbContext _context;
        public IInvoiceMapper(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<InvoiceHed>> GetInvoiceHed()
        {
            List<InvoiceHed> Cliente = await _context.invoiceHed.ToListAsync();
            return Cliente;
        }
        public async Task<List<InvoiceDtl>> GetInvoiceDtl(int id)
        {
            List<InvoiceDtl> invoiceDtls = await _context.invoiceDtl.Where(d => d.InvoiceNum == id).ToListAsync();
            return invoiceDtls;
        }
        public async Task<String> PostCreateInvoiceHed(InvoiceHed invoiceHed2)
        {
            var orderhed = _context.OrderHed.Where(s=> s.Id == invoiceHed2.OrderNum).FirstOrDefault();
            var customer = _context.Clientes.Where(s => s.Id == orderhed.id_cliente).FirstOrDefault();

            invoiceHed2.CliNum = orderhed.id_cliente;
            invoiceHed2.NomCli = orderhed.cliente;
            invoiceHed2.NitCli = orderhed.nit;
            invoiceHed2.Direccion = orderhed.direccion;
            invoiceHed2.Numero = customer.Telefono;
            invoiceHed2.Correo = customer.email;


            InvoiceHed invoice = invoiceHed2;

            _context.invoiceHed.Add(invoice);
            await _context.SaveChangesAsync();
            return $"Se creo la Cabecera de la factura ";

        }    

        public async Task<String> PostCreateInvoiceDtl(int invoicenum)
        {
            /* var ordernum2 = _context.invoiceHed.Where(s => s.Id == invoicenum).Select(s=> s.OrderNum).FirstOrDefault();
             var orderDtl = _context.orderDtl.Where(s => s.ID_OrderHed == ordernum2);
             InvoiceDtl invoiceDtl = new InvoiceDtl();
             if (orderDtl != null)
             {

                 foreach (var item in orderDtl)
                 {
                     //var invoiceline = _context.invoiceDtl.Where(s => s.InvoiceNum == invoicenum).Select(s => s.invoiceLine).DefaultIfEmpty().Max();

                     //int linenum = invoiceline + 1;
                     var invoicedtl2 = new InvoiceDtl
                     {
                         Id = 0,
                         InvoiceNum = invoicenum,
                         invoiceLine = item.OrderLine,
                         ordernum = item.ID_OrderHed,
                         orderline = item.OrderLine,
                         Cod_prod = item.Id_Producto,
                         Nom_prod = item.Nom_Prod,
                         cantidad = item.Cantidad,
                         valor = item.Valor,
                         impuestos = item.Impuestos,
                         fechaCrea = DateTime.Now
                     };
                     _context.invoiceDtl.Add(invoicedtl2);
                     _context.SaveChangesAsync();

                 }
                 return $"Se crearon satisfactoriamente los detalles de la FE.";
             }
             else
             {
                 return null;
             }*/
            var ordernum2 = await _context.invoiceHed
             .Where(s => s.Id == invoicenum)
             .Select(s => s.OrderNum)
             .FirstOrDefaultAsync();

            bool estadoorden = await _context.OrderHed.Where(s => s.Id == ordernum2).Select(s => s.invoice).FirstOrDefaultAsync();
            if (estadoorden == null)
            {
                return "Estado not found.";
            }
            if (ordernum2 == default)
            {
                return "Invoice not found."; // Return a meaningful message
            }

            var orderDtl = await _context.orderDtl
                .Where(s => s.ID_OrderHed == ordernum2)
                .ToListAsync();

            if (orderDtl == null || !orderDtl.Any())
            {
                return "No order details found."; // Return a meaningful message
            }
            if (!estadoorden)
            {
                foreach (var item in orderDtl)
                {
                    var invoicedtl2 = new InvoiceDtl
                    {
                        Id = 0,
                        InvoiceNum = invoicenum,
                        invoiceLine = item.OrderLine,
                        ordernum = item.ID_OrderHed,
                        orderline = item.OrderLine,
                        Cod_prod = item.Id_Producto,
                        Nom_prod = item.Nom_Prod,
                        cantidad = item.Cantidad,
                        valor = item.Valor,
                        impuestos = item.Impuestos,
                        fechaCrea = DateTime.Now
                    };
                    _context.invoiceDtl.Add(invoicedtl2);
                }
                OrderHed orden = await _context.OrderHed.Where(s => s.Id == ordernum2).FirstOrDefaultAsync();
                orden.invoice = true;
                _context.OrderHed.Update(orden);
                await _context.SaveChangesAsync();
                

                return "Se crearon satisfactoriamente los detalles de la FE.";
            }
            else
            {
                return "La orden ya fue facturada";
            }
        }
        
    }
}
