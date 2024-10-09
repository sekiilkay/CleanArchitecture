﻿namespace App.Application.Contracts.Caching
{
	public interface ICacheService
	{
		Task<T?> GetAsync<T>(string cacheKey);
		Task AddAsync<T>(string cacheKey, T value, TimeSpan expTimeSpan);
		Task RemoveAsync<T>(string cacheKey);
	}
}
