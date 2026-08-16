using CargoMindApi.Data;
using CargoMindApi.Models.Entities;
using CargoMindApi.Services.Chunking;

namespace CargoMindApi.Services
{
	public class DocumentIngestionService
	{
		private readonly CargoMindDbContext _dbContext;
		private readonly DocumentChunker _chunker;			//Phase 1
		private readonly DocumentChunkerV2 _chunker2;		//Phase 2

		public DocumentIngestionService(CargoMindDbContext dbContext, DocumentChunker chunker, DocumentChunkerV2 chunker2)
		{
			_dbContext = dbContext;
			_chunker = chunker;
			_chunker2 = chunker2;
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


			//var chunks = _chunker.Chunk(content);		//phase 1 chunking

			var chunks = _chunker2.Chunk(content);		//Phase 2

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
