using CargoMindApi.Data;
using CargoMindApi.Services;
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

			return services;
		}
	}
}
