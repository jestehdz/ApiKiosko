using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKiosko.Models
{
    public class Ventas
    {
        public int Id { get; set; }
        [ForeignKey("Clientes")]
        public int id_cliente { get; set; }
        public string cliente { get; set; }
        [ForeignKey("Productos")]
        public int id_producto { get; set; }
        public string producto { get; set; }
        public decimal cantidad { get; set; }
        public decimal Valor { get; set; }
        public DateTime fecha { get; set; }
    }
}
