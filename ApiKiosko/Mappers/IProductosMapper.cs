using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiKiosko.Mappers
{

    public class IProductosMapper : IProductos
    {
        private readonly AppDbContext appDb;

        public IProductosMapper(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<List<Productos>> GetProductos()
        {
            List<Productos> productos = await appDb.Productos.ToListAsync();
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

        public async Task<bool> DeleteProducto(int Id)
        {

            Productos productos = await appDb.Productos.FindAsync(Id);
            if (productos == null)
            {
                return false;
            }
            appDb.Productos.Remove(productos);
            await appDb.SaveChangesAsync();
            return true;
        }
        public async Task<Productos> CreateProductos(Productos productos)
        {
            Productos productos_ = productos;
            appDb.Productos.Add(productos_);
            await appDb.SaveChangesAsync();
            return productos_;
        }
        public async Task<Productos> UpdateProductos(int id, Productos producto)
        {
            if (id != producto.Id)
            {
                producto.Id = id;
            }
            appDb.Productos.Update(producto);
            await appDb.SaveChangesAsync();
            return producto;
            //Productos productos_ = //appDb.Productos.Update(producto);
            return null;

        }

    }
}
