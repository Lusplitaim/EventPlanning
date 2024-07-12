using EventPlanning.Core.DTOs.Event;
using EventPlanning.Core.Models.Constants;
using EventPlanning.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanning.API.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IEventsService m_EventsService;
        public EventsController(IEventsService eventsService)
        {
            m_EventsService = eventsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await m_EventsService.GetEventsAsync();
            return Ok(events);
        }

        [HttpPost]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public async Task<IActionResult> CreateEvent(CreateEventDto model)
        {
            var userEvent = await m_EventsService.CreateEventAsync(model);
            return CreatedAtAction(nameof(CreateEvent), userEvent);
        }

        [HttpPost("{eventId:int}/members")]
        public async Task<IActionResult> RegisterToEvent(int eventId)
        {
            var success = await m_EventsService.RegisterToEvent(eventId);
            return success ? Ok() : BadRequest("Failed to register to event");
        }
    }
}
