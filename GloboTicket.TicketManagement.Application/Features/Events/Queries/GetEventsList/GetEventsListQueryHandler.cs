using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events
{
    // triggered by GetEventsListQuery and return a list of EventListVm
    public class GetEventsListQueryHandler : IRequestHandler<GetEventsListQuery, List<EventListVm>>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public GetEventsListQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        // ListAllAsync() is a method of IAsyncRepository 
        public async Task<List<EventListVm>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
        {
            // get all events ordered in by date
            // return a list of IOrderedEnumerable of events
            var allEvents = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);
            // use AutoMapper to map into the list of EventListVm
            return _mapper.Map<List<EventListVm>>(allEvents);
        }
    }
}
