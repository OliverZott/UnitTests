using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class ProductTests
    {

        [Test]
        public void GetPrice_CustomerIsGold_ReturnsCheaperPrice()
        {
            var product = new Product { ListPrice = 100 };
            var customer = new Customer { IsGold = true };

            var result = product.GetPrice(customer);

            Assert.That(result, Is.EqualTo(product.ListPrice));
        }

        // Bad exmaple --> Mocking abuse
        [Test]
        public void GetPrice_CustomerIsGold_ReturnsCheaperPrice2()
        {
            var customer = new Mock<ICustomer>();
            customer.Setup(c => c.IsGold).Returns(true);

            var product = new Product { ListPrice = 100 };

            var result = product.GetPrice(customer.Object);

            Assert.That(result, Is.EqualTo(product.ListPrice));
        }

        [Test]
        public void GetPrice_CustomerIsNótGold_ReturnsFullPrice()
        {
        }
    }
}
