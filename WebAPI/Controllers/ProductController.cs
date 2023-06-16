using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Paging;
using Entities.Concrete;
using Entities.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //[SecuredOperation]   //login olmuş kullanıcıların yetkiye sahip olduğunu gösteren Aspect
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll([FromQuery] PageRequest pageRequest)
        {
            var result=_productService.GetAll(pageRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //Ürün detayını kullanıcı ismi ile birlikte getirir 
        [HttpGet("getproductdetails")]
        public IActionResult GetProductDetails(int productId)
        {
            var result = _productService.GetProductDetails(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //Login olan Kullanıcıya ait tüm ürünleri listeleyen metot
        [HttpGet("getproductbyuserid")]
        public IActionResult GetProductByUserId([FromQuery] PageRequest pageRequest)
        {
            var result = _productService.GetByUserId(pageRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }      

        [HttpPost("add")]
        public IActionResult Add(CreatedProductDto product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(UpdatedProductDto product)
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(DeletedProductDto product)
        {
            var result = _productService.Delete(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
