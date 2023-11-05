using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository.Specifications
{
    public class ProductSpecifications
    {
        public int? ProductBrandId { get; set; }
        public int? ProductCategoryId { get; set; }
        public string? Sort { get; set; }
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _PageSize = 6;
        private string? _Search;

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value; 
        }

        public string? Search
        {
            get => _Search;
            set => value.Trim().ToLower();
        }




    }
}
