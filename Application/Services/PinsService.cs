using System.Transactions;
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

        public async Task<CommentDto> AddComment(CommentDto createCommentDto)
        {
            var userClaim = _httpContextAccessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(userClaim);

            if (user is null) return new CommentDto();

            var pinComment = new PinComment
            {
                CommentMessage = createCommentDto.CommentMessage,
                PinId = createCommentDto.Id,
                UserId = user.Id
            };

            _unitOfWork.PinsComment.Add(pinComment);
            await _unitOfWork.SaveChangesAsync();

            var commentDto = new CommentDto
            {
                CommentMessage = createCommentDto.CommentMessage,
                Id = createCommentDto.Id,
                UserName = user.UserName!,
                ProfilePicture = Convert.ToBase64String(user.ProfilePicture ?? Array.Empty<byte>())
            };

            return commentDto;
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

        public async Task<List<CommentDto>> GetCommentsByPinId(Guid pinId)
        {
            var comments = await _unitOfWork.PinsComment.GetAsync(pinComment => pinComment.PinId == pinId);
            var commentsDtos = new List<CommentDto>();
            foreach (var comment in comments)
            {
                var user = await _userManager.FindByIdAsync(comment.UserId.ToString());
                if (user is null)
                    continue;

                var commentDto = new CommentDto
                {
                    CommentMessage = comment.CommentMessage,
                    Id = comment.Id,
                    UserName = user.UserName!,
                    ProfilePicture = Convert.ToBase64String(user.ProfilePicture ?? Array.Empty<byte>())
                };
                commentsDtos.Add(commentDto);
            }

            return commentsDtos;
        }
    }
}