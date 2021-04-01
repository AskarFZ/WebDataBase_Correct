using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebDataBase_Correct.DB
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        [Display(Name="Category Name")]
        public string Name { get; set; }
        public string Disription { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
