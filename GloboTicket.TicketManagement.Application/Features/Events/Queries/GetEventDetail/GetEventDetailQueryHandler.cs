using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Exceptions;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    // triggered by GetEventDetailQuery and return a single EventDetailVm
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVm>
    {
        // dependency injection
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        // triggered when the message is received
        public async Task<EventDetailVm> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            // return an EventDetailVm
            var @event = await _eventRepository.GetByIdAsync(request.Id);
            // use AutoMapper to map from Event to EventDetailVm
            var eventDetailDto = _mapper.Map<EventDetailVm>(@event);

            // since categoryDto will not be returned automatically by GetByIdAsync for an event
            // need to get the event's category from categoryRepository 
            var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);
            // map a Category entity to a CategoryDto
            eventDetailDto.Category = _mapper.Map<CategoryDto>(category);

            return eventDetailDto;
        }
    }
}
