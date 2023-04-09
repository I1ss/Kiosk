namespace Kiosk.DeliveryWebApi.Controllers
{
    using Kiosk.Core.Dtos.Delivery;
    using Kiosk.DeliveryWebApi.Repositories;
    using Kiosk.Core.Requests;
    using Kiosk.Core.Responses;

    using MassTransit;

    using Microsoft.AspNetCore.Mvc;
    using Kiosk.Core.Enums;

    /// <summary>
    /// Контроллер для заданий.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        /// <summary>
        /// Репозиторий заданий.
        /// </summary>
        private readonly IIssueRepository _taskRepository;

        /// <summary>
        /// Клиент для запроса обновления.
        /// </summary>
        private readonly IRequestClient<UpdateOrderRequest> _requestUpdateClient;

        /// <summary>
        /// Клиент для запроса получения.
        /// </summary>
        private readonly IRequestClient<GetProductsInOrderRequest> _requestGetProductsClient;

        /// <summary>
        /// Конструктор контроллера для заданий.
        /// </summary>
        /// <param name="taskRepository"> Репозиторий заданий. </param>
        /// <param name="requestUpdateClient"> Клиент для запроса обновления. </param>
        /// <param name="requestGetProductsClient"> Клиент для запроса получения. </param>
        public IssueController(IIssueRepository taskRepository, IRequestClient<UpdateOrderRequest> requestUpdateClient, 
            IRequestClient<GetProductsInOrderRequest> requestGetProductsClient)
        {
            _taskRepository = taskRepository;
            _requestUpdateClient = requestUpdateClient;
            _requestGetProductsClient = requestGetProductsClient;
        }

        /// <summary>
        /// Получить задание по идентификатору. 
        /// </summary>
        /// <param name="issueId"> Идентификатор задания. </param>
        /// <remarks> Задание берётся из базы данных. </remarks>
        /// <returns> Задание. </returns>
        /// <response code="200"> Задание возвращено удачно. </response>
        /// <response code="502"> Задание не обнаружено. Проблема на стороне сервера. </response>
        [HttpGet("{issueId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IssueDto> GetIssue(int issueId)
        {
            var issue = await _taskRepository.GetIssue(issueId);

            var request = _requestGetProductsClient.Create(new GetProductsInOrderRequest());
            var response = await request.GetResponse<ProductsInOrderResponse>();

            var products = response.Message.Products.Where(product => product.OrderId == issue.OrderId);
            issue.Products = products.ToList();

            return issue;
        }

        /// <summary>
        /// Получить все задания. 
        /// </summary>
        /// <remarks> Задания берутся из базы данных. </remarks>
        /// <returns> Задания товаров. </returns>
        /// <response code="200"> Задания возвращены удачно. </response>
        /// <response code="502"> Задания не обнаружены. Проблема на стороне сервера. </response>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<IssueDto>> GetIssues()
        {
            var issues = (await _taskRepository.GetIssues()).ToList();

            var request = _requestGetProductsClient.Create(new GetProductsInOrderRequest());
            var response = await request.GetResponse<ProductsInOrderResponse>();

            var products = response.Message.Products.ToList();
            issues.ForEach(issue => issue.Products = products.Where(product => product.OrderId == issue.OrderId).ToList());

            return issues;
        }

        /// <summary>
        /// Создать новое задание.
        /// </summary>
        /// <param name="issue"> ДТО задания. </param>
        /// <remarks> Создаётся новое задание на основе ДТО. </remarks>
        /// <response code="200"> Задание создано удачно. </response>
        /// <response code="502"> Задание не обнаружено. Проблема на стороне сервера. </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> CreateTask([FromBody]IssueDto issue)
        {
            await _taskRepository.CreateIssue(issue);
            return Ok();
        }

        /// <summary>
        /// Обновить существующее задание.
        /// </summary>
        /// <param name="issue"> ДТО задания. </param>
        /// <remarks> Обновляется существующее задание на основе ДТО. </remarks>
        /// <response code="200"> Задание обновлено удачно. </response>
        /// <response code="502"> Задание не обновлено. Проблема на стороне сервера. </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> UpdateTask([FromBody]IssueDto issue)
        {
            await _taskRepository.UpdateIssue(issue);

            var request = _requestUpdateClient.Create(new UpdateOrderRequest { Issue = issue });
            var response = await request.GetResponse<OrderResponse>();

            return Ok(response);
        }

        /// <summary>
        /// Удалить существующее задание.
        /// </summary>
        /// <param name="issueId"> Идентификатор задания. </param>
        /// <remarks> Удаляется существующее задание на основе ДТО. </remarks>
        /// <response code="200"> Задание удалено удачно. </response>
        /// <response code="502"> Задание не удалено. Проблема на стороне сервера. </response>
        [HttpDelete("{issueId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> DeleteTask(int issueId)
        {
            await _taskRepository.DeleteIssue(issueId);
            return Ok();
        }

        /// <summary>
        /// Доставить заказ.
        /// </summary>
        /// <param name="issue"> ДТО задания. </param>
        /// <remarks> Доставляется существующий заказ на основе ДТО. </remarks>
        /// <response code="200"> Заказ доставлен удачно. </response>
        /// <response code="502"> Заказ не доставлен. Проблема на стороне сервера. </response>
        [HttpPut("completed-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> CompletedOrder([FromBody]IssueDto issue)
        {
            issue.OrderStatus = OrderStatusEnum.Completed;
            await UpdateTask(issue);

            return Ok();
        }

        /// <summary>
        /// Отменить/вернуть заказ.
        /// </summary>
        /// <param name="issue"> ДТО задания. </param>
        /// <remarks> Отменяется существующий заказ на основе ДТО. </remarks>
        /// <response code="200"> Заказ отменен удачно. </response>
        /// <response code="502"> Заказ не отменен. Проблема на стороне сервера. </response>
        [HttpPut("cancel-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> CancelOrder([FromBody]IssueDto issue)
        {
            issue.OrderStatus = OrderStatusEnum.Canceled;
            await UpdateTask(issue);

            return Ok();
        }
    }
}