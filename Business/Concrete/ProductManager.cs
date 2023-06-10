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
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
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

        public IDataResult<Product> GetById(int productId)
        {
            var productdb = _productDal.Get(c => c.Id == productId && c.UserId == Convert.ToInt32(_userDal.CurrentUser(_contextAccessor)));
            if (productdb == null)
            {
                return new ErrorDataResult<Product>( productdb,false,Messages.ProductInvalid);
            }
            return new SuccessDataResult<Product>(productdb, Messages.ProductDetail);
        }

        public IDataResult<IPaginate<Product>> GetByUserId(int userId, PageRequest pageRequest)
        {
            return new SuccessDataResult<IPaginate<Product>>(_productDal.GetList
                (x => x.UserId == userId, index: pageRequest.Page, size: pageRequest.PageSize));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails(int userId)
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetListProductWithUser(userId),Messages.ProductListWithUser);
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
