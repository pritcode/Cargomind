using CargoMindApi.Data;
using CargoMindApi.Services;
using CargoMindApi.Services.Chunking;
using Microsoft.EntityFrameworkCore;

namespace CargoMindApi.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCargoMindData(
		this IServiceCollection services,
		IConfiguration configuration)
		{
			services.AddDbContext<CargoMindDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("CargoMind")));


			services.AddSingleton<AiService>();
			services.AddScoped<DocumentIngestionService>();
			services.AddScoped<DocumentChunker>();

			return services;
		}
	}
}
