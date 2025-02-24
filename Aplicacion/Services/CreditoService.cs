using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class CreditoService
    {
        private readonly AppDbContext _context;

        public CreditoService(AppDbContext context)
        {
            _context = context;
        }
        public decimal CalcularCuotaMensual(decimal montoPrestamo, decimal tasaInteresAnual, int plazoMeses)
        {
            if (montoPrestamo <= 0 || tasaInteresAnual <= 0 || plazoMeses <= 0)
                throw new ArgumentException("Los valores del préstamo, tasa de interés y plazo deben ser positivos.");

            // Calcular la tasa mensual
            decimal tasaMensual = tasaInteresAnual / 100 / 12;

            // Fórmula del Método Francés
            decimal cuotaMensual = (montoPrestamo * tasaMensual) / (1 - (decimal)Math.Pow((double)(1 + tasaMensual), -plazoMeses));

            return cuotaMensual;
        }

        // Obtener todos los créditos
        public async Task<List<Credito>> ObtenerTodosAsync()
        {
            return await _context.Creditos
                .Include(c => c.Cliente)
                .ToListAsync();
        }

        // Aprobar crédito si está pendiente
        public async Task<bool> AprobarCredito(int id)
        {
            var credito = await _context.Creditos.FindAsync(id);
            if (credito == null || credito.Estado != "Pendiente") return false;

            credito.Estado = "Aprobado";
            await _context.SaveChangesAsync();
            return true;
        }

        // Solicitar un nuevo crédito
        public async Task<bool> SolicitarCredito(Credito credito)
        {
            var cliente = await _context.Clientes.FindAsync(credito.ClienteId);
            if (cliente == null)
            {
                throw new Exception("El cliente no existe.");
            }

            credito.Estado = "Pendiente";
            credito.FechaSolicitud = DateTime.UtcNow;
            credito.SaldoPendiente = credito.Monto;

        
            credito.CuotaMensual = CalcularCuotaMensual(credito.Monto, credito.TasaInteres, credito.PlazoMeses);

            _context.Creditos.Add(credito);
            await _context.SaveChangesAsync();
            return true;
        }



        // Obtener la cantidad de créditos por cliente
        public async Task<Dictionary<int, int>> ObtenerCreditosPorCliente()
        {
            return await _context.Creditos
                .GroupBy(c => c.ClienteId)
                .Select(g => new { ClienteId = g.Key, Cantidad = g.Count() })
                .ToDictionaryAsync(g => g.ClienteId, g => g.Cantidad);
        }
        public async Task<Cliente> GetClienteByIdAsync(int clienteId)
        {
            return await _context.Clientes.FindAsync(clienteId);
        }


    }
}
