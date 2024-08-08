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
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        //private readonly ApplicationDbContext _db;
        private readonly IVillaRepositorio _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepo,IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Obtener todos los Espacios");
                //return Ok(VillaStore.villaList);

                IEnumerable<Villa> villaList = await _villaRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<VillaDto>>(villaList);
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
        [HttpGet("{id:int}", Name = "GetVilla")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Espacio con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                
                var villa = await _villaRepo.Obtener(v => v.Id == id);
                

                if (villa == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<VillaDto>(villa);
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
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //if (VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
                if (await _villaRepo.Obtener(v => v.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "La vista con ese nombre ya existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                //villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
                //VillaStore.villaList.Add(villaDto);

                Villa modelo = _mapper.Map<Villa>(createDto);

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

                await _villaRepo.Crear(modelo);
                _response.Resultado= modelo;
                _response.statusCode=HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = modelo.Id }, _response);
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
        public async Task<IActionResult> DeleteVilla(int id)
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
                var villa = await _villaRepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                //VillaStore.villaList.Remove(villa);
                /*_db.Villas.Remove(villa);   
                await _db.SaveChangesAsync(); */
                
                await _villaRepo.Remover(villa);
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
        
        public async Task <IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;   

                return BadRequest(_response);
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //villa.Nombre= villaDto.Nombre;
            //villa.Ocupantes= villaDto.Ocupantes;
            //villa.MetrosCuadrados = villaDto.MetrosCuadrados;

            if (await _villaRepo.Obtener(V=>V.Id==updateDto.Id,false)==null)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages= new List<string>() { "el Id no existe" };
                return BadRequest(_response);

            }

            Villa modelo=_mapper.Map<Villa>(updateDto); 


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
           
           await _villaRepo.Actualizar(modelo);
            _response.statusCode=HttpStatusCode.NoContent;

            return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task <IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id ==0)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
            var villa = await _villaRepo.Obtener(v => v.Id == id,tracked:false);
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);           
            if(villa==null) { return BadRequest(); }    

            patchDto.ApplyTo(villaDto,ModelState);

            if(!ModelState.IsValid) 
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Villa modelo = _mapper.Map<Villa>(villaDto);
            /*_db.Update(modelo);
            await _db.SaveChangesAsync();*/
            await  _villaRepo.Actualizar(modelo);
            _response.statusCode=HttpStatusCode.NoContent;;
            return Ok(_response);
        }
    }
}
