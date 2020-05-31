using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test_Alevel.ApiService.BasketService;
using Test_Alevel.DTOs;

namespace Test_Alevel.Controllers
{
    public class BasketController : Controller
    {
        private readonly BasketApiService basketApiService;

        public BasketController(BasketApiService basketApiService)
        {
            this.basketApiService = basketApiService;
        }
        [HttpGet]
        public IActionResult AddBasket()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBasket(AddToBasketDto addToBasketDto)
        {
            var a = await basketApiService.Basket(addToBasketDto);
            return Json(a);
        }
    }
}
