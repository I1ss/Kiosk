namespace Kiosk.Catalog.Tests.Common
{
    using Kiosk.CatalogWebApi;
    using Kiosk.CatalogWebApi.Models;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Класс для создания контекста базы данных для тестов.
    /// </summary>
    public class CatalogContextFactory
    {
        /// <summary>
        /// Константа идентификатора первой категории.
        /// </summary>
        public const int FIRST_CATEGORY_ID = 1;

        /// <summary>
        /// Константа идентификатора второй категории.
        /// </summary>
        public const int SECOND_CATEGORY_ID = 2;

        /// <summary>
        /// Константа идентификатора первого бренда.
        /// </summary>
        public const int FIRST_BRAND_ID = 1;

        /// <summary>
        /// Константа идентификатора второго бренда.
        /// </summary>
        public const int SECOND_BRAND_ID = 2;

        /// <summary>
        /// Константа идентификатора первого продукта.
        /// </summary>
        public const int FIRST_PRODUCT_ID = 1;

        /// <summary>
        /// Константа идентификатора второго продукта.
        /// </summary>
        public const int SECOND_PRODUCT_ID = 2;

        /// <summary>
        /// Создание контекста базы данных.
        /// </summary>
        /// <returns> Контекст БД для каталога. </returns>
        public static CatalogDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CatalogDbContext(options);
            context.Database.EnsureCreated();

            context.Categories.AddRange(
                new Category
                {
                    CategoryId = FIRST_CATEGORY_ID,
                    CategoryName = "Готовые завтраки"
                },
                new Category
                {
                    CategoryId = SECOND_CATEGORY_ID,
                    CategoryName = "Спортивные товары"
                }
            );

            context.Brands.AddRange(
                new Brand
                {
                    CategoryId = FIRST_CATEGORY_ID,
                    BrandId = FIRST_BRAND_ID,
                    BrandName = "Мираторг"
                },
                new Brand
                {
                    CategoryId = SECOND_CATEGORY_ID,
                    BrandId = SECOND_BRAND_ID,
                    BrandName = "Спортмастер"
                }
            );

            context.Products.AddRange(
                new Product
                {
                    BrandId = FIRST_BRAND_ID,
                    ProductCode = "123",
                    ProductId = FIRST_PRODUCT_ID,
                    ProductName = "Каша овсяная",
                    ProductPrice = 75
                },
                new Product
                {
                    BrandId = SECOND_BRAND_ID,
                    ProductCode = "234",
                    ProductId = SECOND_PRODUCT_ID,
                    ProductName = "Спортивные шорты",
                    ProductPrice = 1000
                }
            );

            context.SaveChanges();
            return context;
        }

        /// <summary>
        /// Удаление контекста базы данных.
        /// </summary>
        /// <param name="context"> Контекст БД для каталога. </param>
        public static void Destroy(CatalogDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
