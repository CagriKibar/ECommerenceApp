    using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfCoreGenericRepository<Category, TezContext>, ICategoryDal
    {
        public Category CategoryByIdWithProduct(int id)
        {
            using (var context = new TezContext())
            {
                return context.Categories
                    .Where(i=>i.CategoryId == id)
                    .Include(i=>i.ProductCategories)
                    .ThenInclude(i=>i.Product)
                    .FirstOrDefault();
            }
        }

        public void DeleteFromCategory(int categoryId, int productId)
        {
           using (var context=new TezContext())
            {
                var cmd = @"delete from ProductCategory where productId=@p0 And CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd, productId, categoryId);

            }
        }
    }
}
