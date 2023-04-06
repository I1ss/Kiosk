namespace Kiosk.OrdersWebApi.Consumers
{
    using Kiosk.Core.Requests;
    using Kiosk.Core.Responses;
    using Kiosk.OrdersWebApi.Repositories;
    using MassTransit;

    /// <summary>
    /// Консьюмер для получения продуктов из заказа.
    /// </summary>
    public class GetProductsInOrderConsumer : IConsumer<GetProductsInOrderRequest>
    {
        /// <summary>
        /// Репозиторий продуктов в заказе.
        /// </summary>
        private readonly IProductInOrderRepository _productInOrderRepository;

        /// <summary>
        /// Конструктор консьюмера для получения продуктов из заказа.
        /// </summary>
        /// <param name="productInOrderRepository"> Репозиторий продуктов в заказе. </param>
        public GetProductsInOrderConsumer(IProductInOrderRepository productInOrderRepository)
        {
            _productInOrderRepository = productInOrderRepository;
        }

        /// <summary>
        /// Получение продуктов из заказа.
        /// </summary>
        /// <param name="context"> Контекст с запросом. </param>
        public async Task Consume(ConsumeContext<GetProductsInOrderRequest> context)
        {
            var products = await _productInOrderRepository.GetProductsInOrder();
            await context.RespondAsync(new ProductsInOrderResponse { Products = products });
        }
    }
}
