using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKiosko.Models
{
    public class OrderHed
    {
        public int Id { get; set; }
        [ForeignKey("Clientes")]
        public int id_cliente { get; set; }
        public string cliente { get; set; }
        public string nit { get; set; }
        public string direccion {  get; set; }       
        public DateTime fecha { get; set; }
        public bool invoice {  get; set; }
    }
}
