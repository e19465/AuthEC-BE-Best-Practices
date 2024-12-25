using System.Linq.Expressions;
using AuthEC.DataAccess.Data;
using AuthEC.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AuthEC.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<T> _dbSet;

		public Repository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = dbContext.Set<T>();
		}

		public void Add(T entity)
		{
			try
			{
				_dbSet.Add(entity);
			}
			catch (Exception) 
			{
				throw;
			}
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			try
			{
				IQueryable<T> query = _dbSet;
				if (!string.IsNullOrEmpty(includeProperties))
				{
					foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					{
						query = query.Include(prop);
					}

				}
				return query.ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			try
			{
				IQueryable<T> query = _dbSet;
				if (!string.IsNullOrEmpty(includeProperties))
				{
					foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					{
						query = query.Include(prop);
					}

				}
				query = query.Where(filter);
				return query.FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Remove(T entity)
		{
			try
			{
				_dbSet.Remove(entity);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			try
			{
				_dbSet.RemoveRange(entities);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
