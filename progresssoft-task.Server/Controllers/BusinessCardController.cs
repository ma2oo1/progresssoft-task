using Microsoft.AspNetCore.Mvc;
using progresssoft_task.Server.DTOs;
using progresssoft_task.Server.Services.Interfaces;

namespace progresssoft_task.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessCardController : ControllerBase
    {
        private readonly IBusinessCardService _businessCardService;
        public BusinessCardController(IBusinessCardService businessCardService)
        {
            _businessCardService = businessCardService;
        }

        [HttpPost("CreateBusinessCard")]
        public async Task<IActionResult> CreateBusinessCardAsync([FromBody]CreateCardRequestDto request)
        {
            var Result = await _businessCardService.CreateBusinessCardAsync(request);
            return Ok(Result);
        }

        [HttpGet("ListBusinessCards")]
        public async Task<IActionResult> ListBusinessCardsAsync()
        {
            var Result = await _businessCardService.ListAllCards();
            return Ok(Result);
        }

        [HttpGet("ListFilteredBusinessCards")]
        public async Task<IActionResult> ListFilteredBusinessCardsAsync([FromQuery]GetFilteredCardsRequestDto request)
        {
            var Result = await _businessCardService.GetFilteredList(request);
            return Ok(Result);
        }

        [HttpDelete("DeleteBusinessCard/{id}")]
        public async Task<IActionResult> DeleteBusinessCard(int id)
        {
            await _businessCardService.DeleteCard(id);
            return NoContent();
        }

        [HttpGet("ExportToXML")]
        public async Task<IActionResult> ExportToXML([FromQuery] GetFilteredCardsRequestDto request)
        {
            var xmlBytes = await _businessCardService.ExportToXML(request);
            return File(xmlBytes, "application/xml", "businesscards.xml");
        }

        [HttpGet("ExportToCSV")]
        public async Task<IActionResult> ExportToCSV([FromQuery] GetFilteredCardsRequestDto request)
        {
            var csvBytes = await _businessCardService.ExportToCSV(request);
            return File(csvBytes, "text/csv", "businesscards.csv");
        }
    }
}
