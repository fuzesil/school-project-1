using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using QKNWZ1_HFT_2021221.Logic;
using QKNWZ1_HFT_2021221.Models;
using QKNWZ1_HFT_2021221.Repository;

namespace QKNWZ1_HFT_2021221.Test
{
	/// <summary>
	/// Tests the functionalities of <see cref="InternalAuditLogic"/> methods.
	/// </summary>
	[TestFixture]
	public class InternalLogicTesting
	{
		private InternalAuditLogic internalLogic;

		private Mock<ICountryRepository> countryRepo;
		private Mock<IMemberRepository> memberRepo;
		private Mock<IExpertGroupRepository> egRepo;

		private IList<ExpertGroup> egList;
		private IList<Country> countryList;
		private IList<Member> memberList;

		/// <summary>
		/// The method to be called immediately before each <see cref="InternalLogicTesting"/> test is run.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			this.countryRepo = new Mock<ICountryRepository>(MockBehavior.Loose);
			this.memberRepo = new Mock<IMemberRepository>(MockBehavior.Loose);
			this.egRepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
			this.internalLogic = new InternalAuditLogic(this.memberRepo.Object, this.countryRepo.Object, this.egRepo.Object);

			// this.countryList = DataSource.GetCountries.ToList();
			// this.egList = DataSource.GetExpertGroups.ToList();
			// this.memberList = DataSource.GetMembers.ToList();
			// this.productList = DataSource.GetProducts.ToList();
			this.countryList = TestDataSource.GetCountryArray().ToList();
			this.egList = TestDataSource.GetExpertGroupArray().ToList();
			this.memberList = TestDataSource.GetMemberArray().ToList();
		}

		/// <summary>
		/// Testing the return value of <see cref="InternalAuditLogic.GetOneCountry(int, string)"/> method.
		/// </summary>
		/// <param name="id">Any integer for <see cref="Country.Id"/>.</param>
		/// <param name="name">Any string for <see cref="Country.Name"/>.</param>
		[TestCase(int.MinValue, "")]
		[TestCase(-1, " ")]
		[TestCase(0, "HUN")]
		[TestCase(1, "Aus")]
		[TestCase(31, "UK")]
		[TestCase(int.MaxValue, "qwertzuiop")]
		public void TestGetOneCountryReturnsValidOrNull(int id, string name)
		{
			this.countryRepo
				.Setup(repo => repo.ReadAll())
				.Returns(this.countryList.AsQueryable);
			this.countryRepo
				.Setup(repo => repo.Read(id))
				.Returns<int>(idArg => this.countryRepo.Object.ReadAll().SingleOrDefault(cntry => cntry.Id == idArg));
			this.countryRepo
				.Setup(repo => repo.Read(name))
				.Returns<string>(nameArg => this.countryRepo.Object.ReadAll().FirstOrDefault(country => country.Name.Contains(nameArg)));

			Country resultById = this.internalLogic.GetOneCountry(id);
			Country resultByName = this.internalLogic.GetOneCountry(name);

			if (id > 0)
			{
				Assert.That(resultById, Is.EqualTo(this.countryList.SingleOrDefault(cntry => cntry.Id == id)));
				this.countryRepo.Verify(repo => repo.Read(id), Times.Once);
				this.countryRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Once);
			}
			else if (!string.IsNullOrWhiteSpace(name))
			{
				Assert.That(resultByName, Is.EqualTo(this.countryList.FirstOrDefault(cntry => cntry.Name.Contains(name))));
				this.countryRepo.Verify(repo => repo.Read(name), Times.Once);
				this.countryRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Once);
			}
			else
			{
				this.countryRepo.Verify(repo => repo.ReadAll(), Times.Never);
			}

			this.egRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Never);
			this.egRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
			this.egRepo.Verify(repo => repo.ReadAll(), Times.Never);
			this.memberRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
			this.memberRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Never);
			this.memberRepo.Verify(repo => repo.ReadAll(), Times.Never);
		}

		/*
		/// <summary>
		/// Testing whether exception is thrown in <see cref="InternalAuditLogic"/><c>.GetOneX(int, string)</c> if the argument is invalid.
		/// </summary>
		/// <param name="n1">A negative number.</param>
		/// <param name="n2">Another negative number.</param>
		[TestCase(int.MinValue / 2, null)]
		[TestCase(-9, " ")]
		[TestCase(0, "")]
		[TestCase(1, " ")]
		public void TestGetOneThrowsException(int id, string name)
		{
			this.countryRepo.Setup(repo => repo.Read(id))
				.Returns(this.countryList.ElementAtOrDefault(id));
			this.egRepo.Setup(repo => repo.Read(id))
				.Returns(this.egList.ElementAtOrDefault(id));
			this.memberRepo.Setup(repo => repo.Read(id))
				.Returns(this.memberList.ElementAtOrDefault(id));

			this.countryRepo.Setup(repo => repo.Read(name))
				.Returns(this.countryList.SingleOrDefault(country => country.Name.Contains(name)));
			this.egRepo.Setup(repo => repo.Read(id))
				.Returns(this.egList.SingleOrDefault(eg => eg.Name.Contains(name)));
			this.memberRepo.Setup(repo => repo.Read(id))
				.Returns(this.memberList.SingleOrDefault(member => member.Name.Contains(name)));

			System.InvalidOperationException invalidOpEx = Assert.Throws<System.InvalidOperationException>(() => this.myLogic.GetOneSomething(index));
			string msg = "Neither the ID nor the Name was valid.";
			System.Exception ex = Assert.Catch(typeof(System.InvalidOperationException), () => this.internalLogic.GetOneCountry(index));
			Assert.AreEqual(msg, ex.Message);
			Assert.Throws<System.InvalidOperationException>(() => this.internalLogic.GetOneMember(index), msg);
			Assert.Throws(Is.TypeOf<System.InvalidOperationException>().And.Message.EqualTo(msg), () => this.internalLogic.GetOneExpertGroup(index));

			this.countryRepo.Verify(repo => repo.ReadAll(), Times.Never);
			this.egRepo.Verify(repo => repo.ReadAll(), Times.Never);
			this.memberRepo.Verify(repo => repo.ReadAll(), Times.Never);
		}
		*/

		/// <summary>
		/// Testing for the <see cref="InternalAuditLogic"/>.<c>ListAll...()</c> methods.
		/// </summary>
		[Test]
		public void TestGetAll()
		{
			this.countryRepo.Setup(repo => repo.ReadAll()).Returns(this.countryList.AsQueryable);
			this.memberRepo.Setup(repo => repo.ReadAll()).Returns(this.memberList.AsQueryable);
			this.egRepo.Setup(repo => repo.ReadAll()).Returns(this.egList.AsQueryable);
			var firstMember = this.memberList.ElementAt(0);
			var lastCountry = this.countryList[^1];

			var allCountries = this.internalLogic.ListAllCountries(out int countAllCountries);
			var allEGs = this.internalLogic.ListAllExpertgroups(out _);
			var allMembers = this.internalLogic.ListAllMembers(out _);

			Assert.That(countAllCountries, Is.EqualTo(this.countryList.Count));
			Assert.That(allCountries.ElementAt(countAllCountries - 1), Is.EqualTo(lastCountry));
			Assert.That(allEGs, Is.EquivalentTo(this.egList));
			Assert.That(allMembers.ElementAt(0), Is.EqualTo(firstMember));
			this.countryRepo.Verify(repo => repo.ReadAll(), Times.Once);
			this.egRepo.Verify(repo => repo.ReadAll(), Times.Once);
			this.memberRepo.Verify(repo => repo.ReadAll(), Times.Once);
			this.countryRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Never);
			this.egRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Never);
			this.memberRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Never);
		}

		/// <summary>
		/// (NON-CRUD) Testing for <see cref="InternalAuditLogic.ListMembersInCapitalCity(bool)"/>.
		/// </summary>
		[Test]
		public void TestListMembersInCapitalCity()
		{
			this.countryRepo.Setup(repo => repo.ReadAll()).Returns(this.countryList.AsQueryable);
			this.memberRepo.Setup(repo => repo.ReadAll()).Returns(this.memberList.AsQueryable);

			var membersInCaps = this.internalLogic.ListMembersInCapitalCity(true);
			var expectedIn = new MemberCountry[]
			{
				new() { Member = this.memberList[0], Country = this.countryList[21],},
				new() { Member = this.memberList[1], Country = this.countryList[22],},
				new() { Member = this.memberList[2], Country = this.countryList[16],},
				new() { Member = this.memberList[6], Country = this.countryList[24],},
				new() { Member = this.memberList[7], Country = this.countryList[9],},
				new() { Member = this.memberList[9], Country = this.countryList[10],},
				new() { Member = this.memberList[11], Country = this.countryList[31],},
				new() { Member = this.memberList[12], Country = this.countryList[14],},
				new() { Member = this.memberList[14], Country = this.countryList[16],},
				new() { Member = this.memberList[13], Country = this.countryList[22],},
				new() { Member = this.memberList[19], Country = this.countryList[10],},
				new() { Member = this.memberList[20], Country = this.countryList[26],},
				new() { Member = this.memberList[23], Country = this.countryList[7],},
				new() { Member = this.memberList[24], Country = this.countryList[14],},
				new() { Member = this.memberList[25], Country = this.countryList[21],},
				new() { Member = this.memberList[26], Country = this.countryList[22],},
				new() { Member = this.memberList[27], Country = this.countryList[17],},
				new() { Member = this.memberList[28], Country = this.countryList[16],},
				new() { Member = this.memberList[33], Country = this.countryList[24],},
				new() { Member = this.memberList[35], Country = this.countryList[9],},
				new() { Member = this.memberList[38], Country = this.countryList[4],},
				new() { Member = this.memberList[40], Country = this.countryList[7],},
				new() { Member = this.memberList[43], Country = this.countryList[14],},
				new() { Member = this.memberList[45], Country = this.countryList[23],},
				new() { Member = this.memberList[46], Country = this.countryList[16],},
				new() { Member = this.memberList[50], Country = this.countryList[31],},
				new() { Member = this.memberList[52], Country = this.countryList[6],},
				new() { Member = this.memberList[55], Country = this.countryList[24],},
				new() { Member = this.memberList[56], Country = this.countryList[3],},
				new() { Member = this.memberList[54], Country = this.countryList[8],},
				new() { Member = this.memberList[59], Country = this.countryList[7 ],},
				new() { Member = this.memberList[57], Country = this.countryList[26],},
				new() { Member = this.memberList[60], Country = this.countryList[25],},
			};
			var membersOutOfCaps = this.internalLogic.ListMembersInCapitalCity(false);
			var expectedOut = this.memberList.Join(this.countryList,
					member => member.CountryId,
					country => country.Id,
					(member, country) => new MemberCountry { Member = member, Country = country, })
				.Except(expectedIn);

			Assert.That(membersInCaps, Is.EquivalentTo(expectedIn));
			Assert.That(membersOutOfCaps, Is.EquivalentTo(expectedOut));
			this.countryRepo.Verify(repo => repo.ReadAll(), Times.Exactly(2));
			this.memberRepo.Verify(repo => repo.ReadAll(), Times.Exactly(2));
		}

		/// <summary>
		/// (NON-CRUD) Testing for <see cref="InternalAuditLogic.GetRichestMemberInExpertGroup"/>.
		/// </summary>
		[Test]
		public void TestGetRichestMemberInExpertGroup()
		{
			this.countryRepo.Setup(repo => repo.ReadAll()).Returns(this.countryList.AsQueryable);
			this.memberRepo.Setup(repo => repo.ReadAll()).Returns(this.memberList.AsQueryable);
			this.egRepo.Setup(repo => repo.ReadAll()).Returns(this.egList.AsQueryable);

			var results = this.internalLogic.GetRichestMemberInExpertGroup();
			var expectedResult = new ExpertgroupMemberCountry[]
			{
				new() { ExpertGroup = this.egList[0], Member = this.memberList[41], Country = this.countryList[20] },
				new() { ExpertGroup = this.egList[1], Member = this.memberList[10], Country = this.countryList[32] },
				new() { ExpertGroup = this.egList[2], Member = this.memberList[17], Country = this.countryList[11] },
				new() { ExpertGroup = this.egList[3], Member = this.memberList[49], Country = this.countryList[11] },
				new() { ExpertGroup = this.egList[4], Member = this.memberList[53], Country = this.countryList[19] },
			};

			Assert.That(results, Is.EquivalentTo(expectedResult));
			this.countryRepo.Verify(repo => repo.ReadAll(), Times.Exactly(2));
			this.memberRepo.Verify(repo => repo.ReadAll(), Times.Exactly(2));
			this.egRepo.Verify(repo => repo.ReadAll(), Times.Once);
		}
	}
}
