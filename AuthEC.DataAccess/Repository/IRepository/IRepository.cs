using System.Linq.Expressions;

namespace AuthEC.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		/// <summary>
		/// This is used to get all items from a table in DB
		/// </summary>
		/// <param name="includeProperties">The "," sep. prop. that need to be included using foreign key</param>
		/// <returns>List of entities</returns>
		IEnumerable<T> GetAll(string? includeProperties = null);


		/// <summary>
		/// This is the method to get first entity that matches the filtering logic
		/// </summary>
		/// <param name="filter">Filtering logic</param>
		/// <param name="includeProperties">The "," sep. prop. that need to be included using foreign key</param>
		/// <returns></returns>
		T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);


		/// <summary>
		/// This is to add new record to DB
		/// </summary>
		/// <param name="entity">Record to be added to DB</param>
		void Add(T entity);

		/// <summary>
		/// This is to remove entity from DB
		/// </summary>
		/// <param name="entity">The record to be removed</param>
		void Remove(T entity);


		/// <summary>
		/// This is to remove set of entities from DB
		/// </summary>
		/// <param name="entities">Set of entities to be removed</param>
		void RemoveRange(IEnumerable<T> entities);
	}
}
