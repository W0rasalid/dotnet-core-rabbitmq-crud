using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using RabbitMQ_Consumer.Models.Entities;
using SharedLibrary;
using SharedLibrary.Models.Entities;
namespace RabbitMQ_Consumer.Repositories
{
    public class LogSystemRepository
    {
        private readonly ApplicationDbContext _context;

        public LogSystemRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LogSystemEntity> GetLogSystemById(string Id)
        {
            try
            {
                var result = await _context.logSystems.FirstOrDefaultAsync(o => o.Id == Id);
                if (result == null)
                {
                    throw new KeyNotFoundException($"LogSystem with ID '{Id}' not found.");
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<LogSystemEntity> UpdateLogSystem(string Id)
        {
            try
            {
                var result = await _context.logSystems.FirstOrDefaultAsync(o => o.Id == Id);
                if (result == null)
                {
                    throw new KeyNotFoundException($"LogSystem with ID '{Id}' not found.");
                }

                result.Reference = "Updated";
                result.IsSuccess = true;

                await _context.SaveChangesAsync();
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
