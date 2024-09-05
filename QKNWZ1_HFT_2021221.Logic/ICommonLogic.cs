namespace QKNWZ1_HFT_2021221.Logic
{
	using Models;

	/// <summary>
	/// Declares read-only operations on <see cref="ExpertGroup"/> objects.
	/// </summary>
	public interface ICommonLogic
	{
		/// <summary>
		/// Returns the sequence of all <see cref="ExpertGroup"/> objects from the database.
		/// </summary>
		/// <param name="count">The count of total elements in the resulting sequence.</param>
		/// <returns>The sequence of all <see cref="ExpertGroup"/> objects from the database.</returns>
		System.Collections.Generic.IEnumerable<ExpertGroup> ListAllExpertgroups(out int count);

		/// <summary>
		/// Returns the <see cref="ExpertGroup"/> having the given <see cref="ExpertGroup.Id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="ExpertGroup.Id"/> in the object to retrieve.</param>
		/// <returns>The matching <see cref="ExpertGroup"/>.</returns>
		ExpertGroup GetOneExpertGroup(int id);

		/// <summary>
		/// Returns the <see cref="ExpertGroup"/> having the given <see cref="ExpertGroup.Name"/> <c>==</c> <paramref name="name"/>.
		/// </summary>
		/// <param name="name">The value of <see cref="ExpertGroup.Name"/> in the record to retrieve.</param>
		/// <returns>The matching <see cref="ExpertGroup"/>.</returns>
		ExpertGroup GetOneExpertGroup(string name);
	}
}
