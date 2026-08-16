using CargoMindApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoMindApi.Data
{
	public class CargoMindDbContext : DbContext
	{
		public CargoMindDbContext(
		DbContextOptions<CargoMindDbContext> options)
		: base(options)
		{
		}

		public DbSet<Shipment> Shipments => Set<Shipment>();
		public DbSet<Document> Documents => Set<Document>();
		public DbSet<DocumentChunk> DocumentChunks => Set<DocumentChunk>();


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Shipment>(entity =>
			{
				entity.Property(x => x.RecordedWeightKg)
					.HasPrecision(10, 2);

				entity.Property(x => x.RecordedLengthCm)
					.HasPrecision(10, 2);

				entity.Property(x => x.RecordedWidthCm)
					.HasPrecision(10, 2);

				entity.Property(x => x.RecordedHeightCm)
					.HasPrecision(10, 2);
			});

			modelBuilder.Entity<DocumentChunk>(entity =>
			{
				entity.HasOne(x => x.Document)
					.WithMany(x => x.Chunks)
					.HasForeignKey(x => x.DocumentId)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}
