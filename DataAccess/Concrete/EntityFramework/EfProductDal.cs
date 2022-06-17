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
        public Product GetByIdWithCategories(int id)
        {
            using (var context = new TezContext())
            {
                return context.Products
                    .Where(i => i.ProductId == id)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new TezContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .Where(i => i.ProductCategories.Any(a => a.Category.CategoryName.ToLower() == category.ToLower()));
                }
                return products.Count();
            }
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new TezContext())
            {
                return context.Products
                    .Where(i => i.ProductId == id)
                    .Include(i => i.ProductCategories)
                   .ThenInclude(i => i.Category)
                   .FirstOrDefault();

            }
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            using (var context = new TezContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .Where(i => i.ProductCategories.Any(a => a.Category.CategoryName.ToLower() == category.ToLower()));
                }
                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new TezContext())
            {
                var product = context.Products
                    .Include(i => i.ProductCategories)
                    .FirstOrDefault(i => i.ProductId == entity.ProductId);
                if (product!=null)
                {
                    product.ProductName = entity.ProductName;
                    product.Description = entity.Description;
                    product.Price = entity.Price;

                    product.ProductCategories = categoryIds.Select(i => new ProductCategory()
                    {
                        CategoryId = i,
                        ProductId = entity.ProductId
                    }).ToList() ;

                    context.SaveChanges();
                }
            }
        }
    }
}
