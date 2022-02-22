using GloboTicket.TicketManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        // passing in a boolean value either getting the historical events or not
        Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents);
    }
}
