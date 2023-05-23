using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public CommentsService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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
    }
}