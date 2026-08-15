namespace CargoMindApi.Models.DTOs
{
	public class ShipmentDto
	{
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
	}
}
