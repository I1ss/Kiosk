namespace Kiosk.Catalog.Tests.Common
{
    using Kiosk.CatalogWebApi;

    /// <summary>
    /// Класс базовой команды для тестирования.
    /// </summary>
    public abstract class TestCommandBase : IDisposable
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        protected readonly CatalogDbContext Context;

        /// <summary>
        /// Инициализация класса базовой команды с созданием контекста бд.
        /// </summary>
        public TestCommandBase()
        {
            Context = CatalogContextFactory.Create();
        }

        /// <summary>
        /// Класс освобождения ресурсов с удалением контекста бд.
        /// </summary>
        public void Dispose()
        {
            CatalogContextFactory.Destroy(Context);
        }
    }
}
