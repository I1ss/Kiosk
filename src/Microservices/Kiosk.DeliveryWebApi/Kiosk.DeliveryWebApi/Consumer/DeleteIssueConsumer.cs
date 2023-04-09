using Kiosk.Core.Dtos.Delivery;

namespace Kiosk.DeliveryWebApi.Consumer
{
    using Kiosk.Core.Requests;
    using Kiosk.Core.Responses;
    using Kiosk.DeliveryWebApi.Repositories;

    using MassTransit;

    /// <summary>
    /// Консьюмер для удаления задачи на доставку.
    /// </summary>
    public class DeleteIssueConsumer : IConsumer<DeleteIssueRequest>
    {
        /// <summary>
        /// Репозиторий заданий.
        /// </summary>
        private readonly IIssueRepository _issueRepository;

        /// <summary>
        /// Конструктор консьюмера для удаления задачи на доставку.
        /// </summary>
        /// <param name="issueRepository"> Репозиторий заданий. </param>
        public DeleteIssueConsumer(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }
        
        /// <summary>
        /// Удаление задачи на доставку.
        /// </summary>
        /// <param name="context"> Контекст с запросом. </param>
        public async Task Consume(ConsumeContext<DeleteIssueRequest> context)
        {
            var orderId = context.Message.OrderId;

            var issue = await _issueRepository.GetIssueByOrderId(orderId);
            if (issue == null)
            {
                await context.RespondAsync(new IssueResponse { Issue = new IssueDto() });
                return;
            }

            await _issueRepository.DeleteIssue(issue.IssueId);
            await context.RespondAsync(new IssueResponse { Issue = issue });
        }
    }
}
