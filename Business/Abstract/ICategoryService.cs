using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface ICategoryService
    {
        Category GetById(int id);
        Category CategoryByIdWithProduct( int id);
        List<Category> GetAll();
        void Create(Category entity);
        void Delete(Category entity);
        void Update(Category entity);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
