using System.Linq;
using Microsoft.EntityFrameworkCore;
using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.Repository
{
	/// <summary>
	/// Repository class for the <see cref="Member"/> entity.
	/// </summary>
	public class MemberRepository : RepositoryBase<Member>, IMemberRepository
	{
		// private readonly DbContext db;

		/// <summary>
		/// Initializes a new instance of the <see cref="MemberRepository"/> class.
		/// </summary>
		/// <param name="dbContext">The instance of type <see cref="DbContext"/> that represents the connection to the database.</param>
		public MemberRepository(DbContext dbContext)
			: base(dbContext)
		{
			// this.db = db ?? throw new System.ArgumentNullException(nameof(db));
		}

		/// <inheritdoc/>
		public bool ChangeChiefEditor(int id, string chiefEditor)
		{
			Member thisMember = this.Read(id);
			thisMember.ChiefEditor = chiefEditor;
			return this.Update(thisMember);
		}

		/// <inheritdoc/>
		public bool ChangeName(int id, string name)
		{
			Member thisMember = this.Read(id);
			thisMember.Name = name;
			return this.Update(thisMember);
		}

		/// <inheritdoc/>
		public bool ChangeOfficeLocation(int id, string officeLocation)
		{
			Member thisMember = this.Read(id);
			thisMember.OfficeLocation = officeLocation;
			return this.Update(thisMember);
		}

		/// <inheritdoc/>
		public bool ChangePhoneNumber(int id, string phoneNumber)
		{
			Member thisMember = this.Read(id);
			thisMember.PhoneNumber = phoneNumber;
			return this.Update(thisMember);
		}

		/// <inheritdoc/>
		public bool ChangePublisher(int id, string publisher)
		{
			Member thisMember = this.Read(id);
			thisMember.Publisher = publisher;
			return this.Update(thisMember);
		}

		/// <inheritdoc/>
		public bool ChangeWebsite(int id, string website)
		{
			Member thisMember = this.Read(id);
			thisMember.Website = website;
			return this.Update(thisMember);
		}

		/// <inheritdoc/>
		public override Member Read(int id) => this.ReadAll().SingleOrDefault(member => member.Id == id);

		/// <inheritdoc/>
		public override Member Read(string name) => this.ReadAll().FirstOrDefault(member => member.Name.Contains(name));

		///// <inheritdoc/>
		//public override Member GetOne(string name)
		//{
		//    return this.GetAll().FirstOrDefault(member => member.Name.Contains(name));
		//}

		/// <inheritdoc/>
		public bool Move(int id, int countryId, string officeLocation)
		{
			Member thisMember = this.Read(id);
			thisMember.CountryId = countryId;
			thisMember.OfficeLocation = officeLocation;
			return this.Update(thisMember);
		}

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
		private Member GetMember(int id)
		{
			Member thisMember;
			try
			{
				thisMember = this.GetAll().Single(member => member.MemberID == id);
			}
			catch (System.InvalidOperationException ex)
			{
				throw new System.ApplicationException($"No record with the given ID [{id}] found by {nameof(this.GetMember)}.", ex);
			}

			return thisMember;
		}
		*/
	}
}
