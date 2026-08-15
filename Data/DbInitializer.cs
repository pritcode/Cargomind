using CargoMindApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoMindApi.Data
{
	public class DbInitializer
	{
		public static async Task InitializeAsync(
		CargoMindDbContext db)
		{
			await db.Database.MigrateAsync();

			if (await db.Shipments.AnyAsync())
			{
				return;
			}

			var shipments = new[]
			{
			new Shipment
			{
				Id = Guid.NewGuid(),
				AwbNumber = "AWB100001",
				Status = "In Transit",
				CurrentLocation = "Delhi Hub",
				Origin = "Delhi",
				Destination = "Mumbai",
				ExpectedDeliveryDate = DateTime.UtcNow.Date.AddDays(2),
				RecordedWeightKg = 8.50m,
				RecordedLengthCm = 40.00m,
				RecordedWidthCm = 30.00m,
				RecordedHeightCm = 20.00m,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			},

			new Shipment
			{
				Id = Guid.NewGuid(),
				AwbNumber = "AWB100002",
				Status = "Delayed",
				CurrentLocation = "Jaipur Hub",
				Origin = "Delhi",
				Destination = "Bangalore",
				ExpectedDeliveryDate = DateTime.UtcNow.Date.AddDays(-1),
				RecordedWeightKg = 12.20m,
				RecordedLengthCm = 50.00m,
				RecordedWidthCm = 40.00m,
				RecordedHeightCm = 25.00m,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			}
		};

			await db.Shipments.AddRangeAsync(shipments);
			await db.SaveChangesAsync();
		}
	}
}
