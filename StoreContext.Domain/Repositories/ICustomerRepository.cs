using StoreContext.Domain.Entities;

namespace StoreContext.Domain.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer Get(string document); // Só tem a assinatura do método
    }
}
