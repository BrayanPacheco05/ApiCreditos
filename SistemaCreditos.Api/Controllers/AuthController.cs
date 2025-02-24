using Aplication.Services;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaCreditos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO usuario)
        {
            var user = _authService.ValidarUsuario(usuario);
            if (user != null) // Simulación de Autenticación
            {
                var token = _authService.GenerarToken(user);
                return Ok(new { token });
            }

            return Unauthorized("Credenciales inválidas");
        }
    }
}
