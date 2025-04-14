using EventHub_Api.Models;

namespace EventHub_Api.Services
{
    public class EventLogger
    {
        public static void LogEventCreation(Event ev)
        {
            Console.WriteLine($"[EVENT CREATED] {ev.Title} by {ev.OrganizerId} at {DateTime.UtcNow}");
        }

        public static void LogEventUpdate(Guid id)
        {
            Console.WriteLine($"[EVENT UPDATED] ID: {id} at {DateTime.UtcNow}");
        }

        public static void LogEventDeletion(Guid id)
        {
            Console.WriteLine($"[EVENT DELETED] ID: {id} at {DateTime.UtcNow}");
        }
    }
}
