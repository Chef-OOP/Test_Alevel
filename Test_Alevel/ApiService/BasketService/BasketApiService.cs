using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test_Alevel.DTOs;

namespace Test_Alevel.ApiService.BasketService
{
    public class BasketApiService
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor accessor;

        public BasketApiService(HttpClient client, IHttpContextAccessor accessor)
        {
            this.client = client;
            this.accessor = accessor;
        }

        public async Task<BasketDto> Basket(AddToBasketDto addToBasketDto)
        {
            addToBasketDto.CustomerId =
                Convert.ToInt32(accessor.HttpContext.Request.Cookies["customer"]);
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(addToBasketDto), Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("Auth/AddToBasket", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var basketDto =
                    JsonConvert
                    .DeserializeObject<BasketDto>(await responseMessage.Content.ReadAsStringAsync());
                accessor.HttpContext.Response.Cookies.Append("customer", basketDto.customerId.ToString());
                return basketDto;
            }
            else
                return null;
        }
    }
}
