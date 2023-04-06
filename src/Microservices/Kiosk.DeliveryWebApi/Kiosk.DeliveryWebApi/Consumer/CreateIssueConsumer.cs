namespace Kiosk.DeliveryWebApi.Consumer
{
    using Kiosk.DeliveryWebApi.Repositories;
    using Kiosk.Core.Dtos.Delivery;
    using Kiosk.Core.Requests;
    using Kiosk.Core.Responses;

    using MassTransit;

    /// <summary>
    /// Консьюмер для создания задачи для доставки.
    /// </summary>
    public class CreateIssueConsumer : IConsumer<CreateIssueRequest>
    {
        /// <summary>
        /// Репозиторий заданий.
        /// </summary>
        private readonly IIssueRepository _issueRepository;

        /// <summary>
        /// Конструктор консьюмера для создания задачи для доставки.
        /// </summary>
        /// <param name="issueRepository"> Репозиторий заданий. </param>
        public CreateIssueConsumer(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        /// <summary>
        /// Создание задачи для заказа.
        /// </summary>
        /// <param name="context"> Контекст с запросом. </param>
        public async Task Consume(ConsumeContext<CreateIssueRequest> context)
        {
            var order = context.Message.Order;
            var issue = new IssueDto
            {
                OrderId = order.OrderId,
                Products = order.Products,
                OrderStatus = order.OrderStatus,
                TotalPrice = order.TotalPrice,
                Payment = order.TotalPrice * 0.15d
            };

            await _issueRepository.CreateIssue(issue);
            await context.RespondAsync(new IssueResponse { Issue = issue });
        }
    }
}