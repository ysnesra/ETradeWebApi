using Core.DataAccess;
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
        //ürünleri kullanıcı isimleri ile listeleyen metot
        List<ProductDetailDto> GetListProductWithUser(int userId);

        bool GetProductByUserId(int userId, int productId);
    }
}
