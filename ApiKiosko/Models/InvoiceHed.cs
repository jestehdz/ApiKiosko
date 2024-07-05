using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKiosko.Models
{
    public class InvoiceHed
    {

        public int Id { get; set; }
        [ForeignKey("OrderHed")]
        public int OrderNum { get; set; }
        public int CliNum { get; set; }
        public string NomCli { get; set; }
        public string NitCli { get; set; }
        public string Direccion {  get; set; }
        public string Numero { get; set; }
        public string Correo { get; set; }

    }
}
