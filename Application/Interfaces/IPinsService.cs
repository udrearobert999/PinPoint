using Application.Dto;

namespace Application.Interfaces
{
    public interface IPinsService
    {
        public Task CreatePin(PinDto pinDto);
        public Task<List<PinDto>> GetPinsByTitleString(string query);
        public Task<List<PinDto>> GetLastPins(int numberOfPins);

        public Task AddComment(CommentDto commentDto);
        public Task<IEnumerable<PinDto>> GetAllPinsAsync();
        public Task<PinDto?> GetPinByIdAsync(Guid id);
        public Task UpdatePinAsync(PinDto pinDto);
        public Task DeletePinAsync(Guid id);
    }
}
