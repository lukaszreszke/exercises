using Moq;
using NUnit.Framework;
using Tests.Order;

namespace Orders
{
    public class OrderTests
    {
        private IRebatePolicy _rebatePolicy;
        private ITaxCalculator _taxCalculator;
        private Order _sut;
        private Guid _productId;
        private Product _product;


        [OneTimeSetUp]
        public void Initialize()
        {
            var zeroMoney = GetMoneyMock(0).Object;

            var taxCalculatorMock = new Mock<ITaxCalculator>();
            taxCalculatorMock.Setup(x => x.calculateTax(It.IsAny<List<Product>>())).Returns(zeroMoney);

            _rebatePolicy = GetRebatePolicy(0);
            _taxCalculator = taxCalculatorMock.Object;

            var productMock = new Mock<Product>();
            _productId = Guid.NewGuid();
            productMock.Setup(x => x.GetPrice()).Returns(GetMoneyMock(0).Object);
            productMock.Setup(x => x.ProductId).Returns(_productId);
            _product = productMock.Object;

            _sut = new(Guid.NewGuid(), new ClientId(), _rebatePolicy, _taxCalculator);
            _sut.AddProduct(_product);
        }

        [SetUp]
        public void BeforeTest()
        {
            if (_sut.GetStatus() == OrderStatus.SUBMITTED)
                _sut = new(Guid.NewGuid(), new ClientId(), _rebatePolicy, _taxCalculator);
        }

        [Test]
        public void NewOrder_GetStatus_Returns_Draft()
        {
            // Act
            var result = _sut.GetStatus();

            // Assert
            Assert.AreEqual(result, OrderStatus.DRAFT);
        }

        [Test]
        public void DraftOrder_AddProduct_IncreasesGetProductsCountAndContainsProduct()
        {
            // Act
            _sut.AddProduct(_product);

            // Assert
            Assert.True(_sut.GetProducts().Count == 1);
            Assert.Contains(_productId, _sut.GetProducts().Select(x => x.ProductId).ToList());
        }

        [Test]
        public void AddProduct_ThrowsException_WhenOrderHasSubmittedStatus()
        {
            // Arrange
            _sut.Submit();

            // Act && Assert
            Assert.Throws<Exception>(() => _sut.AddProduct(_product));
        }


        [Test]
        public void TestTotalPrice()
        {
            var productMock = new Mock<Product>();
            productMock.Setup(x => x.GetPrice()).Returns(GetMoneyMock(100).Object);

            _sut.AddProduct(productMock.Object);

            Assert.AreEqual(100, _sut.GetTotalPrice().Value);
        }

        [Test]
        public void TestTotalPrice_IncludeTax()
        {
            var productMock = new Mock<Product>();
            productMock.Setup(x => x.GetPrice()).Returns(GetMoneyMock(100).Object);
            var tenPercentTax = GetMoneyMock(10).Object;

            var taxCalculatorMock = new Mock<ITaxCalculator>();
            taxCalculatorMock.Setup(x => x.calculateTax(It.IsAny<List<Product>>())).Returns(tenPercentTax);
            _sut.SetTaxCalc(taxCalculatorMock.Object);
            _sut.AddProduct(productMock.Object);

            Assert.AreEqual(298, _sut.GetTotalPrice().Value);
        }

        [Test]
        public void TestTotalPrice_DoesntChangeProductPrice()
        {
            var productMock = new Mock<Product>();
            productMock.Setup(x => x.GetPrice()).Returns(GetMoneyMock(100).Object);
            var tenPercentTax = GetMoneyMock(10).Object;
            var taxCalculatorMock = new Mock<ITaxCalculator>();
            taxCalculatorMock.Setup(x => x.calculateTax(It.IsAny<List<Product>>())).Returns(tenPercentTax);
            _sut.SetTaxCalc(taxCalculatorMock.Object);
            _sut.SetRebatePolicy(GetRebatePolicy(12));
            _sut.AddProduct(productMock.Object);

            var result = _sut.GetProducts().First(x => x.ProductId == Guid.Empty);

            Assert.AreEqual(100, result.GetPrice().Value);
            Assert.AreEqual(_sut.GetTotalPrice().Value, 198);
        }

        [Test]
        public void TestTotalPrice_SubmittedStatus()
        {
            var productMock = new Mock<Product>();
            productMock.Setup(x => x.GetPrice()).Returns(GetMoneyMock(100).Object);
            var tenPercentTax = GetMoneyMock(10).Object;
            var taxCalculatorMock = new Mock<ITaxCalculator>();
            taxCalculatorMock.Setup(x => x.calculateTax(It.IsAny<List<Product>>())).Returns(tenPercentTax);
            _sut.SetTaxCalc(taxCalculatorMock.Object);
            _sut.SetRebatePolicy(GetRebatePolicy(12));
            _sut.AddProduct(productMock.Object);

            _sut.Submit();

            Assert.AreEqual(OrderStatus.SUBMITTED, _sut.GetStatus());
            Assert.AreEqual(398, _sut.GetTotalPrice().Value);
        }

        private static Mock<Money> GetMoneyMock(decimal value)
        {
            var moneyMock = new Mock<Money>(value);
            moneyMock.Setup(x => x.GetValue()).Returns(value);
            moneyMock.Setup(x => x.Value).Returns(value);
            moneyMock.Setup(x => x.GetCurrencyCode()).Returns("EUR");
            return moneyMock;
        }

        private static IRebatePolicy GetRebatePolicy(decimal rebateValue)
        {
            var tenPercentDiscount = new Mock<Money>(rebateValue);
            tenPercentDiscount.Setup(x => x.Value).Returns(rebateValue);
            tenPercentDiscount.Setup(x => x.GetCurrencyCode()).Returns("EUR");
            tenPercentDiscount.Setup(x => x.GetValue()).Returns(rebateValue);

            var rebateMock = new Mock<IRebatePolicy>();
            rebateMock.Setup(x => x.calculateRebate(It.IsAny<List<Product>>())).Returns(tenPercentDiscount.Object);
            return rebateMock.Object;
        }
    }
}