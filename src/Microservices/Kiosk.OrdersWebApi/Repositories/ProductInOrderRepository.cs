namespace Kiosk.OrdersWebApi.Repositories
{
    using AutoMapper;

    using Kiosk.Core.Dtos.Catalog;
    using Kiosk.OrdersWebApi.Models;

    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc cref="IProductInOrderRepository"/>
    public class ProductInOrderRepository : IProductInOrderRepository
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
        /// Конструктор репозитория продуктов в заказе.
        /// </summary>
        /// <param name="orderDbContext"> Контекст базы данных. </param>
        /// <param name="mapper"> Маппер. </param>
        public ProductInOrderRepository(OrderDbContext orderDbContext, IMapper mapper)
        {
            _orderDbContext = orderDbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProductDto>> GetProductsInOrder()
        {
            var productsInOrder = await _orderDbContext.ProductsInOrder.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(productsInOrder);
        }

        /// <inheritdoc />
        public async Task CreateProductsInOrder(IEnumerable<ProductDto> productsInOrder)
        {
            var dbProductInOrder = _mapper.Map<IEnumerable<ProductInOrder>>(productsInOrder);

            await _orderDbContext.AddRangeAsync(dbProductInOrder);
            await _orderDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateProductsInOrder(IEnumerable<ProductDto> productsInOrder)
        {
            var dbProductInOrder = _mapper.Map<IEnumerable<ProductInOrder>>(productsInOrder);

            _orderDbContext.UpdateRange(dbProductInOrder);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
