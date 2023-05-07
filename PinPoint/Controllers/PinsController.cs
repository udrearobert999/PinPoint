using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace PinPoint.Controllers
{
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

        public async Task<IActionResult> Create([Bind("Title,Picture")] PinDto pinDto)
        {
            if (ModelState.IsValid)
            {
                await _pinsService.CreatePin(pinDto);
                return RedirectToAction("Index", pinDto);
            }

            return BadRequest(pinDto);
        }
    }
}