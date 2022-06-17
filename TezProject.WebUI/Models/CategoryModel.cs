using Entities;
using System.Collections.Generic;

namespace TezProject.WebUI.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }
}
