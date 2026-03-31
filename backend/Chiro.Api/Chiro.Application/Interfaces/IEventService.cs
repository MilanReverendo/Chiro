using Chiro.Application.Dtos.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto> GetEventByIdAsync(Guid id);
        Task<EventDto> CreateEventAsync(EventDto createEventDto);
        Task<EventDto> UpdateEventAsync(Guid id, EventDto updateEventDto);
        Task DeleteEventAsync(Guid id);
    }
}
