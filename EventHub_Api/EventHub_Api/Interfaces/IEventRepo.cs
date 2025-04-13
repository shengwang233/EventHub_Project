using EventHub_Api.Models;

namespace EventHub_Api.Interfaces
{
    public interface IEventRepo
    {
        Task<List<Event>> GetAllAsync(string? query);
        Task<Event?> GetByIdAsync(Guid id);
        Task<Event> CreateAsync(Event newEvent);
        Task<Event?> UpdateAsync(Guid id, Event updatedEvent);
        Task<bool> DeleteAsync(Guid id);
    }
}
