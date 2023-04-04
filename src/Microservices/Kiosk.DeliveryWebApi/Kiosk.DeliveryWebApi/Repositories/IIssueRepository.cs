namespace Kiosk.DeliveryWebApi.Repositories
{
    using Kiosk.Core.Dtos.Delivery;

    /// <summary>
    /// Репозиторий заданий.
    /// </summary>
    public interface IIssueRepository
    {
        /// <summary>
        /// Получить задание по идентификатору. 
        /// </summary>
        /// <param name="issueId"> Идентификатор задания. </param>
        /// <remarks> Задание берётся из базы данных. </remarks>
        /// <returns> Задание товаров. </returns>
        Task<IssueDto> GetIssue(int issueId);

        /// <summary>
        /// Получить все задания. 
        /// </summary>
        /// <remarks> Задания берутся из базы данных. </remarks>
        /// <returns> Задания товаров. </returns>
        Task<IEnumerable<IssueDto>> GetIssues();

        /// <summary>
        /// Создать новое задание.
        /// </summary>
        /// <param name="issue"> ДТО задания. </param>
        /// <remarks> Создаётся новое задание на основе ДТО. </remarks>
        Task CreateIssue(IssueDto issue);

        /// <summary>
        /// Обновить существующее задание.
        /// </summary>
        /// <param name="issue"> ДТО задания. </param>
        /// <remarks> Обновляется существующее задание на основе ДТО. </remarks>
        Task UpdateIssue(IssueDto issue);

        /// <summary>
        /// Удалить существующее задание.
        /// </summary>
        /// <param name="issueId"> Идентификатор задания. </param>
        /// <remarks> Удаляется существующее задание на основе ДТО. </remarks>
        Task DeleteIssue(int issueId);
    }
}
