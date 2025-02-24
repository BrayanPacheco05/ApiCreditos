using Aplication.Abstractions;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SistemaCreditos.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUserService _usuarioService;

        // Constructor del controlador donde se inyecta el servicio
        public UsuarioController(IUserService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Acción para registrar un usuario
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioCreateDTO usuarioDto)
        {
            try
            {
                // Crear el nuevo usuario
                var nuevoUsuario = new Usuario
                {
                    Nombre = usuarioDto.Nombre,
                    Email = usuarioDto.Email,
                    // Llamamos al servicio para registrar el usuario y hashear la contraseña
                };

                // Registrar el usuario usando el servicio y la lógica de hash de la contraseña
                var usuarioRegistrado = await _usuarioService.RegisterUserAsync(nuevoUsuario, usuarioDto.Password);

                return Ok(usuarioRegistrado); // Retorna el usuario registrado
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // En caso de error, devuelve un BadRequest
            }
        }

        // Obtener todos los usuarios
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerTodosAsync();
            return Ok(usuarios);
        }
    }
}
