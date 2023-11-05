using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Repository.Specifications
{
    public class OrderWithItemSpecifications : BaseSpecifications<Orders>
    {
        public OrderWithItemSpecifications(string email) : base(order => order.BuyerEmail == email)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDesc(order => order.OrderDate);
        }

        public OrderWithItemSpecifications(int id, string email) 
            : base(order => order.BuyerEmail == email && order.Id == id)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }
    }
}
