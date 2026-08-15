namespace CargoMindApi.Models.Entities
{
	public class Shipment
	{
		public Guid Id { get; set; }

		public string AwbNumber { get; set; } = string.Empty;

		public string Status { get; set; } = string.Empty;

		public string CurrentLocation { get; set; } = string.Empty;

		public string Origin { get; set; } = string.Empty;

		public string Destination { get; set; } = string.Empty;

		public DateTime ExpectedDeliveryDate { get; set; }

		public decimal RecordedWeightKg { get; set; }

		public decimal RecordedLengthCm { get; set; }

		public decimal RecordedWidthCm { get; set; }

		public decimal RecordedHeightCm { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}
