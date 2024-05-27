using MagicVilla_API.Modelos.Dto;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto{ Id = 1,Nombre="Espacio de Oficina",Ocupantes=2,MetrosCuadrados=20},
            new VillaDto{ Id = 2,Nombre="Espacio del GYM",Ocupantes=5,MetrosCuadrados=50},    
            new VillaDto{ Id = 3,Nombre="Espacio de Cocina",Ocupantes=4,MetrosCuadrados=30}
        };
    }
}
