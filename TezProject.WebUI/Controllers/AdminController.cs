using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TezProject.WebUI.Models;

namespace TezProject.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult ProductList()
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            
            return View(new ProductModel());
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    ProductName = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    ImageUrl = model.ImageUrl
                };
                _productService.Create(product);
                return RedirectToAction("Index");

            }
           
            return View(model);
            
        }

        
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetByIdWithCategory((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                Description = entity.Description,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList()
            };
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        public  async Task<IActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                var entity = _productService.GetById(model.ProductId);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.ProductName = model.ProductName;
                entity.Description = model.Description;
                entity.Price = model.Price;

                if (file != null)
                {
                    entity.ImageUrl = file.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _productService.Update(entity, categoryIds);

                return RedirectToAction("ProductList");
            }

            ViewBag.Categories = _categoryService.GetAll();

            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteProduct(int ProductId)
        {
            var entity = _productService.GetById(ProductId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }
            return RedirectToAction("ProductList");
        }

        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel categoryModel)
        {
            var category = new Category()
            {

                CategoryName = categoryModel.CategoryName,
            };
            _categoryService.Create(category);

            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _categoryService.CategoryByIdWithProduct(id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                CategoryName = entity.CategoryName,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()
                
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel categoryModel)
        {
            var entity = _categoryService.GetById(categoryModel.CategoryId);

            if (entity == null)
            {
                return NotFound();
            }
            entity.CategoryName = categoryModel.CategoryName;
           
            _categoryService.Update(entity);
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int CategoryId)
        {
            var entity = _categoryService.GetById(CategoryId);
            if (CategoryId != null)
            {
                _categoryService.Delete(entity);

            }

            return RedirectToAction("CategoryList");
        }

        [HttpPost]

        public IActionResult DeleteFromCategory(int categoryId, int productId)
        {
            _categoryService.DeleteFromCategory(categoryId, productId);
            return Redirect("/admin/EditCategory/"+categoryId);
        }
    }
}
