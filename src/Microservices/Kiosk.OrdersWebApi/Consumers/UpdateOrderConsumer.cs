namespace Kiosk.OrdersWebApi.Consumers
{
    using Kiosk.Core.Dtos.Order;
    using Kiosk.Core.Enums;
    using Kiosk.Core.Requests;
    using Kiosk.Core.Responses;
    using Kiosk.OrdersWebApi.Repositories;

    using MassTransit;

    /// <summary>
    /// Консьюмер обновления заказа.
    /// </summary>
    public class UpdateOrderConsumer : IConsumer<UpdateOrderRequest>
    {
        /// <summary>
        /// Репозиторий заказов.
        /// </summary>
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Конструктор консьюмера обновления заказа.
        /// </summary>
        /// <param name="orderRepository"> Репозиторий заказов. </param>
        public UpdateOrderConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Обновление заказа.
        /// </summary>
        /// <param name="context"> Контекст с запросом. </param>
        public async Task Consume(ConsumeContext<UpdateOrderRequest> context)
        {
            var issue = context.Message.Issue;
            var order = await _orderRepository.GetOrder(issue.OrderId);
            order.OrderStatus = issue.OrderStatus;

            await _orderRepository.UpdateOrder(order);
            await context.RespondAsync(new OrderResponse { Order = order });
        }
    }
}
