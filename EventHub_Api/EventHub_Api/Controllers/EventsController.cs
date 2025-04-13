using EventHub_Api.Interfaces;
using EventHub_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventHub_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepo _eventRepo;

        public EventsController(IEventRepo eventRepo)
        {
            _eventRepo = eventRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAll([FromQuery] string? query)
        {
            var events = await _eventRepo.GetAllAsync(query);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetById(Guid id)
        {
            var ev = await _eventRepo.GetByIdAsync(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> Create([FromBody] Event newEvent)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _eventRepo.CreateAsync(newEvent);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Event>> Update(Guid id, [FromBody] Event updatedEvent)
        {
            if (id != updatedEvent.Id)
                return BadRequest("Mismatched Event ID");

            var result = await _eventRepo.UpdateAsync(id, updatedEvent);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _eventRepo.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
