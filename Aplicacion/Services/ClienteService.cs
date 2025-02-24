using Aplication.Abstractions;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ClientService> _logger;

        public ClientService(AppDbContext context, ILogger<ClientService> logger)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Inyección de logger 
        }

        public async Task<Cliente> CreateClientAsync(Cliente client)
        {
            _context.Clientes.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Cliente> GetClientByIdAsync(int clientId)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<IEnumerable<Cliente>> GetAllClientsAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<bool> DeleteClientAsync(int clientId)
        {
            var client = await GetClientByIdAsync(clientId);
            if (client != null)
            {
                _context.Clientes.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<bool> RegistrarCliente(Cliente cliente)
        {
            try
            {
                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al registrar cliente: {ex.Message}");
                return false;
            }
        }

        public async Task<int> ObtenerCreditosPorCliente(int clienteId)
        {
            return await _context.Creditos.CountAsync(c => c.ClienteId == clienteId);
        }
    }
}
