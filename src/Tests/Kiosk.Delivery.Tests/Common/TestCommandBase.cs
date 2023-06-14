namespace Kiosk.Delivery.Tests.Common
{
    using Kiosk.DeliveryWebApi;

    /// <summary>
    /// Класс базовой команды для тестирования.
    /// </summary>
    public abstract class TestCommandBase : IDisposable
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        protected readonly DeliveryDbContext Context;

        /// <summary>
        /// Инициализация класса базовой команды с созданием контекста бд.
        /// </summary>
        public TestCommandBase()
        {
            Context = DeliveryContextFactory.Create();
        }

        /// <summary>
        /// Класс освобождения ресурсов с удалением контекста бд.
        /// </summary>
        public void Dispose()
        {
            DeliveryContextFactory.Destroy(Context);
        }
    }
}
