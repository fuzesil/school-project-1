using System.Linq;
using Microsoft.EntityFrameworkCore;

[assembly: System.CLSCompliant(false)]
namespace QKNWZ1_HFT_2021221.Repository
{
	/// <summary>
	/// Implements the most general methods mandated by the interfaces and declares the rest as <see langword="abstract"/> - to be implemeted by inheriting types.
	/// </summary>
	/// <typeparam name="T">The generic type that is a class.</typeparam>
	public abstract class RepositoryBase<T> : IRepository<T>, IChangeManager
		where T : class
	{
		private DbContext dbContext;

		/// <summary>Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.</summary>
		/// <param name="dbContext">The <see cref="DbContext"/> instance that represents the connection to the database.</param>
		protected RepositoryBase(DbContext dbContext) => this.dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));

		/// <inheritdoc/>
		public bool Create(T item)
		{
			var entityEntry = this.dbContext.Add<T>(item);
			return entityEntry.State == EntityState.Added && entityEntry.Entity.Equals(item);
		}

		/// <inheritdoc/>
		public IQueryable<T> ReadAll() => this.dbContext.Set<T>();

		/// <inheritdoc/>
		public abstract T Read(int id);

		/// <inheritdoc/>
		public abstract T Read(string name);


		/// <inheritdoc/>
		public bool Update(T item)
		{
			var entityEntry = this.dbContext.Update<T>(item);
			return entityEntry.State == EntityState.Modified && entityEntry.Entity.Equals(item);
		}

		/// <inheritdoc/>
		public bool Delete(T item)
		{
			var entityEntry = this.dbContext.Remove<T>(item);
			return entityEntry.State == EntityState.Deleted && entityEntry.Entity.Equals(item);
		}

		/// <inheritdoc/>
		public bool Delete(int id) => this.Delete(this.Read(id));

		/// <inheritdoc/>
		public int SaveChanges() => this.dbContext.SaveChanges();

		/// <inheritdoc/>
		public string GetPendingChanges()
		{
			var entries = this.dbContext.ChangeTracker.Entries<T>()
				.Where(entityEntry => entityEntry.State != EntityState.Unchanged)
				.ToArray();
			if (!entries.Any())
			{
				return "No changes pending.";
			}
			var tType = typeof(T);
			var sb = new System.Text.StringBuilder($"Pending change(s) for {tType.Name} entries:").AppendLine();
			foreach (var entry in entries)
			{
				sb.AppendLine($"{entry.State} - {entry.Entity}");
			}
			return sb.ToString();
		}
	}
}
