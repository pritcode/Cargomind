using CargoMindApi.Data;
using CargoMindApi.Models.Entities;
using CargoMindApi.Services.Chunking;

namespace CargoMindApi.Services
{
	public class DocumentIngestionService
	{
		private readonly CargoMindDbContext _dbContext;
		private readonly DocumentChunker _chunker;

		public DocumentIngestionService(CargoMindDbContext dbContext, DocumentChunker chunker)
		{
			_dbContext = dbContext;
			_chunker = chunker;
		}

		public async Task<Guid> IngestAsync(
			string fileName,
			string documentType,
			string content)
		{
			var document = new Document
			{
				Id = Guid.NewGuid(),
				FileName = fileName,
				DocumentType = documentType,
				CreatedAt = DateTime.UtcNow
			};

			//var chunk = new DocumentChunk		//Phase 0 Chunking
			//{
			//	Id = Guid.NewGuid(),
			//	DocumentId = document.Id,
			//	ChunkIndex = 0,
			//	Content = content
			//};

			//document.Chunks.Add(chunk);


			var chunks = _chunker.Chunk(content);

			foreach (var chunkResult in chunks)
			{
				document.Chunks.Add(new DocumentChunk
				{
					Id = Guid.NewGuid(),
					DocumentId = document.Id,
					ChunkIndex = chunkResult.ChunkIndex,
					Content = chunkResult.Content
				});
			}

			_dbContext.Documents.Add(document);

			await _dbContext.SaveChangesAsync();

			return document.Id;
		}
	}
}
