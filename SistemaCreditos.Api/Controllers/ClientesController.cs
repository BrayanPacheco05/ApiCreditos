using Aplication.Abstractions;
using Aplication.Services;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaCreditos.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientService _clienteService;

        public ClientesController(IClientService clienteService)
        {
            _clienteService = clienteService;
        }

        //[Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> ObtenerClientes()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            return Ok(clientes);
        }

        [HttpPost("registrarCliente")]
        public async Task<IActionResult> RegistrarCliente([FromBody] ClienteCreateDTO clienteDto)
        {
            try
            {
                var cliente = new Cliente
                {
                    Nombre = clienteDto.Nombre,
                    Documento = clienteDto.Documento,
                    Telefono = clienteDto.Telefono,
                    UsuarioId = clienteDto.UsuarioId
                };

                var nuevoCliente = await _clienteService.RegistrarCliente(cliente);
                return Ok(nuevoCliente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Esto devolverá el mensaje del error
            }
        }
    }
}

