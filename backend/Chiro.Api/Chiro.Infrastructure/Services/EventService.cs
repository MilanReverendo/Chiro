using Chiro.Application.Interfaces;
using Chiro.Application.Dtos.Event;
using Chiro.Domain.Entities;
using Chiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chiro.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventMapper _mapper;
        private readonly ChiroDbContext _context;

        public EventService(IEventMapper mapper, ChiroDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var eventEntities = await _context.Events.ToListAsync();
            return eventEntities.Select(e => _mapper.MapToEventDto(e));
        }

        public async Task<EventDto> GetEventByIdAsync(Guid id)
        {
            var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            return eventEntity == null ? null : _mapper.MapToEventDto(eventEntity);
        }

        public async Task<EventDto> CreateEventAsync(EventDto eventDto)
        {
            var eventEntity = _mapper.MapFromEventDto(eventDto);

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();

            return _mapper.MapToEventDto(eventEntity);
        }

        public async Task<EventDto> UpdateEventAsync(Guid id, EventDto eventDto)
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEvent == null)
                throw new Exception("Event not found");


            existingEvent.Name = eventDto.Name;
            existingEvent.Description = eventDto.Description;
            existingEvent.StartDate = eventDto.StartDate;
            existingEvent.EndDate = eventDto.EndDate;

            await _context.SaveChangesAsync();
            return _mapper.MapToEventDto(existingEvent);
        }

        public async Task DeleteEventAsync(Guid id)
        {
            var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (eventEntity == null)
            {
                throw new Exception("Event not found");
            }

            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
        }
    }
}