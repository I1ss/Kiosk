namespace Kiosk.Order.Tests.Common
{
    using Kiosk.OrdersWebApi;

    /// <summary>
    /// Класс базовой команды для тестирования.
    /// </summary>
    public abstract class TestCommandBase : IDisposable
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        protected readonly OrderDbContext Context;

        /// <summary>
        /// Инициализация класса базовой команды с созданием контекста бд.
        /// </summary>
        public TestCommandBase()
        {
            Context = OrderContextFactory.Create();
        }

        /// <summary>
        /// Класс освобождения ресурсов с удалением контекста бд.
        /// </summary>
        public void Dispose()
        {
            OrderContextFactory.Destroy(Context);
        }
    }
}
