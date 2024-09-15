﻿namespace EisaAwards.Repository
{
    using EisaAwards.Data;

    /// <summary>
    /// Declares operations applicable only to the Members table.
    /// </summary>
    public interface IMemberRepository : IRepository<Member>
    {
        /// <summary>
        /// Updates the <see cref="Member.Name"/> field (i.e. a new name can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.Id"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newName">The new value of <see cref="Member.Name"/> (i.e. new name for the chosen record).</param>
        void ChangeName(int id, string newName);

        /// <summary>
        /// Updates the <see cref="Member.Website"/> field (i.e. a new website can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.Id"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newWebsite">The new value of <see cref="Member.Website"/> (i.e. new website for the chosen record).</param>
        void ChangeWebsite(int id, string newWebsite);

        /// <summary>
        /// Updates the <see cref="Member.ChiefEditor"/> field (i.e. a new Chief Editor can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.Id"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newChiefEditor">The new value of <see cref="Member.ChiefEditor"/> (i.e. new Chief Editor for the chosen record).</param>
        void ChangeChiefEditor(int id, string newChiefEditor);

        /// <summary>
        /// Updates the <see cref="Member.PhoneNumber"/> field (i.e. a new phone number can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.Id"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newPhoneNumber">The new value of <see cref="Member.PhoneNumber"/> (i.e. new phone number for the chosen record).</param>
        void ChangePhoneNumber(int id, string newPhoneNumber);

        /// <summary>
        /// Updates the <see cref="Member.Publisher"/> field (i.e. a new publisher can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.Id"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newPublisher">The new value of <see cref="Member.Publisher"/> (i.e. new publisher for the chosen record).</param>
        void ChangePublisher(int id, string newPublisher);

        /// <summary>
        /// Updates the <see cref="Member.OfficeLocation"/> field (i.e. a new office location (address) can be specified).
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.Id"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newOfficeLocation">The new value of <see cref="Member.OfficeLocation"/> (i.e. new office location for the chosen record).</param>
        void ChangeOfficeLocation(int id, string newOfficeLocation);

        /// <summary>
        /// Updates the <see cref="Member.CountryID"/> and the <see cref="Member.OfficeLocation"/> of the only <see cref="Member"/> record with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID [<see cref="Member.Id"/>] of the <see cref="Member"/> record to be updated.</param>
        /// <param name="newCountryID">The ID of the country [<see cref="Country.Id"/>] for the chosen record.
        /// If &lt;1, it will be ignored.</param>
        /// <param name="newLocation">New value for <see cref="Member.OfficeLocation"/> (i.e. new office location for the chosen record).</param>
        void Move(int id, int newCountryID, string newLocation);
    }
}