using Business.Abstract;
using Business.Constants;
using Core.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IUserDal _userDal;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _contextAccessor;

        public ProductManager(IProductDal productDal, IUserDal userDal, Microsoft.AspNetCore.Http.IHttpContextAccessor contextAccessor)
        {
            _userDal = userDal;
            _productDal = productDal;
            _contextAccessor = contextAccessor;
        }

        public IResult Add(CreatedProductDto model)
        {
            Product newProduct = new Product()
                { 
                   ProductName=model.ProductName,
                   Description=model.Description,
                   Price=model.Price,
                   UserId=Convert.ToInt32(_userDal.CurrentUser(_contextAccessor))
                };
            _productDal.Add(newProduct);   
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(DeletedProductDto model)
        {
            var result = _productDal.GetProductByUserId(Convert.ToInt32(_userDal.CurrentUser(_contextAccessor)), model.ProductId);

            if (!result)
                return new ErrorDataResult<Product>(Messages.ProductInvalid);

            Product deleteProduct = new Product()
            {
                Id=model.ProductId,                         
            };           
            _productDal.Delete(deleteProduct);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<IPaginate<Product>> GetAll(PageRequest pageRequest)
        {
           return new SuccessDataResult<IPaginate<Product>>(_productDal.GetList(index:pageRequest.Page,size:pageRequest.PageSize),Messages.ProductsListed);
        }
        //Ürün adına göre filtreleme
        public IDataResult<IPaginate<Product>> GetProductsByProductName(string filter,PageRequest pageRequest)
        {
            Expression<Func<Product, bool>> filterExpression = p => p.ProductName.Contains(filter) && p.UserId == Convert.ToInt32(_userDal.CurrentUser(_contextAccessor));
            IPaginate<Product> products = _productDal.GetByFilter(filterExpression,index:pageRequest.Page,size:pageRequest.PageSize);
            if (products.Items.Any())
            {
                return new SuccessDataResult<IPaginate<Product>>(products, Messages.ProductNameFilter);
            }            
            return new ErrorDataResult<IPaginate<Product>>( Messages.ProductNameNoFilter);
        }
       
        //Ürün fiyatına göre filtreleme
        public IDataResult<IPaginate<Product>> GetProductsByPrice(decimal filter, PageRequest pageRequest)
        {
            Expression<Func<Product, bool>> filterExpression = p => p.Price==filter && p.UserId== Convert.ToInt32(_userDal.CurrentUser(_contextAccessor));
            IPaginate<Product> products = _productDal.GetByFilter(filterExpression, index: pageRequest.Page, size: pageRequest.PageSize);
            if (products.Items.Any())
            {
                return new SuccessDataResult<IPaginate<Product>>(products, Messages.ProductPriceFilter);
            }
            return new ErrorDataResult<IPaginate<Product>>(Messages.ProductPriceNoFilter);
        }

        //Tek bir ürün detayını getiren metot
        public IDataResult<Product> GetById(int productId)
        {
            var productdb = _productDal.Get(c => c.Id == productId && c.UserId == Convert.ToInt32(_userDal.CurrentUser(_contextAccessor)));
            if (productdb == null)
            {
                return new ErrorDataResult<Product>(productdb,false,Messages.ProductInvalid);
            }
            return new SuccessDataResult<Product>(productdb, Messages.ProductDetail);
        }

        //Ürünün detay bilgilerini kullanıcı ismiyle birlikte getirdi
        public IDataResult<ProductDetailDto> GetProductDetails(int productId)
        {
            int userId=Convert.ToInt32(_userDal.CurrentUser(_contextAccessor));
            var result=_productDal.GetProductWithUser(userId, productId);
            if(result == null)
            {
                return new ErrorDataResult<ProductDetailDto>(Messages.NoExist_ProductWithUser);
            }

            return new SuccessDataResult<ProductDetailDto>(result,Messages.ProductWithUser);
        }

        //Login olan Kullanıcıya ait tüm ürünleri listeleyen metot
        public IDataResult<IPaginate<Product>> GetByUserId(PageRequest pageRequest)
        {
            return new SuccessDataResult<IPaginate<Product>>(_productDal.GetList
                (x => x.UserId == Convert.ToInt32(_userDal.CurrentUser(_contextAccessor)), index: pageRequest.Page, size: pageRequest.PageSize));
        }
        public IResult Update(UpdatedProductDto model)
        {
            var result = _productDal.GetProductByUserId(Convert.ToInt32(_userDal.CurrentUser(_contextAccessor)), model.ProductId);

            if(!result)
                return new ErrorDataResult<Product>(Messages.ProductInvalid);
            

            Product updateProduct = new Product()
            {
                Id = model.ProductId,
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                UserId = Convert.ToInt32(_userDal.CurrentUser(_contextAccessor))
            };
            _productDal.Update(updateProduct);
            return new SuccessResult(Messages.ProductUpdated);
        }

    }
}
