using progresssoft_task.Server.DTOs;
using progresssoft_task.Server.Repositories.Interfaces;
using progresssoft_task.Server.Services.Interfaces;
using progresssoft_task.Server.Models;
using AutoMapper;
using progresssoft_task.Server.Common.Mapping;
using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Serialization;
using System.Text;
using Azure.Core;
using progresssoft_task.Server.DTOs.Exports;

namespace progresssoft_task.Server.Services
{
    public class BusinessCardService : IBusinessCardService
    {
        private readonly IGenericRepository<BusinessCard> _businessCardRepo;
        private readonly Mapper _mapper;
        public BusinessCardService(IGenericRepository<BusinessCard> businessCardRepo)
        {
            _businessCardRepo = businessCardRepo;
            _mapper = MapperConfig.InitializeAutomapper();

        }
        public async Task<BusinessCardDto> CreateBusinessCardAsync(CreateCardRequestDto businessCardDto)
        {
            var businessCard = _mapper.Map<BusinessCard>(businessCardDto);

            await _businessCardRepo.AddAsync(businessCard);
            await _businessCardRepo.SaveChangesAsync();

            return _mapper.Map<BusinessCardDto>(businessCard);


        }

        public async Task<List<BusinessCardDto>> ListAllCards()
        {
            var businessCards = await _businessCardRepo.GetAllAsync(x => new BusinessCard
            {
                Id = x.Id,
                Address = x.Address,
                BirthDate = x.BirthDate,
                Email = x.Email,
                Gender = x.Gender,
                Name = x.Name,
                Phone = x.Phone,
                ProfilePic = x.ProfilePic,
            });

            return _mapper.Map<List<BusinessCardDto>>(businessCards);
        }
        public async Task<List<BusinessCardDto>> GetFilteredList(GetFilteredCardsRequestDto request)
        {
            var filters = new List<Expression<Func<BusinessCard, bool>>>();

            if (!request.Name.IsNullOrEmpty())
            {
                filters.Add(x => x.Name.ToLower().Contains(request.Name!.ToLower()));
            }
            if (!request.Email.IsNullOrEmpty())
            {
                filters.Add(x => x.Email.ToLower().Contains(request.Email!.ToLower()));
            }

            if (!request.Phone.IsNullOrEmpty())
            {
                filters.Add(x => x.Phone == request.Phone);
            }
            
            if (request.Gender != null)
            {
                filters.Add(x =>  (int)x.Gender == request.Gender);
            }
            
            if (request.BirthDate != null)
            {
                filters.Add(x => x.BirthDate == request.BirthDate);
            }

            var businessCards = await _businessCardRepo.GetFilteredListAsync(filters, x => new BusinessCard
            {
                Id = x.Id,
                Address = x.Address,
                BirthDate = x.BirthDate,
                Email = x.Email,
                Gender = x.Gender,
                Name = x.Name,
                Phone = x.Phone,
                ProfilePic = x.ProfilePic,
            });

            return _mapper.Map<List<BusinessCardDto>>(businessCards);
        }

        public async Task DeleteCard(int id)
        {
            var businessCard = await _businessCardRepo.GetByIdAsync(id);

            if (businessCard == null)
                throw new KeyNotFoundException($"Business card with ID {id} not found.");

            _businessCardRepo.Remove(businessCard);
            await _businessCardRepo.SaveChangesAsync();
        }


        public async Task<byte[]> ExportToXML(GetFilteredCardsRequestDto request)
        {
            var businessCards = await this.GetFilteredList(request);
            var wrapper = new BusinessCardExport
            {
                Cards = businessCards
            };
            var serializer = new XmlSerializer(typeof(BusinessCardExport));

            using var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, wrapper);
            memoryStream.Position = 0;

            return memoryStream.ToArray();
        }

        public async Task<byte[]> ExportToCSV(GetFilteredCardsRequestDto request)
        {
            var businessCards = await this.GetFilteredList(request);

            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream, Encoding.UTF8);

            await writer.WriteLineAsync("Id,Name,Email,Phone,Gender,BirthDate,Address");

            foreach (var card in businessCards)
            {
                await writer.WriteLineAsync($"{card.Id},{card.Name},{card.Email},\"{card.Phone}\",{card.Gender},{card.BirthDateString},{card.Address}");
            }

            await writer.FlushAsync(); // Important! Flush the writer into the memory stream
            memoryStream.Position = 0;

            return memoryStream.ToArray();
        }
    }
}
