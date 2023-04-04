using Kiosk.Delivery.Tests.Common;
using Kiosk.DeliveryWebApi;
using Kiosk.DeliveryWebApi.Repositories;

namespace Kiosk.Delivery.Tests.Commands.Issues
{
    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования удаления задач.
    /// </summary>
    public class DeleteIssueRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное удаление задачи.
        /// </summary>
        [Test]
        public async Task DeleteIssue_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new IssueRepository(Context, mapper);

            // Act
            await repository.DeleteIssue(DeliveryContextFactory.FIRST_ISSUE_ID);

            // Assert
            Assert.Null(Context.Issues.SingleOrDefault(issue =>
                issue.IssueId == DeliveryContextFactory.FIRST_ISSUE_ID));
        }

        /// <summary>
        /// Ожидается неуспешное удаление задачи.
        /// </summary>
        /// <remarks> Некорректный идентификатор задачи используется. </remarks>
        [Test]
        public void DeleteIssue_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new IssueRepository(Context, mapper);

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await repository.DeleteIssue(-1));
        }
    }
}
