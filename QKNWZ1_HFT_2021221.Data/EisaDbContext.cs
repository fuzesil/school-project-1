using Microsoft.EntityFrameworkCore;
using QKNWZ1_HFT_2021221.Models;

[assembly: System.CLSCompliant(false)]
namespace QKNWZ1_HFT_2021221.Data
{
	/// <summary>
	/// The custom class that derives from <see cref="DbContext"/>.
	/// </summary>
	public partial class EisaDbContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of <see cref="EisaDbContext"/>.
		/// </summary>
		/// <param name="options">The options used by this context.</param>
		public EisaDbContext([System.Diagnostics.CodeAnalysis.NotNull] DbContextOptions options)
			: base(options)
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="EisaDbContext"/>.
		/// </summary>
		public EisaDbContext() => this.Database.EnsureCreated();

		/// <summary>
		/// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Brand"/>.
		/// </summary>
		public virtual DbSet<Brand> Brands { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Product"/>.
		/// </summary>
		public virtual DbSet<Product> Products { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="ExpertGroup"/>.
		/// </summary>
		public virtual DbSet<ExpertGroup> ExpertGroups { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Member"/>.
		/// </summary>
		public virtual DbSet<Member> Members { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Country"/>.
		/// </summary>
		public virtual DbSet<Country> Countries { get; set; }

		/// <inheritdoc/>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder is null)
			{
				throw new System.ArgumentNullException(nameof(optionsBuilder));
			}

			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder
					.UseLazyLoadingProxies()
					.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\EisaDb.mdf; Integrated Security=True; MultipleActiveResultSets=True;");
			}
		}

		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if (modelBuilder is null)
			{
				throw new System.ArgumentNullException(nameof(modelBuilder));
			}

			modelBuilder.Entity<Member>(entity =>
			{
				entity.HasOne(member => member.ExpertGroup)
					.WithMany(expertgroup => expertgroup.Members)
					.HasForeignKey(member => member.ExpertGroupId)
					.OnDelete(DeleteBehavior.ClientCascade);

				entity.HasOne(member => member.Country)
					.WithMany(country => country.Members)
					.HasForeignKey(member => member.CountryId)
					.OnDelete(DeleteBehavior.ClientCascade);
			});

			modelBuilder.Entity<Brand>()
				.HasOne(brand => brand.Country)
				.WithMany(country => country.Brands)
				.HasForeignKey(manufacturer => manufacturer.CountryId)
				.OnDelete(DeleteBehavior.ClientCascade);

			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasOne(product => product.Brand)
					.WithMany(brand => brand.Products)
					.HasForeignKey(product => product.BrandId)
					.OnDelete(DeleteBehavior.ClientCascade);

				entity.HasOne(product => product.ExpertGroup)
					.WithMany(expertgroup => expertgroup.Products)
					.HasForeignKey(product => product.ExpertGroupId)
					.OnDelete(DeleteBehavior.ClientCascade);
			});

			#region ExpertGroup declarations

			ExpertGroup hifi = new() { Id = 1, Name = "Hi-Fi", };
			ExpertGroup hta = new() { Id = 2, Name = "Home Theatre Audio" };
			ExpertGroup htdv = new() { Id = 3, Name = "Home Theatre Display & Video" };
			ExpertGroup ice = new() { Id = 4, Name = "In-car Electronics" };
			ExpertGroup md = new() { Id = 5, Name = "Mobile Devices" };
			ExpertGroup photo = new() { Id = 6, Name = "Photography" };

			#endregion

			#region Country declarations

			Country aus = new() { Id = 1, Name = "Australia", CapitalCity = "Canberra", CallingCode = 61, PPPperCapita = 51885 };
			Country aut = new() { Id = 2, Name = "Austria", CapitalCity = "Wien", CallingCode = 43, PPPperCapita = 51936 };
			Country bel = new() { Id = 3, Name = "Belgium", CapitalCity = "Brussels", CallingCode = 32, PPPperCapita = 48224 };
			Country bgr = new() { Id = 4, Name = "Bulgaria", CapitalCity = "Sofia", CallingCode = 359, PPPperCapita = 26034 };
			Country can = new() { Id = 5, Name = "Canada", CapitalCity = "Ottawa", CallingCode = 1, PPPperCapita = 52144 };
			Country chn = new() { Id = 6, Name = "China", CapitalCity = "Beijing", CallingCode = 86, PPPperCapita = 17206 };
			Country hrv = new() { Id = 7, Name = "Croatia", CapitalCity = "Zagreb", CallingCode = 385, PPPperCapita = 29207 };
			Country cze = new() { Id = 8, Name = "Czech Republic", CapitalCity = "Praha", CallingCode = 420, PPPperCapita = 40585 };
			Country dnk = new() { Id = 9, Name = "Denmark", CapitalCity = "Copenhagen", CallingCode = 45, PPPperCapita = 51643 };
			Country fin = new() { Id = 10, Name = "Finland", CapitalCity = "Helsinki", CallingCode = 358, PPPperCapita = 49334 };
			Country fra = new() { Id = 11, Name = "France", CapitalCity = "Paris", CallingCode = 33, PPPperCapita = 45454 };
			Country deu = new() { Id = 12, Name = "Germany", CapitalCity = "Berlin", CallingCode = 49, PPPperCapita = 53571 };
			Country grc = new() { Id = 13, Name = "Greece", CapitalCity = "Athens", CallingCode = 30, PPPperCapita = 29045 };
			Country hkg = new() { Id = 14, Name = "Hong Kong", CapitalCity = "", CallingCode = 852, PPPperCapita = 58165 };
			Country hun = new() { Id = 15, Name = "Hungary", CapitalCity = "Budapest", CallingCode = 36, PPPperCapita = 35941 };
			Country ind = new() { Id = 16, Name = "India", CapitalCity = "New Delhi", CallingCode = 91, PPPperCapita = 6283 };
			Country ita = new() { Id = 17, Name = "Italy", CapitalCity = "Roma", CallingCode = 39, PPPperCapita = 40470 };
			Country jpn = new() { Id = 18, Name = "Japan", CapitalCity = "Tokyo", CallingCode = 81, PPPperCapita = 43194 };
			Country kor = new() { Id = 19, Name = "South Korea", CapitalCity = "Seoul", CallingCode = 82, PPPperCapita = 44292 };
			Country nld = new() { Id = 20, Name = "Netherlands", CapitalCity = "Amsterdam", CallingCode = 31, PPPperCapita = 58255 };
			Country nor = new() { Id = 21, Name = "Norway", CapitalCity = "Oslo", CallingCode = 47, PPPperCapita = 79638 };
			Country pol = new() { Id = 22, Name = "Poland", CapitalCity = "Warszawa", CallingCode = 48, PPPperCapita = 35651 };
			Country prt = new() { Id = 23, Name = "Portugal", CapitalCity = "Lisboa", CallingCode = 351, PPPperCapita = 36246 };
			Country rus = new() { Id = 24, Name = "Russia", CapitalCity = "Moscow", CallingCode = 7, PPPperCapita = 28184 };
			Country srb = new() { Id = 25, Name = "Serbia", CapitalCity = "Belgrade", CallingCode = 381, PPPperCapita = 19767 };
			Country svk = new() { Id = 26, Name = "Slovakia", CapitalCity = "Bratislava", CallingCode = 421, PPPperCapita = 38321 };
			Country esp = new() { Id = 27, Name = "Spain", CapitalCity = "Madrid", CallingCode = 34, PPPperCapita = 38143 };
			Country swe = new() { Id = 28, Name = "Sweden", CapitalCity = "Stockholm", CallingCode = 46, PPPperCapita = 52477 };
			Country che = new() { Id = 29, Name = "Switzerland", CapitalCity = "Bern", CallingCode = 41, PPPperCapita = 67557 };
			Country twn = new() { Id = 30, Name = "Taiwan", CapitalCity = "Taipei", CallingCode = 886, PPPperCapita = 54019 };
			Country gbr = new() { Id = 31, Name = "UK", CapitalCity = "London", CallingCode = 44, PPPperCapita = 46827 };
			Country ukr = new() { Id = 32, Name = "Ukraine", CapitalCity = "Kiev", CallingCode = 380, PPPperCapita = 10310 };
			Country usa = new() { Id = 33, Name = "USA", CapitalCity = "Washington, D.C.", CallingCode = 1, PPPperCapita = 63051 };

			#endregion

			#region Brand declarations

			Brand alpine = new()
			{
				Id = 1,
				Name = "Alpine",
				Homepage = "http://www.alpine.com/",
				Address = "1-7, Yukigayaotsuka-machi, Ota-ku, Tokyo 1458501",
				CountryId = jpn.Id,
			};
			Brand apple = new()
			{
				Id = 2,
				Name = "Apple",
				Homepage = "https://www.apple.com/",
				Address = "1 Infinite Loop, Cupertino, CA 95014",
				CountryId = usa.Id,
			};
			Brand audiotecfischer = new()
			{
				Id = 3,
				Name = "Audiotec Fischer",
				Homepage = "https://www.audiotec-fischer.de/en",
				Address = "Hünegräben 26, 57392 Schmallenberg",
				CountryId = deu.Id,
			};
			Brand benq = new()
			{
				Id = 4,
				Name = "BenQ",
				Homepage = "https://www.benq.com/",
				Address = "12 Jihu Road, Neihu, Taipei 114",
				CountryId = twn.Id,
			};
			Brand bluesound = new()
			{
				Id = 5,
				Name = "Bluesound",
				Homepage = "https://www.bluesound.com/",
				Address = "633 Granite Court, Pickering, Ontario L1W 3K1",
				CountryId = can.Id,
			};
			Brand cambridgeaudio = new()
			{
				Id = 6,
				Name = "Cambridge Audio",
				Homepage = "https://www.cambridgeaudio.com/",
				Address = "Gallery Court, Hankey Place, Chaucer, The Borough, London SE1 4BG",
				CountryId = gbr.Id,
			};
			Brand canon = new()
			{
				Id = 7,
				Name = "Canon",
				Homepage = "https://global.canon/en/",
				Address = "Ota City, Tokyo",
				CountryId = jpn.Id,
			};
			Brand cewe = new()
			{
				Id = 8,
				Name = "CEWE",
				Homepage = "https://www.cewe.co.uk/",
				Address = "4 Spartan Close, Tachbrook Park, Warwick, Warwickshire CV34 6RR",
				CountryId = gbr.Id,
			};
			Brand denon = new()
			{
				Id = 9,
				Name = "Denon",
				Homepage = "https://www.denon.com/",
				Address = "5541 Fermi Court, Carlsbad, CA 92008",
				CountryId = usa.Id,
			};
			Brand dxo = new()
			{
				Id = 10,
				Name = "DxO",
				Homepage = "https://www.dxo.com/",
				Address = "3 Rue Nationale, Boulogne-Billancourt, Ile-de-France 92100",
				CountryId = fra.Id,
			};
			Brand epson = new()
			{
				Id = 11,
				Name = "Epson",
				Homepage = "https://global.epson.com/",
				Address = "3-3-5 Owa, Suwa, Nagano 392-8502",
				CountryId = jpn.Id,
			};
			Brand esx = new()
			{
				Id = 12,
				Name = "ESX",
				Homepage = "https://esxaudio.de/",
				Address = "Am Breilingsweg 3, D-76709 Kronau",
				CountryId = deu.Id,
			};
			Brand eton = new()
			{
				Id = 13,
				Name = "Eton",
				Homepage = "https://www.eton-gmbh.com/en/",
				Address = "Pfaffenweg 21, 89231 Neu-Ulm",
				CountryId = deu.Id,
			};
			Brand ferrum = new()
			{
				Id = 14,
				Name = "Ferrum",
				Homepage = "https://ferrum.audio/",
				Address = "Al. Jerozolimskie 475, Pruszków, Mazovia 05-800",
				CountryId = pol.Id,
			};
			Brand focal = new()
			{
				Id = 15,
				Name = "Focal",
				Homepage = "https://www.focal.com/en",
				Address = "Paris",
				CountryId = fra.Id,
			};
			Brand fujifilm = new()
			{
				Id = 16,
				Name = "Fujifilm",
				Homepage = "https://www.fujifilm.com/",
				Address = "Minato, Tokyo",
				CountryId = jpn.Id,
			};
			Brand groundzero = new()
			{
				Id = 17,
				Name = "Ground Zero",
				Homepage = "https://groundzerousa.com/",
				Address = "Erlenweg 25, 85658 Egmating",
				CountryId = deu.Id,
			};
			Brand hegel = new()
			{
				Id = 18,
				Name = "Hegel",
				Homepage = "https://www.hegel.com/en/",
				Address = "Oslo",
				CountryId = nor.Id,
			};
			Brand hifirose = new()
			{
				Id = 19,
				Name = "HiFi Rose",
				Homepage = "https://eng.hifirose.com/",
				Address = "11F, 932 Yangjae-daero, Songpa-gu, Seoul",
				CountryId = kor.Id,
			};
			Brand hisense = new()
			{
				Id = 20,
				Name = "Hisense",
				Homepage = "https://global.hisense.com/",
				Address = "Floor 22, Hisense Tower, 17 Donghai Xi Road, Qingdao, 266071",
				CountryId = chn.Id,
			};
			Brand huawei = new()
			{
				Id = 21,
				Name = "Huawei",
				Homepage = "https://www.huawei.com/en/",
				Address = "Shenzhen",
				CountryId = chn.Id,
			};
			Brand jbl = new()
			{
				Id = 22,
				Name = "JBL",
				Homepage = "https://www.jbl.com/",
				Address = "400 Atlantic Street, Stamford, CT 06901",
				CountryId = usa.Id,
			};
			Brand kef = new()
			{
				Id = 23,
				Name = "KEF",
				Homepage = "https://www.kef.com/",
				Address = "Eccleston Road, Tovil, Maidstone Kent, ME15 6QP",
				CountryId = gbr.Id,
			};
			Brand kenwood = new()
			{
				Id = 24,
				Name = "Kenwood",
				Homepage = "https://www.kenwood.com/",
				Address = "Hachioji-shi, Tokyo",
				CountryId = jpn.Id,
			};
			Brand laowa = new()
			{
				Id = 25,
				Name = "Laowa",
				Homepage = "https://www.venuslens.net/",
				Address = "3-9-19 Nishiochiai, Shinjuku-ku, Tokyo",
				CountryId = jpn.Id,
			};
			Brand lg = new()
			{
				Id = 26,
				Name = "LG",
				Homepage = "https://www.lg.com/global",
				Address = "Seoul",
				CountryId = kor.Id,
			};
			Brand marantz = new()
			{
				Id = 27,
				Name = "Marantz",
				Homepage = "https://www.marantz.com/",
				Address = "5541 Fermi Court, Carlsbad, CA 92008",
				CountryId = usa.Id,
			};
			Brand monitoraudio = new()
			{
				Id = 28,
				Name = "Monitor Audio",
				Homepage = "https://www.monitoraudio.com/",
				Address = "Cambridge, England",
				CountryId = gbr.Id,
			};
			Brand nad = new()
			{
				Id = 29,
				Name = "NAD",
				Homepage = "https://nadelectronics.com/",
				Address = "633 Granite Court, Pickering, Ontario L1W 3K1",
				CountryId = can.Id,
			};
			Brand naim = new()
			{
				Id = 30,
				Name = "Naim",
				Homepage = "https://www.naimaudio.com/",
				Address = "Southampton Road, Salisbury SP1 2LN",
				CountryId = gbr.Id,
			};
			Brand nikon = new()
			{
				Id = 31,
				Name = "Nikon",
				Homepage = "https://www.nikon.com/",
				Address = "Tokyo",
				CountryId = jpn.Id,
			};
			Brand oppo = new()
			{
				Id = 32,
				Name = "OPPO",
				Homepage = "https://www.oppo.com/en/",
				Address = "Dongguan, Guangdong",
				CountryId = chn.Id,
			};
			Brand ortofon = new()
			{
				Id = 33,
				Name = "Ortofon",
				Homepage = "https://www.ortofon.com/",
				Address = "Stavangervej 9, DK-4900 Nakskov",
				CountryId = dnk.Id,
			};
			Brand philips = new()
			{
				Id = 34,
				Name = "Philips",
				Homepage = "https://www.philips.com/global",
				Address = "Amsterdam",
				CountryId = nld.Id,
			};
			Brand pioneer = new()
			{
				Id = 35,
				Name = "Pioneer",
				Homepage = "https://global.pioneer/en/car/",
				Address = "28-8, Honkomagome 2-chome, Bunky, Tokyo 113-0021",
				CountryId = jpn.Id,
			};
			Brand polkaudio = new()
			{
				Id = 36,
				Name = "Polk Audio",
				Homepage = "https://www.polkaudio.com/en",
				Address = "5541 Fermi Court, Carlsbad, CA 92008",
				CountryId = usa.Id,
			};
			Brand project = new()
			{
				Id = 37,
				Name = "Pro-Ject",
				Homepage = "https://www.project-audio.com/en/",
				Address = "Analogweg 1, A-2130 Mistelbach",
				CountryId = aut.Id,
			};
			Brand reavon = new()
			{
				Id = 38,
				Name = "Reavon",
				Homepage = "https://www.reavon.com/",
				Address = "66, Boulevard Malesherbes, 75008 Paris",
				CountryId = fra.Id,
			};
			Brand rotelmichi = new()
			{
				Id = 39,
				Name = "ROTEL Michi",
				Homepage = "http://www.rotel.com/en-gb/michi",
				Address = "Dale Road, Worthing, West Sussex",
				CountryId = gbr.Id,
			};
			Brand samsung = new()
			{
				Id = 40,
				Name = "Samsung",
				Homepage = "https://www.samsung.com/",
				Address = "Seoul",
				CountryId = kor.Id,
			};
			Brand sennheiser = new()
			{
				Id = 41,
				Name = "Sennheiser",
				Homepage = "https://en-uk.sennheiser.com/",
				Address = "Am Labor 1, DE-30900 Wedemark",
				CountryId = deu.Id,
			};
			Brand sigma = new()
			{
				Id = 42,
				Name = "Sigma",
				Homepage = "https://www.sigma-global.com/",
				Address = "Asao-ku, Kawasaki, Kanagawa 215-8530",
				CountryId = jpn.Id,
			};
			Brand sonusfaber = new()
			{
				Id = 43,
				Name = "Sonus Faber",
				Homepage = "https://www.sonusfaber.com/en/",
				Address = "Via Antonio Meucci 10 - 36057 Arcugnano (VI)",
				CountryId = ita.Id,
			};
			Brand sony = new()
			{
				Id = 44,
				Name = "Sony",
				Homepage = "https://www.sony.net/",
				Address = "Sony City, Minato, Tokyo",
				CountryId = jpn.Id,
			};
			Brand svs = new()
			{
				Id = 45,
				Name = "SVS",
				Homepage = "https://www.svsound.com/",
				Address = "260 Victoria Road, Youngtown, OH 44515",
				CountryId = usa.Id,
			};
			Brand systemaudio = new()
			{
				Id = 46,
				Name = "System Audio",
				Homepage = "https://system-audio.com/",
				Address = "Klosterengen 137 L, 4000 Roskilde.",
				CountryId = dnk.Id,
			};
			Brand ta = new()
			{
				Id = 47,
				Name = "T+A",
				Homepage = "https://www.ta-hifi.de/en",
				Address = "Planckstraße 9-11, D-32052 Herford",
				CountryId = deu.Id,
			};
			Brand tamron = new()
			{
				Id = 48,
				Name = "Tamron",
				Homepage = "https://tamron.com/",
				Address = "1385 Hasunuma, Minuma-ku, Saitama-shi, Saitama 337-8556",
				CountryId = jpn.Id,
			};
			Brand tcl = new()
			{
				Id = 49,
				Name = "TCL",
				Homepage = "https://www.tcl.com/",
				Address = "Huizhou, Guangdong",
				CountryId = chn.Id,
			};
			Brand thorens = new()
			{
				Id = 50,
				Name = "Thorens",
				Homepage = "https://thorens.com/",
				Address = "Lustheide 85, 51427 Bergisch Gladbach",
				CountryId = deu.Id,
			};
			Brand webyloewe = new()
			{
				Id = 51,
				Name = "We. by Loewe",
				Homepage = "https://we-by-loewe.com/",
				Address = "Industriestraße 11, 96317 Kronach",
				CountryId = deu.Id,
			};
			Brand wilsonaudio = new()
			{
				Id = 52,
				Name = "Wilson Audio",
				Homepage = "https://wilsonaudio.com/",
				Address = "2233 Mountain Vista Ln, Provo, UT 84606",
				CountryId = usa.Id,
			};
			Brand xgimi = new()
			{
				Id = 53,
				Name = "XGIMI",
				Homepage = "https://global.xgimi.com/",
				Address = "5/F 7 Building Tianfu Software Park, High-tech Zone, Chengdu, Sichuan 61-0041",
				CountryId = chn.Id,
			};
			Brand yamaha = new()
			{
				Id = 54,
				Name = "Yamaha",
				Homepage = "https://www.yamaha.com/audio_visual/",
				Address = "10-1, Nakazawacho, Naka-ku Hamamatsu-shi, Shizuoka, 430-8650",
				CountryId = jpn.Id,
			};
			Brand zenec = new()
			{
				Id = 55,
				Name = "Zenec",
				Homepage = "http://www.zenec.com/",
				Address = "Bohrturmweg 1, 5330 Bad Zurzach",
				CountryId = che.Id,
			};
			Brand zoner = new()
			{
				Id = 56,
				Name = "Zoner",
				Homepage = "https://www.zoner.com/",
				Address = "3902 Henderson Blvd Suite 208-187, Tampa, FL 33629",
				CountryId = usa.Id,
			};

			#endregion

			#region Member declarations

			Member member0 = new()
			{
				Id = 1,
				Name = "Audio",
				Website = "http://www.audio.com.pl/",
				ChiefEditor = "Andrzej Kisiel",
				PhoneNumber = "222 578 430",
				OfficeLocation = "ul. Leszczynowa 11, 03-197 Warszawa",
				Publisher = "AVT Corporation",
				ExpertGroupId = hta.Id,
				CountryId = pol.Id,
			};
			Member member1 = new()
			{
				Id = 2,
				Name = "Audio & Cinema em Casa",
				Website = "http://www.audiopt.com/",
				ChiefEditor = "Jorge Goncalves",
				PhoneNumber = "91 723 31 08",
				OfficeLocation = "Rua Prof. Alfredo de Sousa, 7,  5o. Esq.1600-188 Lisboa",
				Publisher = "Cadernos do Som Publicacoes",
				ExpertGroupId = hta.Id,
				CountryId = prt.Id,
			};
			Member member2 = new()
			{
				Id = 3,
				Name = "AUDIOreview",
				Website = "http://www.audioreview.it/",
				ChiefEditor = "Rocco Patriarca",
				PhoneNumber = "06 412 182 87",
				OfficeLocation = "Via Nomentana, 1018  00137 Roma",
				Publisher = "Audiovideoteam Societa Cooperativa",
				ExpertGroupId = hta.Id,
				CountryId = ita.Id,
			};
			Member member3 = new()
			{
				Id = 4,
				Name = "FWD Magazine",
				Website = "http://www.fwdmagazine.be/",
				ChiefEditor = "Jamie Biesemans",
				PhoneNumber = "473 946 983",
				OfficeLocation = "Van den Hautelei 101,  2100 Deurne",
				Publisher = "Xingo Invest",
				ExpertGroupId = hta.Id,
				CountryId = bel.Id,
			};
			Member member4 = new()
			{
				Id = 5,
				Name = "Heimkino",
				Website = "http://www.heimkino-magazin.de/",
				ChiefEditor = "Michael Voigt",
				PhoneNumber = "203 42 92 222",
				OfficeLocation = "Gartroper Strasse 42, D-47138 - Duisburg",
				Publisher = "Michael E. Brieden Verlag GmbH",
				ExpertGroupId = hta.Id,
				CountryId = deu.Id,
			};
			Member member5 = new()
			{
				Id = 6,
				Name = "HemmaBio",
				Website = "http://www.hemmabiotidningen.se/",
				ChiefEditor = "Magnus Fredholm",
				PhoneNumber = "-(0)733 60 33 88",
				OfficeLocation = "Vattenverksvägen 8 - 131 41 Nacka",
				Publisher = "Förlaget Hifi & Musik AB",
				ExpertGroupId = hta.Id,
				CountryId = swe.Id,
			};
			Member member6 = new()
			{
				Id = 7,
				Name = "Hi-Files",
				Website = "http://www.hi-files.com/",
				ChiefEditor = "Ljubiša Miodragović",
				PhoneNumber = "638 866 486",
				OfficeLocation = "Hektorovićeva 1B / lokal 6, 11050 Belgrade",
				Publisher = "Hi-Files Group d.o.o.",
				ExpertGroupId = hta.Id,
				CountryId = srb.Id,
			};
			Member member7 = new()
			{
				Id = 8,
				Name = "Hifimaailma",
				Website = "http://www.hifimaailma.fi/",
				ChiefEditor = "Mauri Eronen",
				PhoneNumber = "4585 65 090",
				OfficeLocation = "Hämeentie 153, - 00560 Helsinki",
				Publisher = "Fokus Media Finland Oy",
				ExpertGroupId = hta.Id,
				CountryId = fin.Id,
			};
			Member member8 = new()
			{
				Id = 9,
				Name = "Home Cinema Choice",
				Website = "http://www.homecinemachoice.com/",
				ChiefEditor = "Mark Craven",
				PhoneNumber = "1 689 869 909",
				OfficeLocation = "Enterprise House, Enterprise Way, Edenbridge, Kent, TN8 6HF",
				Publisher = "AVTech Media Ltd",
				ExpertGroupId = hta.Id,
				CountryId = gbr.Id,
			};
			Member member9 = new()
			{
				Id = 10,
				Name = "Les Années Laser",
				Website = "http://www.annees-laser.com/",
				ChiefEditor = "Gilles Gerin",
				PhoneNumber = "1 55 25 80 00",
				OfficeLocation = "20, passage Turquetil, - 75011 Paris",
				Publisher = "Le 15 du mois SAS",
				ExpertGroupId = hta.Id,
				CountryId = fra.Id,
			};
			Member member10 = new()
			{
				Id = 11,
				Name = "Sound & Vision",
				Website = "https://www.soundandvision.com/",
				ChiefEditor = "Al Griffin",
				PhoneNumber = "",
				OfficeLocation = "Madison Avenue, 8 th floor, New York, NY 10016",
				Publisher = "AVTech Media Ltd",
				ExpertGroupId = hta.Id,
				CountryId = usa.Id,
			};
			Member member11 = new()
			{
				Id = 12,
				Name = "Stereo, Video & Multimedia",
				Website = "http://stereo-video.kiev.ua/",
				ChiefEditor = "Julian Zarembovsky",
				PhoneNumber = "044 223 44 24",
				OfficeLocation = "Levanevskogo 5, office 'Brand', 03058 Kiev",
				Publisher = "Media Most",
				ExpertGroupId = hta.Id,
				CountryId = ukr.Id,
			};
			Member member12 = new()
			{
				Id = 13,
				Name = "Sztereó Sound&Vision",
				Website = "http://www.sztereomagazin.hu/",
				ChiefEditor = "Rezső Soltész",
				PhoneNumber = "14 531 040",
				OfficeLocation = "Attila út 101, H-1012 Budapest",
				Publisher = "Soltész Reklám KFT.",
				ExpertGroupId = hta.Id,
				CountryId = hun.Id,
			};
			Member member13 = new()
			{
				Id = 14,
				Name = "Audio & Cinema em Casa",
				Website = "http://www.audiopt.com/",
				ChiefEditor = "Jorge Goncalves",
				PhoneNumber = "91 723 31 08",
				OfficeLocation = "Rua Prof. Alfredo de Sousa, 7,  5o. Esq.1600-188 Lisboa",
				Publisher = "Cadernos do Som Publicacoes",
				ExpertGroupId = htdv.Id,
				CountryId = prt.Id,
			};
			Member member14 = new()
			{
				Id = 15,
				Name = "Digital Video HT",
				Website = "http://www.digitalvideoht.it/",
				ChiefEditor = "Mario Mollo",
				PhoneNumber = "06 412 182 87",
				OfficeLocation = "Via Nomentana, 1018  00137 Roma",
				Publisher = "Audiovideoteam Societa Cooperativa",
				ExpertGroupId = htdv.Id,
				CountryId = ita.Id,
			};
			Member member15 = new()
			{
				Id = 16,
				Name = "FWD Magazine",
				Website = "http://www.fwdmagazine.be/",
				ChiefEditor = "Jamie Biesemans",
				PhoneNumber = "473 946 983",
				OfficeLocation = "Van den Hautelei 101,   2100 Deurne",
				Publisher = "Xingo Invest",
				ExpertGroupId = htdv.Id,
				CountryId = bel.Id,
			};
			Member member16 = new()
			{
				Id = 17,
				Name = "HemmaBio",
				Website = "http://www.hemmabiotidningen.se/",
				ChiefEditor = "Magnus Fredholm",
				PhoneNumber = "(0)733 60 33 88",
				OfficeLocation = "Vattenverksvägen 8   131 41 Nacka",
				Publisher = "Förlaget Hifi & Musik AB",
				ExpertGroupId = htdv.Id,
				CountryId = swe.Id,
			};
			Member member17 = new()
			{
				Id = 18,
				Name = "Hifi Test TV Video",
				Website = "http://www.hifitest-magazin.de/",
				ChiefEditor = "Michael Voigt",
				PhoneNumber = "203 42 92 222",
				OfficeLocation = "Gartroper Strasse 42, D 47138   Duisburg",
				Publisher = "Michael E. Brieden Verlag GmbH",
				ExpertGroupId = htdv.Id,
				CountryId = deu.Id,
			};
			Member member18 = new()
			{
				Id = 19,
				Name = "Home Cinema Choice",
				Website = "http://www.homecinemachoice.com/",
				ChiefEditor = "Mark Craven",
				PhoneNumber = "1 689 869 909",
				OfficeLocation = "Enterprise House, Enterprise Way, Edenbridge, Kent, TN8 6HF",
				Publisher = "AVTech Media Ltd",
				ExpertGroupId = htdv.Id,
				CountryId = gbr.Id,
			};
			Member member19 = new()
			{
				Id = 20,
				Name = "Les Années Laser",
				Website = "http://www.annees-laser.com/",
				ChiefEditor = "Gilles Gerin",
				PhoneNumber = "1 55 25 80 00",
				OfficeLocation = "20, passage Turquetil, 75011 Paris",
				Publisher = "Le 15 du mois SAS",
				ExpertGroupId = htdv.Id,
				CountryId = fra.Id,
			};
			Member member20 = new()
			{
				Id = 21,
				Name = "ON OFF",
				Website = "http://www.revistaonoff.es/",
				ChiefEditor = "Juan Manuel Urraca",
				PhoneNumber = "670 717 218",
				OfficeLocation = "Avda. Reyes Católicos, 6. Planta 1 3A. 28220 - Majadahonda, Madrid",
				Publisher = "TVAV Editorial Multimedia SL",
				ExpertGroupId = htdv.Id,
				CountryId = esp.Id,
			};
			Member member21 = new()
			{
				Id = 22,
				Name = "Sound Vision (Hxos)",
				Website = "http://www.hxoseikona.gr/",
				ChiefEditor = "Fotios Fotiadis",
				PhoneNumber = " 211 411 39 58",
				OfficeLocation = "49, Agisilaou str., 163.46 Hlioupolis",
				Publisher = "En Sofia Press",
				ExpertGroupId = htdv.Id,
				CountryId = grc.Id,
			};
			Member member22 = new()
			{
				Id = 23,
				Name = "Sound+Image",
				Website = "http://www.avhub.com.au/sound-image",
				ChiefEditor = "Jez Ford",
				PhoneNumber = "291 602 135",
				OfficeLocation = "Suite 3, Level 10, 100 Walker Street, North Sydney, NSW 2060,",
				Publisher = "Future (Overseas) Limited",
				ExpertGroupId = htdv.Id,
				CountryId = aus.Id,
			};
			Member member23 = new()
			{
				Id = 24,
				Name = "Stereo & Video",
				Website = "http://www.stereovideo.cz/",
				ChiefEditor = "Lubos Horcic",
				PhoneNumber = "602 369 369",
				OfficeLocation = "Thámova 7, 186 00 Praha 8",
				Publisher = "Trade & Leisure Publications, spol.s.r.o.",
				ExpertGroupId = htdv.Id,
				CountryId = cze.Id,
			};
			Member member24 = new()
			{
				Id = 25,
				Name = "Sztereó Sound&Vision",
				Website = "http://www.sztereomagazin.hu/",
				ChiefEditor = "Rezső Soltész",
				PhoneNumber = "14 531 040",
				OfficeLocation = "Attila út 101, H-1012 Budapest",
				Publisher = "Soltész Reklám KFT.",
				ExpertGroupId = htdv.Id,
				CountryId = hun.Id,
			};
			Member member25 = new()
			{
				Id = 26,
				Name = "Audio",
				Website = "http://www.audio.com.pl/",
				ChiefEditor = "Andrzej Kisiel",
				PhoneNumber = "222 578 430",
				OfficeLocation = "ul. Leszczynowa 11, 03-197 Warszawa",
				Publisher = "AVT Corporation",
				ExpertGroupId = hifi.Id,
				CountryId = pol.Id,
			};
			Member member26 = new()
			{
				Id = 27,
				Name = "Audio & Cinema em Casa",
				Website = "http://www.audiopt.com/",
				ChiefEditor = "Jorge Goncalves",
				PhoneNumber = "91 723 31 08",
				OfficeLocation = "Rua Prof. Alfredo de Sousa, 7,  5o. Esq.1600-188 Lisboa",
				Publisher = "Cadernos do Som Publicacoes",
				ExpertGroupId = hifi.Id,
				CountryId = prt.Id,
			};
			Member member27 = new()
			{
				Id = 28,
				Name = "Audio Accessory",
				Website = "https://www.phileweb.com/",
				ChiefEditor = "Katsunori Isayama",
				PhoneNumber = "-7724",
				OfficeLocation = "7th AZUMA BLDG, 1-9, Kanda Sakuma-cho, Chiyoda-ku, Tokyo, 101-0025",
				Publisher = "ONGEN PUBLISHING CO.,LTD.",
				ExpertGroupId = hifi.Id,
				CountryId = jpn.Id,
			};
			Member member28 = new()
			{
				Id = 29,
				Name = "AUDIOreview",
				Website = "http://www.audioreview.it/",
				ChiefEditor = "Rocco Patriarca",
				PhoneNumber = "06 412 182 87",
				OfficeLocation = "Via Nomentana, 1018, 00137 Roma",
				Publisher = "Audiovideoteam Societa Cooperativa",
				ExpertGroupId = hifi.Id,
				CountryId = ita.Id,
			};
			Member member29 = new()
			{
				Id = 30,
				Name = "AUDIOTECHNIQUE",
				Website = "https://www.audiotechnique.com/",
				ChiefEditor = "",
				PhoneNumber = "9611-938",
				OfficeLocation = "Room 1101, 11 Floor, SUP Tower, No. 75-83 King's Road, North Point",
				Publisher = "",
				ExpertGroupId = hifi.Id,
				CountryId = hkg.Id,
			};
			Member member30 = new()
			{
				Id = 31,
				Name = "Australian Hi-Fi",
				Website = "http://www.avhub.com.au/hi-fi",
				ChiefEditor = "Greg Borrowman",
				PhoneNumber = "291 602 135",
				OfficeLocation = "Suite 3, Level 10, 100 Walker Street, North Sydney, NSW 2060,",
				Publisher = "Future (Overseas) Limited",
				ExpertGroupId = hifi.Id,
				CountryId = aus.Id,
			};
			Member member31 = new()
			{
				Id = 32,
				Name = "FWD Magazine",
				Website = "http://www.fwdmagazine.be/",
				ChiefEditor = "Jamie Biesemans",
				PhoneNumber = "473 946 983",
				OfficeLocation = "Van den Hautelei 101,  2100 Deurne",
				Publisher = "Xingo Invest",
				ExpertGroupId = hifi.Id,
				CountryId = bel.Id,
			};
			Member member32 = new()
			{
				Id = 33,
				Name = "Hi-Fi News",
				Website = "http://www.hifinews.co.uk/",
				ChiefEditor = "Paul Miller",
				PhoneNumber = "1 689 869 909",
				OfficeLocation = "Enterprise House, Enterprise Way, Edenbridge, Kent, TN8 6HF",
				Publisher = "AVTech Media Ltd",
				ExpertGroupId = hifi.Id,
				CountryId = gbr.Id,
			};
			Member member33 = new()
			{
				Id = 34,
				Name = "Hi-Files",
				Website = "http://www.hi-files.com/",
				ChiefEditor = "Ljubiša Miodragović",
				PhoneNumber = "638 866 486",
				OfficeLocation = "Hektorovićeva 1B / lokal 6, 11050 Belgrade",
				Publisher = "Hi-Files Group d.o.o.",
				ExpertGroupId = hifi.Id,
				CountryId = srb.Id,
			};
			Member member34 = new()
			{
				Id = 35,
				Name = "Hifi & Musik",
				Website = "http://www.hifi-musik.se/",
				ChiefEditor = "Jonas Bryngelsson",
				PhoneNumber = "-(0)733 60 33 88",
				OfficeLocation = "Vattenverksvägen 8 - 131 41 Nacka",
				Publisher = "Förlaget Hifi & Musik AB",
				ExpertGroupId = hifi.Id,
				CountryId = swe.Id,
			};
			Member member35 = new()
			{
				Id = 36,
				Name = "Hifimaailma",
				Website = "http://www.hifimaailma.fi/",
				ChiefEditor = "Mauri Eronen",
				PhoneNumber = "4585 65 090",
				OfficeLocation = "Hämeentie 153, 00560 Helsinki",
				Publisher = "Fokus Media Finland Oy",
				ExpertGroupId = hifi.Id,
				CountryId = fin.Id,
			};
			Member member36 = new()
			{
				Id = 37,
				Name = "HVT",
				Website = "http://www.hvt.nl/",
				ChiefEditor = "Ward Maas",
				PhoneNumber = "24 76 00 66 0",
				OfficeLocation = "Kerkenbos 1015E, 6546 BB Nijmegen",
				Publisher = "Brinkman Media Group BV",
				ExpertGroupId = hifi.Id,
				CountryId = nld.Id,
			};
			Member member37 = new()
			{
				Id = 38,
				Name = "Sound Vision (Hxos)",
				Website = "http://www.hxoseikona.gr/",
				ChiefEditor = "Fotios Fotiadis",
				PhoneNumber = " 211 411 39 58",
				OfficeLocation = "49, Agisilaou str., 163.46 Hlioupolis",
				Publisher = "En Sofia Press",
				ExpertGroupId = hifi.Id,
				CountryId = grc.Id,
			};
			Member member38 = new()
			{
				Id = 39,
				Name = "SOUNDSTAGE! HI-FI",
				Website = "https://www.soundstagehifi.com/",
				ChiefEditor = "Doug Schneider",
				PhoneNumber = "613-297-7651",
				OfficeLocation = "1953 Meldrum Avenue Ottawa, ON K1J 7V6",
				Publisher = "c/o Schneider Publishing Inc.",
				ExpertGroupId = hifi.Id,
				CountryId = can.Id,
			};
			Member member39 = new()
			{
				Id = 40,
				Name = "Stereo",
				Website = "http://www.stereo.de/",
				ChiefEditor = "Matthias Böde",
				PhoneNumber = "225 165 04 60",
				OfficeLocation = "Eifelring 28, D-53879 Euskirchen",
				Publisher = "R.H. Nitschke Verlag GmbH",
				ExpertGroupId = hifi.Id,
				CountryId = deu.Id,
			};
			Member member40 = new()
			{
				Id = 41,
				Name = "Stereo & Video",
				Website = "http://www.stereovideo.cz/",
				ChiefEditor = "Lubos Horcic",
				PhoneNumber = "602 369 369",
				OfficeLocation = "Thámova 7, 186 00 Praha 8",
				Publisher = "Trade & Leisure Publications, spol.s.r.o.",
				ExpertGroupId = hifi.Id,
				CountryId = cze.Id,
			};
			Member member41 = new()
			{
				Id = 42,
				Name = "Stereo+",
				Website = "http://www.stereopluss.no/",
				ChiefEditor = "Havard Holmedal",
				PhoneNumber = "907 43 646",
				OfficeLocation = "Fridtjof Nansens vei 1E 1472 Fjellhamar",
				Publisher = "Exciter AS",
				ExpertGroupId = hifi.Id,
				CountryId = nor.Id,
			};
			Member member42 = new()
			{
				Id = 43,
				Name = "Stereophile",
				Website = "https://www.stereophile.com/",
				ChiefEditor = "Jim Austin",
				PhoneNumber = "",
				OfficeLocation = "Madison Avenue, 8 th floor, New York, NY 10016",
				Publisher = "AVTech Media Ltd",
				ExpertGroupId = hifi.Id,
				CountryId = usa.Id,
			};
			Member member43 = new()
			{
				Id = 44,
				Name = "Sztereó Sound&Vision",
				Website = "http://www.sztereomagazin.hu/",
				ChiefEditor = "Rezső Soltész",
				PhoneNumber = "14 531 040",
				OfficeLocation = "Attila út 101, H-1012 Budapest",
				Publisher = "Soltész Reklám KFT.",
				ExpertGroupId = hifi.Id,
				CountryId = hun.Id,
			};
			Member member44 = new()
			{
				Id = 45,
				Name = "12 Voltios Car Audio & Technology",
				Website = "http://www.12vpersonalcar.com/",
				ChiefEditor = "Edgar Compte",
				PhoneNumber = "934 238 404",
				OfficeLocation = "C/Tarragona 84-90, 1?-8?, 08015 Barcelona",
				Publisher = "Ideas Editoriales 3003 SL",
				ExpertGroupId = ice.Id,
				CountryId = esp.Id,
			};
			Member member45 = new()
			{
				Id = 46,
				Name = "A3/AvtoZvuk",
				Website = "http://xn--80aeatqv1al.xn--p1ai/",
				ChiefEditor = "Anatoly Shikhatov",
				PhoneNumber = "495 788 05 44",
				OfficeLocation = "Rumyantsevo business park, block D, office 841, Moscow",
				Publisher = "Anatoly Shikhatov",
				ExpertGroupId = ice.Id,
				CountryId = rus.Id,
			};
			Member member46 = new()
			{
				Id = 47,
				Name = "ACS AudioCarStereo",
				Website = "http://www.audiocarstereo.it/",
				ChiefEditor = "Rocco Patriarca",
				PhoneNumber = "06 412 182 87",
				OfficeLocation = "Via Nomentana, 1018  00137 Roma",
				Publisher = "Audiovideoteam Societa Cooperativa",
				ExpertGroupId = ice.Id,
				CountryId = ita.Id,
			};
			Member member47 = new()
			{
				Id = 48,
				Name = "Australian InCar",
				Website = "http://www.avhub.com.au/incar",
				ChiefEditor = "Jez Ford",
				PhoneNumber = "299 016 100",
				OfficeLocation = "Level 6, Building A, 207 Pacific Highway, St Leonards, NSW 2065,",
				Publisher = "Nextmedia Pty LTD",
				ExpertGroupId = ice.Id,
				CountryId = aus.Id,
			};
			Member member48 = new()
			{
				Id = 49,
				Name = "AutoSound Technical Magazine",
				Website = "http://www.autosound.fi/",
				ChiefEditor = "Teppo Hirvikunnas",
				PhoneNumber = "810 778 64 01",
				OfficeLocation = "Teerlakiantie 262, 21530 PAIMIO",
				Publisher = "JTS-Media",
				ExpertGroupId = ice.Id,
				CountryId = fin.Id,
			};
			Member member49 = new()
			{
				Id = 50,
				Name = "Car&Hifi",
				Website = "http://www.carhifi-magazin.de/",
				ChiefEditor = "Elmar Michels",
				PhoneNumber = "203 42 92 222",
				OfficeLocation = "Gartroper Strasse 42, D 47138   Duisburg",
				Publisher = "Michael E. Brieden Verlag GmbH",
				ExpertGroupId = ice.Id,
				CountryId = deu.Id,
			};
			Member member50 = new()
			{
				Id = 51,
				Name = "Stereo, Video & Multimedia",
				Website = "http://stereo-video.kiev.ua/",
				ChiefEditor = "Julian Zarembovsky",
				PhoneNumber = "044 223 44 24",
				OfficeLocation = "Levanevskogo 5, office 'Brand', 03058 Kiev",
				Publisher = "Media Most",
				ExpertGroupId = ice.Id,
				CountryId = ukr.Id,
			};
			Member member51 = new()
			{
				Id = 52,
				Name = "AV Magazine",
				Website = "https://www.avmagazine.it/",
				ChiefEditor = "Emidio Frattaroli",
				PhoneNumber = "3 939 013 731",
				OfficeLocation = "via G. Cameli 21, 64100 Teramo (TE)",
				Publisher = "AV Raw S.n.c.",
				ExpertGroupId = md.Id,
				CountryId = ita.Id,
			};
			Member member52 = new()
			{
				Id = 53,
				Name = "Bug",
				Website = "http://www.bug.hr/",
				ChiefEditor = "Dragan Petric",
				PhoneNumber = "513 821 555",
				OfficeLocation = "Ogrizoviceva 36A, HR-1000, Zagreb",
				Publisher = "Bug d.o.o.",
				ExpertGroupId = md.Id,
				CountryId = hrv.Id,
			};
			Member member53 = new()
			{
				Id = 54,
				Name = "Computer!Totaal",
				Website = "https://computertotaal.nl/",
				ChiefEditor = "Jeroen de Jager",
				PhoneNumber = "235 430 000",
				OfficeLocation = "Richard Holkade 8, NL-2033 PZ Haarlem",
				Publisher = "Reshift Digital",
				ExpertGroupId = md.Id,
				CountryId = nld.Id,
			};
			Member member54 = new()
			{
				Id = 55,
				Name = "Datatid TechLife",
				Website = "http://www.datatid.dk/",
				ChiefEditor = "Knud Sondergaard",
				PhoneNumber = "221 199 40",
				OfficeLocation = "Lautrupsgade 7, DK-2100 Copenhagen O",
				Publisher = "Audio Media A/S",
				ExpertGroupId = md.Id,
				CountryId = dnk.Id,
			};
			Member member55 = new()
			{
				Id = 56,
				Name = "Hi-Files",
				Website = "http://www.hi-files.com/",
				ChiefEditor = "Ljubiša Miodragović",
				PhoneNumber = "638 866 486",
				OfficeLocation = "Hektorovićeva 1B / lokal 6, 11050 Belgrade",
				Publisher = "Hi-Files Group d.o.o.",
				ExpertGroupId = md.Id,
				CountryId = srb.Id,
			};
			Member member56 = new()
			{
				Id = 57,
				Name = "HiComm",
				Website = "http://www.hicomm.bg/",
				ChiefEditor = "Yoana Andreeva",
				PhoneNumber = "888 818 887",
				OfficeLocation = "145B, Rakovski Blvd., 1000, Sofia",
				Publisher = "HiEnd Publishing",
				ExpertGroupId = md.Id,
				CountryId = bgr.Id,
			};
			Member member57 = new()
			{
				Id = 58,
				Name = "ON OFF",
				Website = "http://www.revistaonoff.es/",
				ChiefEditor = "Juan Manuel Urraca",
				PhoneNumber = "670 717 218",
				OfficeLocation = "Avda. Reyes Cat?licos, 6. Planta 1 3A. 28220 - Majadahonda, Madrid",
				Publisher = "TVAV Editorial Multimedia SL",
				ExpertGroupId = md.Id,
				CountryId = esp.Id,
			};
			Member member58 = new()
			{
				Id = 59,
				Name = "Sound Vision (Hxos)",
				Website = "http://www.hxoseikona.gr/",
				ChiefEditor = "Fotios Fotiadis",
				PhoneNumber = " 211 411 39 58",
				OfficeLocation = "49, Agisilaou str., 163.46 Hlioupolis",
				Publisher = "En Sofia Press",
				ExpertGroupId = md.Id,
				CountryId = grc.Id,
			};
			Member member59 = new()
			{
				Id = 60,
				Name = "Stereo & Video",
				Website = "http://www.stereovideo.cz/",
				ChiefEditor = "Lubos Horcic",
				PhoneNumber = "602 369 369",
				OfficeLocation = "Thámova 7, 186 00 Praha 8",
				Publisher = "Trade & Leisure Publications, spol.s.r.o.",
				ExpertGroupId = md.Id,
				CountryId = cze.Id,
			};
			Member member60 = new()
			{
				Id = 61,
				Name = "touchIT",
				Website = "https://touchit.sk/",
				ChiefEditor = "Ondrej Macko",
				PhoneNumber = "19 11 04 60 06",
				OfficeLocation = "Reding Tower 2, Račianska 153, 831 54 Bratislava - Rača",
				Publisher = "ASTON ITM",
				ExpertGroupId = md.Id,
				CountryId = 26,
			};

			#endregion

			#region Product declarations

			Product prod0 = new()
			{
				Id = 1,
				Name = "Silver 500 7G",
				Category = "EISA FLOORSTANDING LOUDSPEAKERS 2021-2022",
				Price = 505308,
				LaunchDate = new System.DateTime(2021, 8, 6),
				EstimatedLifetime = 4,
				BrandId = monitoraudio.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod1 = new()
			{
				Id = 2,
				Name = "LS50 Meta",
				Category = "EISA STANDMOUNT LOUDSPEAKERS 2021-2022",
				Price = 822986,
				LaunchDate = new System.DateTime(2021, 8, 20),
				EstimatedLifetime = 8,
				BrandId = kef.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod2 = new()
			{
				Id = 3,
				Name = "Legend 40.2 Silverback",
				Category = "EISA WIRELESS FLOORSTANDING LOUDSPEAKERS 2021-2022",
				Price = 574916,
				LaunchDate = new System.DateTime(2021, 8, 6),
				EstimatedLifetime = 7,
				BrandId = systemaudio.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod3 = new()
			{
				Id = 4,
				Name = "LS50 Wireless II",
				Category = "EISA WIRELESS STANDMOUNT LOUDSPEAKERS 2021-2022",
				Price = 550047,
				LaunchDate = new System.DateTime(2021, 7, 13),
				EstimatedLifetime = 7,
				BrandId = kef.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod4 = new()
			{
				Id = 5,
				Name = "SabrinaX",
				Category = "EISA HIGH-END LOUDSPEAKERS 2021-2022",
				Price = 845859,
				LaunchDate = new System.DateTime(2021, 8, 8),
				EstimatedLifetime = 9,
				BrandId = wilsonaudio.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod5 = new()
			{
				Id = 6,
				Name = "KC62",
				Category = "EISA HI-FI SUBWOOFER 2021-2022",
				Price = 335008,
				LaunchDate = new System.DateTime(2021, 6, 27),
				EstimatedLifetime = 3,
				BrandId = kef.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod6 = new()
			{
				Id = 7,
				Name = "EVO 150",
				Category = "EISA STREAMING AMPLIFIER 2021-2022",
				Price = 429827,
				LaunchDate = new System.DateTime(2021, 7, 29),
				EstimatedLifetime = 1,
				BrandId = cambridgeaudio.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod7 = new()
			{
				Id = 8,
				Name = "X3",
				Category = "EISA INTEGRATED AMPLIFIER 2021-2022",
				Price = 312877,
				LaunchDate = new System.DateTime(2021, 7, 2),
				EstimatedLifetime = 1,
				BrandId = rotelmichi.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod8 = new()
			{
				Id = 9,
				Name = "C 298",
				Category = "EISA POWER AMPLIFIER 2021-2022",
				Price = 371116,
				LaunchDate = new System.DateTime(2021, 8, 22),
				EstimatedLifetime = 6,
				BrandId = nad.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod9 = new()
			{
				Id = 10,
				Name = "V10",
				Category = "EISA PHONO STAGE 2021-2022",
				Price = 590186,
				LaunchDate = new System.DateTime(2021, 6, 11),
				EstimatedLifetime = 9,
				BrandId = hegel.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod10 = new()
			{
				Id = 11,
				Name = "NODE",
				Category = "EISA DIGITAL MUSIC PLAYER 2021-2022",
				Price = 171361,
				LaunchDate = new System.DateTime(2021, 6, 4),
				EstimatedLifetime = 8,
				BrandId = bluesound.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod11 = new()
			{
				Id = 12,
				Name = "RS150",
				Category = "EISA HIGH-END MUSIC PLAYER 2021-2022",
				Price = 873431,
				LaunchDate = new System.DateTime(2021, 8, 13),
				EstimatedLifetime = 2,
				BrandId = hifirose.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod12 = new()
			{
				Id = 13,
				Name = "DacMagic 200M",
				Category = "EISA DAC 2021-2022",
				Price = 657159,
				LaunchDate = new System.DateTime(2021, 7, 24),
				EstimatedLifetime = 0,
				BrandId = cambridgeaudio.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod13 = new()
			{
				Id = 14,
				Name = "Debut PRO",
				Category = "EISA BEST VALUE TURNTABLE 2021-2022",
				Price = 537186,
				LaunchDate = new System.DateTime(2021, 6, 23),
				EstimatedLifetime = 5,
				BrandId = project.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod14 = new()
			{
				Id = 15,
				Name = "TD 124 DD",
				Category = "EISA HIGH-END TURNTABLE 2021-2022",
				Price = 189807,
				LaunchDate = new System.DateTime(2021, 5, 31),
				EstimatedLifetime = 6,
				BrandId = thorens.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod15 = new()
			{
				Id = 16,
				Name = "2M Black LVB 250",
				Category = "EISA PHONO CARTRIDGE 2021-2022",
				Price = 199150,
				LaunchDate = new System.DateTime(2021, 7, 21),
				EstimatedLifetime = 3,
				BrandId = ortofon.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod16 = new()
			{
				Id = 17,
				Name = "Model 30/SACD 30n",
				Category = "EISA STEREO SYSTEM 2021-2022",
				Price = 373501,
				LaunchDate = new System.DateTime(2021, 6, 22),
				EstimatedLifetime = 8,
				BrandId = marantz.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod17 = new()
			{
				Id = 18,
				Name = "Uniti Atom Headphone Edition",
				Category = "EISA HEADPHONE SOLUTION 2021-2022",
				Price = 578575,
				LaunchDate = new System.DateTime(2021, 6, 7),
				EstimatedLifetime = 8,
				BrandId = naim.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod18 = new()
			{
				Id = 19,
				Name = "Clear Mg",
				Category = "EISA HEADPHONES 2021-2022",
				Price = 449966,
				LaunchDate = new System.DateTime(2021, 7, 23),
				EstimatedLifetime = 1,
				BrandId = focal.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod19 = new()
			{
				Id = 20,
				Name = "Solitaire P-SE",
				Category = "EISA HIGH-END HEADPHONES 2021-2022",
				Price = 615551,
				LaunchDate = new System.DateTime(2021, 7, 31),
				EstimatedLifetime = 4,
				BrandId = ta.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod20 = new()
			{
				Id = 21,
				Name = "HD 560 S",
				Category = "EISA BEST VALUE HEADPHONES 2021-2022",
				Price = 31931,
				LaunchDate = new System.DateTime(2021, 6, 14),
				EstimatedLifetime = 9,
				BrandId = sennheiser.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod21 = new()
			{
				Id = 22,
				Name = "Hypsos",
				Category = "EISA HI-FI ACCESSORY 2021-2022",
				Price = 383778,
				LaunchDate = new System.DateTime(2021, 6, 6),
				EstimatedLifetime = 3,
				BrandId = ferrum.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod22 = new()
			{
				Id = 23,
				Name = "3000 Micro",
				Category = "EISA COMPACT SUBWOOFER 2021-2022",
				Price = 579308,
				LaunchDate = new System.DateTime(2021, 6, 21),
				EstimatedLifetime = 9,
				BrandId = svs.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod23 = new()
			{
				Id = 24,
				Name = "Fidelio L3",
				Category = "EISA WIRELESS HEADPHONES 2021-2022",
				Price = 60741,
				LaunchDate = new System.DateTime(2021, 7, 25),
				EstimatedLifetime = 6,
				BrandId = philips.Id,
				ExpertGroupId = hifi.Id,
			};
			Product prod24 = new()
			{
				Id = 25,
				Name = "Home Sound Bar 550",
				Category = "EISA SMART SOUNDBAR 2021-2022",
				Price = 294412,
				LaunchDate = new System.DateTime(2021, 6, 30),
				EstimatedLifetime = 8,
				BrandId = denon.Id,
				ExpertGroupId = hta.Id,
			};
			Product prod25 = new()
			{
				Id = 26,
				Name = "Eclair xQP5",
				Category = "EISA SOUNDBAR INNOVATION 2021-2022",
				Price = 606062,
				LaunchDate = new System.DateTime(2021, 7, 13),
				EstimatedLifetime = 1,
				BrandId = lg.Id,
				ExpertGroupId = hta.Id,
			};
			Product prod26 = new()
			{
				Id = 27,
				Name = "Bar 5.0 Multibeam",
				Category = "EISA COMPACT SOUNDBAR 2021-2022",
				Price = 647728,
				LaunchDate = new System.DateTime(2021, 7, 25),
				EstimatedLifetime = 1,
				BrandId = jbl.Id,
				ExpertGroupId = hta.Id,
			};
			Product prod27 = new()
			{
				Id = 28,
				Name = "TS8132",
				Category = "EISA BEST BUY SOUNDBAR 2021-2022",
				Price = 644485,
				LaunchDate = new System.DateTime(2021, 8, 15),
				EstimatedLifetime = 2,
				BrandId = tcl.Id,
				ExpertGroupId = hta.Id,
			};
			Product prod28 = new()
			{
				Id = 29,
				Name = "Fidelio B97",
				Category = "EISA HOME THEATRE SOUNDBAR 2021-2022",
				Price = 138034,
				LaunchDate = new System.DateTime(2021, 5, 25),
				EstimatedLifetime = 5,
				BrandId = philips.Id,
				ExpertGroupId = hta.Id,
			};
			Product prod29 = new()
			{
				Id = 30,
				Name = "3000 Micro",
				Category = "EISA COMPACT SUBWOOFER 2021-2022",
				Price = 34686,
				LaunchDate = new System.DateTime(2021, 8, 16),
				EstimatedLifetime = 4,
				BrandId = svs.Id,
				ExpertGroupId = hta.Id,
			};
			Product prod30 = new()
			{
				Id = 31,
				Name = "Reserve",
				Category = "EISA HOME THEATRE SPEAKER SYSTEM 2021-2022",
				Price = 775368,
				LaunchDate = new System.DateTime(2021, 6, 28),
				EstimatedLifetime = 0,
				BrandId = polkaudio.Id,
				ExpertGroupId = hta.Id,
			};
			Product prod31 = new()
			{
				Id = 32,
				Name = "RX-V6A",
				Category = "EISA HOME THEATRE RECEIVER 2021-2022",
				Price = 94617,
				LaunchDate = new System.DateTime(2021, 6, 30),
				EstimatedLifetime = 5,
				BrandId = yamaha.Id,
				ExpertGroupId = hta.Id,
			};
			Product prod32 = new()
			{
				Id = 33,
				Name = "UBR-X200",
				Category = "EISA UHD PLAYER 2021-2022",
				Price = 899610,
				LaunchDate = new System.DateTime(2021, 7, 28),
				EstimatedLifetime = 8,
				BrandId = reavon.Id,
				ExpertGroupId = hta.Id,
			};
			Product prod33 = new()
			{
				Id = 34,
				Name = "75QNED99",
				Category = "EISA 8K TV 2021-2022",
				Price = 79929,
				LaunchDate = new System.DateTime(2021, 7, 24),
				EstimatedLifetime = 6,
				BrandId = lg.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod34 = new()
			{
				Id = 35,
				Name = "OLED65G1",
				Category = "EISA BEST PREMIUM OLED TV 2021-2022",
				Price = 681526,
				LaunchDate = new System.DateTime(2021, 8, 7),
				EstimatedLifetime = 3,
				BrandId = lg.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod35 = new()
			{
				Id = 36,
				Name = "OLED48C1",
				Category = "EISA BEST GAMING TV 2021-2022",
				Price = 368089,
				LaunchDate = new System.DateTime(2021, 5, 29),
				EstimatedLifetime = 2,
				BrandId = lg.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod36 = new()
			{
				Id = 37,
				Name = "65U8GQ",
				Category = "EISA FAMILY TV 2021-2022",
				Price = 259861,
				LaunchDate = new System.DateTime(2021, 8, 17),
				EstimatedLifetime = 8,
				BrandId = hisense.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod37 = new()
			{
				Id = 38,
				Name = "55C728 (55C727 & 55C729)",
				Category = "EISA BEST BUY LCD TV 2021-2022",
				Price = 887466,
				LaunchDate = new System.DateTime(2021, 7, 29),
				EstimatedLifetime = 9,
				BrandId = tcl.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod38 = new()
			{
				Id = 39,
				Name = "65C825 (65C821 & 65C822)",
				Category = "EISA PREMIUM LCD TV 2021-2022",
				Price = 936384,
				LaunchDate = new System.DateTime(2021, 6, 15),
				EstimatedLifetime = 3,
				BrandId = tcl.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod39 = new()
			{
				Id = 40,
				Name = "65OLED936",
				Category = "EISA BEST HOME THEATRE TV 2021-2022",
				Price = 689121,
				LaunchDate = new System.DateTime(2021, 6, 12),
				EstimatedLifetime = 2,
				BrandId = philips.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod40 = new()
			{
				Id = 41,
				Name = "55OLED806",
				Category = "EISA BEST BUY OLED TV 2021-2022",
				Price = 870543,
				LaunchDate = new System.DateTime(2021, 7, 5),
				EstimatedLifetime = 5,
				BrandId = philips.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod41 = new()
			{
				Id = 42,
				Name = "The Premiere LSP9T",
				Category = "EISA PREMIUM UST PROJECTOR 2021-2022",
				Price = 614354,
				LaunchDate = new System.DateTime(2021, 6, 27),
				EstimatedLifetime = 2,
				BrandId = samsung.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod42 = new()
			{
				Id = 43,
				Name = "VPL-VW590ES",
				Category = "EISA HOME THEATRE PROJECTOR 2021-2022",
				Price = 707050,
				LaunchDate = new System.DateTime(2021, 7, 20),
				EstimatedLifetime = 2,
				BrandId = sony.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod43 = new()
			{
				Id = 44,
				Name = "Horizon Pro",
				Category = "EISA BEST BUY PROJECTOR 2021-2022",
				Price = 10497,
				LaunchDate = new System.DateTime(2021, 7, 25),
				EstimatedLifetime = 7,
				BrandId = xgimi.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod44 = new()
			{
				Id = 45,
				Name = "V7000i/V7050i & ALRS01",
				Category = "EISA LASER TV PROJECTION SYSTEM 2021-2022",
				Price = 167651,
				LaunchDate = new System.DateTime(2021, 7, 4),
				EstimatedLifetime = 5,
				BrandId = benq.Id,
				ExpertGroupId = htdv.Id,
			};
			Product prod45 = new()
			{
				Id = 46,
				Name = "?1",
				Category = "EISA CAMERA OF THE YEAR 2021-2022",
				Price = 983035,
				LaunchDate = new System.DateTime(2021, 6, 2),
				EstimatedLifetime = 9,
				BrandId = sony.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod46 = new()
			{
				Id = 47,
				Name = "X-S10",
				Category = "EISA BEST BUY CAMERA (APS-C) 2021-2022",
				Price = 917082,
				LaunchDate = new System.DateTime(2021, 7, 27),
				EstimatedLifetime = 7,
				BrandId = fujifilm.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod47 = new()
			{
				Id = 48,
				Name = "Z 5",
				Category = "EISA BEST BUY CAMERA (FULL-FRAME) 2021-2022",
				Price = 48655,
				LaunchDate = new System.DateTime(2021, 7, 1),
				EstimatedLifetime = 7,
				BrandId = nikon.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod48 = new()
			{
				Id = 49,
				Name = "Z 6II",
				Category = "EISA ADVANCED CAMERA 2021-2022",
				Price = 23031,
				LaunchDate = new System.DateTime(2021, 8, 17),
				EstimatedLifetime = 8,
				BrandId = nikon.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod49 = new()
			{
				Id = 50,
				Name = "EOS R5",
				Category = "EISA PREMIUM CAMERA 2021-2022",
				Price = 340686,
				LaunchDate = new System.DateTime(2021, 8, 12),
				EstimatedLifetime = 1,
				BrandId = canon.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod50 = new()
			{
				Id = 51,
				Name = "GFX 100S",
				Category = "EISA PROFESSIONAL CAMERA 2021-2022",
				Price = 838827,
				LaunchDate = new System.DateTime(2021, 7, 4),
				EstimatedLifetime = 2,
				BrandId = fujifilm.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod51 = new()
			{
				Id = 52,
				Name = "?7S III",
				Category = "EISA PHOTO/VIDEO CAMERA 2021-2022",
				Price = 506958,
				LaunchDate = new System.DateTime(2021, 8, 18),
				EstimatedLifetime = 2,
				BrandId = sony.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod52 = new()
			{
				Id = 53,
				Name = "17-70mm F/2.8 Di III-A VC RXD",
				Category = "EISA LENS OF THE YEAR 2021-2022",
				Price = 794218,
				LaunchDate = new System.DateTime(2021, 8, 20),
				EstimatedLifetime = 0,
				BrandId = tamron.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod53 = new()
			{
				Id = 54,
				Name = "FE 14mm F1.8 GM",
				Category = "EISA WIDEANGLE LENS 2021-2022",
				Price = 547905,
				LaunchDate = new System.DateTime(2021, 8, 16),
				EstimatedLifetime = 6,
				BrandId = sony.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod54 = new()
			{
				Id = 55,
				Name = "11-20mm F/2.8 Di III-A RXD",
				Category = "EISA WIDEANGLE ZOOM LENS (APS-C) 2021-2022",
				Price = 577164,
				LaunchDate = new System.DateTime(2021, 8, 11),
				EstimatedLifetime = 2,
				BrandId = tamron.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod55 = new()
			{
				Id = 56,
				Name = "FE 12-24mm F2.8 GM",
				Category = "EISA WIDEANGLE ZOOM LENS (FULL-FRAME) 2021-2022",
				Price = 752317,
				LaunchDate = new System.DateTime(2021, 7, 19),
				EstimatedLifetime = 7,
				BrandId = sony.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod56 = new()
			{
				Id = 57,
				Name = "FE 50mm F1.2 GM",
				Category = "EISA STANDARD LENS 2021-2022",
				Price = 727778,
				LaunchDate = new System.DateTime(2021, 7, 16),
				EstimatedLifetime = 9,
				BrandId = sony.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod57 = new()
			{
				Id = 58,
				Name = "150-500mm F/5-6.7 Di III VC VXD",
				Category = "EISA TELEPHOTO ZOOM LENS 2021-2022",
				Price = 864119,
				LaunchDate = new System.DateTime(2021, 7, 11),
				EstimatedLifetime = 7,
				BrandId = tamron.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod58 = new()
			{
				Id = 59,
				Name = "NIKKOR Z 70-200mm f/2.8 VR S",
				Category = "EISA PROFESSIONAL TELEPHOTO ZOOM LENS 2021-2022",
				Price = 497274,
				LaunchDate = new System.DateTime(2021, 6, 14),
				EstimatedLifetime = 8,
				BrandId = nikon.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod59 = new()
			{
				Id = 60,
				Name = "85mm F1.4 DG DN Art",
				Category = "EISA PORTRAIT LENS 2021-2022",
				Price = 959404,
				LaunchDate = new System.DateTime(2021, 7, 19),
				EstimatedLifetime = 5,
				BrandId = sigma.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod60 = new()
			{
				Id = 61,
				Name = "Argus 33mm f/0.95 CF APO",
				Category = "EISA MANUAL LENS 2021-2022",
				Price = 562454,
				LaunchDate = new System.DateTime(2021, 8, 19),
				EstimatedLifetime = 3,
				BrandId = laowa.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod61 = new()
			{
				Id = 62,
				Name = "NIKKOR Z MC 50mm f/2.8",
				Category = "EISA MACRO LENS 2021-2022",
				Price = 259579,
				LaunchDate = new System.DateTime(2021, 7, 10),
				EstimatedLifetime = 1,
				BrandId = nikon.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod62 = new()
			{
				Id = 63,
				Name = "15mm f/4.5 Zero-D Shift",
				Category = "EISA SPECIAL PURPOSE LENS 2021-2022",
				Price = 766757,
				LaunchDate = new System.DateTime(2021, 7, 21),
				EstimatedLifetime = 6,
				BrandId = laowa.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod63 = new()
			{
				Id = 64,
				Name = "RF 100mm F 2.8L Macro IS USM",
				Category = "EISA LENS INNOVATION 2021-2022",
				Price = 496904,
				LaunchDate = new System.DateTime(2021, 6, 3),
				EstimatedLifetime = 1,
				BrandId = canon.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod64 = new()
			{
				Id = 65,
				Name = "Photo Studio X",
				Category = "EISA PHOTO SOFTWARE 2021-2022",
				Price = 866540,
				LaunchDate = new System.DateTime(2021, 8, 13),
				EstimatedLifetime = 3,
				BrandId = zoner.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod65 = new()
			{
				Id = 66,
				Name = "PureRAW",
				Category = "EISA ADVANCED PHOTO SOFTWARE 2021-2022",
				Price = 224256,
				LaunchDate = new System.DateTime(2021, 7, 7),
				EstimatedLifetime = 4,
				BrandId = dxo.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod66 = new()
			{
				Id = 67,
				Name = "Photoworld",
				Category = "EISA PHOTO SERVICE 2021-2022",
				Price = 452108,
				LaunchDate = new System.DateTime(2021, 8, 9),
				EstimatedLifetime = 2,
				BrandId = cewe.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod67 = new()
			{
				Id = 68,
				Name = "SureColor SC-P700",
				Category = "EISA PHOTO PRINTER 2021-2022",
				Price = 683761,
				LaunchDate = new System.DateTime(2021, 8, 6),
				EstimatedLifetime = 6,
				BrandId = epson.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod68 = new()
			{
				Id = 69,
				Name = "Xperia 1 III",
				Category = "EISA MULTIMEDIA SMARTPHONE 2021-2022",
				Price = 713319,
				LaunchDate = new System.DateTime(2021, 7, 14),
				EstimatedLifetime = 9,
				BrandId = sony.Id,
				ExpertGroupId = photo.Id,
			};
			Product prod69 = new()
			{
				Id = 70,
				Name = "Find X3 Pro",
				Category = "EISA ADVANCED SMARTPHONE 2021-2022",
				Price = 84484,
				LaunchDate = new System.DateTime(2021, 7, 21),
				EstimatedLifetime = 0,
				BrandId = oppo.Id,
				ExpertGroupId = md.Id,
			};
			Product prod70 = new()
			{
				Id = 71,
				Name = "iPhone 12 Pro Max",
				Category = "EISA BEST SMARTPHONE 2021-2022",
				Price = 295421,
				LaunchDate = new System.DateTime(2021, 7, 12),
				EstimatedLifetime = 8,
				BrandId = apple.Id,
				ExpertGroupId = md.Id,
			};
			Product prod71 = new()
			{
				Id = 72,
				Name = "Mate X2",
				Category = "EISA FOLDABLE SMARTPHONE 2021-2022",
				Price = 649912,
				LaunchDate = new System.DateTime(2021, 7, 28),
				EstimatedLifetime = 9,
				BrandId = huawei.Id,
				ExpertGroupId = md.Id,
			};
			Product prod72 = new()
			{
				Id = 73,
				Name = "20 Pro 5G",
				Category = "EISA BEST BUY SMARTPHONE 2021-2022",
				Price = 42804,
				LaunchDate = new System.DateTime(2021, 8, 15),
				EstimatedLifetime = 3,
				BrandId = tcl.Id,
				ExpertGroupId = md.Id,
			};
			Product prod73 = new()
			{
				Id = 74,
				Name = "Fidelio L3",
				Category = "EISA WIRELESS HEADPHONES 2021-2022",
				Price = 651486,
				LaunchDate = new System.DateTime(2021, 8, 17),
				EstimatedLifetime = 9,
				BrandId = philips.Id,
				ExpertGroupId = md.Id,
			};
			Product prod74 = new()
			{
				Id = 75,
				Name = "TONE Free FP8 (DFP8, UFP8, DFP8W, FP8W, UFP8W, FP8E, TFP8E)",
				Category = "EISA IN-EAR HEADPHONES 2021-2022",
				Price = 31611,
				LaunchDate = new System.DateTime(2021, 6, 15),
				EstimatedLifetime = 2,
				BrandId = lg.Id,
				ExpertGroupId = md.Id,
			};
			Product prod75 = new()
			{
				Id = 76,
				Name = "HEAR 2",
				Category = "EISA MOBILE SPEAKER 2021-2022",
				Price = 528699,
				LaunchDate = new System.DateTime(2021, 7, 18),
				EstimatedLifetime = 9,
				BrandId = webyloewe.Id,
				ExpertGroupId = md.Id,
			};
			Product prod76 = new()
			{
				Id = 77,
				Name = "Watch 3 Pro",
				Category = "EISA SMARTWATCH 2021-2022",
				Price = 376891,
				LaunchDate = new System.DateTime(2021, 7, 21),
				EstimatedLifetime = 2,
				BrandId = huawei.Id,
				ExpertGroupId = md.Id,
			};
			Product prod77 = new()
			{
				Id = 78,
				Name = "DMX8020DABS",
				Category = "EISA IN-CAR HEAD UNIT 2021-2022",
				Price = 264335,
				LaunchDate = new System.DateTime(2021, 7, 24),
				EstimatedLifetime = 5,
				BrandId = kenwood.Id,
				ExpertGroupId = ice.Id,
			};
			Product prod78 = new()
			{
				Id = 79,
				Name = "Conductor",
				Category = "EISA IN-CAR INNOVATION 2021-2022",
				Price = 239382,
				LaunchDate = new System.DateTime(2021, 6, 15),
				EstimatedLifetime = 8,
				BrandId = audiotecfischer.Id,
				ExpertGroupId = ice.Id,
			};
			Product prod79 = new()
			{
				Id = 80,
				Name = "Uranium SQ series",
				Category = "EISA IN-CAR SPEAKER SERIES 2021-2022",
				Price = 856676,
				LaunchDate = new System.DateTime(2021, 8, 6),
				EstimatedLifetime = 5,
				BrandId = groundzero.Id,
				ExpertGroupId = ice.Id,
			};
			Product prod80 = new()
			{
				Id = 81,
				Name = "D68SP",
				Category = "EISA IN-CAR SOUND PROCESSOR 2021-2022",
				Price = 57514,
				LaunchDate = new System.DateTime(2021, 6, 26),
				EstimatedLifetime = 6,
				BrandId = esx.Id,
				ExpertGroupId = ice.Id,
			};
			Product prod81 = new()
			{
				Id = 82,
				Name = "ONYX 16, ONYX 80, ONYX 28",
				Category = "EISA IN-CAR SPEAKER SYSTEM 2021-2022",
				Price = 10911,
				LaunchDate = new System.DateTime(2021, 7, 22),
				EstimatedLifetime = 2,
				BrandId = eton.Id,
				ExpertGroupId = ice.Id,
			};
			Product prod82 = new()
			{
				Id = 83,
				Name = "MATCH UP 10DSP",
				Category = "EISA IN-CAR DSP AMPLIFIER 2021-2022",
				Price = 429936,
				LaunchDate = new System.DateTime(2021, 7, 19),
				EstimatedLifetime = 2,
				BrandId = audiotecfischer.Id,
				ExpertGroupId = ice.Id,
			};
			Product prod83 = new()
			{
				Id = 84,
				Name = "Adventure Audio",
				Category = "EISA IN-CAR CAMPER VAN COMPONENT 2021-2022",
				Price = 824230,
				LaunchDate = new System.DateTime(2021, 8, 10),
				EstimatedLifetime = 1,
				BrandId = alpine.Id,
				ExpertGroupId = ice.Id,
			};
			Product prod84 = new()
			{
				Id = 85,
				Name = "TS-WX010A",
				Category = "EISA IN-CAR COMPACT SUBWOOFER 2021-2022",
				Price = 437767,
				LaunchDate = new System.DateTime(2021, 7, 29),
				EstimatedLifetime = 7,
				BrandId = pioneer.Id,
				ExpertGroupId = ice.Id,
			};
			Product prod85 = new()
			{
				Id = 86,
				Name = "Z-E2055",
				Category = "EISA IN-CAR INTEGRATION 2021-2022",
				Price = 98966,
				LaunchDate = new System.DateTime(2021, 7, 20),
				EstimatedLifetime = 6,
				BrandId = zenec.Id,
				ExpertGroupId = ice.Id,
			};
			Product prod86 = new()
			{
				Id = 87,
				Name = "Maserati MC20 with Sonus faber",
				Category = "EISA IN-CAR OEM PREMIUM AUDIO SYSTEM 2021-2022",
				Price = 440351,
				LaunchDate = new System.DateTime(2021, 5, 27),
				EstimatedLifetime = 1,
				BrandId = sonusfaber.Id,
				ExpertGroupId = ice.Id,
			};

			#endregion

			#region Entity.HasData()

			modelBuilder.Entity<ExpertGroup>().HasData(hifi, hta, htdv, ice, md, photo);

			modelBuilder.Entity<Country>().HasData(aus, aut, bel, bgr, can, chn, hrv, cze, dnk, fin, fra, deu, grc, hkg, hun, ind, ita, jpn, kor, nld, nor, pol, prt, rus, srb, svk, esp, swe, che, twn, gbr, ukr, usa);

			modelBuilder.Entity<Brand>().HasData(alpine, apple, audiotecfischer, benq, bluesound, cambridgeaudio, canon, cewe, denon, dxo, epson, esx, eton, ferrum, focal, fujifilm, groundzero, hegel, hifirose, hisense, huawei, jbl, kef, kenwood, laowa, lg, marantz, monitoraudio, nad, naim, nikon, oppo, ortofon, philips, pioneer, polkaudio, project, reavon, rotelmichi, samsung, sennheiser, sigma, sonusfaber, sony, svs, systemaudio, ta, tamron, tcl, thorens, webyloewe, wilsonaudio, xgimi, yamaha, zenec, zoner);

			modelBuilder.Entity<Member>().HasData(member0, member1, member2, member3, member4, member5, member6, member7, member8, member9, member10, member11, member12, member13, member14, member15, member16, member17, member18, member19, member20, member21, member22, member23, member24, member25, member26, member27, member28, member29, member30, member31, member32, member33, member34, member35, member36, member37, member38, member39, member40, member41, member42, member43, member44, member45, member46, member47, member48, member49, member50, member51, member52, member53, member54, member55, member56, member57, member58, member59, member60);

			modelBuilder.Entity<Product>().HasData(prod0, prod1, prod2, prod3, prod4, prod5, prod6, prod7, prod8, prod9, prod10, prod11, prod12, prod13, prod14, prod15, prod16, prod17, prod18, prod19, prod20, prod21, prod22, prod23, prod24, prod25, prod26, prod27, prod28, prod29, prod30, prod31, prod32, prod33, prod34, prod35, prod36, prod37, prod38, prod39, prod40, prod41, prod42, prod43, prod44, prod45, prod46, prod47, prod48, prod49, prod50, prod51, prod52, prod53, prod54, prod55, prod56, prod57, prod58, prod59, prod60, prod61, prod62, prod63, prod64, prod65, prod66, prod67, prod68, prod69, prod70, prod71, prod72, prod73, prod74, prod75, prod76, prod77, prod78, prod79, prod80, prod81, prod82, prod83, prod84, prod85, prod86);

			#endregion
		}
	}
}
