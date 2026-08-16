using CargoMindApi.Models.DTOs;
using CargoMindApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoMindApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DocumentsController : ControllerBase
	{
		private readonly DocumentIngestionService _ingestionService;

		public DocumentsController(
			DocumentIngestionService ingestionService)
		{
			_ingestionService = ingestionService;
		}

		[HttpPost("ingest")]
		public async Task<ActionResult<Guid>> Ingest([FromBody] DocumentIngestionRequest request)
		{
			var documentId = await _ingestionService.IngestAsync(
				request.FileName,
				request.DocumentType,
				request.Content);

			return Ok(documentId);
		}
	}
}
