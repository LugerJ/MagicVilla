using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaCreateDto
    {
   
        [Required(ErrorMessage ="Nombre es Requerido")]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public String Detalle { get; set; }
        [Required(ErrorMessage ="Tarifa es Requerida")]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public String ImageUrl { get; set; }
        public string Amenidad { get; set; }

       

    }
}
