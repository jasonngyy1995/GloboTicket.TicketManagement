using System;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    // info about the category of the event
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
