using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
