namespace Kiosk.Delivery.Tests.Commands.Issues
{
    using Kiosk.Delivery.Tests.Common;
    using Kiosk.DeliveryWebApi;
    using Kiosk.Core.Dtos.Delivery;
    using Kiosk.DeliveryWebApi.Repositories;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования создания задач.
    /// </summary>
    public class CreateIssueRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное создание задачи.
        /// </summary>
        [Test]
        public async Task CreateIssue_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new IssueRepository(Context, mapper);
            var random = new Random();
            var issueId = random.Next(10, 20);

            var issueDto = new IssueDto()
            {
                IssueId = issueId
            };

            // Act
            await repository.CreateIssue(issueDto);

            // Assert
            Assert.NotNull(await Context.Issues.SingleOrDefaultAsync(issue => issue.IssueId == issueId));
        }
    }
}
