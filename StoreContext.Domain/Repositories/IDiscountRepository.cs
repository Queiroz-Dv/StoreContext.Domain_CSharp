using StoreContext.Domain.Entities;

namespace StoreContext.Domain.Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        Discount Get(string code);
    }
}
