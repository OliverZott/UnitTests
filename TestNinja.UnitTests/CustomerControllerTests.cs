using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class CustomerControllerTests
    {
        private CustomerController _customerController;

        [SetUp]
        public void SetUp()
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void GetCustomer_IfIdIsZero_ReturnNotFoundAction()
        {
            var result = _customerController.GetCustomer(0);

            // NotFound
            Assert.That(result, Is.TypeOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IfIdIsNotZero_ReturnOkAction()
        {
            var result = _customerController.GetCustomer(5);

            // NotFound or one of its derivatives
            Assert.That(result, Is.InstanceOf<ActionResult>());
        }

    }
}
