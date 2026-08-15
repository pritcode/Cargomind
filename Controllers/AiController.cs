using CargoMindApi.Models.DTOs;
using CargoMindApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoMindApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AiController : ControllerBase
	{
		private readonly AiService _aiService;

		public AiController(AiService aiService)
		{
			_aiService = aiService;
		}

		[HttpPost("chat")]
		public ActionResult<string> Chat([FromBody] AiChatRequest request)
		{
			var response = _aiService.Ask(request.Message);

			return Ok(response);
		}
	}
}
