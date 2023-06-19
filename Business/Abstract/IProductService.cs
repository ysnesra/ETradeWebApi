using Core.DataAccess;
using Core.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Product;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService 
    {
        IResult Add(CreatedProductDto model);
        IResult Update(UpdatedProductDto model);
        IResult Delete(DeletedProductDto model);
        IDataResult<IPaginate<Product>> GetAll(PageRequest pageRequest);
        IDataResult<Product> GetById(int productId);

        //ürünleri kullanıcı isimleri ile listeleyen metot
        IDataResult<ProductDetailDto> GetProductDetails(int productId);
        IDataResult<IPaginate<Product>> GetByUserId(PageRequest pageRequest);

        //Ürünleri; ürün adına göre filtreli getiren metot
        IDataResult<IPaginate<Product>> GetProductsByProductName(string filter, PageRequest pageRequest);

        //Ürünleri; ürün fiyatına göre filtreli getiren metot
        public IDataResult<IPaginate<Product>> GetProductsByPrice(decimal filter, PageRequest pageRequest);
    }
}
