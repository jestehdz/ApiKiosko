using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiKiosko.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : Controller
    {
        private readonly AppDbContext _context;
        private IVentas Ventas;
        protected Response Response;
        public VentasController(AppDbContext context, IVentas ventas)
        {
            _context = context;
            Ventas = ventas;
            Response = new Response();
        }
        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderHed>>> GetVentas()
        {
            try
            {
                //return await _context.Ventas.ToListAsync();
                var lista = await Ventas.GetVentas();
                Response.Result = lista;
                Response.DisplayMessage = "Listado de Ventas";
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(Response);
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderHed>> GetClienteById(int id)
        {
            try
            {
                var producto = await Ventas.GetVentasById(id);
                Response.Result = producto;
                Response.DisplayMessage = "producto por ID";
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(Response);
        }

        [HttpGet("RetornarOV")]
        public async Task<ActionResult<IEnumerable<OrderHed>>> GetByIDOrderDtl(int id_Ov)
        {
            try
            {
                var producto = await Ventas.GetOrderDtlByID_Ov(id_Ov);
                Response.Result = producto;
                Response.DisplayMessage = $"Lista de OV N° {id_Ov}";
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(Response);
        }

        // PUT: api/Ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentas(int id, OrderHed Ventas_)
        {
            try
            {
                var producto = await Ventas.UpdateVenta(id, Ventas_);
                Response.Result = producto;
                Response.DisplayMessage = $"El Producto {producto.Id} ha sido actualizado correctamente";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de actualizar el producto {Ventas_.Id}, " +
                    $"por favor contate al administrador.";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }
        [HttpPut("UpdateLine")]
        public async Task<IActionResult> putlinepedido(OrderDtl orderDtl)
        {
            try
            {
                var line = await Ventas.UpdateLinea(orderDtl);
                Response.Result = line;
                Response.DisplayMessage = $"La linea N° {orderDtl.OrderLine} del pedido N° {orderDtl.ID_OrderHed} ha sido actualizado correctamente";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de actualizar la linea N° {orderDtl.OrderLine} del pedido N° {orderDtl.ID_OrderHed}. \n" +
                    $"Por favor contacte al adminstrador";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CrearPedido")]
        public async Task<ActionResult<string>> PostVentas(OrderHed Ventas_)
        {
            try
            {
                var producto = await Ventas.CreateVenta(Ventas_);
                Response.Result = producto;
                Response.DisplayMessage = $"Se ha creado satisfactoriamente el pedido.";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de crear el pedido," +
                    $"por favor contacte con el administrador";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        [HttpPost("CrearLineaPedido")]
        public async Task<ActionResult<string>> PostVentasDetalle(OrderDtl orderDtl)
        {
            try
            {
                var orderdtl = await Ventas.venta(orderDtl);
                Response.Result = orderdtl;
                Response.DisplayMessage = $"Se ha creado satisfactoiamente la linea del pedido.";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de crear la linea del pedido," +
                    $"por favor contacte con el administrador";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }
        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentas(int id)
        {
            try
            {
                bool estadoeliminado = await Ventas.DeleteVentas(id);
                if (estadoeliminado)
                {
                    Response.Result = estadoeliminado;
                    Response.DisplayMessage = $"El producto con id N°{id} ha sido eliminado satisfactoriamente";
                    return Ok(Response);
                }
                else
                {
                    Response.IsSuccess = false;
                    Response.DisplayMessage = $"Se ha produccido un error al momento de eliminar el producto con identificador {id}," +
                        $" por favor contacte al administrador.";
                    return BadRequest(Response);
                }
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }
        [HttpDelete("DeleteLinePedido")]
        public async Task<IActionResult> DeleteLinePedido(OrderDtl orderDtl)
        {
            try
            {
                var lineDelete = await Ventas.deleteorderline(orderDtl);
                Response.Result = lineDelete;
                Response.DisplayMessage = "Se elimino correctamente";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se ha producido un error al momento de eliminar la linea N° {orderDtl.OrderLine}" +
                    $" de la orden N° {orderDtl.ID_OrderHed}.\n" +
                    "Por favor contactese con el administrador.";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        
    }
}
