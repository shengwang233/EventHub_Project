using System.ComponentModel.DataAnnotations;

namespace EventHub_Api.Models
{
    public class Event
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public string Location { get; set; } = "";

        public string ImageUrl { get; set; } = "";

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string CategoryId { get; set; } = "";

        public string Price { get; set; } = "";

        public bool IsFree { get; set; }

        public string Url { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
