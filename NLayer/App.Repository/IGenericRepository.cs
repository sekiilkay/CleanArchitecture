﻿using System.Linq.Expressions;

namespace App.Repository
{
	public interface IGenericRepository<T, TId> where T : class where TId : struct
	{
		Task<bool> AnyAsync(TId id);
		IQueryable<T> GetAll();
		IQueryable<T> Where(Expression<Func<T, bool>> predicate);
		ValueTask<T?> GetByIdAsync(int id);
		ValueTask AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
