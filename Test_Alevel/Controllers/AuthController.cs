using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test_Alevel.ApiService.AuthService;
using Test_Alevel.DTOs;

namespace Test_Alevel.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthApi authApi;

        public AuthController(AuthApi authApi)
        {
            this.authApi = authApi;
        }
        [HttpPost]
        public async Task<IActionResult> Login2(AppUserLoginDto app)
        {
            if (ModelState.IsValid)
            {
                if (await authApi.Login(app))
                {
                    return View();
                }
            }
            return Content("Login Başarısız");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
