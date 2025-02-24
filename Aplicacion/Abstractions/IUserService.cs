using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Abstractions
{
    public interface IUserService
    {
        Task<Usuario> RegisterUserAsync(Usuario user, string password);
        Task<Usuario> GetUserByIdAsync(int userId);
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
        Task<bool> RegistrarUsuario(Usuario usuario);
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
    }
}
