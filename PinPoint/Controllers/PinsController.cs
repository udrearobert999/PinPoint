using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Services;

namespace PinPoint.Controllers
{
    [Authorize]
    public class PinsController : Controller
    {
        private readonly IPinsService _pinsService;

        public PinsController(IPinsService pinsService)
        {
            _pinsService = pinsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDto createCommentDto)
        {
            var comment = await _pinsService.AddComment(createCommentDto);
            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(Guid pinId)
        {
            var comments = await _pinsService.GetCommentsByPinId(pinId);

            return Ok(comments);
        }

        public async Task<IActionResult> Create(PinDto pinDto)
        {
            try
            {
                using var ms = new MemoryStream();
                await pinDto.PictureUpload.CopyToAsync(ms);
                pinDto.Picture = ms.ToArray();

                if (pinDto.Picture.Length > 0)
                {
                    await _pinsService.CreatePin(pinDto);
                    TempData["CommentMessage"] = "Pin was successfully created.";

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["CommentMessage"] = "Failed to create pin.";

                return RedirectToAction("Index");
            }

            TempData["CommentMessage"] = "Failed to create pin. No picture was provided.";
            return RedirectToAction("Index");
        }
    }
}