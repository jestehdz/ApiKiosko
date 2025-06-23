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
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes(string? id)
        {
            try
            {
                //return await _context.Clientes.ToListAsync();
                var lista = await Clientes.GetClientes(id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClienteById(int id)
        {
            try
            {
                var producto = await Clientes.GetClienteById(id);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutClientes(List< Clientes> Clientes_)
        {
            try
            {
                var producto = await Clientes.UpdateClientes( Clientes_);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de actualizar los Clientes, " +
                    $"por favor contate al administrador.";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(List<Clientes> Clientes_)
        {
            try
            {
                var producto = await Clientes.CreateClientes(Clientes_);                
                return Ok(producto);
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de crear los Clientes," +
                    $"por favor contacte con el administrador";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // DELETE: api/Clientes/5
        [HttpDelete]
        public async Task<IActionResult> DeleteClientes(List<Clientes> clientes)
        {
            try
            {
                var estadoeliminado = await Clientes.DeleteCliente(clientes);                
                return Ok(estadoeliminado);
                
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se ha produccido un error al momento de eliminar el Cliente con identificador ," +
                        $" por favor contacte al administrador.";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }
    }
}
