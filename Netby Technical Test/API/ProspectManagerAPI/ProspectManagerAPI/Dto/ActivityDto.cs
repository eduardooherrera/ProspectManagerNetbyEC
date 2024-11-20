namespace ProspectManagerAPI.Dto
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public required string Date { get; set; }
        public required string Type { get; set; }
        public byte Rating { get; set; }
    }
}
