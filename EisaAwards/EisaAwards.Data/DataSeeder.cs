﻿namespace EisaAwards.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Helper class to fill <see cref="EisaAwardsDbContext"/>.
    /// </summary>
    public static class DataSeeder
    {
        private static readonly Random Rnd = new ();

        /// <summary>
        /// Gets a sequence of records from the provided CSV file.
        /// </summary>
        public static IEnumerable<Brand> Brands
        {
            get
            {
                int counter = 0;
                string line = string.Empty;
                string[] fields;
                using StreamReader sr = new (@"DataSeed\Brand.csv");
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    yield return new Brand
                    {
                        Id = ++counter,
                        Name = fields[0],
                        Address = fields[1],
                        CountryID = Countries.FirstOrDefault(cntry => cntry.Name.Equals(fields[2], StringComparison.OrdinalIgnoreCase)).Id,
                        Homepage = fields[3],
                    };
                }
            }
        }

        /// <summary>
        /// Gets a sequence of records from the provided CSV file.
        /// </summary>
        public static IEnumerable<Country> Countries
        {
            get
            {
                int counter = 0;
                string line = string.Empty;
                string[] fields;
                using StreamReader sr = new (@"DataSeed\Country.csv");
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    yield return new Country
                    {
                        Id = ++counter,
                        Name = fields[0],
                        CapitalCity = fields[1],
                        CallingCode = ((Func<int>)(() => string.IsNullOrWhiteSpace(fields[2]) ? 0 : int.Parse(fields[2], System.Globalization.NumberFormatInfo.InvariantInfo)))(),
                        PPPperCapita = ((Func<int>)(() => string.IsNullOrWhiteSpace(fields[3]) ? 0 : int.Parse(fields[3], System.Globalization.NumberFormatInfo.InvariantInfo)))(),
                    };
                }
            }
        }

        /// <summary>
        /// Gets a sequence of records from the provided CSV file.
        /// </summary>
        public static IEnumerable<ExpertGroup> ExpertGroups
        {
            get
            {
                int counter = 0;
                string line;
                string[] fields;
                using StreamReader sr = new (@"DataSeed\ExpertGroup.csv");
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    yield return new ExpertGroup
                    {
                        Id = ++counter,
                        Name = fields[0],
                    };
                }
            }
        }

        /// <summary>
        /// Gets a sequence of records from the provided CSV file.
        /// </summary>
        public static IEnumerable<Member> Members
        {
            get
            {
                int counter = 0;
                string line = string.Empty;
                string[] fields;
                using StreamReader sr = new (@"DataSeed\Member.csv");
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    yield return new Member
                    {
                        Id = ++counter,
                        ExpertGroupID = ExpertGroups.First(eg => eg.Name.Equals(fields[0], StringComparison.OrdinalIgnoreCase)).Id,
                        Name = fields[1],
                        OfficeLocation = fields[3],
                        CountryID = Countries.First(country => country.Name.Equals(fields[2], StringComparison.OrdinalIgnoreCase)).Id,
                        ChiefEditor = fields[4],
                        Publisher = fields[5],
                        PhoneNumber = fields[6],
                        Website = fields[7],
                    };
                }
            }
        }

        /// <summary>
        /// Gets a sequence of records from the provided CSV file.
        /// </summary>
        public static IEnumerable<Product> Products
        {
            get
            {
                int counter = 0;
                Brand newbrand = null;
                string line = string.Empty;
                string[] fields;
                using StreamReader sr = new (@"DataSeed\Product.csv");
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    newbrand = Brands.FirstOrDefault(manu => manu.Name.Equals(fields[1], StringComparison.OrdinalIgnoreCase));
                    yield return new Product
                    {
                        Id = ++counter,
                        BrandId = newbrand.Id,
                        Name = fields[2],
                        ExpertGroupID = int.Parse(fields[3], System.Globalization.NumberFormatInfo.InvariantInfo),
                        Category = fields[4],
                        Price = Rnd.Next(1000000),
                        LaunchDate = DateTime.Today.AddDays(Rnd.Next(7, 366) * -1),
                        EstimatedLifetime = Rnd.Next(10),
                    };
                }
            }
        }
    }
}
