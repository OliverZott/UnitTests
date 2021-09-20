using NUnit.Framework;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class StackTests
    {

        [Test]
        public void Peek_IfStackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new Fundamentals.Stack<object>();
            var object1 = new object();

            stack.Push(object1);
            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(object1));
        }

        [Test]
        public void Peek_WhenCalled_ReturnsElement()
        {
            var stack = new Fundamentals.Stack<object>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }


        [Test]
        public void Push_ArgumentIsNull_ThrowsArgumentNullException()
        {
            var stack = new Fundamentals.Stack<object>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidObject_AddsObjectToCollection()
        {
            var obj = new object();
            var stack = new Fundamentals.Stack<object>();

            stack.Push(obj);

            var result = stack.Peek();

            Assert.That(result, Is.TypeOf<object>());
        }

        [Test]
        public void Pop_IfStackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new Fundamentals.Stack<object>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_WhenCalled_ReturnsCorrectCount()
        {
            var stack = new Fundamentals.Stack<object>();

            var object1 = new object();
            var object2 = new object();
            stack.Push(object1);
            stack.Push(object2);

            _ = stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(1));
        }


        [Test]
        public void Pop_WhenCalled_ReturnsCorrectElement()
        {
            var stack = new Fundamentals.Stack<object>();

            var object1 = new object();
            var object2 = new object();
            var object3 = new object();
            stack.Push(object1);
            stack.Push(object2);
            stack.Push(object3);

            _ = stack.Pop();
            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(object2));
        }
    }
}
