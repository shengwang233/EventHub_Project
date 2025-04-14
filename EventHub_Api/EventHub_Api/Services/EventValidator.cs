using EventHub_Api.Models;

namespace EventHub_Api.Services
{
    public static class EventValidator
    {
        public static bool IsDateTimeRangeValid(Event ev)
        {
            return ev.StartDateTime < ev.EndDateTime;
        }
    }

}
