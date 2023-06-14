namespace Kiosk.CatalogWebApi.Repositories
{
    using AutoMapper;

    using Kiosk.CatalogWebApi.Models;
    using Kiosk.Core.Dtos.Catalog;

    using Microsoft.EntityFrameworkCore;

    using System;

    /// <inheritdoc cref="ICategoryRepository" />
    public class CategoryRepository : ICategoryRepository
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
        /// Конструктор репозитория категорий.
        /// </summary>
        /// <param name="dbCatalogDbContext"> Контекст базы данных. </param>
        /// <param name="mapper"> Маппер. </param>
        public CategoryRepository(CatalogDbContext dbCatalogDbContext, IMapper mapper)
        {
            _dbCatalogDbContext = dbCatalogDbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<CategoryDto> GetCategory(int categoryId)
        {
            var category = await _dbCatalogDbContext.Categories.FindAsync(categoryId);
            return _mapper.Map<CategoryDto>(category);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _dbCatalogDbContext.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        /// <inheritdoc />
        public async Task CreateCategory(CategoryDto category)
        {
            var dbCategory = _mapper.Map<Category>(category);

            await _dbCatalogDbContext.AddAsync(dbCategory);
            await _dbCatalogDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateCategory(CategoryDto category)
        {
            try
            {
                var dbCategory = _mapper.Map<Category>(category);

                _dbCatalogDbContext.Update(dbCategory);
                await _dbCatalogDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        /// <inheritdoc />
        public async Task DeleteCategory(int categoryId)
        {
            try
            {
                var category = await _dbCatalogDbContext.Categories.FindAsync(categoryId);
                _dbCatalogDbContext.Remove(category);
                await _dbCatalogDbContext.SaveChangesAsync();
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException(exception.Message);
            }
        }
    }
}
