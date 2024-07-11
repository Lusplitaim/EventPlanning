using EventPlanning.Core.Data.Entities;

namespace EventPlanning.Core.DTOs.Event
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Venue { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatorId { get; set; }

        public static EventDto From(EventEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Venue = entity.Venue,
                IsOnline = entity.IsOnline,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                CreatorId = entity.CreatorId,
            };
        }
    }
}
