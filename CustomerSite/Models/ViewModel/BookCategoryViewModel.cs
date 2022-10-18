using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CustomerSite.Models
{
    public class BookCategoryViewModel
    {
        public List<Product>? Products { get; set; }
        public SelectList? Categories { get; set; }
        public string? BookCategory { get; set; }
        public string? SearchString { get; set; }
    }
}
