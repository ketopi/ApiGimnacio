
using Backend_Gimnacio.Data;
using Backend_Gimnacio.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gimnacio.Repositories.MembresiaRepository
{
    public class MembresiaRepository : IMembresiaRepository
    {
        private readonly ApplicationDbContext _context;

        public MembresiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //metodo para obtener
        public async Task<IEnumerable<Membresia>> GetMembresiasAsync()
        {
            return await _context.Membresias.
            AsNoTracking()
            .ToListAsync();       
            
        }

        //metodo para obtener por id
        public async Task<Membresia?> GetMembresiaByIdAsync(int id)
        {
            return await _context.Membresias.
            AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);      
            
        }
        
        //metodo para crear
        public async Task<Membresia> CreateAsync(Membresia membresia)
        {
            _context.Membresias.Add(membresia);
            await _context.SaveChangesAsync();
            return membresia;
        }






        
    }
}