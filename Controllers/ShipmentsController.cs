using CargoMindApi.Data;
using CargoMindApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoMindApi.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class ShipmentsController : ControllerBase
	{
		private readonly CargoMindDbContext _db;

		public ShipmentsController(CargoMindDbContext db)
		{
			_db = db;
		}

		[HttpGet("{awb}")]
		public async Task<ActionResult<ShipmentDto>> GetByAwb(string awb)
		{
			var shipment = await _db.Shipments
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.AwbNumber == awb);

			if (shipment is null)
			{
				return NotFound();
			}

			var result = new ShipmentDto
			{
				AwbNumber = shipment.AwbNumber,
				Status = shipment.Status,
				CurrentLocation = shipment.CurrentLocation,
				Origin = shipment.Origin,
				Destination = shipment.Destination,
				ExpectedDeliveryDate = shipment.ExpectedDeliveryDate,
				RecordedWeightKg = shipment.RecordedWeightKg,
				RecordedLengthCm = shipment.RecordedLengthCm,
				RecordedWidthCm = shipment.RecordedWidthCm,
				RecordedHeightCm = shipment.RecordedHeightCm
			};

			return Ok(result);
		}

	}
}
