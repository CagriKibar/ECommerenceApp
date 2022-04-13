using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TezProject.WebUI.Models;

namespace TezProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IProductDal _productDal;

        public HomeController(IProductDal productDal)
        {
            _productDal = productDal;
        }

       
        public IActionResult Index()
        {
            return View(new ProductListModel
            {
                Products = _productDal.GetAll()
            }) ;
        }

        
    }
}
