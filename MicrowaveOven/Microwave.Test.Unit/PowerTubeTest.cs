using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class PowerTubeTest
    {
        private PowerTube uut;
        private IOutput output;
      
        

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(699)]
        [TestCase(700)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
        {
            uut = new PowerTube(output, 700);
            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(701)]
        [TestCase(750)]
        public void TurnOn_WasOffOutOfRangePower_ThrowsException(int power)
        {
            
            uut = new PowerTube(output, 700);
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut = new PowerTube(output, 700);
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut = new PowerTube(output, 700);
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            uut = new PowerTube(output, 700);
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }

        [Test]
        public void TurnOn_WasOff_CorrectOutput()
        {
            uut = new PowerTube(output, 700);
            uut.TurnOn(50);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void TurnOn_WasOff_CorrectPower()
        {
            uut = new PowerTube(output, 700);
            uut.TurnOn(50);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }
            
        [Test]
        public void GetMaxPower_wasCorrect()
        {
            uut = new PowerTube(output, 700);
            Assert.That(uut.GetMaxPower, Is.EqualTo(700));
        }


    }
}