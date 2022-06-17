using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TezProject.WebUI.Models;

namespace TezProject.WebUI.Controllers
{
    public class ShopController : Controller
    {
        IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Details(int? id)
        {
            if (id==null)
            {
                return NotFound();  

            }
            Product product = _productService.GetProductDetails((int)id);
            if (id == null)
            {
                return NotFound();

            }
            return View(new ProductDetailsModel()
            {
                Product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()
            }); 
            
        }
        public IActionResult List(string category, int page=1)
        {
            const int pageSize = 3;
            return View(new ProductListModel
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemsPerPage= pageSize,
                    CurrentCategory=category

                },
                Products = _productService.GetProductByCategory(category, page, pageSize)
            }) ;
        }
    }
}
