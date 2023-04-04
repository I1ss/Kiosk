namespace Kiosk.OrdersWebApi.Controllers
{
    using Kiosk.Core.Dtos.Order;
    using Kiosk.OrdersWebApi.Repositories;

    using Microsoft.AspNetCore.Mvc;

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
        /// Конструктор контроллера для заказов.
        /// </summary>
        /// <param name="orderRepository"> Репозиторий заказов. </param>
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Получить заказ по идентификатору. 
        /// </summary>
        /// <param name="orderId"> Идентификатор заказа. </param>
        /// <remarks> Заказ берётся из базы данных. </remarks>
        /// <returns> Заказ товаров. </returns>
        /// <response code="200"> Заказ возвращен удачно. </response>
        /// <response code="502"> Заказ не обнаружен. Проблема на стороне сервера. </response>
        [HttpGet("order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
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
        [HttpGet("orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
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
        [HttpPost("order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> CreateOrder([FromBody]OrderDto order)
        {
            await _orderRepository.CreateOrder(order);
            return Ok();
        }

        /// <summary>
        /// Обновить существующий заказ.
        /// </summary>
        /// <param name="order"> ДТО заказа. </param>
        /// <remarks> Обновляется существующий заказ на основе ДТО. </remarks>
        /// <response code="200"> Заказ обновлен удачно. </response>
        /// <response code="502"> Заказ не обновлен. Проблема на стороне сервера. </response>
        [HttpPut("order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> UpdateOrder([FromBody]OrderDto order)
        {
            await _orderRepository.UpdateOrder(order);
            return Ok();
        }

        /// <summary>
        /// Удалить существующий заказ.
        /// </summary>
        /// <param name="orderId"> Идентификатор заказа. </param>
        /// <remarks> Удаляется существующий заказ на основе ДТО. </remarks>
        /// <response code="200"> Заказ удален удачно. </response>
        /// <response code="502"> Заказ не удален. Проблема на стороне сервера. </response>
        [HttpDelete("order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await _orderRepository.DeleteOrder(orderId);
            return Ok();
        }
    }
}
