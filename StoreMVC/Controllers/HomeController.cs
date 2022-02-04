﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StoreMVC.Models;
using StoreMVC.Service;
using StoreMVC.ViewModels;
using StoreMVC.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;


        public HomeController(IProductService _productService)
        {
            productService = _productService;
        }

        public async Task<IActionResult> Index(string query)
        {
            if (query == null)
            {
                return View();
            }
            else
            {
                await productService.GetProductsAsync(query);

                if (productService.Products is null)
                {
                    return View();
                }

                return View("IndexByQuery", productService.Products);
            }
        }

        public async Task<IActionResult> GetProductsByCategory(string category, Ordering ordering)
        {
            var model = await productService.GetProductsByParams(new GetProductsParams(category, ordering, 8));

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        { 
            await productService.GetProductAsync(id);

            if (productService.Product.Id == 0)
            {
                return RedirectToAction("Index");
            }

            return View(productService.Product);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
