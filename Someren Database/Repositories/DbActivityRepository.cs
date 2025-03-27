using Microsoft.EntityFrameworkCore;
using Someren_Database.Data;
using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public class DbActivityRepository : IActivityRepository
    {

        private readonly ApplicationDbContext _context;
        public DbActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Someren_Database.Models.Activity>> GetAllActivitesAsync() 
            //use full namespace because Activity is also a built in class in.NET
        {
            return await _context.Activity.ToListAsync();
        }
    }
}
