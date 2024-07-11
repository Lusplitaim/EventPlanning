namespace EventPlanning.Core.Data.Entities
{
    public class EventEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Venue { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }
        public UserEntity Creator { get; set; }
        public ICollection<UserEntity> Members { get; set; } = [];
    }
}
