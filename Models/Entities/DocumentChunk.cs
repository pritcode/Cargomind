namespace CargoMindApi.Models.Entities
{
	public class DocumentChunk
	{
		public Guid Id { get; set; }		
		public int ChunkIndex { get; set; }
		public string Content { get; set; } = string.Empty;
		public Guid DocumentId { get; set; }
		public Document Document { get; set; } = null!;
	}
}
