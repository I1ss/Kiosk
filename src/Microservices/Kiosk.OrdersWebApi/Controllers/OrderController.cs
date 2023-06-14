namespace Kiosk.OrdersWebApi.Controllers
{
    using Kiosk.Core.Dtos.Order;
    using Kiosk.Core.Enums;
    using Kiosk.Core.Responses;
    using Kiosk.OrdersWebApi.Repositories;
    using Kiosk.Core.Requests;

    using MassTransit;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Контроллер для заказов.
    /// </summary>
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Репозиторий заказов.
        /// </summary>
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Клиент для запроса создания.
        /// </summary>
        private readonly IRequestClient<CreateIssueRequest> _requestCreateClient;

        /// <summary>
        /// Клиент для запроса обновления.
        /// </summary>
        private readonly IRequestClient<UpdateIssueRequest> _requestUpdateClient;

        /// <summary>
        /// Клиент для запроса удаления.
        /// </summary>
        private readonly IRequestClient<DeleteIssueRequest> _requestDeleteClient;

        /// <summary>
        /// Конструктор контроллера для заказов.
        /// </summary>
        /// <param name="orderRepository"> Репозиторий заказов. </param>
        /// <param name="requestCreateClient"> Клиент для запроса создания. </param>
        /// <param name="requestUpdateClient"> Клиент для запроса обновления. </param>
        /// <param name="requestDeleteClient"> Клиент для запроса удаления. </param>
        public OrderController(IOrderRepository orderRepository, IRequestClient<CreateIssueRequest> requestCreateClient, 
            IRequestClient<UpdateIssueRequest> requestUpdateClient, IRequestClient<DeleteIssueRequest> requestDeleteClient)
        {
            _orderRepository = orderRepository;
            _requestCreateClient = requestCreateClient;
            _requestUpdateClient = requestUpdateClient;
            _requestDeleteClient = requestDeleteClient;
        }

        /// <summary>
        /// Получить заказ по идентификатору. 
        /// </summary>
        /// <param name="orderId"> Идентификатор заказа. </param>
        /// <remarks> Заказ берётся из базы данных. </remarks>
        /// <returns> Заказ товаров. </returns>
        /// <response code="200"> Заказ возвращен удачно. </response>
        /// <response code="502"> Заказ не обнаружен. Проблема на стороне сервера. </response>
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<OrderDto> GetOrder(int orderId)
        {
            var order = await _orderRepository.GetOrder(orderId);
            return order;
        }

        /// <summary>
        /// Получить все заказы. 
        /// </summary>
        /// <remarks> Заказы берутся из базы данных. </remarks>
        /// <returns> Заказы товаров. </returns>
        /// <response code="200"> Заказы возвращены удачно. </response>
        /// <response code="502"> Заказы не обнаружены. Проблема на стороне сервера. </response>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            var orders = await _orderRepository.GetOrders();
            return orders;
        }

        /// <summary>
        /// Создать новый заказ.
        /// </summary>
        /// <param name="order"> ДТО заказа. </param>
        /// <remarks> Создаётся новый заказ на основе ДТО. </remarks>
        /// <response code="200"> Заказ создан удачно. </response>
        /// <response code="502"> Заказ не обнаружен. Проблема на стороне сервера. </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody]OrderDto order)
        {
            order.OrderId = await _orderRepository.CreateOrder(order);
            if (order.DeliveryType == DeliveryTypeEnum.Client || order.Products?.Any() != true)
                return Ok();

            // Создаётся экземпляр задания в доставку, если указан тип получения "курьером".
            var issueRequest = new CreateIssueRequest { Order = order };
            var response = await _requestCreateClient.GetResponse<IssueResponse>(issueRequest);
            var issue = response.Message.Issue;

            return Ok(issue);
        }

        /// <summary>
        /// Обновить существующий заказ.
        /// </summary>
        /// <param name="order"> ДТО заказа. </param>
        /// <remarks> Обновляется существующий заказ на основе ДТО. </remarks>
        /// <response code="200"> Заказ обновлен удачно. </response>
        /// <response code="502"> Заказ не обновлен. Проблема на стороне сервера. </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> UpdateOrder([FromBody]OrderDto order)
        {
            await _orderRepository.UpdateOrder(order);

            var issueRequest = new UpdateIssueRequest { Order = order };
            var response = await _requestUpdateClient.GetResponse<IssueResponse>(issueRequest);
            var issue = response.Message.Issue;

            return Ok(issue);
        }

        /// <summary>
        /// Удалить существующий заказ.
        /// </summary>
        /// <param name="orderId"> Идентификатор заказа. </param>
        /// <remarks> Удаляется существующий заказ на основе ДТО. </remarks>
        /// <response code="200"> Заказ удален удачно. </response>
        /// <response code="502"> Заказ не удален. Проблема на стороне сервера. </response>
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await _orderRepository.DeleteOrder(orderId);

            var issueRequest = new DeleteIssueRequest { OrderId = orderId };
            var response = await _requestDeleteClient.GetResponse<IssueResponse>(issueRequest);

            return Ok(orderId);
        }

        /// <summary>
        /// Отменить заказ.
        /// </summary>
        /// <param name="order"> ДТО заказа. </param>
        /// <remarks> Отменяется существующий заказ на основе ДТО. </remarks>
        /// <response code="200"> Заказ отменен удачно. </response>
        /// <response code="502"> Заказ не отменен. Проблема на стороне сервера. </response>
        [HttpPut("cancel-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize(Roles = "Admin,Seller,Courier")]
        public async Task<IActionResult> CancelOrder([FromBody]OrderDto order)
        {
            order.OrderStatus = OrderStatusEnum.Canceled;
            await UpdateOrder(order);

            return Ok();
        }
    }
}
