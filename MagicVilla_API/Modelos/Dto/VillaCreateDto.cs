using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.Dto
{
    public class VillaCreateDto
    {
   
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public String Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public String ImageUrl { get; set; }
        public string Amenidad { get; set; }

       

    }
}
