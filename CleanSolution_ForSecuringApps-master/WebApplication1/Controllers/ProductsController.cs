using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        public readonly IProductsService _prodService;

        public ProductsController(IProductsService productsService)
        {
            _prodService = productsService;
        }
        public IActionResult Index()
        {
            var list = _prodService.GetProducts();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel data)
        {
            if(ModelState.IsValid)
            {
                if(data.Category.Id < 0 || data.Category.Id > 4)
                {
                    ModelState.AddModelError("Category.Id", "Category is not valid");
                    return View(data);
                }
                _prodService.AddProduct(data);
                TempData["message"] = "Product inserted!";
                return View();
            } else
            {
                return View(data);
            }
        }
    }
}
