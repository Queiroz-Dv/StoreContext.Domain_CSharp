using StoreContext.Domain.Entities;
using StoreContext.Domain.Repositories.Interfaces;

namespace StoreContext.Tests.Repositories
{
    public class FakeOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {

        }
    }
}
