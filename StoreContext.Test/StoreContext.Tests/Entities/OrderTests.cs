using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreContext.Domain.Entities;
using StoreContext.Domain.Enums;
using System;

namespace StoreContext.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        // Pra evitar outras instâncias nos métodos
        private readonly Customer _customer = new Customer("Eduardo Queiroz", "queiroz@eduardo.dv");
        private readonly Product _product = new Product("Produto 1", 10, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

        [TestMethod]
        [TestCategory("Domain")]
        public void DeveRetornar_OitoCaracteres()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void StatusDeveSer_AguardandoPagamento()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void StatusDeveSer_AguardandoEntrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1); //Aqui deve ser 10 
            order.Pay(10);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void StatusDeveSer_AguardandoCancelado()
        {
            var order = new Order(_customer, 0, null);
            order.Cancel();
            Assert.AreEqual(order.Status, EOrderStatus.Canceled);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ItemSemProduto_NãoDeveSerAdicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(null, 10); // produto nulo
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ItemComQuantidadeZeroOuMenor_NãoDeveSerAdicioando()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 0);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void NovoPedidoValido_TotalDeveRetornar50()
        {
            var order = new Order(_customer, 10, null); //-10 +10 = 0
            order.AddItem(_product, 5); // 10 * 5 = 50
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ValorDeveRetornar60_ParaDescontoExpirado()
        {
            var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5));
            var order = new Order(_customer, 10, expiredDiscount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);

        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ValorDeveRetornar60_ParaDescontoInválido()
        {
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void PedidoDeveSer50_ParaDescontoDe10()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ValorDeveRetornar60_ParaTaxaDeEntregaDe10()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 6);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void PedidoSemCliente_ValorDevSerInvalido()
        {
            var order = new Order(null, 10, _discount);
            Assert.AreEqual(order.Valid, false);
        }
    }
}
