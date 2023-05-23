using Domain.Entities;

namespace Domain.Core
{
    public interface IPinsRepository : IRepository<Pin, Guid>
    {
        public Task<List<Pin>> GetPinsByTitleString(string title);
        public Task<List<Pin>> GetLastPins(int numberOfPins);
    }
}
