using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class LightTest
    {
        private Light uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();

            uut = new Light(output);
        }

        [Test]
        public void TurnOn_WasOff_CorrectOutput()
        {
            uut.TurnOn();
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn();
            uut.TurnOff();
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOn_WasOn_CorrectOutput()
        {
            uut.TurnOn();
            uut.TurnOn();
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void TurnOff_WasOff_CorrectOutput()
        {
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }



    }
}