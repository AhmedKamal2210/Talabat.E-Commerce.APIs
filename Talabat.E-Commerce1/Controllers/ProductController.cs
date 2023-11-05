using Microsoft.AspNetCore.Mvc;
using Talabat.E_Commerce1.HandleRespones;
using Talabat.E_Commerce1.Helpers;
using Talabat.Repository.Specifications;
using Talabat.Sevices.Dtos.ProductDto;
using Talabat.Sevices.Helpers;
using Talabat.Sevices.IServices.IProductServices;

namespace Talabat.E_Commerce1.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        [Cache(30)]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts( [FromQuery] ProductSpecifications productSpecs)
        {
            var products = await _productServices.GetAllProductsAsync(productSpecs);

            return Ok(products);
        }
            


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status200OK)]
        [Cache(30)]
        public async Task<ActionResult<ProductDto>> GetProduct(int? id)
        {
            var product = await _productServices.GetProductAsync(id);

            if (product is null)
                return NotFound(new ApiResponse(400));

            return Ok(product);
        }
            
    }
}
