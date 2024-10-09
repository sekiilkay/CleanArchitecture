using App.Service.ExceptionHandlers;
using CleanApp.API.ExceptionHandlers;

namespace CleanApp.API.Extensions
{
	public static class ExceptionHandlerExtension
	{
		public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
		{
			services.AddExceptionHandler<CriticalExceptionHandler>();
			services.AddExceptionHandler<GlobalExceptionHandler>();

			return services;
		}
	}
}
