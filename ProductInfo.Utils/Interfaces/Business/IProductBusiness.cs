using System;
using System.Collections.Generic;
using System.Text;

namespace ProductInfo.Utils.Interfaces.Business
{
    public interface IProductBusiness
    {
        List<Object> ListAllProducts();
        Object GetProductById(long id);
        void CreateOrUpdateProduct(Object obj);
        void DeleteProduct(long id);
    }
}
