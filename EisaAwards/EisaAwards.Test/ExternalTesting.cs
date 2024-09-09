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
    /// Tests the funcionalities of <see cref="ExternalLogic"/> and its descendants.
    /// </summary>
    [TestFixture]
    public class ExternalTesting
    {
        private ExternalAuditLogic externalLogic;

        private Mock<IBrandRepository> brandRepo;
        private Mock<IProductRepository> productRepo;
        private Mock<IExpertGroupRepository> egRepo;

        private IList<Brand> brandList;
        private IList<Product> productList;
        private IList<ExpertGroup> egList;

        /// <summary>
        /// The method to be called immediately before each <see cref="ExternalTesting"/> test is run.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.brandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            this.productRepo = new Mock<IProductRepository>(MockBehavior.Loose);
            this.egRepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            this.externalLogic = new ExternalAuditLogic(2, this.brandRepo.Object, this.productRepo.Object, this.egRepo.Object);
            this.brandList = DataSource.GetBrands.ToList();
            this.productList = DataSource.GetProducts.ToList();
            this.egList = DataSource.GetExpertGroups.ToList();
        }

        /// <summary>
        /// Testing for the ExternalLogic.ListAllEntities method.
        /// </summary>
        [Test]
        public void TestGetAll()
        {
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(this.brandList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);
            this.productRepo.Setup(repo => repo.GetAll()).Returns(this.productList.AsQueryable);
            Product firstProduct = this.productList[0];
            ExpertGroup lastEG = this.egList[^1];

            IEnumerable<Brand> allBrands = this.externalLogic.ListAllBrands(out int countAllBrands);
            IEnumerable<ExpertGroup> allEGs = this.externalLogic.ListAllExpertgroups(out int countAllEGs);
            IEnumerable<Product> allProducts = this.externalLogic.ListAllProducts(out _);

            Assert.That(countAllBrands, Is.EqualTo(this.brandList.Count));
            Assert.That(this.egList, Is.EquivalentTo(allEGs));
            Assert.That(allProducts.ElementAt(0), Is.EqualTo(firstProduct));
            Assert.That(allEGs.ElementAt(countAllEGs - 1), Is.EqualTo(lastEG));
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.productRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.brandRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            this.egRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            this.productRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Testing the return value of <see cref="ExternalLogic.GetOneBrand(int, string)"/> method.
        /// </summary>
        /// <param name="id">Any positive integer.</param>
        /// <param name="name">Any of the names in the data set.</param>
        [TestCase(1, "NAD")]
        [TestCase(5, "Focal")]
        [TestCase(int.MaxValue / 2, "qwertzuiop")]
        public void TestGetOneBrandReturnsValidOrNull(int id, string name)
        {
            this.brandRepo.Setup(repo => repo.GetOne(id))
                .Returns<int>(idArg => this.brandList.SingleOrDefault(brand => brand.BrandId == idArg));
            this.brandRepo.Setup(repo => repo.GetOne(name))
                .Returns<string>(nameArg => this.brandList.FirstOrDefault(brand => brand.Name == nameArg));

            Brand resultBrandById = this.externalLogic.GetOneBrand(id);
            Brand resultBrandByName = this.externalLogic.GetOneBrand(It.Is<int>(num => num < 1), name);

            Assert.AreEqual(resultBrandById, this.brandList.SingleOrDefault(brand => brand.BrandId == id));
            Assert.AreEqual(resultBrandByName, this.brandList.FirstOrDefault(brandItem => brandItem.Name == name));
            Assert.AreEqual(resultBrandById, resultBrandByName);
            this.brandRepo.Verify(repo => repo.GetOne(id), Times.Once);
            this.brandRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Once);
            this.brandRepo.Verify(repo => repo.GetOne(name), Times.Once);
            this.brandRepo.Verify(repo => repo.GetOne(It.IsAny<string>()), Times.Once);
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing the return value of <see cref="ExternalLogic.GetOneExpertGroup(int, string)"/> method.
        /// </summary>
        /// <param name="id">Any positive integer.</param>
        /// <param name="name">Any of the names in the data set.</param>
        [TestCase(1, "Hi-Fi")]
        [TestCase(3, "Home Theatre Display & Video")]
        [TestCase(int.MaxValue / 2, "qwertzuiop")]
        public void TestGetOneExpertgroupReturnsValidOrNull(int id, string name)
        {
            this.egRepo.Setup(repo => repo.GetOne(id))
                .Returns<int>(idArg => this.egList.SingleOrDefault(eg => eg.ExpertGroupID == idArg));
            this.egRepo.Setup(repo => repo.GetOne(name))
                .Returns<string>(nameArg => this.egList.FirstOrDefault(eg => eg.Name == nameArg));

            ExpertGroup resultEGById = this.externalLogic.GetOneExpertGroup(id);
            ExpertGroup resultEGByName = this.externalLogic.GetOneExpertGroup(It.Is<int>(num => num < 1), name);

            Assert.AreEqual(resultEGById, this.egList.SingleOrDefault(eg => eg.ExpertGroupID == id));
            Assert.AreEqual(resultEGByName, this.egList.FirstOrDefault(eg => eg.Name == name));
            Assert.AreEqual(resultEGById, resultEGByName);
            this.egRepo.Verify(repo => repo.GetOne(id), Times.Once);
            this.egRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Once);
            this.egRepo.Verify(repo => repo.GetOne(name), Times.Once);
            this.egRepo.Verify(repo => repo.GetOne(It.IsAny<string>()), Times.Once);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing the return value of <see cref="ExternalLogic.GetOneProduct(int, string)"/> method.
        /// </summary>
        /// <param name="id">Any positive integer.</param>
        /// <param name="name">Any of the names in the data set.</param>
        [TestCase(2, "EOS-1D X Mark III")]
        [TestCase(6, "X100V")]
        [TestCase(int.MaxValue / 2, "qwertzuiop")]
        public void TestGetOneProductReturnsValidOrNull(int id, string name)
        {
            this.productRepo.Setup(repo => repo.GetOne(id))
                .Returns<int>(idArg => this.productList.SingleOrDefault(prod => prod.ProductID == idArg));
            this.productRepo.Setup(repo => repo.GetOne(name))
                .Returns<string>(nameArg => this.productList.FirstOrDefault(prod => prod.Name == nameArg));

            Product resultProductById = this.externalLogic.GetOneProduct(id);
            Product resultProductByName = this.externalLogic.GetOneProduct(It.Is<int>(num => num < 1), name);

            Assert.AreEqual(resultProductById, this.productList.SingleOrDefault(prod => prod.ProductID == id));
            Assert.AreEqual(resultProductByName, this.productList.FirstOrDefault(prod => prod.Name == name));
            Assert.AreEqual(resultProductById, resultProductByName);
            this.productRepo.Verify(repo => repo.GetOne(id), Times.Once);
            this.productRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Once);
            this.productRepo.Verify(repo => repo.GetOne(name), Times.Once);
            this.productRepo.Verify(repo => repo.GetOne(It.IsAny<string>()), Times.Once);
            this.productRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing whether exception is thrown in <see cref="ExternalLogic"/><c>.GetOneX(int, string)</c> if the argument is invalid.
        /// </summary>
        /// <param name="n1">A negtive number.</param>
        /// <param name="n2">Another negtive number.</param>
        [TestCase(int.MinValue / 2, int.MinValue / 4)]
        [TestCase(-9, -1)]
        public void TestGetOneThrowsException(int n1, int n2)
        {
            int index = DataSource.Rnd.Next(n1, n2);
            this.brandRepo.Setup(repo => repo.GetOne(index)).Returns(this.brandList.ElementAtOrDefault(index));
            this.egRepo.Setup(repo => repo.GetOne(index)).Returns(this.egList.ElementAtOrDefault(index));
            this.productRepo.Setup(repo => repo.GetOne(index)).Returns(this.productList.ElementAtOrDefault(index));

            // System.InvalidOperationException invalopex = Assert.Throws<System.InvalidOperationException>(() => this.externalLogic.GetOneProduct(index));
            string msg = "Neither the ID nor the Name was valid.";
            System.Exception ex = Assert.Catch(typeof(System.InvalidOperationException), () => this.externalLogic.GetOneProduct(index));
            Assert.AreEqual(msg, ex.Message);
            Assert.Throws<System.InvalidOperationException>(() => this.externalLogic.GetOneBrand(index), msg);
            Assert.Throws(Is.TypeOf<System.InvalidOperationException>().And.Message.EqualTo(msg), () => this.externalLogic.GetOneExpertGroup(index));

            this.brandRepo.Verify(repo => repo.GetAll(), Times.Never);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Never);
            this.productRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="ExternalAuditLogic.ListTopBrands"/>.
        /// </summary>
        [Test]
        public void TestListTopBrands()
        {
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(this.brandList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);
            this.productRepo.Setup(repo => repo.GetAll()).Returns(this.productList.AsQueryable);

            IEnumerable<BrandAndNumber> results = this.externalLogic.ListTopBrands();
            BrandAndNumber[] expectedResults = new BrandAndNumber[]
            {
                new BrandAndNumber { Brand = this.brandList[11], Number = 3 },
                new BrandAndNumber { Brand = this.brandList[3], Number = 2 },
            };

            Assert.That(results, Is.EquivalentTo(expectedResults));
            this.productRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="ExternalAuditLogic.GetMaxPriceProdInEveryEG"/>.
        /// </summary>
        [Test]
        public void TestGetMaxPriceProdInEveryEG()
        {
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(this.brandList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);
            this.productRepo.Setup(repo => repo.GetAll()).Returns(this.productList.AsQueryable);

            IEnumerable<ExpertgroupProduct> result = this.externalLogic.GetMaxPriceProdInEveryEG();
            ExpertgroupProduct[] expectedResult = new ExpertgroupProduct[]
            {
                new ExpertgroupProduct { ExpertGroup = this.egList[0], Product = this.productList[2] },
                new ExpertgroupProduct { ExpertGroup = this.egList[1], Product = this.productList[0] },
                new ExpertgroupProduct { ExpertGroup = this.egList[2], Product = this.productList[7] },
                new ExpertgroupProduct { ExpertGroup = this.egList[3], Product = this.productList[1] },
            };

            Assert.That(result, Is.EquivalentTo(expectedResult));
            this.productRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.egRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Never);
        }
    }
}
