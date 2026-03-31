using Chiro.Application.Dtos.Event;
using Chiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Interfaces
{
    public interface IEventMapper
    {
        public EventDto MapToEventDto(Event eventEntity);
        public Event MapFromEventDto(EventDto eventDto);
    }
}
