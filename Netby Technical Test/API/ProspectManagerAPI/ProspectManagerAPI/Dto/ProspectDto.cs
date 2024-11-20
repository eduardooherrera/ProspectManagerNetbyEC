namespace ProspectManagerAPI.Dto
{
    public class ProspectDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string CellPhoneNumber { get; set; }
        public required string Email { get; set; }
        public List<ActivityDto> Activities { get; set; } = [];

    }


}
