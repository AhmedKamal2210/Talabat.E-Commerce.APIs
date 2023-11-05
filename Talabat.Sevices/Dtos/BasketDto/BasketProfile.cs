using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.BasketEntities;

namespace Talabat.Sevices.Dtos.BasketDto
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket , CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItems , BasketItemsDto>().ReverseMap();
        }
    }
}
