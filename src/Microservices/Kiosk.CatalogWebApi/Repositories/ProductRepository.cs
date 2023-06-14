namespace Kiosk.CatalogWebApi.Repositories
{
    using AutoMapper;

    using Kiosk.CatalogWebApi.Models;
    using Kiosk.Core.Dtos.Catalog;

    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc cref="IProductRepository"/>
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly CatalogDbContext _dbCatalogDbContext;

        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор репозитория товаров.
        /// </summary>
        /// <param name="dbCatalogDbContext"> Контекст базы данных. </param>
        /// <param name="mapper"> Маппер. </param>
        public ProductRepository(CatalogDbContext dbCatalogDbContext, IMapper mapper)
        {
            _dbCatalogDbContext = dbCatalogDbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<ProductDto> GetProduct(int productId)
        {
            var product = await _dbCatalogDbContext.Products.FindAsync(productId);
            return _mapper.Map<ProductDto>(product);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _dbCatalogDbContext.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        /// <inheritdoc />
        public async Task CreateProduct(ProductDto product)
        {
            var dbProduct = _mapper.Map<Product>(product);

            await _dbCatalogDbContext.AddAsync(dbProduct);
            await _dbCatalogDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateProduct(ProductDto product)
        {
            var dbProduct = _mapper.Map<Product>(product);

            _dbCatalogDbContext.Update(dbProduct);
            await _dbCatalogDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteProduct(int productId)
        {
            try
            {
                var product = await _dbCatalogDbContext.Products.FindAsync(productId);
                _dbCatalogDbContext.Remove(product);
                await _dbCatalogDbContext.SaveChangesAsync();
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException(exception.Message);
            }
        }
    }
}
