using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Alevel.DTOs
{
    public class AddToBasketDto
    {
        public int id { get; set; }
        public int CustomerId { get; set; } = 0;
        public int count { get; set; } = 1;
    }
}
