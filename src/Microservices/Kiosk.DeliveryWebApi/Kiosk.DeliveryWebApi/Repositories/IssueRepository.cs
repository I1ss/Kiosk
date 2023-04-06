namespace Kiosk.DeliveryWebApi.Repositories
{
    using AutoMapper;
    
    using Kiosk.Core.Dtos.Delivery;
    using Kiosk.DeliveryWebApi.Models;

    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc cref="IIssueRepository"/>
    public class IssueRepository : IIssueRepository
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly DeliveryDbContext _deliveryDbContext;

        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор репозитория брендов.
        /// </summary>
        /// <param name="deliveryDbContext"> Контекст базы данных. </param>
        /// <param name="mapper"> Маппер. </param>
        public IssueRepository(DeliveryDbContext deliveryDbContext, IMapper mapper)
        {
            _deliveryDbContext = deliveryDbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IssueDto> GetIssue(int issueId)
        {
            var issue = await _deliveryDbContext.Issues.FindAsync(issueId);
            return _mapper.Map<IssueDto>(issue);
        }

        /// <inheritdoc />
        public async Task<IssueDto> GetIssueByOrderId(int orderId)
        {
            var issue = await _deliveryDbContext.Issues.FirstOrDefaultAsync(issue => issue.OrderId == orderId);
            return _mapper.Map<IssueDto>(issue);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IssueDto>> GetIssues()
        {
            var issues = await _deliveryDbContext.Issues.ToListAsync();
            return _mapper.Map<IEnumerable<IssueDto>>(issues);
        }

        /// <inheritdoc />
        public async Task CreateIssue(IssueDto issue)
        {
            var dbIssue = _mapper.Map<Issue>(issue);

            await _deliveryDbContext.AddAsync(dbIssue);
            await _deliveryDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateIssue(IssueDto issue)
        {
            var dbIssue = _mapper.Map<Issue>(issue);

            _deliveryDbContext.Update(dbIssue);
            await _deliveryDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteIssue(int issueId)
        {
            try
            {
                var issue = await _deliveryDbContext.Issues.FindAsync(issueId);
                _deliveryDbContext.Remove(issue);
                await _deliveryDbContext.SaveChangesAsync();
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException(exception.Message);
            }
        }
    }
}
