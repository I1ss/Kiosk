namespace Kiosk.Delivery.Tests.Commands.Issues
{
    using Kiosk.Core.Dtos.Delivery;
    using Kiosk.Delivery.Tests.Common;
    using Kiosk.DeliveryWebApi;
    using Kiosk.DeliveryWebApi.Repositories;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования обновления задач.
    /// </summary>
    public class UpdateIssueRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное обновление задачи.
        /// </summary>
        [Test]
        public async Task UpdateIssue_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new IssueRepository(Context, mapper);
            var updatedTotalPrice = 555;
            var issueId = DeliveryContextFactory.FIRST_ISSUE_ID;

            // Избавляемся от отслеживания сущностей во избежание ошибок.
            foreach (var entity in Context.ChangeTracker.Entries()) 
                entity.State = EntityState.Detached;

            var issueDto = new IssueDto
            {
                IssueId = DeliveryContextFactory.FIRST_ISSUE_ID,
                TotalPrice = updatedTotalPrice
            };

            // Act
            await repository.UpdateIssue(issueDto);

            // Assert
            Assert.NotNull(await Context.Issues.SingleOrDefaultAsync(issue =>
                issue.IssueId == issueId && issue.TotalPrice.Equals(updatedTotalPrice)));
        }

        /// <summary>
        /// Ожидается неуспешное обновление задачи.
        /// </summary>
        /// <remarks> Некорректный идентификатор задачи используется. </remarks>
        [Test]
        public void UpdateIssue_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new IssueRepository(Context, mapper);
            var issue = new IssueDto { IssueId = -1, };

            // Act
            // Assert
            Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repository.UpdateIssue(issue));
        }
    }
}
