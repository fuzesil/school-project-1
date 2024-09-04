namespace EisaAwards.Test
{
    using System.Collections.Generic;
    using EisaAwards.Data;

    /// <summary>
    /// Contains list generators for entities.
    /// </summary>
    public static class DataSource
    {
        /// <summary>
        /// The <see cref="System.Random"/> pseudo-random number generator.
        /// </summary>
        public static readonly System.Random Rnd = new System.Random();

        /// <summary>
        /// Gets a sequence of <see cref="Brand"/> elements.
        /// </summary>
        public static IEnumerable<Brand> GetBrands
        {
            get
            {
                yield return new Brand { BrandId = 1, Name = "NAD", CountryID = 3, Address = "Pickering", Homepage = "nad.com" };
                yield return new Brand { BrandId = 2, Name = "Bang & Olufsen", CountryID = 4, Address = "Struer", Homepage = "bo.com" };
                yield return new Brand { BrandId = 3, Name = "DALI", CountryID = 4, Address = "Norager", Homepage = "dali.dk" };
                yield return new Brand { BrandId = 4, Name = "DxO Labs", CountryID = 5, Address = "Paris", Homepage = "dxo.fr" };
                yield return new Brand { BrandId = 5, Name = "Focal", CountryID = 5, Address = "Paris", Homepage = "focal.fr" };
                yield return new Brand { BrandId = 6, Name = "Ground Zero", CountryID = 6, Address = "Egmting", Homepage = "gzero.de" };
                yield return new Brand { BrandId = 7, Name = "Sennheiser", CountryID = 6, Address = "Wedemark", Homepage = "sennheiser.com" };
                yield return new Brand { BrandId = 8, Name = "Volumio", CountryID = 8, Address = "Firenze", Homepage = "volumio.it" };
                yield return new Brand { BrandId = 9, Name = "Canon", CountryID = 9, Address = "Tokyo", Homepage = "canon.jp" };
                yield return new Brand { BrandId = 10, Name = "Fujifilm", CountryID = 9, Address = "Tokyo", Homepage = "fujifilm.jp" };
                yield return new Brand { BrandId = 11, Name = "Sony", CountryID = 9, Address = "Tokyo", Homepage = "sony.jp" };
                yield return new Brand { BrandId = 12, Name = "LG", CountryID = 9, Address = "Seoul", Homepage = "lg.com" };
                yield return new Brand { BrandId = 13, Name = "Samsung", CountryID = 9, Address = "Seoul", Homepage = "samsung.com" };
            }
        }

        /// <summary>
        /// Gets a sequence of <see cref="Country"/> elements.
        /// </summary>
        public static IEnumerable<Country> GetCountries
        {
            get
            {
                yield return new Country { CountryID = 1, Name = "Australia", CapitalCity = "Canberra", CallingCode = 61, PPPperCapita = 51885 };
                yield return new Country { CountryID = 2, Name = "Belgium", CapitalCity = "Brussels", CallingCode = 32, PPPperCapita = 48224 };
                yield return new Country { CountryID = 3, Name = "Canada", CapitalCity = "Ottawa", CallingCode = 1, PPPperCapita = 52144 };
                yield return new Country { CountryID = 4, Name = "Denmark", CapitalCity = "Copenhagen", CallingCode = 45, PPPperCapita = 51643 };
                yield return new Country { CountryID = 5, Name = "France", CapitalCity = "Paris", CallingCode = 33, PPPperCapita = 45454 };
                yield return new Country { CountryID = 6, Name = "Germany", CapitalCity = "Berlin", CallingCode = 49, PPPperCapita = 53571 };
                yield return new Country { CountryID = 7, Name = "Hungary", CapitalCity = "Budapest", CallingCode = 36, PPPperCapita = 35941 };
                yield return new Country { CountryID = 8, Name = "Italy", CapitalCity = "Roma", CallingCode = 39, PPPperCapita = 40470 };
                yield return new Country { CountryID = 9, Name = "Japan", CapitalCity = "Tokyo", CallingCode = 81, PPPperCapita = 43194 };
                yield return new Country { CountryID = 10, Name = "South Korea", CapitalCity = "Seoul", CallingCode = 82, PPPperCapita = 44292 };
                yield return new Country { CountryID = 11, Name = "Poland", CapitalCity = "Warszawa", CallingCode = 48, PPPperCapita = 35651 };
                yield return new Country { CountryID = 12, Name = "USA", CapitalCity = "Washington, D.C.", CallingCode = 1, PPPperCapita = 63051 };
            }
        }

        /// <summary>
        /// Gets a sequence of <see cref="ExpertGroup"/> elements.
        /// </summary>
        public static IEnumerable<ExpertGroup> GetExpertGroups
        {
            get
            {
                yield return new ExpertGroup { ExpertGroupID = 1, Name = "Hi-Fi" };
                yield return new ExpertGroup { ExpertGroupID = 2, Name = "Home Theatre Audio" };
                yield return new ExpertGroup { ExpertGroupID = 3, Name = "Home Theatre Display & Video" };
                yield return new ExpertGroup { ExpertGroupID = 6, Name = "Photography" };
            }
        }

        /// <summary>
        /// Gets a sequence of <see cref="Member"/> elements.
        /// </summary>
        public static IEnumerable<Member> GetMembers
        {
            get
            {
                yield return new Member { MemberID = 1, Name = "Australian Hi-Fi", CountryID = 1, ExpertGroupID = 1, OfficeLocation = "Suite 3, Level 10, 100 Walker Street, North Sydney, NSW 2060," };
                yield return new Member { MemberID = 2, Name = "FWD Magazine", CountryID = 2, ExpertGroupID = 1, OfficeLocation = "Van den Hautelei 101,   2100 Deurne" };
                yield return new Member { MemberID = 3, Name = "SOUNDSTAGE! HI-FI", CountryID = 3, ExpertGroupID = 1, OfficeLocation = "1953 Meldrum Avenue Ottawa, ON K1J 7V6" };
                yield return new Member { MemberID = 4, Name = "SOUNDSTAGE! HTA", CountryID = 3, ExpertGroupID = 2, OfficeLocation = "1953 Meldrum Avenue Ottawa, ON K1J 7V6" };
                yield return new Member { MemberID = 5, Name = "Les Années Laser", CountryID = 5, ExpertGroupID = 2, OfficeLocation = "20, passage Turquetil, - 75011 Paris" };
                yield return new Member { MemberID = 6, Name = "Heimkino", CountryID = 6, ExpertGroupID = 2, OfficeLocation = "Gartroper Strasse 42, D-47138 - Duisburg" };
                yield return new Member { MemberID = 7, Name = "Hifi Test TV Video", CountryID = 6, ExpertGroupID = 3, OfficeLocation = "Gartroper Strasse 42, D-47138 - Duisburg", };
                yield return new Member { MemberID = 8, Name = "Stereo", CountryID = 6, ExpertGroupID = 1, OfficeLocation = "Eifelring 28, D-53879 Euskirchen" };
                yield return new Member { MemberID = 9, Name = "Sztereó Sound&Vision", CountryID = 7, ExpertGroupID = 1, OfficeLocation = "Attila út 101, H-1012 Budapest" };
                yield return new Member { MemberID = 10, Name = "Sztereó Sound&Vision", CountryID = 7, ExpertGroupID = 2, OfficeLocation = "Attila út 101, H-1012 Budapest" };
                yield return new Member { MemberID = 11, Name = "Sztereó Sound&Vision", CountryID = 7, ExpertGroupID = 3, OfficeLocation = "Attila út 101, H-1012 Budapest" };
                yield return new Member { MemberID = 12, Name = "AUDIOreview", CountryID = 8, ExpertGroupID = 2, OfficeLocation = "Via Nomentana, 1018  00137 Roma" };
                yield return new Member { MemberID = 13, Name = "Digital Video HT", CountryID = 8, ExpertGroupID = 3, OfficeLocation = "Via Nomentana, 1018  00137 Roma" };
                yield return new Member { MemberID = 14, Name = "Audio Accessory", CountryID = 9, ExpertGroupID = 1, OfficeLocation = "7th AZUMA BLDG, 1-9, Kanda Sakuma-cho, Chiyoda-ku, Tokyo, 101-0025" };
                yield return new Member { MemberID = 15, Name = "Audio", CountryID = 11, ExpertGroupID = 1, OfficeLocation = "Warszawa" };
                yield return new Member { MemberID = 16, Name = "Sound & Vision", CountryID = 12, ExpertGroupID = 2, OfficeLocation = "Madison Avenue, 8 th floor, New York, NY 10016" };
                yield return new Member { MemberID = 17, Name = "Stereophile", CountryID = 9, ExpertGroupID = 1, OfficeLocation = "Madison Avenue, 8 th floor, New York, NY 10016" };
            }
        }

        /// <summary>
        /// Gets a sequence of <see cref="Product"/> elements.
        /// </summary>
        public static IEnumerable<Product> GetProducts
        {
            get
            {
                yield return new Product { ProductID = 1, BrandId = 2, Name = "Beosound Stage", Category = "BEST PREMIUM SOUNDBAR", ExpertGroupID = 2, Price = 2000 };
                yield return new Product { ProductID = 2, BrandId = 9, Name = "EOS-1D X Mark III", Category = "BEST PROFESSIONAL CAMERA", ExpertGroupID = 6, Price = 9183 };
                yield return new Product { ProductID = 3, BrandId = 3, Name = "Epikon", Category = "BEST DALI FLOORSTANDER", ExpertGroupID = 1, Price = 1999 };
                yield return new Product { ProductID = 4, BrandId = 4, Name = "Nik Collection 3", Category = "BEST PHOTO SOFTWARE", ExpertGroupID = 6, Price = 5000 };
                yield return new Product { ProductID = 5, BrandId = 4, Name = "Chora 826", Category = "BEST VALUE FLOORSTANDING LOUDSPEAKER", ExpertGroupID = 1, Price = 1899 };
                yield return new Product { ProductID = 6, BrandId = 10, Name = "X100V", Category = "BEST COMPACT CAMERA", ExpertGroupID = 6, Price = 3500 };
                yield return new Product { ProductID = 7, BrandId = 12, Name = "SN8YG", Category = "BEST SOUNDBAR", ExpertGroupID = 2 };
                yield return new Product { ProductID = 8, BrandId = 12, Name = "OLED65GX", Category = "BEST PREMIUM OLED TV", ExpertGroupID = 3, Price = 29999 };
                yield return new Product { ProductID = 9, BrandId = 12, Name = "75NANO99", Category = "BEST 8K TV", ExpertGroupID = 3, Price = 2199 };
                yield return new Product { ProductID = 10, BrandId = 13, Name = "QE75Q950TS", Category = "BEST LARGE SCREEN TV", ExpertGroupID = 3, Price = 2299 };
                yield return new Product { ProductID = 11, BrandId = 11, Name = "Vlog Camera ZV-1", Category = "Best Vlogging Camera", ExpertGroupID = 6, Price = 2500 };
            }
        }
    }
}
