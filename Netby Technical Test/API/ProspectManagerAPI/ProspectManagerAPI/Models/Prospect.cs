namespace ProspectManagerAPI.Models
{
    public class Prospect
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string CellPhoneNumber { get; set; }
        public required string Email { get; set; }
        public DateTime CreationDate { get; set; }

        public ICollection<Activity> Activities { get; set; } = [];
    }
}
