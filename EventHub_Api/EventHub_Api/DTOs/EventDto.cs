namespace EventHub_Api.DTOs
{
    public class EventDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string CategoryId { get; set; } = "";
        public string Price { get; set; } = "";
        public bool IsFree { get; set; }
        public string Url { get; set; } = "";
    }
}
