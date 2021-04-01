using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebDataBase_Correct.DB
{
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        public string ProductionId { get; set; }
        public string Discription { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ProductionDate { get; set; }
        public decimal? Price { get; set; }
        public int Discount { get; set; }
        public bool Exist { get; set; }
        public int Amount { get; set; }
        
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
