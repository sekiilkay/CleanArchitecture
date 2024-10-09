
using App.Application.Contracts.Persistance;

namespace App.Repository
{
	public class UnitOfWork(AppDbContext context) : IUnitOfWork
	{
		public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
	}
}
