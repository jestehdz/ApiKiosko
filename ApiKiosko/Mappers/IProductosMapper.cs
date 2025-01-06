using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace ApiKiosko.Mappers
{

    public class IProductosMapper : IProductos
    {
        private readonly AppDbContext appDb;

        public IProductosMapper(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<List<Productos>> GetProductos(string id)
        {
            List<Productos> productos = await appDb.Productos.ToListAsync();
            if(!string.IsNullOrEmpty(id))
            {
                int ids = Convert.ToInt32(id);
                List<Productos> products = await appDb.Productos.Where(x => x.Id == ids).ToListAsync();
                return products;
            }
            return productos;
        }

        public async Task<Productos> GetProductosById(int Id)
        {
            Productos productos = await appDb.Productos.FindAsync(Id);
            if (productos != null)
            {
                return productos;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Productos>> DeleteProducto(List<Productos> productos)
        {

            List<Productos> productos1 = new List<Productos>();

            foreach(var item in productos)
            {
                Productos productosd = await appDb.Productos.FindAsync(item.Id);
                if (productosd != null)
                {
                    //return false;
                    appDb.Productos.Remove(productosd);
                    await appDb.SaveChangesAsync();
                    productos1.Add(item);
                }
                
            }
            return productos1;
        }
                
        public async Task<List<Productos>> CreateProductos(List<Productos> productos)
        {
            List<Productos> productos1 = new List<Productos>();
            foreach (var item in productos)
            {
                Productos productos_ = item;
                appDb.Productos.Add(productos_);
                await appDb.SaveChangesAsync();
                productos1.Add(productos_);
                //return productos_;
            }
            return productos1;
        }
        public async Task<List<Productos>> UpdateProductos(List<Productos> producto)
        {
            List<Productos> reproductos = new List<Productos>();
            foreach (var item in producto)
            {
                
                    var existingProduct = appDb.Productos
                    .Local
                    .FirstOrDefault(p => p.Id == item.Id);

                    if (existingProduct != null)
                    {
                        // Si la entidad ya está rastreada, solo actualiza sus propiedades
                        appDb.Entry(existingProduct).CurrentValues.SetValues(item);
                    }
                    else
                    {
                        // Si no está rastreada, realiza el seguimiento de la entidad
                        appDb.Productos.Update(item);
                    }

                    reproductos.Add(item);
                              
            }
            await appDb.SaveChangesAsync();
            return reproductos;
        }
        

    }
}
