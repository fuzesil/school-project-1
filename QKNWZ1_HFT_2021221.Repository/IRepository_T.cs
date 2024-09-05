namespace QKNWZ1_HFT_2021221.Repository
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.ChangeTracking;

	/// <summary>
	/// Declares basic CRUD methods on a generic type where <typeparamref name="T"/> is a <see langword="class"/>.
	/// </summary>
	/// <typeparam name="T">The generic type that is a class.</typeparam>
	public interface IRepository<T>
		where T : class
	{
		/// <summary>
		/// Creates a new <typeparamref name="T"/> to be inserted into the database, and returns <see langword="true"/> if it succeeds.
		/// </summary>
		/// <param name="item">The new <typeparamref name="T"/> to insert.</param>
		/// <returns><see langword="true"/> if <see cref="DbContext.Add{TEntity}(TEntity)"/> returns an <see cref="EntityEntry{TEntity}"/> whose <list type="bullet">
		/// <item><see cref="EntityEntry.Entity"/> <c>==</c> <paramref name="item"/></item>
		/// <item><see cref="EntityEntry.State"/> <c>==</c> <see cref="EntityState.Added"/></item>
		/// </list></returns>
		bool Create(T item);

		/// <summary>Returns a query to fetch all <typeparamref name="T"/> objects from the database.</summary>
		/// <returns>A query to return all <typeparamref name="T"/> objects in the DB when requested.</returns>
		System.Linq.IQueryable<T> ReadAll();

		/// <summary>
		/// Returns the <typeparamref name="T"/> having the matching primary key, or <see langword="default"/>(<typeparamref name="T"/>).
		/// </summary>
		/// <param name="id">The primary key of the record sought.</param>
		/// <returns>The single <typeparamref name="T"/> having the primary key specified by <paramref name="id"/>.</returns>
		T Read(int id);

		/// <summary>
		/// Returns the <typeparamref name="T"/> having the matching <paramref name="name"/>, or <see langword="default"/>(<typeparamref name="T"/>).
		/// </summary>
		/// <param name="name">The value of the name field of the record sought.</param>
		/// <returns>The single <typeparamref name="T"/> type record having the value specified by <paramref name="name"/>.</returns>
		public T Read(string name);

		/// <summary>
		/// Updates the <typeparamref name="T"/> specified by <paramref name="item"/>.<br/>
		/// Usually, anything but the primary key(s) can be edited.
		/// </summary>
		/// <param name="item">The record that replaces the old one.</param>
		/// <returns><see langword="true"/> when <see cref="DbContext.Update{TEntity}(TEntity)"/> returns an <see cref="EntityEntry{TEntity}"/> whose <list type="bullet">
		/// <item><see cref="EntityEntry.Entity"/> <c>==</c> <paramref name="item"/></item>
		/// <item><see cref="EntityEntry.State"/> <c>==</c> <see cref="EntityState.Modified"/></item>
		/// </list></returns>
		bool Update(T item);

		/// <summary>
		/// Deletes the <typeparamref name="T"/> having the matching primary key <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The primary key of the object to be deleted.</param>
		/// <returns><see langword="true"/> when <see cref="DbContext.Remove{TEntity}(TEntity)"/> returns an <see cref="EntityEntry{TEntity}"/> whose <list type="bullet">
		/// <item><see cref="EntityEntry.Entity"/> <c>==</c> <typeparamref name="T"/></item>
		/// <item><see cref="EntityEntry.State"/> <c>==</c> <see cref="EntityState.Deleted"/></item>
		/// </list></returns>
		bool Delete(int id);

		/// <summary>
		/// Deletes the <typeparamref name="T"/> specified by <paramref name="item"/>.
		/// </summary>
		/// <param name="item">The record to be deleted.</param>
		/// <returns><see langword="true"/> when <see cref="DbContext.Remove{TEntity}(TEntity)"/> returns an <see cref="EntityEntry{TEntity}"/> whose <list type="bullet">
		/// <item><see cref="EntityEntry.Entity"/> <c>==</c> <paramref name="item"/></item>
		/// <item><see cref="EntityEntry.State"/> <c>==</c> <see cref="EntityState.Deleted"/></item>
		/// </list></returns>
		bool Delete(T item);
	}
}
