namespace QKNWZ1_HFT_2021221.Repository
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.ChangeTracking;

	/// <summary>
	/// Declares the <c>GetPendingChanges</c> method to display the pending changes in the database.
	/// </summary>
	public interface IChangeManager
	{
		/// <summary>
		/// Assembles a <see cref="string"/> representation of the <see cref="EntityEntry{TEntity}.Entity"/> and its <see cref="EntityEntry.State"/> by getting <see cref="DbContext.ChangeTracker"/>.<br/>
		/// That is, a sequence of records and the new (uncommitted) state of each, as one <see cref="string"/>.
		/// </summary>
		/// <returns>The change tracking information (state) of tracked records as one <see cref="string"/>.</returns>
		string GetPendingChanges();

		/// <summary>
		/// Calls <see cref="DbContext.SaveChanges()"/> on the underlying database (i.e. <see cref="DbContext"/> instance)
		/// and returns the number of entries saved to the database.
		/// </summary>
		/// <returns>The number of state entries written to the database.</returns>
		int SaveChanges();
	}
}
