namespace ProspectManagerAPI.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public Guid ProspectId { get; set; }
        public DateTime Date { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
        public byte Rating { get; set; }

        public Prospect? Prospect { get; set; }

    }
}
