using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Repository.Specifications
{
    public class OrderWithPaymentIntentSpecifications : BaseSpecifications<Orders>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId) 
            : base(order => order.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
