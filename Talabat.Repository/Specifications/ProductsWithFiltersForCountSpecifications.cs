using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.ProductEntities;

namespace Talabat.Repository.Specifications
{
    public class ProductsWithFiltersForCountSpecifications : BaseSpecifications<Product>
    {
        public ProductsWithFiltersForCountSpecifications(ProductSpecifications productSpecs)
            : base(p =>
                 (string.IsNullOrEmpty(productSpecs.Search) || p.Name.Trim().ToLower().Contains(productSpecs.Search)) && 
                 (!productSpecs.ProductBrandId.HasValue || p.BrandId == productSpecs.ProductBrandId) &&
                 (!productSpecs.ProductCategoryId.HasValue || p.CategoryId == productSpecs.ProductCategoryId)
                  )
        {

        }
    }
}
