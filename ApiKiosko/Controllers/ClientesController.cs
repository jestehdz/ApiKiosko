using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiKiosko.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;
        private IClientes Clientes;
        protected Response Response;
        public ClientesController(AppDbContext context, IClientes clientes)
        {
            _context = context;
            Clientes = clientes;
            Response = new Response();
        }
        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            try
            {
                //return await _context.Clientes.ToListAsync();
                var lista = await Clientes.GetClientes();
                Response.Result = lista ;
                Response.DisplayMessage = "Listado de Clientes";
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(Response);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClienteById(int id)
        {
            try
            {
                var producto = await Clientes.GetClienteById(id);
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

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, Clientes Clientes_)
        {
            try
            {
                var producto = await Clientes.UpdateClientes(id, Clientes_);
                Response.Result = producto;
                Response.DisplayMessage = $"El Producto {producto.Nombres} ha sido actualizado correctamente";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de actualizar el producto {Clientes_.Nombres}, " +
                    $"por favor contate al administrador.";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes Clientes_)
        {
            try
            {
                var producto = await Clientes.CreateClientes(Clientes_);
                Response.Result = producto;
                Response.DisplayMessage = $"Se ha creado satisfactoriamente el producto {producto.Nombres} " +
                    $"que cuenta con el ID N°{producto.Id}";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de crear el producto {Clientes_.Nombres}," +
                    $"por favor contacte con el administrador";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientes(int id)
        {
            try
            {
                bool estadoeliminado = await Clientes.DeleteCliente(id);
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
