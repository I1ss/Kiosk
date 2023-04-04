namespace Kiosk.Delivery.Tests.Commands.Issues
{
    using Kiosk.Delivery.Tests.Common;
    using Kiosk.DeliveryWebApi;
    using Kiosk.DeliveryWebApi.Repositories;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования получения задач.
    /// </summary>
    public class GetIssueRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное получение задачи.
        /// </summary>
        [Test]
        public async Task GetIssue_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new IssueRepository(Context, mapper);

            // Act
            var issue = await repository.GetIssue(DeliveryContextFactory.FIRST_ISSUE_ID);

            // Assert
            Assert.NotNull(issue);
        }

        /// <summary>
        /// Ожидается успешное получение задач.
        /// </summary>
        [Test]
        public async Task GetIssues_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new IssueRepository(Context, mapper);

            // Act
            var issues = await repository.GetIssues();

            // Assert
            Assert.That(issues.Count(), Is.EqualTo(Context.Issues.Count()));
        }
    }
}
