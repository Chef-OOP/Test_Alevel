using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_Alevel.ApiService.MasterCategoryService;
using Test_Alevel.DTOs;
using Test_Alevel.Filter;

namespace Test_Alevel.Controllers
{
    public class MasterCategoryController : Controller
    {
        private readonly MasterCategoryApi _masterCategoryApi;
        HttpClient HttpClient = new HttpClient();
        public MasterCategoryController(MasterCategoryApi masterCategoryApi)
        {
            _masterCategoryApi = masterCategoryApi;
        }
        [JwtAuth]
        public async Task<IActionResult> Index()
        {
            var master =await _masterCategoryApi.GetAllAsync();
            return View(master);
        }

        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }
      
    }
}
