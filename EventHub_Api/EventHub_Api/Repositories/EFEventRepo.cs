using EventHub_Api.Data;
using EventHub_Api.Interfaces;
using EventHub_Api.Models;
using Microsoft.EntityFrameworkCore;
namespace EventHub_Api.Repositories
{
    public class EFEventRepo : IEventRepo
    {
        private readonly AppDbContext _context;

        public EFEventRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllAsync(string? query)
        {
            var events = _context.Events.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                events = events.Where(e => e.Title.Contains(query));
            }

            return await events.OrderByDescending(e => e.CreatedAt).ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(Guid id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> CreateAsync(Event newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<Event?> UpdateAsync(Guid id, Event updatedEvent)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(updatedEvent);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null) return false;

            _context.Events.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
