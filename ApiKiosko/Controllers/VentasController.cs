using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiKiosko.Controllers
{
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
    }
}
