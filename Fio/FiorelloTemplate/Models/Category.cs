﻿using System.Diagnostics.CodeAnalysis;

namespace FiorelloTemplate.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<Product>? Products { get; set; }
    }
}
