using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class AbonoService
    {
        private readonly AppDbContext _context;

        public AbonoService(AppDbContext context)
        {
            _context = context;
        }

        // Realizar un abono a un crédito específico
        public async Task<bool> RealizarAbono(Abono abono)
        {
            var credito = await _context.Creditos.FindAsync(abono.CreditoId);
            if (credito == null)
            {
                throw new Exception("El crédito no existe.");
            }

            abono.FechaAbono = DateTime.UtcNow;
            _context.Abonos.Add(abono);

            // Actualizar el saldo pendiente del crédito
            credito.SaldoPendiente -= abono.Monto;

            await _context.SaveChangesAsync();
            return true;
        }


        // Obtener todos los abonos de un crédito específico
        public async Task<IEnumerable<Abono>> ObtenerAbonosPorCredito(int creditoId)
        {
            return await _context.Abonos
                .Where(a => a.CreditoId == creditoId)
                .ToListAsync();
        }

        // Obtener el total abonado por cada crédito
        public async Task<Dictionary<int, decimal>> ObtenerTotalAbonadoPorCredito()
        {
            return await _context.Abonos
                .GroupBy(a => a.CreditoId)
                .Select(g => new { CreditoId = g.Key, TotalAbonado = g.Sum(a => a.Monto) })
                .ToDictionaryAsync(g => g.CreditoId, g => g.TotalAbonado);
        }

        // Obtener el total de abonos realizados y la cantidad total abonada
        public async Task<(int cantidadAbonos, decimal totalAbonado)> ObtenerEstadisticasAbonos()
        {
            var cantidadAbonos = await _context.Abonos.CountAsync();
            var totalAbonado = await _context.Abonos.SumAsync(a => a.Monto);
            return (cantidadAbonos, totalAbonado);
        }
    }
}
