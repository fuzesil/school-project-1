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
	/// Tests the funcionalities of <see cref="ExternalAuditLogic"/> methods.
	/// </summary>
	[TestFixture]
	public class ExternalLogicTests
	{
		private ExternalAuditLogic externalLogic;

		private Mock<IBrandRepository> brandRepo;
		private Mock<IProductRepository> productRepo;
		private Mock<IExpertGroupRepository> egRepo;

		private IList<Brand> brandList;
		private IList<Product> productList;
		private IList<ExpertGroup> egList;

		/// <summary>
		/// The method to be called immediately before each <see cref="ExternalLogicTests"/> test is run.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			this.brandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
			this.productRepo = new Mock<IProductRepository>(MockBehavior.Loose);
			this.egRepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
			this.externalLogic = new ExternalAuditLogic(this.brandRepo.Object, this.productRepo.Object, this.egRepo.Object);
			// this.brandList = DataSource.GetBrands.ToList();
			// this.egList = DataSource.GetExpertGroups.ToList();
			// this.productList = DataSource.GetProducts.ToList();
			this.brandList = TestDataSource.GetBrandArray().ToList();
			this.egList = TestDataSource.GetExpertGroupArray().ToList();
			this.productList = TestDataSource.GetProductArray().ToList();
		}

		/// <summary>
		/// Testing for the ExternalLogic.ListAllEntities method.
		/// </summary>
		[Test]
		public void TestReadAll()
		{
			this.brandRepo.Setup(repo => repo.ReadAll()).Returns(this.brandList.AsQueryable);
			this.egRepo.Setup(repo => repo.ReadAll()).Returns(this.egList.AsQueryable);
			this.productRepo.Setup(repo => repo.ReadAll()).Returns(this.productList.AsQueryable);
			var firstProduct = this.productList[0];
			var lastEG = this.egList[^1];

			_ = this.externalLogic.ListAllBrands(out var countAllBrands);
			var allEGs = this.externalLogic.ListAllExpertgroups(out var countAllEGs).ToList();
			var allProducts = this.externalLogic.ListAllProducts(out _);

			Assert.That(countAllBrands, Is.EqualTo(this.brandList.Count));
			Assert.That(this.egList, Is.EquivalentTo(allEGs));
			Assert.That(allProducts.ElementAt(0), Is.EqualTo(firstProduct));
			Assert.That(allEGs.ElementAt(countAllEGs - 1), Is.EqualTo(lastEG));
			this.brandRepo.Verify(repo => repo.ReadAll(), Times.Once);
			this.egRepo.Verify(repo => repo.ReadAll(), Times.Once);
			this.productRepo.Verify(repo => repo.ReadAll(), Times.Once);
			this.brandRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Never);
			this.egRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Never);
			this.productRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Never);
		}

		/// <summary>
		/// Testing the return value of <see cref="ExternalAuditLogic.GetOneBrand(int)"/>
		/// as well as the <see cref="ExternalAuditLogic.GetOneBrand(string)"/> method.
		/// </summary>
		/// <param name="id">Any value for a <see cref="Brand.Id"/>.</param>
		/// <param name="name">Any value for a <see cref="Brand.Name"/>.</param>
		[TestCase(int.MinValue, "")]
		[TestCase(-1, " ")]
		[TestCase(0, "nothing")]
		[TestCase(1, "Arcam")]
		[TestCase(17, "Focal")]
		[TestCase(int.MaxValue / 2, "qwertzuiop")]
		public void TestReadBrandReturnsValidOrNull(int id, string name)
		{
			this.brandRepo
				.Setup(repo => repo.ReadAll())
				.Returns(this.brandList.AsQueryable);
			this.brandRepo
				.Setup(repo => repo.Read(id))
				.Returns<int>(idArg => this.brandRepo.Object.ReadAll().SingleOrDefault(brand => brand.Id == idArg));
			this.brandRepo
				.Setup(repo => repo.Read(name))
				.Returns<string>(nameArg => this.brandRepo.Object.ReadAll().FirstOrDefault(brand => brand.Name == nameArg));

			var resultBrandById = this.externalLogic.GetOneBrand(id);
			var resultBrandByName = this.externalLogic.GetOneBrand(name);

			Assert.That(resultBrandById, Is.EqualTo(this.brandList.SingleOrDefault(brand => brand.Id == id)));
			Assert.That(resultBrandByName, Is.EqualTo(this.brandList.FirstOrDefault(brandItem => brandItem.Name == name)));
			Assert.That(resultBrandById, Is.EqualTo(resultBrandByName));

			this.brandRepo.Verify(repo => repo.Read(id), Times.Once);
			this.brandRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Once);
			this.brandRepo.Verify(repo => repo.Read(name), Times.Once);
			this.brandRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Once);
			this.brandRepo.Verify(repo => repo.ReadAll(), Times.Exactly(2));
		}

		/// <summary>
		/// Testing the return value of <see cref="ExternalAuditLogic.GetOneExpertGroup(int)"/> as well as the
		/// <see cref="ExternalAuditLogic.GetOneExpertGroup(string)"/> method.
		/// </summary>
		/// <param name="id">Any value for a <see cref="ExpertGroup.Id"/>.</param>
		/// <param name="name">Any value for a <see cref="ExpertGroup.Name"/>.</param>
		[TestCase(int.MinValue, "")]
		[TestCase(-1, " ")]
		[TestCase(1, "Hi-Fi")]
		[TestCase(3, "Home Theatre Display & Video")]
		[TestCase(int.MaxValue / 2, "qwertzuiop")]
		public void TestReadExpertgroupReturnsValidOrNull(int id, string name)
		{
			this.egRepo
				.Setup(repo => repo.ReadAll())
				.Returns(this.egList.AsQueryable);
			this.egRepo
				.Setup(repo => repo.Read(id))
				.Returns<int>(idArg => this.egRepo.Object.ReadAll().SingleOrDefault(eg => eg.Id == idArg));
			this.egRepo
				.Setup(repo => repo.Read(name))
				.Returns<string>(nameArg => this.egRepo.Object.ReadAll().FirstOrDefault(eg => eg.Name == nameArg));

			var resultEGById = this.externalLogic.GetOneExpertGroup(id);
			var resultEGByName = this.externalLogic.GetOneExpertGroup(name);

			Assert.That(resultEGById, Is.EqualTo(this.egList.SingleOrDefault(eg => eg.Id == id)));
			Assert.That(resultEGByName, Is.EqualTo(this.egList.FirstOrDefault(eg => eg.Name == name)));
			Assert.That(resultEGById, Is.EqualTo(resultEGByName));

			this.egRepo.Verify(repo => repo.Read(id), Times.Once);
			this.egRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Once);
			this.egRepo.Verify(repo => repo.Read(name), Times.Once);
			this.egRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Once);
			this.egRepo.Verify(repo => repo.ReadAll(), Times.Exactly(2));
		}

		/// <summary>
		/// Testing the return value of <see cref="ExternalAuditLogic.GetOneProduct(int)"/>
		/// as well as the <see cref="ExternalAuditLogic.GetOneExpertGroup(string)"/> method.
		/// </summary>
		/// <param name="id">Any value for a <see cref="Product.Id"/>.</param>
		/// <param name="name">Any value for a <see cref="Product.Name"/>.</param>
		[TestCase(int.MinValue, "")]
		[TestCase(-2, " ")]
		[TestCase(0, "nothing")]
		[TestCase(2, "Prime Pinnacle")]
		[TestCase(61, "EOS-1D X Mark III")]
		[TestCase(int.MaxValue / 2, "qwertzuiop")]
		public void TestReadProductReturnsValidOrNull(int id, string name)
		{
			this.productRepo
				.Setup(repo => repo.ReadAll())
				.Returns(this.productList.AsQueryable);
			this.productRepo
				.Setup(repo => repo.Read(id))
				.Returns<int>(idArg => this.productRepo.Object.ReadAll().SingleOrDefault(prod => prod.Id == idArg));
			this.productRepo
				.Setup(repo => repo.Read(name))
				.Returns<string>(nameArg => this.productRepo.Object.ReadAll().FirstOrDefault(prod => prod.Name == nameArg));

			var resultProductById = this.externalLogic.GetOneProduct(id);
			var resultProductByName = this.externalLogic.GetOneProduct(name);

			Assert.That(resultProductById, Is.EqualTo(this.productList.SingleOrDefault(prod => prod.Id == id)));
			Assert.That(resultProductByName, Is.EqualTo(this.productList.FirstOrDefault(prod => prod.Name == name)));
			Assert.That(resultProductById, Is.EqualTo(resultProductByName));

			this.productRepo.Verify(repo => repo.Read(id), Times.Once);
			this.productRepo.Verify(repo => repo.Read(It.IsAny<int>()), Times.Once);
			this.productRepo.Verify(repo => repo.Read(name), Times.Once);
			this.productRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Once);
			this.productRepo.Verify(repo => repo.ReadAll(), Times.Exactly(2));
		}

		/*
		/// <summary>
		/// Testing whether exception is thrown in <see cref="ExternalAuditLogic"/><c>.ReadOneX(int, string)</c> if the argument is invalid.
		/// </summary>
		/// <param name="id">A negative number.</param>
		/// <param name="name">Another negative number.</param>
		[TestCase(int.MinValue / 2, int.MinValue / 4)]
		[TestCase(-9, -1)]
		public void TestReadThrowsException(int id, string name)
		{
			this.brandRepo.Setup(repo => repo.Read(id))
				.Returns(this.brandList.ElementAtOrDefault(id));
			this.egRepo.Setup(repo => repo.Read(id))
				.Returns(this.egList.ElementAtOrDefault(id));
			this.productRepo.Setup(repo => repo.Read(id))
				.Returns(this.productList.ElementAtOrDefault(id));

			System.InvalidOperationException invalidOpEx = Assert.Throws<System.InvalidOperationException>(() => this.externalLogic.ReadProduct(index));
			var msg = "Neither the ID nor the Name was valid.";
			System.Exception ex = Assert.Catch<System.InvalidOperationException>(() => this.externalLogic.GetOneProduct(id));
			Assert.That(msg, Is.EqualTo(ex.Message));
			Assert.Throws<System.InvalidOperationException>(() => this.externalLogic.GetOneBrand(id), msg);
			Assert.Throws(Is.TypeOf<System.InvalidOperationException>().And.Message.EqualTo(msg), () => this.externalLogic.GetOneExpertGroup(id));

			this.brandRepo.Verify(repo => repo.ReadAll(), Times.Never);
			this.egRepo.Verify(repo => repo.ReadAll(), Times.Never);
			this.productRepo.Verify(repo => repo.ReadAll(), Times.Never);
		}
		*/

		/// <summary>
		/// (NON-CRUD) Testing for <see cref="ExternalAuditLogic.ListTopBrands"/>.
		/// </summary>
		[Test]
		public void TestListTopBrands()
		{
			this.brandRepo.Setup(repo => repo.ReadAll()).Returns(this.brandList.AsQueryable);
			this.egRepo.Setup(repo => repo.ReadAll()).Returns(this.egList.AsQueryable);
			this.productRepo.Setup(repo => repo.ReadAll()).Returns(this.productList.AsQueryable);
			this.externalLogic.TopN = 2;
			var results = this.externalLogic.ListTopBrands();
			var expectedResults = new BrandAndNumber[]
			{
				new() { Brand = this.brandList[26], Number = 4, },
				new() { Brand = this.brandList[6], Number = 2, },
				new() { Brand = this.brandList[7], Number = 2, },
				new() { Brand = this.brandList[15], Number = 2, },
				new() { Brand = this.brandList[17], Number = 2, },
				new() { Brand = this.brandList[22], Number = 2, },
				new() { Brand = this.brandList[25], Number = 2, },
				new() { Brand = this.brandList[31], Number = 2, },
				new() { Brand = this.brandList[35], Number = 2, },
				new() { Brand = this.brandList[37], Number = 2, },
				new() { Brand = this.brandList[39], Number = 2, },
				new() { Brand = this.brandList[41], Number = 2, },
				new() { Brand = this.brandList[42], Number = 2, },
				new() { Brand = this.brandList[44], Number = 2, },
			};

			Assert.That(results, Is.EquivalentTo(expectedResults));
			this.productRepo.Verify(repo => repo.ReadAll(), Times.Exactly(2));
			this.brandRepo.Verify(repo => repo.ReadAll(), Times.Once);
			this.egRepo.Verify(repo => repo.ReadAll(), Times.Never);
		}

		/// <summary>
		/// (NON-CRUD) Testing for <see cref="ExternalAuditLogic.GetMaxPriceProdInEveryEG"/>.
		/// </summary>
		[Test]
		public void TestGetMaxPriceProdInEveryEG()
		{
			this.brandRepo.Setup(repo => repo.ReadAll()).Returns(this.brandList.AsQueryable);
			this.egRepo.Setup(repo => repo.ReadAll()).Returns(this.egList.AsQueryable);
			this.productRepo.Setup(repo => repo.ReadAll()).Returns(this.productList.AsQueryable);

			var result = this.externalLogic.GetMaxPriceProdInEveryEG();
			var expectedResult = new ExpertgroupProduct[]
			{
				new() { ExpertGroup = this.egList[0], Product = this.productList[11], },
				new() { ExpertGroup = this.egList[1], Product = this.productList[28], },
				new() { ExpertGroup = this.egList[2], Product = this.productList[33], },
				new() { ExpertGroup = this.egList[3], Product = this.productList[45], },
				new() { ExpertGroup = this.egList[4], Product = this.productList[51], },
				new() { ExpertGroup = this.egList[5], Product = this.productList[62], },
			};

			Assert.That(result, Is.EquivalentTo(expectedResult));
			this.productRepo.Verify(repo => repo.ReadAll(), Times.Exactly(2));
			this.egRepo.Verify(repo => repo.ReadAll(), Times.Once);
			this.brandRepo.Verify(repo => repo.ReadAll(), Times.Never);
		}
	}
}
