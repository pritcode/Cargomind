using System.Text;
using CargoMindApi.Models.DTOs;

namespace CargoMindApi.Services.Chunking
{
	public class DocumentChunker
	{
		private const int MaxChunkSize = 1000;

		public List<ChunkResult> Chunk(string content)
		{
			var paragraphs = content
				.Split(
					new[] { "\r\n\r\n", "\n\n" },
					StringSplitOptions.RemoveEmptyEntries);

			var chunks = new List<ChunkResult>();

			var currentChunk = new StringBuilder();

			foreach (var paragraph in paragraphs)
			{
				var cleanedParagraph = paragraph.Trim();

				if (string.IsNullOrWhiteSpace(cleanedParagraph))
					continue;

				if (currentChunk.Length + cleanedParagraph.Length > MaxChunkSize)
				{
					if (currentChunk.Length > 0)
					{
						chunks.Add(new ChunkResult
						{
							ChunkIndex = chunks.Count,
							Content = currentChunk.ToString().Trim()
						});

						currentChunk.Clear();
					}
				}

				currentChunk.AppendLine(cleanedParagraph);
			}

			if (currentChunk.Length > 0)
			{
				chunks.Add(new ChunkResult
				{
					ChunkIndex = chunks.Count,
					Content = currentChunk.ToString().Trim()
				});
			}

			return chunks;
		}
	}
}
