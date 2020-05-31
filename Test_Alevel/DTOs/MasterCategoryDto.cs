using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Alevel.DTOs
{

    public class MasterCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile fileLogo { get; set; }
        public IFormFile fileImage { get; set; }
        public string Logo { get; set; }
        public string Image { get; set; }
    }
}
