using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Test_Alevel.DTOs;
using Test_Alevel.Filter;

namespace Test_Alevel.ApiService.MasterCategoryService
{
    public class MasterCategoryApi
    {
        private readonly HttpClient _httpclient;
        private readonly IHttpContextAccessor httpContext;

        public MasterCategoryApi(HttpClient httpclient,IHttpContextAccessor httpContext)
        {
            _httpclient = httpclient;
            this.httpContext = httpContext;
        }
        
        public async Task<IEnumerable<MasterCategoryDto>> GetAllAsync()
        {

            IEnumerable<MasterCategoryDto> masterCategoryDtos;

            _httpclient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", httpContext.HttpContext.Session.GetString("token"));

            var response = await _httpclient.GetAsync("mastercategory");
            if (response.IsSuccessStatusCode)
            {
                masterCategoryDtos =
                    JsonConvert.
                    DeserializeObject<IEnumerable<MasterCategoryDto>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                masterCategoryDtos = null;
            }
            return masterCategoryDtos;
        }


        public async Task<MasterCategoryDto> AddAsync(MasterCategoryDto masterCategoryDto)
        {

            var byteLogo = await System.IO.File.ReadAllBytesAsync(masterCategoryDto.fileLogo.FileName);
            ByteArrayContent logoContent = new ByteArrayContent(byteLogo);

            var byteImage = await System.IO.File.ReadAllBytesAsync(masterCategoryDto.fileImage.FileName);
            ByteArrayContent imageContent = new ByteArrayContent(byteImage);

            //var stringContent =new StringContent(JsonConvert.SerializeObject(masterCategoryDto), Encoding.UTF8, "application/json");

            MultipartFormDataContent formdata = new MultipartFormDataContent();
            //formdata.Headers.ContentType.MediaType = "application/json";


            formdata.Add(logoContent, "fileLogo", masterCategoryDto.fileLogo.FileName);
            formdata.Add(imageContent, "fileImage", masterCategoryDto.fileImage.FileName);
            //formdata.Add(stringContent, "masterCategoryDto");
            formdata.Add(new StringContent(masterCategoryDto.Name), nameof(masterCategoryDto.Name));
            formdata.Add(new StringContent(masterCategoryDto.Description), nameof(masterCategoryDto.Description));

            var response =  _httpclient.PostAsync("mastercategory", formdata).Result;
            if (response.IsSuccessStatusCode)
            {
                masterCategoryDto =
                    JsonConvert.
                    DeserializeObject<MasterCategoryDto>
                    (await response.Content.ReadAsStringAsync());
                return masterCategoryDto;
            }
            else
            {
                return null;
            }
        }
    }//multipart/form-data
}
