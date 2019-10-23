// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PartsUnlimited.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace PartsUnlimited.Controllers
{
public class StoreController : Controller
{
    private readonly IPartsUnlimitedContext _db;
    private readonly IMemoryCache _cache;

    public StoreController(IPartsUnlimitedContext context, IMemoryCache memoryCache)
    {
        _db = context;
        _cache = memoryCache;
    }

    //
    // GET: /Store/

    public IActionResult Index()
    {
        var category = _db.Categories.ToList();

        return View(category);
    }

    //
    // GET: /Store/Browse?category=Brakes

    public async Task<IActionResult> Browse(int categoryId)
    {
        // Retrieve category and its associated products from database
        // TODO [EF] Swap to native support for loading related data when available
        var categoryModel = _db.Categories.Single(g => g.CategoryId == categoryId);

        if (categoryModel.Name.ToLower().Equals("oil"))
        {
            var url = "Replace with Function App Url";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                url += HttpContext.User.Identity.Name.Equals("Administrator@test.com") ? "&UserID=1" : "&UserID=50";
            }
            using (HttpClient client = new HttpClient())
            {
                var jsonProducts = await client.GetStringAsync(url);
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonProducts);
                foreach (Product product in products)
                {
                    product.ProductId = _db.Products.First(a => a.SkuNumber == product.SkuNumber).ProductId;
                }

                categoryModel.Products = products;
            }
        }
        else
        {
            categoryModel.Products = _db.Products.Where(a => a.CategoryId == categoryModel.CategoryId).ToList();
        }
        return View(categoryModel);
    }
    public IActionResult Details(int id)
    {
        Product productData;

        productData = _db.Products.Single(a => a.ProductId == id);
        productData.Category = _db.Categories.Single(g => g.CategoryId == productData.CategoryId);


        return View(productData);
    }
}
}
