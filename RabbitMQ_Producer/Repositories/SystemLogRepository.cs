using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedLibrary;
using SharedLibrary.Models.Entities;

namespace RabbitMQ_Producer.Repositories
{
    public class SystemLogRepository
    {
        private readonly ApplicationDbContext _context;

        public SystemLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogSystemEntity>> GetAllLogSystem()
        {
            return await _context.logSystems.ToListAsync();
        }

        public async Task<IEnumerable<LogSystemEntity>> GetLogWithPagination(int pageSize)
        {
            var query = _context.logSystems.AsQueryable();
            query = query.Where(log => log.IsSuccess == null);
            return await query.Skip(0 * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<LogSystemEntity> GetLogSystemById(string Id)
        {
            var result = await _context.logSystems.FirstOrDefaultAsync(o => o.Id == Id);
            return result;
        }
    }
}
