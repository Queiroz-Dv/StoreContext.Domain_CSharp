using StoreContext.Domain.Entities;

namespace StoreContext.Domain.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
