using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using TezProject.WebUI.Models;

namespace TezProject.WebUI.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(new CategoryListViewModel()
            {
                SelectedCategory = RouteData.Values["Category"]?.ToString(),
                Categories = _categoryService.GetAll()
            }); ;
        }
    }
}
