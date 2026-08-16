namespace CargoMindApi.Models.DTOs
{
	public class DocumentIngestionRequest
	{
		public string FileName { get; set; } = string.Empty;
		public string DocumentType { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
	}
}
