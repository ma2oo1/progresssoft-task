using progresssoft_task.Server.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace progresssoft_task.Server.DTOs
{
    public class BusinessCardDto
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlElement("FullName")]
        public string Name { get; set; } = string.Empty;
        [XmlElement("Gender")]
        public string Gender { get; set; } = string.Empty; 
        [XmlIgnore]
        public DateTime BirthDate { get; set; }
        [XmlElement("EmailAddress")]
        public string Email { get; set; } = string.Empty;
        [XmlElement("PhoneNumber")]
        public string Phone { get; set; } = string.Empty; 
        [XmlElement("ProfilePicture")]
        public string? ProfilePic { get; set; }
        [XmlElement("Address")]
        public string Address { get; set; } = string.Empty;

        [XmlElement("BirthDate")]
        public string BirthDateString
        {
            get => BirthDate.ToString("yyyy-MM-dd");
            set => BirthDate = DateTime.Parse(value);
        }
    }
}
