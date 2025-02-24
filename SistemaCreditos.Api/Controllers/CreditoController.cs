using Aplication.Services;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/creditos")]
    public class CreditoController : ControllerBase
    {
        private readonly CreditoService _creditoService;

        public CreditoController(CreditoService creditoService)
        {
            _creditoService = creditoService;
        }

        // Obtener todos los créditos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credito>>> ObtenerTodos()
        {
            var creditos = await _creditoService.ObtenerTodosAsync();
            return Ok(creditos);
        }

        // Aprobar un crédito
        [HttpPut("{id}/aprobar")]
        public async Task<IActionResult> AprobarCredito(int id)
        {
            var aprobado = await _creditoService.AprobarCredito(id);
            if (!aprobado)
                return NotFound(new { mensaje = "Crédito no encontrado o ya aprobado" });

            return Ok(new { mensaje = "Crédito aprobado correctamente" });
        }

        // Solicitar un nuevo crédito
        [HttpPost]
        public async Task<IActionResult> SolicitarCredito([FromBody] CrearCreditoDto crearCreditoDto)
        {
            Console.WriteLine("YA QUEDO1ASDASD");
            if (crearCreditoDto == null)
                return BadRequest(new { mensaje = "Datos inválidos" });

            if (crearCreditoDto.Monto <= 0)
                return BadRequest(new { mensaje = "El monto debe ser mayor que 0" });

            if (crearCreditoDto.TasaInteres <= 0)
                return BadRequest(new { mensaje = "La tasa de interés debe ser mayor que 0" });

            if (crearCreditoDto.PlazoMeses <= 0)
                return BadRequest(new { mensaje = "El plazo debe ser mayor que 0" });

            // Validar existencia del cliente
            var clienteExistente = await _creditoService.GetClienteByIdAsync(crearCreditoDto.ClienteId);
            if (clienteExistente == null)
                return NotFound(new { mensaje = "Cliente no encontrado" });

            // Mapear el DTO a la entidad Credito
            var credito = new Credito
            {
                ClienteId = crearCreditoDto.ClienteId,
                Monto = crearCreditoDto.Monto,
                TasaInteres = crearCreditoDto.TasaInteres,
                PlazoMeses = crearCreditoDto.PlazoMeses,
                SaldoPendiente = crearCreditoDto.Monto, // El saldo pendiente inicial es igual al monto
                Estado = "Pendiente", // Estado inicial
                FechaSolicitud = DateTime.UtcNow
            };
            try
            {
                await _creditoService.SolicitarCredito(credito);
                return CreatedAtAction(nameof(ObtenerTodos), new { id = credito.Id }, credito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }



        // Obtener cantidad de créditos por cliente
        [HttpGet("estadisticas/creditos-por-cliente")]
        public async Task<ActionResult<Dictionary<int, int>>> ObtenerCreditosPorCliente()
        {
            var estadisticas = await _creditoService.ObtenerCreditosPorCliente();
            return Ok(estadisticas);
        }
    }
}
