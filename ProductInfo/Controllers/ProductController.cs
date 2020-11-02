using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductInfo.Data.Entities;
using ProductInfo.Utils.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductInfo.Controllers
{
    public class ProductController : Controller
    {
        private IProductBusiness _productBusiness;
        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var products = _productBusiness.ListAllProducts().Cast<Product>().ToList();
                return View(products);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetProductInfo(long id)
        {
            try
            {
                Product product = new Product();
                product.Price = new Price();

                if (id > 0)
                {
                    product = _productBusiness.GetProductById(id) as Product;
                    return View("CreateOrUpdate", product);
                }
                else
                {
                    return View("CreateOrUpdate", product);
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOrUpdate(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productBusiness.CreateOrUpdateProduct(product);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("CreateOrUpdate", product);
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        [HttpGet]
        public IActionResult DeleteProduct(long id)
        {
            try
            {
                _productBusiness.DeleteProduct(id);
                return RedirectToAction("Index");
            }catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }
    }
}
