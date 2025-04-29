using progresssoft_task.Server.Enums;

namespace progresssoft_task.Server.DTOs
{
    public class CreateCardRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? ProfilePic { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
