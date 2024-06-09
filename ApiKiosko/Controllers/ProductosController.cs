using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiKiosko.Context;
using ApiKiosko.Models;
using ApiKiosko.Interfaces;

namespace ApiKiosko.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IProductos Productos;
        protected Response Response;
        public ProductosController(AppDbContext context, IProductos productos)
        {
            _context = context;
            Productos = productos;
            Response = new Response();
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
        {
            try
            {
                //return await _context.Productos.ToListAsync();
                var lista = await Productos.GetProductos();
                Response.Result = lista;
                Response.DisplayMessage = "Productos";
            }
            catch (Exception ex)
            {
                Response.IsSuccess=false;
                Response.ErrorMessages=new List<string> { ex.ToString()};
            }
            return Ok(Response);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProductosById(int id)
        {
            try
            {
                var producto = await Productos.GetProductosById(id);
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

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductos(int id, Productos productos)
        {
            try
            {
                var producto = await Productos.UpdateProductos(id, productos);
                Response.Result = producto;
                Response.DisplayMessage = $"El Producto {producto.Producto} ha sido actualizado correctamente";
                return Ok(Response);
            }
            catch(Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de actualizar el producto {productos.Producto}, " +
                    $"por favor contate al administrador.";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Productos>> PostProductos(Productos productos)
        {
            try
            {
                var producto = await Productos.CreateProductos(productos);
                Response.Result = producto;
                Response.DisplayMessage = $"Se ha creado satisfactoriamente el producto {producto.Producto} " +
                    $"que cuenta con el ID N°{producto.Id}";
                return Ok(Response);
            }
            catch( Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de crear el producto {productos.Producto}," +
                    $"por favor contacte con el administrador";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductos(int id)
        {
            try
            {
                bool estadoeliminado = await Productos.DeleteProducto(id);
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

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
