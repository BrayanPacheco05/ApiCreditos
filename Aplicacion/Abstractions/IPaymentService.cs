using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Abstractions
{
    public interface IPaymentService
    {
        Task<Abono> MakePaymentAsync(int clientId, decimal amount);
        Task<IEnumerable<Abono>> GetPaymentsByClientAsync(int clientId);
    }
}
