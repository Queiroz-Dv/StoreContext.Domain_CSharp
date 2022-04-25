using System;
using System.Collections.Generic;
using StoreContext.Domain.Entities;

namespace StoreContext.Domain.Repositories
.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get(IEnumerable<Guid> ids);
    }
}
