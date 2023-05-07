namespace Domain.Entities
{
    public class Like : IEntity<Guid>
    {
        public Guid Id { get; }

        public Guid UserId { get; set; }
        public Guid PinId { get; set; }
        public User User { get; set; } = null!;
        public Pin Pin { get; set; } = null!;
    }
}