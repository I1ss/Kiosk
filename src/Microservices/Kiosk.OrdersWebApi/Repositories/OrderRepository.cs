namespace Kiosk.OrdersWebApi.Repositories
{
    using AutoMapper;

    using Kiosk.Core.Dtos.Catalog;
    using Kiosk.Core.Dtos.Order;
    using Kiosk.OrdersWebApi.Models;

    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc cref="IOrderRepository"/>
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly OrderDbContext _orderDbContext;

        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Репозиторий продуктов в заказе.
        /// </summary>
        private readonly IProductInOrderRepository _productInOrderRepository;

        /// <summary>
        /// Конструктор репозитория брендов.
        /// </summary>
        /// <param name="orderDbContext"> Контекст базы данных. </param>
        /// <param name="mapper"> Маппер. </param>
        /// <param name="productInOrderRepository"> Репозиторий продуктов в заказе. </param>
        public OrderRepository(OrderDbContext orderDbContext, IMapper mapper, 
            IProductInOrderRepository productInOrderRepository)
        {
            _orderDbContext = orderDbContext;
            _mapper = mapper;
            _productInOrderRepository = productInOrderRepository;
        }

        /// <inheritdoc />
        public async Task<OrderDto> GetOrder(int orderId)
        {
            var order = await _orderDbContext.Orders.FindAsync(orderId);
            var blOrder = _mapper.Map<OrderDto>(order);

            if (blOrder == null)
                return null;

            var products = await _productInOrderRepository.GetProductsInOrder();
            blOrder.Products = products.Where(product => product.OrderId == orderId)?.ToList() ?? new List<ProductDto>();

            return blOrder;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            var orders = await _orderDbContext.Orders.ToListAsync();
            var blOrders = _mapper.Map<List<OrderDto>>(orders);
            var productsInOrder = await _productInOrderRepository.GetProductsInOrder();
            blOrders.ForEach(order => order.Products = productsInOrder
                .Where(product => product.OrderId == order.OrderId).ToList());

            return blOrders;
        }

        /// <inheritdoc />
        public async Task CreateOrder(OrderDto order)
        {
            var dbOrder = _mapper.Map<Order>(order);
            await _orderDbContext.AddAsync(dbOrder);
            await _orderDbContext.SaveChangesAsync();

            if (order.Products?.Any() != true)
                return;

            var productsInOrder = order.Products;
            productsInOrder.ForEach(product => product.OrderId = dbOrder.OrderId);
            await _productInOrderRepository.CreateProductsInOrder(productsInOrder);
        }

        /// <inheritdoc />
        public async Task UpdateOrder(OrderDto order)
        {
            var dbOrder = _mapper.Map<Order>(order);
            _orderDbContext.Update(dbOrder);
            await _orderDbContext.SaveChangesAsync();

            if (order.Products?.Any() != true)
                return;

            var productsInOrder = order.Products;
            productsInOrder.ForEach(product => product.OrderId = order.OrderId);
            await _productInOrderRepository.UpdateProductsInOrder(productsInOrder);
        }

        /// <inheritdoc />
        public async Task DeleteOrder(int orderDb)
        {
            try
            {
                var order = await _orderDbContext.Orders.FindAsync(orderDb);
                _orderDbContext.Remove(order);
                await _orderDbContext.SaveChangesAsync();
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException(exception.Message);
            }
        }
    }
}
