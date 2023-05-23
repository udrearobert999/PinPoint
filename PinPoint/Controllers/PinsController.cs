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
        public async Task<IActionResult> AddComment(CommentDto commentDto)
        {
            await _pinsService.AddComment(commentDto);
            return RedirectToAction("Index"); // You might need to adjust this based on your project
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
                    TempData["Message"] = "Pin was successfully created.";

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Failed to create pin.";

                return RedirectToAction("Index");
            }

            TempData["Message"] = "Failed to create pin. No picture was provided.";
            return RedirectToAction("Index");
        }
    }
}