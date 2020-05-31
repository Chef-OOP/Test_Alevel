using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Test_Alevel.DTOs;

namespace Test_Alevel.ApiService.AuthService
{
    public class AuthApi
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor context;

        public AuthApi(
            HttpClient httpClient,
            IHttpContextAccessor context)
        {
            this.httpClient = httpClient;
            this.context = context;
        }
        public async Task<bool> Login(AppUserLoginDto appUser)
        {
            appUser.CustomerId = Convert.ToInt32((context.HttpContext.Request.Cookies["customer"]));
            
            string jsonData = JsonConvert.SerializeObject(appUser);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Auth/Login", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var token =
                     JsonConvert
                     .DeserializeObject<AccessToken>
                     (await response.Content.ReadAsStringAsync());

                context.HttpContext.Session.SetString("token", token.Token);
                context.HttpContext.Response.Cookies.Append("customer", token.CustomerId.ToString());

                return true;
            }
            return false;

        }
    }
}
