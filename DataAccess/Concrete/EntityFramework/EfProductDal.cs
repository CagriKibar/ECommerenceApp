using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfCoreGenericRepository<Product, TezContext>, IProductDal
    {
        public Product GetProductDetails(int id)
        {
            using (var context= new TezContext())
            {
                return context.Products
                    .Where(i => i.ProductId == id)
                    .Include(i =>i.ProductCategories)
                   .ThenInclude(i => i.Category)
                   .FirstOrDefault();

            }
        }
    }
}
