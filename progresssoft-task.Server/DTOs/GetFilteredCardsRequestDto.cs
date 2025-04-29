using progresssoft_task.Server.Enums;

namespace progresssoft_task.Server.DTOs
{
    public class GetFilteredCardsRequestDto
    {
        public string? Name { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
