using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreContext.Domain.Commands;
using System;

namespace StoreContext.Tests.Commands
{
    [TestClass]
    public class CreateOrderCommandsTests
    {
        [TestMethod]
        [TestCategory("Handlers")]
        public void PedidoNãoDeveSerGeradoParaComandoInvalido()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }
    }
}
