using progresssoft_task.Server.DTOs;

namespace progresssoft_task.Server.Services.Interfaces
{
    public interface IBusinessCardService 
    {
        Task<BusinessCardDto> CreateBusinessCardAsync(CreateCardRequestDto businessCardDto);
        Task<List<BusinessCardDto>> ListAllCards();
        Task<List<BusinessCardDto>> GetFilteredList(GetFilteredCardsRequestDto request);
        Task DeleteCard(int id);
        Task<byte[]> ExportToXML(GetFilteredCardsRequestDto request);
        Task<byte[]> ExportToCSV(GetFilteredCardsRequestDto request);

    }
}
