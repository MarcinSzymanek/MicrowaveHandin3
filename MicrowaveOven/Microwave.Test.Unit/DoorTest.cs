using Microwave.Classes.Boundary;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class DoorTest
    {
        private Door uut;

        [SetUp]
        public void Setup()
        {
            uut = new Door();    
        }

        [Test]
        public void Open_NoSubscribers_NoThrow()
        {
            // We don't need an assert, as an exception would fail the test case
            uut.Open();
        }

        [Test]
        public void Open_1subscriber_IsNotified()
        {
            bool notified = false;

            uut.Opened += (sender, args) => notified = true;
            uut.Open();
            Assert.That(notified, Is.EqualTo(true));
        }

        [Test]
        public void Close_NoSubscribers_NoThrow()
        {
            // We don't need an assert, as an exception would fail the test case
            uut.Close();
        }

        [Test]
        public void Close_1subscriber_IsNotified()
        {
            bool notified = false;

            uut.Closed += (sender, args) => notified = true;
            uut.Close();
            Assert.That(notified, Is.EqualTo(true));
        }

    }
}