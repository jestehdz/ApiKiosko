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
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos(string? id)
        {
            try
            {
                //return await _context.Productos.ToListAsync();
                var lista = await Productos.GetProductos(id);
                return Ok(lista);                
            }
            catch (Exception ex)
            {
                Response.IsSuccess=false;
                Response.ErrorMessages=new List<string> { ex.ToString()};
                return BadRequest(Response);
            }
        }        

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutProductos( List<Productos> productos)
        {
            try
            {
                var producto = await Productos.UpdateProductos( productos);
                return Ok(producto);               
            }
            catch(Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de actualizar los productos, " +
                    $"por favor contate al administrador.";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Productos>> PostProductos(List<Productos> productos)
        {
            try
            {
                var producto = await Productos.CreateProductos(productos);                
                return Ok(producto);
            }
            catch( Exception ex)
            {
                Response.IsSuccess = false;
                Response.DisplayMessage = $"Se produjo un error al momento de crear los productos," +
                    $"por favor contacte con el administrador";
                Response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(Response);
            }
        }

        // DELETE: api/Productos/5
        [HttpDelete]
        public async Task<IActionResult> DeleteProductos(List<Productos> productos)
        {
            try
            {
                return Ok(await Productos.DeleteProducto(productos));                 
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
