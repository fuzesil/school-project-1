using System.Linq;
using Microsoft.EntityFrameworkCore;
using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.Repository
{
	/// <summary>
	/// Repository class for the <see cref="ExpertGroup"/> entity.
	/// </summary>
	public class ExpertGroupRepository : RepositoryBase<ExpertGroup>, IExpertGroupRepository
	{
		// private readonly DbContext db;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExpertGroupRepository"/> class.
		/// </summary>
		/// <param name="dbContext">The instance of type <see cref="DbContext"/> that represents the connection to the database.</param>
		public ExpertGroupRepository(DbContext dbContext)
			: base(dbContext)
		{
			// this.db = db ?? throw new System.ArgumentNullException(nameof(db));
		}

		/// <inheritdoc/>
		public bool ChangeName(int id, string name)
		{
			var expertGroup = this.Read(id);
			expertGroup.Name = name;
			return this.Update(expertGroup);
		}

		/// <inheritdoc/>
		public bool Create(string name)
		{
			var expertGroup = new ExpertGroup { Name = name };
			return base.Create(expertGroup);
		}

		/// <inheritdoc/>
		public override ExpertGroup Read(int id) => this.ReadAll().FirstOrDefault(eg => eg.Id == id);

		/// <inheritdoc/>
		public override ExpertGroup Read(string name) => this.ReadAll().FirstOrDefault(eg => eg.Name.Contains(name));

		/*
		/// <inheritdoc/>
		public override bool Delete(int id) => base.Delete(this.Read(id));
		*/
		/*
		/// <summary>
		/// Returns 1 record or throws exception.
		/// </summary>
		/// <param name="id">The ID of the record to be returned.</param>
		/// <returns>The one record with the matching ID.</returns>
		private ExpertGroup GetExpertGroup(int id)
		{
			ExpertGroup thisEG;
			try
			{
				thisEG = this.GetAll().Single(eg => eg.ExpertGroupID == id);
			}
			catch (System.InvalidOperationException ex)
			{
				throw new System.ApplicationException($"No record with the given ID [{id}] found by {nameof(this.GetExpertGroup)}.", ex);
			}

			return thisEG;
		}
		*/
	}
}
