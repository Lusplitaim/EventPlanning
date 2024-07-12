namespace EventPlanning.Core.DTOs.Event
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Venue { get; set; }
        public bool IsOnline { get; set; }
        public int? MaxMembersCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
