using Microwave.Classes.Boundary;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class ButtonTest
    {
        private Button uut;

        [SetUp]
        public void Setup()
        {
            uut = new Button();
        }

        [Test]
        public void Press_NoSubscribers_NoThrow()
        {
            // We don't need an assert, as an exception would fail the test case
            uut.Press();
        }

        [Test]
        public void Press_1subscriber_IsNotified()
        {
            bool notified = false;

            uut.Pressed += (sender, args) => notified = true;
            uut.Press();
            Assert.That(notified, Is.EqualTo(true));
        }

    }
}