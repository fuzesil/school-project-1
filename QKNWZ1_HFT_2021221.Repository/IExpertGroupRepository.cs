namespace QKNWZ1_HFT_2021221.Repository
{
	using Models;

	/// <summary>
	/// Declares basic operations applicable to <see cref="ExpertGroup"/> objects only.
	/// </summary>
	public interface IExpertGroupRepository : IRepository<ExpertGroup>
	{
		/// <summary>
		/// Updates <see cref="ExpertGroup.Name"/> for the object where <see cref="ExpertGroup.Id"/> <c>==</c> <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="ExpertGroup.Id"/>.</param>
		/// <param name="name">The new value of <see cref="ExpertGroup.Name"/>.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Update(T)"/>.</returns>
		bool ChangeName(int id, string name);

		/// <summary>
		/// Creates a new <see cref="ExpertGroup"/> from the input.
		/// </summary>
		/// <param name="name">The value of <see cref="ExpertGroup.Name"/>.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Update(T)"/>.</returns>
		bool Create(string name);
	}
}
