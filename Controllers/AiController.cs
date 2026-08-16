using CargoMindApi.Data;
using CargoMindApi.Models.DTOs;
using CargoMindApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoMindApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AiController : ControllerBase
	{
		private readonly AiService _aiService;
		private readonly CargoMindDbContext _dbContext;

		public AiController(AiService aiService, CargoMindDbContext dbContext)
		{
			_aiService = aiService;
			_dbContext = dbContext;
		}

		[HttpPost("chat")]
		public async Task<ActionResult<string>> Chat([FromBody] AiChatRequest request)
		{
			var response = await _aiService.AskAsync(request.Message);

			return Ok(response);
		}


		[HttpPost("shipment-analysis/{awb}")]
		public async Task<ActionResult<string>> AnalyzeShipment(
	string awb,
	[FromBody] AiChatRequest request)
		{
			var shipment = await _dbContext.Shipments
				.FirstOrDefaultAsync(x => x.AwbNumber == awb);

			if (shipment == null)
			{
				return NotFound($"Shipment with AWB '{awb}' was not found.");
			}

			var response = await _aiService.AnalyzeShipmentAsync(
				shipment,
				request.Message);

			return Ok(response);
		}

	}
}
