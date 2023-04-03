namespace PinPoint.Entities
{
    public class PinComment
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid PinId { get; set; }
        public string CommentMessage { get; set; } = null!;

        public User User { get; set; } = null!;
        public Pin Pin { get; set; } = null!;
    }
}