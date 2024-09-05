[assembly: System.CLSCompliant(false)]

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
    /// Tests the funcionalities of <see cref="AdminLogic"/> and its descendents.
    /// </summary>
    [TestFixture]
    public class AdminTesting
    {
        private AdminLogic adminLogic;

        private Mock<IBrandRepository> brandRepo;
        private Mock<ICountryRepository> countryRepo;
        private Mock<IExpertGroupRepository> egRepo;
        private Mock<IMemberRepository> memberRepo;
        private Mock<IProductRepository> productRepo;

        private IList<Brand> brandList;
        private IList<Product> productList;
        private IList<ExpertGroup> egList;
        private IList<Country> countryList;
        private IList<Member> memberList;

        /// <summary>
        /// The method to be called immediately before each <see cref="AdminTesting"/> test is run.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.brandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            this.countryRepo = new Mock<ICountryRepository>(MockBehavior.Loose);
            this.egRepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            this.memberRepo = new Mock<IMemberRepository>(MockBehavior.Loose);
            this.productRepo = new Mock<IProductRepository>(MockBehavior.Loose);
            this.adminLogic = new AdminLogic(this.brandRepo.Object, this.countryRepo.Object, this.egRepo.Object, this.memberRepo.Object, this.productRepo.Object);
            this.brandList = DataSource.GetBrands.ToList();
            this.countryList = DataSource.GetCountries.ToList();
            this.egList = DataSource.GetExpertGroups.ToList();
            this.memberList = DataSource.GetMembers.ToList();
            this.productList = DataSource.GetProducts.ToList();
        }

        /// <summary>
        /// Testing for the <see cref="AdminLogic.InsertEG(string)"/>.
        /// </summary>
        [Test]
        public void TestInsertExpertGroup()
        {
            int random = DataSource.Rnd.Next(this.egList.Max(eg => eg.Id) + 1, int.MaxValue);
            ExpertGroup eg = new () { Id = random, Name = "Developers", };
            this.egRepo.Setup(repo => repo.Insert(It.IsAny<ExpertGroup>())).Callback<ExpertGroup>(newEG => this.egList.Add(new ExpertGroup { Name = newEG.Name, Id = random }));

            this.adminLogic.InsertEG(eg.Name);

            Assert.That(eg, Is.EqualTo(this.egList[^1]));
            this.egRepo.Verify(repo => repo.Insert(It.IsAny<ExpertGroup>()), Times.Once);
            this.egRepo.Verify(repo => repo.Remove(It.IsAny<ExpertGroup>()), Times.Never);
            this.egRepo.Verify(repo => repo.Update(It.IsAny<ExpertGroup>()), Times.Never);
        }

        /// <summary>
        /// Testing for the <see cref="AdminLogic.RemoveProduct(int)"/>.
        /// </summary>
        [Test]
        public void TestRemoveProduct()
        {
            this.productRepo.Setup(repo => repo.GetAll()).Returns(this.productList.AsQueryable);
            IEnumerable<Product> remaining = Enumerable.Empty<Product>();
            this.productRepo.Setup(repo => repo.Remove(It.IsAny<int>())).Callback<int>(ogArg => remaining = this.productList.Where(prod => prod.Id != ogArg));
            Product prodToRemove = this.productList[DataSource.Rnd.Next(1, this.productList.Count)];

            this.adminLogic.RemoveProduct(prodToRemove.Id);

            Assert.That(remaining, Does.Not.Contain(prodToRemove));
            this.productRepo.Verify(repo => repo.Remove(prodToRemove.Id), Times.Once);
            this.productRepo.Verify(repo => repo.Remove(It.IsAny<int>()), Times.Once);
            this.productRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing for the <see cref="AdminLogic.ChangeEGName(int, string)"/>.
        /// </summary>
        [Test]
        public void TestChangeEGName()
        {
            ExpertGroup oldEG = this.egList.ElementAt(DataSource.Rnd.Next(1, this.egList.Count));
            ExpertGroup newEG = new ();
            this.egRepo.Setup(repo => repo.ChangeName(It.IsAny<int>(), It.IsAny<string>())).Callback<int, string>((id, name) => newEG = new ExpertGroup { Id = id, Name = name });

            string newName = "Something else";
            this.adminLogic.ChangeEGName(oldEG.Id, newName);

            Assert.That(newEG.Id, Is.EqualTo(oldEG.Id));
            Assert.That(newEG.Name, Is.EqualTo(newName));
            this.egRepo.Verify(repo => repo.ChangeName(oldEG.Id, newName), Times.Once);
            this.egRepo.Verify(repo => repo.ChangeName(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="AdminLogic.ListBrandsAndMembersAtSameAdress"/>.
        /// </summary>
        [Test]
        public void TestListBrandsAndMembersAtSameAdress()
        {
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(this.brandList.AsQueryable);
            this.countryRepo.Setup(repo => repo.GetAll()).Returns(this.countryList.AsQueryable);
            this.memberRepo.Setup(repo => repo.GetAll()).Returns(this.memberList.AsQueryable);

            IEnumerable<MemberBrand> actualResult = this.adminLogic.ListBrandsAndMembersAtSameAdress();
            MemberBrand[] expectedResult = new MemberBrand[]
            {
                new MemberBrand { Brand = this.brandList[3], Member = this.memberList[4] },
                new MemberBrand { Brand = this.brandList[4], Member = this.memberList[4] },
                new MemberBrand { Brand = this.brandList[8], Member = this.memberList[13] },
                new MemberBrand { Brand = this.brandList[9], Member = this.memberList[13] },
                new MemberBrand { Brand = this.brandList[10], Member = this.memberList[13] },
            };

            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
