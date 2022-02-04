﻿using System.ComponentModel.DataAnnotations;

namespace Data.Entitites
{
    public class Product : BaseEntity
    {
        [Required]
        public string ProductName { get; set; }

        public string Description { get; set; }
        public DateTime? Release { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
