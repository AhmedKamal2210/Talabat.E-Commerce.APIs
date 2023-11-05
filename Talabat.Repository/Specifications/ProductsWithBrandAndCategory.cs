using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.ProductEntities;

namespace Talabat.Repository.Specifications
{
    public class ProductsWithBrandAndCategory : BaseSpecifications<Product>
    {
        public ProductsWithBrandAndCategory(ProductSpecifications productSpecs) 
            : base( p => 
                 (string.IsNullOrEmpty(productSpecs.Search) || p.Name.Trim().ToLower().Contains(productSpecs.Search)) &&
                 (!productSpecs.ProductBrandId.HasValue || p.BrandId == productSpecs.ProductBrandId) &&
                 (!productSpecs.ProductCategoryId.HasValue || p.CategoryId == productSpecs.ProductCategoryId)
                  )
        {
            AddInclude( p => p.Brand);
            AddInclude( p => p.Category);
            AddOrderBy(p => p.Name);
            AddPagination(productSpecs.PageSize * (productSpecs.PageIndex - 1) , productSpecs.PageSize); // Skip : Size * (Index - 1) , Take : Size

            if (!string.IsNullOrEmpty(productSpecs.Sort))
            {
                switch (productSpecs.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithBrandAndCategory(int? id)
            : base(p => p.Id == id)
            
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Category);
        }
    }
}
