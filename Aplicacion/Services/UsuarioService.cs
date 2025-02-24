using Aplication.Abstractions;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;  // Especificar el tipo 'Usuario'

        public UserService(AppDbContext context, IPasswordHasher<Usuario> passwordHasher)  // Cambiar a IPasswordHasher<Usuario>
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Usuario> RegisterUserAsync(Usuario user, string password)
        {
            // Hashear la contraseña
            user.PasswordHash = _passwordHasher.HashPassword(user, password); // El primer parámetro debe ser el 'user'
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Usuario> GetUserByIdAsync(int userId)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            // Validar si el usuario ya existe
            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuario.Email);
            if (existingUser != null)
            {
                // No permitir registros con emails duplicados
                return false;
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
