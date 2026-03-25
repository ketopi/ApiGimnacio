using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Repositories.ClienteRepository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task<Cliente?> GetClienteByIdAsync(int id); 
        Task<Cliente> CreateAsync(Cliente cliente);
        Task<Cliente> UpdateAsync(Cliente cliente);   

        Task DeleteAsync(Cliente cliente);
    
        
    }
}