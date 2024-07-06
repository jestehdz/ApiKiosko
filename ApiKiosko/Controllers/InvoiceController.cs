using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiKiosko.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly AppDbContext _context;
        private IInvoice invoice;
        protected Response response;
        public InvoiceController(AppDbContext appDbContext , IInvoice invoice) 
        {
            _context = appDbContext;
            this.invoice = invoice;
            response = new Response();
        }

        [HttpGet("GetInvoicesHed")]
        public async Task<ActionResult<IEnumerable<InvoiceHed>>> GetInvoiceHed()
        {
            try
            {
                var lista = await invoice.GetInvoiceHed();
                response.Result = lista;
                response.DisplayMessage = "Listado de las cabeceras de las Facturas";
               
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(response);
        }

        [HttpGet("GetInvoiceDtl")]
        public async Task<ActionResult<IEnumerable<InvoiceDtl>>> GetInvoiceDtl(int invoicenum)
        {
            try
            {
                var lista = await invoice.GetInvoiceDtl(invoicenum);
                response.Result = lista;
                response.DisplayMessage = $"Listado de los detalles de la Factura {invoicenum}";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(response);
        }

        [HttpPost("PostInvoiceHed")]
        public async Task<ActionResult<string>> PostInvoiceHed(InvoiceHed invoicehed)
        {
            try
            {
                var post = await invoice.PostCreateInvoiceHed(invoicehed);
                response.Result = post;
                response.DisplayMessage = $"Se ha creado satisfactoriamente la cabecera de la Factura";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.DisplayMessage = $"Se ha producido un error al momento de crear la factura, por favor contacte con el administrador";
                response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(response);
            }
        }
        [HttpPost("PostInvoiceDtl")]
        public async Task<ActionResult<string>> PostInvoiceDtl(int invoicenum)
        {
            try
            {
                var post = await invoice.PostCreateInvoiceDtl(invoicenum);
                response.Result = post;
                response.DisplayMessage = "Se ha creado satisfactoriamente el detalle de la factura";
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.DisplayMessage = "se ha producido un error al momento de crear la factura, por favor contacte con el administrador";
                response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(response);
            }
        }
    }
}
