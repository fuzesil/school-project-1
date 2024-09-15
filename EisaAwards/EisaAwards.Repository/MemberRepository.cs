namespace EisaAwards.Repository
{
    using System.Linq;
    using EisaAwards.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository class for the <see cref="Member"/> entity.
    /// </summary>
    public class MemberRepository : RepositoryClass<Member>, IMemberRepository
    {
        // private readonly DbContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The instance of type <see cref="DbContext"/> that represents the connection to the database.</param>
        public MemberRepository(ref DbContext dbContext)
            : base(ref dbContext)
        {
            // this.db = db ?? throw new System.ArgumentNullException(nameof(db));
        }

        /// <inheritdoc/>
        public void ChangeChiefEditor(int id, string newChiefEditor)
        {
            Member thisMember = this.GetOne(id);
            thisMember.ChiefEditor = newChiefEditor;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void ChangeName(int id, string newName)
        {
            Member thisMember = this.GetOne(id);
            thisMember.Name = newName;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void ChangeOfficeLocation(int id, string newOfficeLocation)
        {
            Member thisMember = this.GetOne(id);
            thisMember.OfficeLocation = newOfficeLocation;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void ChangePhoneNumber(int id, string newPhoneNumber)
        {
            Member thisMember = this.GetOne(id);
            thisMember.PhoneNumber = newPhoneNumber;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void ChangePublisher(int id, string newPublisher)
        {
            Member thisMember = this.GetOne(id);
            thisMember.Publisher = newPublisher;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public void ChangeWebsite(int id, string newWebsite)
        {
            Member thisMember = this.GetOne(id);
            thisMember.Website = newWebsite;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public override Member GetOne(int id)
        {
            return this.GetAll().Single(member => member.Id == id);
        }

        /// <inheritdoc/>
        public override Member GetOne(string name)
        {
            return this.GetAll().First(member => member.Name.Contains(name));
        }

        /// <inheritdoc/>
        public void Move(int id, int newCountryID, string newLocation)
        {
            Member thisMember = this.GetOne(id);
            if (id > 0)
            {
                thisMember.CountryID = newCountryID;
            }

            thisMember.OfficeLocation = newLocation;
            this.Update(thisMember);
        }

        /// <inheritdoc/>
        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }

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
