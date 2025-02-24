using Aplication.Services;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Roles = "Cliente,Admin")]
    [ApiController]
    [Route("api/abonos")]
    public class AbonoController : ControllerBase
    {
        private readonly AbonoService _abonoService;

        public AbonoController(AbonoService abonoService)
        {
            _abonoService = abonoService;
        }

        // Realizar un abono
        [HttpPost]
        public async Task<IActionResult> RealizarAbono([FromBody] AbonoDTO abonoDto)
        {
            if (abonoDto == null)
                return BadRequest(new { mensaje = "Datos inválidos" });

            try
            {
                var abono = new Abono
                {
                    CreditoId = abonoDto.CreditoId,
                    Monto = abonoDto.Monto,
                    MetodoPago = abonoDto.MetodoPago,
                    FechaAbono = DateTime.UtcNow
                };

                await _abonoService.RealizarAbono(abono);
                return Ok(new { mensaje = "Abono realizado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        // Obtener abonos por crédito
        [HttpGet("credito/{creditoId}")]
        public async Task<ActionResult<IEnumerable<Abono>>> ObtenerAbonosPorCredito(int creditoId)
        {
            var abonos = await _abonoService.ObtenerAbonosPorCredito(creditoId);
            return Ok(abonos);
        }

        // Obtener el total abonado por cada crédito
        [HttpGet("estadisticas/total-abonado-por-credito")]
        public async Task<ActionResult<Dictionary<int, decimal>>> ObtenerTotalAbonadoPorCredito()
        {
            var totalAbonado = await _abonoService.ObtenerTotalAbonadoPorCredito();
            return Ok(totalAbonado);
        }

        // Obtener estadísticas generales de abonos
        [HttpGet("estadisticas")]
        public async Task<ActionResult<object>> ObtenerEstadisticasAbonos()
        {
            var (cantidadAbonos, totalAbonado) = await _abonoService.ObtenerEstadisticasAbonos();
            return Ok(new { cantidadAbonos, totalAbonado });
        }
    }
}
