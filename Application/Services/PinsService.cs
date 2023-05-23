using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Domain.Core;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class PinsService : IPinsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public PinsService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddComment(CommentDto commentDto)
        {
            var userClaim = _httpContextAccessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(userClaim);

            if (user is null) return;

            var pinComment = new PinComment
            {
                CommentMessage = commentDto.Message,
                PinId = commentDto.Id,
                UserId = user.Id
            };

            _unitOfWork.PinsComment.Add(pinComment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreatePin(PinDto pinDto)
        {
            var pin = _mapper.Map<Pin>(pinDto);
            var userClaim = _httpContextAccessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(userClaim);

            if (user is null) return;

            pin.UserId = user.Id;

            _unitOfWork.Pins.Add(pin);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<PinDto>> GetPinsByTitleString(string query)
        {
            var pins = await _unitOfWork.Pins.GetPinsByTitleString(query);

            return _mapper.Map<List<PinDto>>(pins);
        }

        public async Task<List<PinDto>> GetLastPins(int numberOfPins)
        {
            var pins = await _unitOfWork.Pins.GetLastPins(numberOfPins);

            return _mapper.Map<List<PinDto>>(pins);
        }

        public async Task<IEnumerable<PinDto>> GetAllPinsAsync()
        {
            var pins = await _unitOfWork.Pins.GetAllAsync();
            return _mapper.Map<IEnumerable<PinDto>>(pins);
        }

        public async Task<PinDto?> GetPinByIdAsync(Guid id)
        {
            var pin = await _unitOfWork.Pins.GetByIdAsync(id);
            return _mapper.Map<PinDto>(pin);
        }

        public async Task UpdatePinAsync(PinDto pinDto)
        {
            var pin = _mapper.Map<Pin>(pinDto);
            _unitOfWork.Pins.Update(pin);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePinAsync(Guid id)
        {
            var pin = await _unitOfWork.Pins.GetByIdAsync(id);
            if (pin != null)
            {
                _unitOfWork.Pins.Delete(pin);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}