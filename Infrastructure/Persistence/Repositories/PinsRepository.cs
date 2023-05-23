using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PinsRepository : Repository<Pin, Guid>, IPinsRepository
    {
        public PinsRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Pin>> GetPinsByTitleString(string title)
        {
            var pins = await _dbContext.Pins
                .Where(p => EF.Functions.Like(p.Title, $"%{title}%"))
                .ToListAsync();

            return pins;
        }

        public async Task<List<Pin>> GetLastPins(int numberOfPins)
        {
            var pins = await _dbContext.Pins
                .OrderByDescending(pin => pin.CreatedDate)
                .Take(numberOfPins)
                .ToListAsync();

            return pins;
        }
    }
}