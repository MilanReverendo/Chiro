using Microsoft.AspNetCore.Mvc;
using Chiro.Application.Interfaces;
using Chiro.Application.Dtos.Event;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chiro.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            var eventDto = await _eventService.GetEventByIdAsync(id);
            if (eventDto is null)
                return NotFound();

            return Ok(eventDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent(EventDto eventDto)
        {
            var createdEvent = await _eventService.CreateEventAsync(eventDto);
            return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, EventDto eventDto)
        {
            try
            {
                var updatedEvent = await _eventService.UpdateEventAsync(id, eventDto);
                return Ok(updatedEvent);
            }
            catch (Exception ex) when (ex.Message == "Event not found")
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            try
            {
                await _eventService.DeleteEventAsync(id);
                return NoContent();
            }
            catch (Exception ex) when (ex.Message == "Event not found")
            {
                return NotFound();
            }
        }
    }
}