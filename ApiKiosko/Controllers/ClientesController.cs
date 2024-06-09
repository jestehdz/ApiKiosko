using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiKiosko.Controllers
{
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
    }
}
