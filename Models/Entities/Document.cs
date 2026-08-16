namespace CargoMindApi.Models.Entities
{
	public class Document
	{
		public Guid Id { get; set; }
		public string FileName { get; set; } = string.Empty;
		public string DocumentType { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public ICollection<DocumentChunk> Chunks { get; set; } = new List<DocumentChunk>();
	}
}
