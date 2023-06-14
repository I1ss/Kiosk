namespace Kiosk.DeliveryWebApi.Consumer
{
    using Kiosk.Core.Dtos.Delivery;
    using Kiosk.Core.Enums;
    using Kiosk.Core.Requests;
    using Kiosk.Core.Responses;
    using Kiosk.DeliveryWebApi.Repositories;

    using MassTransit;

    /// <summary>
    /// Консьюмер обновления задачи в доставке.
    /// </summary>
    public class UpdateIssueConsumer : IConsumer<UpdateIssueRequest>
    {
        /// <summary>
        /// Репозиторий заданий.
        /// </summary>
        private readonly IIssueRepository _issueRepository;

        /// <summary>
        /// Конструктор консьюмера обновления задачи в доставке.
        /// </summary>
        /// <param name="issueRepository"> Репозиторий заданий. </param>
        public UpdateIssueConsumer(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        /// <summary>
        /// Обновление задачи на доставку.
        /// </summary>
        /// <param name="context"> Контекст с запросом. </param>
        public async Task Consume(ConsumeContext<UpdateIssueRequest> context)
        {
            var order = context.Message.Order;
            var dbIssue = await _issueRepository.GetIssueByOrderId(order.OrderId);
            var issue = new IssueDto
            {
                IssueId = dbIssue?.IssueId ?? 0,
                OrderId = order.OrderId,
                Products = order.Products,
                OrderStatus = order.OrderStatus,
                TotalPrice = order.TotalPrice,
                Payment = order.TotalPrice * 0.15d
            };

            if (dbIssue == null)
                await _issueRepository.CreateIssue(issue);
            else if (issue.Products?.Any() != true || order.DeliveryType == DeliveryTypeEnum.Client)
                await _issueRepository.DeleteIssue(issue.IssueId);
            else
                await _issueRepository.UpdateIssue(issue);

            await context.RespondAsync(new IssueResponse { Issue = issue });
        }
    }
}
