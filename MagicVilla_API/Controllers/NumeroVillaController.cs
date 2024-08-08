using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        //private readonly ApplicationDbContext _db;
        private readonly IVillaRepositorio _villaRepo;
        private readonly INumeroVillaRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, INumeroVillaRepositorio numeroRepo,IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroRepo = numeroRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener Numero de entradas al Rio/Lago/Balneario");
                //return Ok(VillaStore.villaList);

                IEnumerable<NumeroVilla> numerovillaList = await _numeroRepo.ObtenerTodos(incluirPropiedades:"Villa");
                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numerovillaList);
                _response.statusCode = System.Net.HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages=new List<string>() { ex.ToString() };                
            }
            return _response;
        }
        [HttpGet("{id:int}", Name = "GetNumeroVilla")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Numero de Entrada con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var numerovilla = await _numeroRepo.Obtener(v => v.VillaNo == id,incluirPropiedades:"Villa");


                if (numerovilla == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<NumeroVillaDto>(numerovilla);
                _response.statusCode=HttpStatusCode.OK; 
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso=false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };               
            }
            return _response;
            
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //if (VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
                if (await _numeroRepo.Obtener(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El numero de entrada ya existe!");
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Obtener(v=>v.Id==createDto.VillaId)==null)
                {
                    ModelState.AddModelError("ErrorMessages", "El id de la entrada no existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                //villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
                //VillaStore.villaList.Add(villaDto);

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);

                /*Villa modelo = new()
                {
                    Nombre = villaDto.Nombre,
                    Detalle = villaDto.Detalle,
                    ImageUrl = villaDto.ImageUrl,
                    Ocupantes = villaDto.Ocupantes,
                    Tarifa = villaDto.Tarifa,
                    MetrosCuadrados = villaDto.MetrosCuadrados,
                    Amenidad = villaDto.Amenidad
                };*/
                /*await _db.Villas.AddAsync(modelo);
                await _db.SaveChangesAsync(); */

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _numeroRepo.Crear(modelo);
                _response.Resultado= modelo;
                _response.statusCode=HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages=new List<string> { ex.ToString()};               
            }
            return _response;
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                // var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
                var numerovilla = await _numeroRepo.Obtener(v => v.VillaNo == id);
                if (numerovilla == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                //VillaStore.villaList.Remove(villa);
                /*_db.Villas.Remove(villa);   
                await _db.SaveChangesAsync(); */
                
                await _numeroRepo.Remover(numerovilla);
                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);

        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task <IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.VillaNo)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;   

                return BadRequest(_response);
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //villa.Nombre= villaDto.Nombre;
            //villa.Ocupantes= villaDto.Ocupantes;
            //villa.MetrosCuadrados = villaDto.MetrosCuadrados;

            if (await _villaRepo.Obtener(V=>V.Id==updateDto.VillaId)==null )
            {
                ModelState.AddModelError("ErrorMessages", "El Id de la villa no existe");
               

              // _response.IsExitoso = false;
               // _response.statusCode = HttpStatusCode.BadRequest;
                //_response.ErrorMessages= new List<string>() { "el Id no existe" };

                return BadRequest(ModelState);
                //return BadRequest(_response);

            }


            NumeroVilla modelo=_mapper.Map<NumeroVilla>(updateDto); 


            /*Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                ImageUrl=villaDto.ImageUrl,
                Amenidad = villaDto.Amenidad
            };*/

           /* _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();*/
           await _numeroRepo.Actualizar(modelo);
            _response.statusCode=HttpStatusCode.NoContent;

            return Ok(_response);
        }

       
    }
}
