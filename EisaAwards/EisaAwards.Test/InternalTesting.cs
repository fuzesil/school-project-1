namespace EisaAwards.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using EisaAwards.Data;
    using EisaAwards.Logic;
    using EisaAwards.Repository;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the funcionalities of <see cref="InternalLogic"/> and its descendents.
    /// </summary>
    [TestFixture]
    public class InternalTesting
    {
        private InternalAuditLogic internalLogic;

        private Mock<ICountryRepository> countryRepo;
        private Mock<IMemberRepository> memberRepo;
        private Mock<IExpertGroupRepository> egRepo;

        private IList<ExpertGroup> egList;
        private IList<Country> countryList;
        private IList<Member> memberList;

        /// <summary>
        /// The method to be called immediately before each <see cref="InternalTesting"/> test is run.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.countryRepo = new Mock<ICountryRepository>(MockBehavior.Loose);
            this.memberRepo = new Mock<IMemberRepository>(MockBehavior.Loose);
            this.egRepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            this.internalLogic = new InternalAuditLogic(2, this.memberRepo.Object, this.countryRepo.Object, this.egRepo.Object);
            this.egList = DataSource.GetExpertGroups.ToList();
            this.countryList = DataSource.GetCountries.ToList();
            this.memberList = DataSource.GetMembers.ToList();
        }

        /// <summary>
        /// Testing the return value of <see cref="InternalLogic"/><c>.GetOneX(int, string)</c> method.
        /// </summary>
        /// <param name="id">Any positive integer.</param>
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(int.MaxValue / 2)]
        public void TestGetOneReturnsValidOrNull(int id)
        {
            this.countryRepo.Setup(repo => repo.GetOne(id)).Returns<int>(idArg => this.countryList.SingleOrDefault(cntry => cntry.Id == idArg));
            this.egRepo.Setup(repo => repo.GetOne(id)).Returns<int>(idArg => this.egList.SingleOrDefault(eg => eg.Id == idArg));
            this.memberRepo.Setup(repo => repo.GetOne(id)).Returns<int>(idArg => this.memberList.SingleOrDefault(mmbr => mmbr.Id == idArg));

            Country resultCountry = this.internalLogic.GetOneCountry(id);
            Member resultMember = this.internalLogic.GetOneMember(id);
            ExpertGroup resultEG = this.internalLogic.GetOneExpertGroup(id);

            Assert.AreEqual(resultEG, this.egList.SingleOrDefault(eg => eg.Id == id));
            Assert.AreEqual(resultCountry, this.countryList.SingleOrDefault(cntry => cntry.Id == id));
            Assert.AreEqual(resultMember, this.memberList.SingleOrDefault(mmbr => mmbr.Id == id));
            this.countryRepo.Verify(repo => repo.GetOne(id), Times.Once);
            this.countryRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Once);
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Never);
            this.egRepo.Verify(repo => repo.GetOne(id), Times.Once);
            this.egRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Once);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Never);
            this.memberRepo.Verify(repo => repo.GetOne(id), Times.Once);
            this.memberRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Once);
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing whether exception is thrown in <see cref="InternalLogic"/><c>.GetOneX(int, string)</c> if the argument is invalid.
        /// </summary>
        /// <param name="n1">A negtive number.</param>
        /// <param name="n2">Another negtive number.</param>
        [TestCase(int.MinValue / 2, int.MinValue / 4)]
        [TestCase(-9, -1)]
        public void TestGetOneThrowsException(int n1, int n2)
        {
            int index = DataSource.Rnd.Next(n1, n2);
            this.countryRepo.Setup(repo => repo.GetOne(index)).Returns(this.countryList.ElementAtOrDefault(index));
            this.egRepo.Setup(repo => repo.GetOne(index)).Returns(this.egList.ElementAtOrDefault(index));
            this.memberRepo.Setup(repo => repo.GetOne(index)).Returns(this.memberList.ElementAtOrDefault(index));

            // System.InvalidOperationException invalopex = Assert.Throws<System.InvalidOperationException>(() => this.myLogic.GetOneSomething(index));
            string msg = "Neither the ID nor the Name was valid.";
            System.Exception ex = Assert.Catch(typeof(System.InvalidOperationException), () => this.internalLogic.GetOneCountry(index));
            Assert.AreEqual(msg, ex.Message);
            Assert.Throws<System.InvalidOperationException>(() => this.internalLogic.GetOneMember(index), msg);
            Assert.Throws(Is.TypeOf<System.InvalidOperationException>().And.Message.EqualTo(msg), () => this.internalLogic.GetOneExpertGroup(index));

            this.countryRepo.Verify(repo => repo.GetAll(), Times.Never);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Never);
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing for the <see cref="InternalLogic"/> ListAll() methods.
        /// </summary>
        [Test]
        public void TestGetAll()
        {
            this.countryRepo.Setup(repo => repo.GetAll()).Returns(this.countryList.AsQueryable);
            this.memberRepo.Setup(repo => repo.GetAll()).Returns(this.memberList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);
            Member firstMember = this.memberList.ElementAt(0);
            Country lastCountry = this.countryList[^1];

            IEnumerable<Country> allCountries = this.internalLogic.ListAllCountries(out int countAllCountries);
            IEnumerable<ExpertGroup> allEGs = this.internalLogic.ListAllExpertgroups(out _);
            IEnumerable<Member> allMembers = this.internalLogic.ListAllMembers(out _);

            Assert.That(countAllCountries, Is.EqualTo(this.countryList.Count));
            Assert.That(allCountries.ElementAt(countAllCountries - 1), Is.EqualTo(lastCountry));
            Assert.That(allEGs, Is.EquivalentTo(this.egList));
            Assert.That(allMembers.ElementAt(0), Is.EqualTo(firstMember));
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.countryRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            this.egRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            this.memberRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="InternalAuditLogic.ListMembersInCapitalCity(bool)"/>.
        /// </summary>
        [Test]
        public void TestListMembersInCapitalCity()
        {
            this.countryRepo.Setup(repo => repo.GetAll()).Returns(this.countryList.AsQueryable);
            this.memberRepo.Setup(repo => repo.GetAll()).Returns(this.memberList.AsQueryable);

            IEnumerable<MemberCountry> membersInCaps = this.internalLogic.ListMembersInCapitalCity(true);
            MemberCountry[] expectedIn = new MemberCountry[]
            {
                new MemberCountry { Member = this.memberList[2], Country = this.countryList[2] },
                new MemberCountry { Member = this.memberList[3], Country = this.countryList[2] },
                new MemberCountry { Member = this.memberList[4], Country = this.countryList[4] },
                new MemberCountry { Member = this.memberList[8], Country = this.countryList[6] },
                new MemberCountry { Member = this.memberList[9], Country = this.countryList[6] },
                new MemberCountry { Member = this.memberList[10], Country = this.countryList[6] },
                new MemberCountry { Member = this.memberList[11], Country = this.countryList[7] },
                new MemberCountry { Member = this.memberList[12], Country = this.countryList[7] },
                new MemberCountry { Member = this.memberList[13], Country = this.countryList[8] },
                new MemberCountry { Member = this.memberList[14], Country = this.countryList[10] },
            };
            IEnumerable<MemberCountry> membersOutOfCaps = this.internalLogic.ListMembersInCapitalCity(false);
            MemberCountry[] expectedOut = new MemberCountry[]
            {
                new MemberCountry { Member = this.memberList[0], Country = this.countryList[0] },
                new MemberCountry { Member = this.memberList[1], Country = this.countryList[1] },
                new MemberCountry { Member = this.memberList[5], Country = this.countryList[5] },
                new MemberCountry { Member = this.memberList[6], Country = this.countryList[5] },
                new MemberCountry { Member = this.memberList[7], Country = this.countryList[5] },
                new MemberCountry { Member = this.memberList[15], Country = this.countryList[11] },
                new MemberCountry { Member = this.memberList[16], Country = this.countryList[8] },
            };

            Assert.That(membersInCaps, Is.EquivalentTo(expectedIn));
            Assert.That(membersOutOfCaps, Is.EquivalentTo(expectedOut));
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="InternalAuditLogic.GetRichestMemberInExpertGroup"/>.
        /// </summary>
        [Test]
        public void TestGetRichestMemberInExpertGroup()
        {
            this.countryRepo.Setup(repo => repo.GetAll()).Returns(this.countryList.AsQueryable);
            this.memberRepo.Setup(repo => repo.GetAll()).Returns(this.memberList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);

            IEnumerable<ExpertgroupMemberCountry> results = this.internalLogic.GetRichestMemberInExpertGroup();
            ExpertgroupMemberCountry[] expectedResult = new ExpertgroupMemberCountry[]
            {
                new ExpertgroupMemberCountry { Expertgroup = this.egList[0], Member = this.memberList[7], Country = this.countryList[5] },
                new ExpertgroupMemberCountry { Expertgroup = this.egList[1], Member = this.memberList[15], Country = this.countryList[11] },
                new ExpertgroupMemberCountry { Expertgroup = this.egList[2], Member = this.memberList[6], Country = this.countryList[5] },
                /* no members in Photography group */
            };

            Assert.That(results, Is.EquivalentTo(expectedResult));
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.egRepo.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}