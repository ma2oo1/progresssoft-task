using progresssoft_task.Server.Models;
using System.Xml.Serialization;

namespace progresssoft_task.Server.DTOs.Exports
{
    [XmlRoot("BusinessCards")]
    public class BusinessCardExport
    {
        [XmlElement("BusinessCard")]
        public List<BusinessCardDto> Cards { get; set; }
    }
}
