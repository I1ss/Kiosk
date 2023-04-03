namespace Kiosk.CatalogWebApi.Repositories
{
    using AutoMapper;
    using Kiosk.Core.Dtos.Catalog;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc cref="ICatalogRepository"/>
    public class CatalogRepository : ICatalogRepository
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
        /// Конструктор репозитория каталога.
        /// </summary>
        /// <param name="dbCatalogDbContext"> Контекст базы данных. </param>
        /// <param name="mapper"> Маппер. </param>
        public CatalogRepository(CatalogDbContext dbCatalogDbContext, IMapper mapper)
        {
            _dbCatalogDbContext = dbCatalogDbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<CatalogDto> GetCatalog()
        {
            var dbCategories = await _dbCatalogDbContext.Categories.ToListAsync();
            var categories = _mapper.Map<List<CategoryDto>>(dbCategories);

            var dbBrands = await _dbCatalogDbContext.Brands.ToListAsync();
            var brands = _mapper.Map<List<BrandDto>>(dbBrands);

            var dbProducts = await _dbCatalogDbContext.Products.ToListAsync();
            var products = _mapper.Map<List<ProductDto>>(dbProducts);

            brands.ForEach(brand => 
                brand.Products = products.Where(product => product.BrandId == brand.BrandId).ToList());
            categories.ForEach(category => 
                category.Brands = brands.Where(brand => brand.CategoryId == category.CategoryId).ToList());
            
            var catalog = new CatalogDto { Categories = categories };
            return catalog;
        }
    }
}
