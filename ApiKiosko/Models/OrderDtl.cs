using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKiosko.Models
{
    public class OrderDtl
    {
        public int Id { get; set; }
        [ForeignKey("OrderHed")]
        public int ID_OrderHed { get; set; }
        public int OrderLine { get; set; }
        [ForeignKey("Producto")]
        public int Id_Producto { get; set; }

        public string Nom_Prod {  get; set; }
        public decimal Cantidad { get; set; }
        public decimal Valor {  get; set; }
        public decimal Impuestos { get; set; }
    }
}
