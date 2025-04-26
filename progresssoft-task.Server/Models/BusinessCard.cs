using System.ComponentModel.DataAnnotations;
using progresssoft_task.Server.Enums;

namespace progresssoft_task.Server.Models
{
    public class BusinessCard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        public string? ProfilePic {  get; set; }
        [Required]
        public string Address { get; set;} = string.Empty;

    }
}
