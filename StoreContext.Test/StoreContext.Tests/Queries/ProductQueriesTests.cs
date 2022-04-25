using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreContext.Domain.Entities;
using StoreContext.Domain.Queries;
using System.Collections.Generic;
using System.Linq;

namespace StoreContext.Tests.Queries
{
    [TestClass]
    public class ProductQueriesTests
    {
        private IList<Product> _products;
        public ProductQueriesTests()
        {
            _products = new List<Product>
            {
                new Product("Produto 1", 10, true),
                new Product("Produto 2", 20, true),
                new Product("Produto 3", 30, true),
                new Product("Produto 4", 40, false),
                new Product("Produto 5", 50, false)
            };
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void DeveRetornar3_ProdutosAtivosDaConsulta()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void DeveRetornar2_ProdutosInativosDaConsulta()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.AreEqual(result.Count(), 2);
        }
    }
}
