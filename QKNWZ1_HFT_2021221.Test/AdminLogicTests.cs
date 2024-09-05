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
	/// Tests the functionalities of <see cref="AdminLogic"/> and its descendents.
	/// </summary>
	[TestFixture]
	public class AdminLogicTests
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
		/// The method to be called immediately before each <see cref="AdminLogicTests"/> test is run.
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
			//this.brandList = DataSource.GetBrands.ToList();
			//this.countryList = DataSource.GetCountries.ToList();
			//this.egList = DataSource.GetExpertGroups.ToList();
			//this.memberList = DataSource.GetMembers.ToList();
			//this.productList = DataSource.GetProducts.ToList();
			this.brandList = TestDataSource.GetBrandArray().ToList();
			this.countryList = TestDataSource.GetCountryArray().ToList();
			this.egList = TestDataSource.GetExpertGroupArray().ToList();
			this.memberList = TestDataSource.GetMemberArray().ToList();
			this.productList = TestDataSource.GetProductArray().ToList();
		}

		/// <summary>
		/// Testing for the <see cref="AdminLogic.InsertEG(string)"/>.
		/// </summary>
		/// <param name="name"><see cref="ExpertGroup.Name"/> for the object to be inserted.</param>
		[TestCase("New Group")]
		public void TestInsertExpertGroup(string name)
		{
			var id = this.egList.Max(eg => eg.Id) + 1;
			ExpertGroup eg = new() { Id = id, Name = name, };
			this.egRepo.Setup(repo => repo.Create(name))
				.Callback<string>(newEG => this.egList.Add(new ExpertGroup { Name = newEG, Id = id }));

			this.adminLogic.InsertEG(eg.Name);

			Assert.That(eg, Is.EqualTo(this.egList[^1]));
			this.egRepo.Verify(repo => repo.Create(It.IsAny<string>()), Times.Once);
			this.egRepo.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Never);
			this.egRepo.Verify(repo => repo.Update(It.IsAny<ExpertGroup>()), Times.Never);
		}

		[TestCase("", "")]
		public void InsertMemberThrowsException(string eg, string c) => Assert.Throws<System.ArgumentException>(() => this.adminLogic.InsertMember(eg, "", "", "", "", "", c, ""));

		[TestCase("", "")]
		public void InsertProductThrowsException(string eg, string c) => Assert.Throws<System.ArgumentException>(() => this.adminLogic.InsertProduct(eg, "", "", c, 1, System.DateTime.MinValue, 0));

		[TestCase("")]
		public void InsertBrandThrowsException(string country) => Assert.Throws<System.ArgumentException>(() => this.adminLogic.InsertBrand("", "", "", country));

		/// <summary>
		/// Testing for the <see cref="AdminLogic.RemoveProduct(int)"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="Product.Id"/> of the object to remove.</param>
		[TestCase(-1)]
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(3)]
		[TestCase(55)]
		[TestCase(999999)]
		[TestCase(int.MaxValue)]
		public void TestRemoveProduct(int id)
		{
			var remaining = Enumerable.Empty<Product>();
			this.productRepo
				.Setup(repo => repo.ReadAll())
				.Returns(this.productList.AsQueryable);
			this.productRepo
				.Setup(repo => repo.Read(id))
				.Returns(this.productRepo.Object.ReadAll().SingleOrDefault(prod => prod.Id == id));
			this.productRepo
				.Setup(repo => repo.Delete(id))
				.Callback<int>(pKey => remaining = this.productList.Where(prod => prod.Id != pKey));
			var prodToRemove = this.productRepo.Object.Read(id);

			this.adminLogic.RemoveProduct(id);

			Assert.That(remaining, Does.Not.Contain(prodToRemove));
			this.productRepo.Verify(repo => repo.Read(id), Times.Once);
			this.productRepo.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
			this.productRepo.Verify(repo => repo.ReadAll(), Times.Once);
			if (prodToRemove is not null)
			{
				this.productRepo.Verify(repo => repo.Delete(prodToRemove.Id), Times.Once);

			}
		}

		/// <summary>
		/// Testing for the <see cref="AdminLogic.ChangeExpertgroupName(int, string)"/>.
		/// </summary>
		[TestCase(0, "No Name")]
		[TestCase(1, "New Name")]
		public void TestChangingExpertGroupName(int id, string newName)
		{
			ExpertGroup oldEG = this.egList[id];
			ExpertGroup newEG = new();
			this.egRepo.Setup(repo => repo.ChangeName(It.IsAny<int>(), It.IsAny<string>()))
				.Callback<int, string>((pKey, name) => newEG = new ExpertGroup { Id = pKey, Name = name });

			//string newName = "Something else";
			this.adminLogic.ChangeExpertgroupName(oldEG.Id, newName);

			Assert.That(newEG.Id, Is.EqualTo(oldEG.Id));
			Assert.That(newEG.Name, Is.EqualTo(newName));
			this.egRepo.Verify(repo => repo.ChangeName(oldEG.Id, newName), Times.Once);
			this.egRepo.Verify(repo => repo.ChangeName(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
		}

		/// <summary>
		/// (NON-CRUD) Testing for <see cref="AdminLogic.ListBrandsAndMembersAtSameAddress"/>.
		/// </summary>
		[Test]
		public void TestListBrandsAndMembersAtSameAddress()
		{
			this.brandRepo.Setup(repo => repo.ReadAll()).Returns(this.brandList.AsQueryable);
			this.countryRepo.Setup(repo => repo.ReadAll()).Returns(this.countryList.AsQueryable);
			this.memberRepo.Setup(repo => repo.ReadAll()).Returns(this.memberList.AsQueryable);

			var actualResult = this.adminLogic.ListBrandsAndMembersAtSameAddress();
			MemberBrand[] expectedResult =
			{
				new() { Brand = this.brandList[8], Member = this.memberList[27] },
				new() { Brand = this.brandList[10], Member = this.memberList[9] },
				new() { Brand = this.brandList[10], Member = this.memberList[19] },
				new() { Brand = this.brandList[16], Member = this.memberList[9] },
				new() { Brand = this.brandList[16], Member = this.memberList[19] },
				new() { Brand = this.brandList[31], Member = this.memberList[27] },
				new() { Brand = this.brandList[49], Member = this.memberList[9] },
				new() { Brand = this.brandList[49], Member = this.memberList[19] },
			};

			Assert.That(actualResult, Is.EquivalentTo(expectedResult));
			this.brandRepo.Verify(repo => repo.ReadAll(), Times.Once);
			this.countryRepo.Verify(repo => repo.ReadAll(), Times.Never);
			this.memberRepo.Verify(repo => repo.ReadAll(), Times.Once);
		}
	}
}
