using StoreContext.Domain.Entities;
using StoreContext.Domain.Repositories.Interfaces;

namespace StoreContext.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document == "12345678911")
                return new Customer("Eduardo Queiroz", "queiroz@dv.com");
            return null;
        }
    }
}
