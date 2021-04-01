using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDataBase_Correct.DB;

namespace WebDataBase_Correct.Models
{
    public class CategoryDetails
    {
        public Category category { get; set; }
        public List<Product> productList { get; set; }
        


    }
}
