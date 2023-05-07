using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PinsRepository: Repository<Pin, Guid>, IPinsRepository
    {
        public PinsRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Pin>> GetPinsByTitleString(string title)
        {
            var pins = await _dbContext.Pins
                .Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return pins;
        }
    }
}
