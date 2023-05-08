﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Application.Dto;
using PinPoint.ErrorHandling;
using Application.Interfaces;

namespace PinPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPinsService _pinsService;

        public HomeController(ILogger<HomeController> logger, IPinsService pinsService)
        {
            _logger = logger;
            _pinsService = pinsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            var model = new AboutDto
            {
                Title = "About PinPoint"
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<IActionResult> Search(string titleQuery)
        {
            if (string.IsNullOrEmpty(titleQuery))
            {
                return RedirectToAction("Index");
            }

            var pins = await _pinsService.GetPinsByTitleString(titleQuery);
            return View("Index", pins);
        }
    }
}