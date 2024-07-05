using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKiosko.Models
{
    public class InvoiceDtl
    {
        public int Id { get; set; }
        [ForeignKey("InvoiceHed")]
        public int InvoiceNum { get; set; }
        public int invoiceLine {  get; set; }
        [ForeignKey("OrderHed")]

        public int ordernum { get; set; }
        [ForeignKey("OrderDtl")]
        public int orderline {  get; set; }
        public int Cod_prod { get; set; }
        public string Nom_prod { get; set; }
        public decimal cantidad { get; set; }
        public decimal valor {  get; set; }
        public decimal impuestos { get; set; }
        public decimal fechaCrea { get; set; }
    }
}
