using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    /// <summary>
    /// Interaction-test example. 
    /// 
    /// Ensure that right method gets called with right argument.
    /// </summary>
    [TestFixture]
    internal class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoresTheOrder()
        {
            var order = new Order();
            var storage = new Mock<IStorage>();
            var orderService = new OrderService(storage.Object);

            orderService.PlaceOrder(order);

            storage.Verify(s => s.Store(order));
        }
    }
}
