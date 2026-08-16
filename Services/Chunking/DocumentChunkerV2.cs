using System.Text;
using CargoMindApi.Models.DTOs;

namespace CargoMindApi.Services.Chunking
{
	public class DocumentChunkerV2
	{
		private const int MaxChunkSize = 1000;

		public List<ChunkResult> Chunk(string content)
		{
			var sections = SplitIntoSections(content);

			var chunks = new List<ChunkResult>();

			foreach (var section in sections)
			{
				var sectionChunks = SplitSection(section);

				foreach (var chunk in sectionChunks)
				{
					chunks.Add(new ChunkResult
					{
						ChunkIndex = chunks.Count,
						Content = chunk
					});
				}
			}

			return chunks;
		}

		private List<string> SplitIntoSections(string content)
		{
			var lines = content.Split(
				new[] { "\r\n", "\n" },
				StringSplitOptions.None);

			var sections = new List<string>();
			var currentSection = new StringBuilder();

			foreach (var line in lines)
			{
				var trimmedLine = line.Trim();

				if (IsHeading(trimmedLine))
				{
					if (currentSection.Length > 0)
					{
						sections.Add(currentSection.ToString().Trim());
						currentSection.Clear();
					}
				}

				if (!string.IsNullOrWhiteSpace(trimmedLine))
				{
					currentSection.AppendLine(trimmedLine);
				}
			}

			if (currentSection.Length > 0)
			{
				sections.Add(currentSection.ToString().Trim());
			}

			return sections;
		}

		private List<string> SplitSection(string section)
		{
			var paragraphs = section
				.Split(
					new[] { "\r\n\r\n", "\n\n" },
					StringSplitOptions.RemoveEmptyEntries);

			var chunks = new List<string>();
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
						chunks.Add(currentChunk.ToString().Trim());
						currentChunk.Clear();
					}

					if (cleanedParagraph.Length > MaxChunkSize)
					{
						chunks.AddRange(SplitLargeParagraph(cleanedParagraph));
						continue;
					}
				}

				currentChunk.AppendLine(cleanedParagraph);
			}

			if (currentChunk.Length > 0)
			{
				chunks.Add(currentChunk.ToString().Trim());
			}

			return chunks;
		}

		private List<string> SplitLargeParagraph(string paragraph)
		{
			var sentences = paragraph
				.Split(
					new[] { ". ", "! ", "? " },
					StringSplitOptions.RemoveEmptyEntries);

			var chunks = new List<string>();
			var currentChunk = new StringBuilder();

			foreach (var sentence in sentences)
			{
				var cleanedSentence = sentence.Trim();

				if (currentChunk.Length + cleanedSentence.Length + 1 > MaxChunkSize)
				{
					if (currentChunk.Length > 0)
					{
						chunks.Add(currentChunk.ToString().Trim());
						currentChunk.Clear();
					}
				}

				currentChunk.Append(cleanedSentence);
				currentChunk.Append(' ');
			}

			if (currentChunk.Length > 0)
			{
				chunks.Add(currentChunk.ToString().Trim());
			}

			return chunks;
		}

		private bool IsHeading(string line)
		{
			return line.StartsWith("1.") ||
				   line.StartsWith("2.") ||
				   line.StartsWith("3.") ||
				   line.StartsWith("4.") ||
				   line.StartsWith("5.") ||
				   line.StartsWith("6.") ||
				   line.StartsWith("7.") ||
				   line.StartsWith("8.") ||
				   line.StartsWith("9.");
		}
	}
}
