using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IRepository<Category>
    {
        Category  CategoryByIdWithProduct(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
