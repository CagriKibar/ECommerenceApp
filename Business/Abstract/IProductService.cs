using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService 
    {
        Product GetById(int id);
        List<Product> GetAll();
        List<Product> GetProductByCategory(string category, int page, int pageSize);
        Product GetProductDetails(int id);
        void Create(Product entity);
        void Delete(Product entity);
        void Update(Product entity);
        int GetCountByCategory(string category);
        Product GetByIdWithCategory(int productId);
        void Update(Product entity, int[] categoryIds);
    }

}
