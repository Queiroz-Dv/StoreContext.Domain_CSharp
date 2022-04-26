using Flunt.Notifications;
using StoreContext.Domain.Commands;
using StoreContext.Domain.Commands.Interfaces;
using StoreContext.Domain.Entities;
using StoreContext.Domain.Handlers.Interfaces;
using StoreContext.Domain.Repositories.Interfaces;
using StoreContext.Domain.Utils;
using System.Linq;

namespace StoreContext.Domain.Handlers
{
    public class OrderHandler : Notifiable, IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(
            ICustomerRepository customerRepository,
            IDeliveryFeeRepository deliveryFeeRepository,
            IDiscountRepository discountRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _deliveryRepository = deliveryFeeRepository;
            _discountRepository = discountRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }
        public ICommandResult Handle(CreateOrderCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Pedido inválido", command.Notifications);

            // Recupera o cliente
            var customer = _customerRepository.Get(command.Customer);


            // Calcular taxa de entrega
            var deliveryFee = _deliveryRepository.Get(command.ZipCode);

            // Obter cupom de desconto
            var discount = _discountRepository.Get(command.PromoCode);

            // Gerar pedido
            var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
            var order = new Order(customer, deliveryFee, discount);
            foreach (var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            // Agrupa as notificações
            AddNotifications(order.Notifications);

            // Verifica se tudo deu certo
            if (Invalid)
                return new GenericCommandResult(false, "Falha ao gerar o pedido", Notifications);

            // Retorna o resultado
            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
        }
    }
}
