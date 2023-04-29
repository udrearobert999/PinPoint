namespace Domain.Entities
{
    public class PinTag
    {
        public Guid Id { get; set; }
        public Guid PinId { get; set; }
        public Pin Pin { get; set; } = null!;

        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}