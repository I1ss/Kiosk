namespace Kiosk.CatalogWebApi.Repositories
{
    using AutoMapper;
    using Kiosk.CatalogWebApi.Models;

    using Microsoft.EntityFrameworkCore;
    using Kiosk.Core.Dtos.Catalog;

    /// <inheritdoc cref="IBrandRepository" />
    public class BrandRepository : IBrandRepository
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
        /// Конструктор репозитория брендов.
        /// </summary>
        /// <param name="dbCatalogDbContext"> Контекст базы данных. </param>
        /// <param name="mapper"> Маппер. </param>
        public BrandRepository(CatalogDbContext dbCatalogDbContext, IMapper mapper)
        {
            _dbCatalogDbContext = dbCatalogDbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<BrandDto> GetBrand(int brandId)
        {
            var brand = await _dbCatalogDbContext.Brands.FindAsync(brandId);
            return _mapper.Map<BrandDto>(brand);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BrandDto>> GetBrands()
        {
            var brands = await _dbCatalogDbContext.Brands.ToListAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        /// <inheritdoc />
        public async Task CreateBrand(BrandDto brand)
        {
            var dbBrand = _mapper.Map<Brand>(brand);

            await _dbCatalogDbContext.AddAsync(dbBrand);
            await _dbCatalogDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateBrand(BrandDto brand)
        {
            var dbBrand = _mapper.Map<Brand>(brand);

            _dbCatalogDbContext.Update(dbBrand);
            await _dbCatalogDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteBrand(int brandId)
        {
            try
            {
                var brand = await _dbCatalogDbContext.Brands.FindAsync(brandId);
                _dbCatalogDbContext.Remove(brand);
                await _dbCatalogDbContext.SaveChangesAsync();
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException(exception.Message);
            }
        }
    }
}
