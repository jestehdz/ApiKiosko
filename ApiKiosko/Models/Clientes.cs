using System.ComponentModel.DataAnnotations;

namespace ApiKiosko.Models
{
    public class Clientes
    {
       
        public required int Id { get; set; }

        
        public required String Nombres { get; set; }

        
        public  required String Apellidos { get; set; }


        public required string Direccion { get; set; }


        public required string Telefono { get; set; }
        public required string Num_Doc {  get; set; }
        public required string email { get; set; }

    }
}
