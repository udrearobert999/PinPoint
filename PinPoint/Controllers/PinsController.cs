using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        public async Task<IActionResult> Create(PinDto pinDto)
        {
            using var ms = new MemoryStream();
            await pinDto.PictureUpload.CopyToAsync(ms);
            pinDto.Picture = ms.ToArray();

            if (pinDto.Picture.Length > 0)
            {
                await _pinsService.CreatePin(pinDto);
                return RedirectToAction("Index", pinDto);
            }

            return BadRequest(pinDto);
        }
    }
}