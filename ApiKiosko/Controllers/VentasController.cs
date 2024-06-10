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
        public async Task<ActionResult<IEnumerable<Ventas>>> GetVentas()
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
        public async Task<ActionResult<Ventas>> GetClienteById(int id)
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

        // PUT: api/Ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentas(int id, Ventas Ventas_)
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

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> PostVentas(Ventas Ventas_)
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

    }
}
