﻿using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private APIResponse _response;
        public UsuarioController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _response = new();
        }
        [HttpPost("login")]// /api/usuario/login
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO modelo)
        {
            var loginResponse = await _usuarioRepo.Login(modelo);
            if (loginResponse.Usuario==null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.statusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsExitoso= false;
                _response.ErrorMessages.Add("UserName o Password son Incorrectos");
                return BadRequest(_response);
            }
            _response.IsExitoso=true;
            _response.statusCode=System.Net.HttpStatusCode.OK;
            _response.Resultado = loginResponse;
            return Ok(_response);
        }
        [HttpPost("registrar")]// /api/usuario/login
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDTO modelo)
        {
            bool isUsuarioUnico =_usuarioRepo.IsUsuarioUnico(modelo.UserName);
            if(!isUsuarioUnico)
            {
                _response.statusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("¡Usuario ya Existe!");
                return BadRequest(Response);
            }
            var usuario = await _usuarioRepo.Registrar(modelo);
            if (usuario == null)
            {
                _response.statusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("¡Error al registrar el Usuario!");
                return BadRequest(Response);
            }
            _response.statusCode = HttpStatusCode.OK;
            _response.IsExitoso = true;
            return Ok(_response);

        }
    }


}
