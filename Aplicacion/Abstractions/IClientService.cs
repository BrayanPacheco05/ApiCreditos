using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Abstractions
{
    public interface IClientService
    {
        Task<Cliente> CreateClientAsync(Cliente client);
        Task<Cliente> GetClientByIdAsync(int clientId);
        Task<IEnumerable<Cliente>> GetAllClientsAsync();
        Task<bool> DeleteClientAsync(int clientId);
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<bool> RegistrarCliente(Cliente cliente);
    }
}
