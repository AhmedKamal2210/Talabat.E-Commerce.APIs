using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.ProductEntities;
using Talabat.Repository.IRepository;
using Talabat.Repository.Specifications;
using Talabat.Sevices.Dtos.ProductDto;
using Talabat.Sevices.Helpers;
using Talabat.Sevices.IServices.IProductServices;

namespace Talabat.Sevices.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        //    => await _unitOfWork.Repository<Product>().GetAllAsync();

        //public async Task<Product> GetProductAsync(int? id)
        //    => await _unitOfWork.Repository<Product>().GetAsync(id);

        public async Task<Pagination<ProductDto>> GetAllProductsAsync(ProductSpecifications productSpecs)
        {
            var specs = new ProductsWithBrandAndCategory(productSpecs);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecsAsync(specs);

            var totalItems = await _unitOfWork.Repository<Product>().CountAsync(specs);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return new Pagination<ProductDto>(productSpecs.PageIndex, productSpecs.PageSize, totalItems, mappedProducts);
        }


        public async Task<ProductDto> GetProductAsync(int? id)
        {
            var specs = new ProductsWithBrandAndCategory(id);
            var product = await _unitOfWork.Repository<Product>().GetWithSpecsAsync(specs);

            var mappeProduct = _mapper.Map<ProductDto>(product);

            return mappeProduct;
        }

    }
}
