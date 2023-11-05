using Microsoft.AspNetCore.Mvc;
using Talabat.E_Commerce1.HandleRespones;
using Talabat.Repository.IRepository;
using Talabat.Repository.Repsository;
using Talabat.Sevices.Dtos.BasketDto;
using Talabat.Sevices.Dtos.OrderDto;
using Talabat.Sevices.Dtos.ProductDto;
using Talabat.Sevices.IServices.IBasketServices;
using Talabat.Sevices.IServices.ICacheServices;
using Talabat.Sevices.IServices.IOrderServices;
using Talabat.Sevices.IServices.IPaymentServices;
using Talabat.Sevices.IServices.IProductServices;
using Talabat.Sevices.IServices.ITokenServices;
using Talabat.Sevices.IServices.IUserServices;
using Talabat.Sevices.Services.BasketSevices;
using Talabat.Sevices.Services.CasheService;
using Talabat.Sevices.Services.OrderServices;
using Talabat.Sevices.Services.PaymentServices;
using Talabat.Sevices.Services.ProductServices;
using Talabat.Sevices.Services.TokenServices;
using Talabat.Sevices.Services.UserServices;

namespace Talabat.E_Commerce1.Extensions
{
    public static class ApplicationServiceExtensions
    {
        // To Refactor Program.

        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
            //builder.Services.AddScoped<IGenericRepository<Products>, GenericRepository<Products>>();
            //builder.Services.AddScoped<IGenericRepository<ProductBrands>, GenericRepository<ProductBrands>>();
            //builder.Services.AddScoped<IGenericRepository<ProducatCategories>, GenericRepository<ProducatCategories>>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // => This Better.
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketServices, BasketServices>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();


            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                             .Where(model => model.Value.Errors.Count > 0)
                                             .SelectMany(model => model.Value.Errors)
                                             .Select(error => error.ErrorMessage).ToList();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };

            });
                return services;
        }

        
    }
}
