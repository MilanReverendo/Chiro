using Chiro.Application.Dtos.Event;
using Chiro.Application.Interfaces;
using Chiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Mappers
{
    public class EventMapper : IEventMapper
    {
        public Event MapFromEventDto(EventDto eventDto)
        {
            if (eventDto == null) return null!;

            return new Event
            {
                Id = eventDto.Id,
                Name = eventDto.Name,
                Description = eventDto.Description,
                StartDate = eventDto.StartDate,
                EndDate = eventDto.EndDate
            };
        }

        public EventDto MapToEventDto(Event eventEntity)
        {
            if (eventEntity == null) return null!;

            return new EventDto
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name ?? string.Empty,
                Description = eventEntity.Description ?? string.Empty,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate
            };
        }
    }
}
