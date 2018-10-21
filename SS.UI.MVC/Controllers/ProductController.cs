using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS.UI.MVC.Clients;
using SS.UI.MVC.Models;

namespace SS.UI.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public ProductController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }
        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<ProductModel> products = _productApiClient.GetProductsAsync().Result;
            //return View(products);

            return View("List", products);
        }

        // GET: Product/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel product = _productApiClient.GetProductByIdAsync(id).Result;
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] ProductModel productModel, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _productApiClient.CreateAsync(productModel).Result;
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(Guid id)
        {
            ProductModel product = _productApiClient.GetProductByIdAsync(id).Result;
            return View(product); 
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [Bind] ProductModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _productApiClient.PutAsync(productModel, id).Result;
                    if (result)
                        return RedirectToAction("Index");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(Guid id)
        {
            ProductModel product = _productApiClient.GetProductByIdAsync(id).Result;
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var result = _productApiClient.DeleteAsync(id).Result;
                 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}