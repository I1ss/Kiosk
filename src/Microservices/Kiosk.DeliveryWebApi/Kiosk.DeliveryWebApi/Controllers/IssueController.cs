namespace Kiosk.DeliveryWebApi.Controllers
{
    using Kiosk.Core.Dtos.Delivery;
    using Kiosk.DeliveryWebApi.Repositories;
     
    using Microsoft.AspNetCore.Mvc;

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
        /// Конструктор контроллера для заданий.
        /// </summary>
        /// <param name="taskRepository"> Репозиторий заданий. </param>
        public IssueController(IIssueRepository taskRepository)
        {
            _taskRepository = taskRepository;
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
            var issues = await _taskRepository.GetIssues();
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
            return Ok();
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
    }
}