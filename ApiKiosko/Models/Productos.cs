namespace ApiKiosko.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public required string Producto { get; set; }
        public required decimal Precio { get; set;}
        public required decimal Stock { get; set;}
        public required decimal Inv_Act { get; set;}
    }
}
