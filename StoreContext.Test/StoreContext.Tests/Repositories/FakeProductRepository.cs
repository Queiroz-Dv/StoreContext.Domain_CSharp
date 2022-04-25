using System;
using System.Collections.Generic;
using StoreContext.Domain.Entities;
using StoreContext.Domain.Repositories.Interfaces;


namespace StoreContext.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>();
            products.Add(new Product("Produto 1", 10, true));
            products.Add(new Product("Produto 2", 10, true));
            products.Add(new Product("Produto 3", 10, true));
            products.Add(new Product("Produto 4", 10, false));
            products.Add(new Product("Produto 5", 10, false));

            return products
        }
    }
}
