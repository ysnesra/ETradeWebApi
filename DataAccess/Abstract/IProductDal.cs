using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        //Ürün detayını kullanıcı isimi ile getiren metot //User-Product Joinleme
        ProductDetailDto GetProductWithUser(int userId, int productId);

        //Kullanıcının böyle bir ürünü var mı kontrolünü yaptığım metot
        bool GetProductByUserId(int userId, int productId);
    }
}
