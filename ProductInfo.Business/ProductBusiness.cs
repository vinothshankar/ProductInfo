using AutoMapper;
using Microsoft.VisualBasic.CompilerServices;
using ProductInfo.Data.Entities;
using ProductInfo.Utils.Interfaces.Business;
using ProductInfo.Utils.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace ProductInfo.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private IUnitOfWork _productUOW;
        private readonly IMapper _mapper;
        public ProductBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productUOW = unitOfWork;
            _mapper = mapper;
        }
        public void CreateOrUpdateProduct(object obj)
        {
            try
            {
                _productUOW.BeginTransaction();
                Product product = obj as Product;
                if (product != null)
                {
                    if (product.Id > 0)
                    {
                        var existingProduct = getProductById(product.Id);
                        if (existingProduct != null)
                        {
                            existingProduct = _mapper.Map(product, existingProduct);
                            existingProduct.Price = _mapper.Map(product.Price, existingProduct.Price);
                            existingProduct.Price.ProductId = existingProduct.Id;
                            existingProduct.Price.IsActive = true;
                            existingProduct.IsActive = true;
                            _productUOW.Repository<Product>().Update(existingProduct);
                            _productUOW.Repository<Price>().Update(existingProduct.Price);
                            _productUOW.SaveChanges();
                        }
                    }
                    else
                    {
                        product.IsActive = true;
                        _productUOW.Repository<Product>().Insert(product);
                        _productUOW.SaveChanges();
                        product.Price.ProductId = product.Id;
                        product.Price.IsActive = true;
                        _productUOW.Repository<Price>().Insert(product.Price);
                        _productUOW.SaveChanges();
                    }
                }


                _productUOW.Commit();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProduct(long id)
        {
            try
            {

                _productUOW.BeginTransaction();
                var product = getProductById(id);
                if (product != null)
                {
                    product.IsActive = false;
                    product.Price.IsActive = false;
                    _productUOW.Repository<Product>().Update(product);
                    _productUOW.Repository<Price>().Update(product.Price);
                }
                _productUOW.SaveChanges();
                _productUOW.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetProductById(long id)
        {
            try
            {
                return getProductById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<object> ListAllProducts()
        {
            try
            {
                return (from u in _productUOW.Repository<Product>().Querable()
                        join p in _productUOW.Repository<Price>().Querable() on u.Id equals p.ProductId
                        where u.IsActive && p.IsActive
                        select new Product()
                        {
                            Description = u.Description,
                            Id = u.Id,
                            Price = p,
                            ProductName = u.ProductName,
                            Quantity = u.Quantity
                        }).Cast<Object>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Product getProductById(long id)
        {
            return (from u in _productUOW.Repository<Product>().Querable()
                    join p in _productUOW.Repository<Price>().Querable() on u.Id equals p.ProductId
                    where u.Id == id && u.IsActive && p.IsActive
                    select new Product()
                    {
                        Description = u.Description,
                        Id = u.Id,
                        Price = p,
                        ProductName = u.ProductName,
                        Quantity = u.Quantity
                    }).FirstOrDefault();
        }
    }
}
