using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class DisplayTest
    {
        private Display uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            uut = new Display(output);
        }

        [Test]
        public void ShowTime_ZeroMinuteZeroSeconds_CorrectOutput()
        {
            uut.ShowTime(0,0);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:00")));
        }

        [Test]
        public void ShowTime_ZeroMinuteSomeSecond_CorrectOutput()
        {
            uut.ShowTime(0, 5);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:05")));
        }

        [Test]
        public void ShowTime_SomeMinuteZeroSecond_CorrectOutput()
        {
            uut.ShowTime(5, 0);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("05:00")));
        }

        [Test]
        public void ShowTime_SomeMinuteSomeSecond_CorrectOutput()
        {
            uut.ShowTime(10, 15);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("10:15")));
        }

        [Test]
        public void ShowPower_Zero_CorrectOutput()
        {
            uut.ShowPower(0);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("0 W")));
        }

        [Test]
        public void ShowPower_NotZero_CorrectOutput()
        {
            uut.ShowPower(150);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("150 W")));
        }

        [Test]
        public void Clear_CorrectOutput()
        {
            uut.Clear();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }

    }
}