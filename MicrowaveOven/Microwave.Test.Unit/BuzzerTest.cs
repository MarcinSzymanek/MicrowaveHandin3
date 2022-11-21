using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    public class BuzzerTest
    {
        private Buzzer _uut;
        private IOutput _output;
        private IAudio _audio;
        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _audio = Substitute.For<IAudio>();
            _uut = new Buzzer(_output, _audio);
        }

        [Test]
        public void Toggle_WasOff_CorrectOutput()
        {
            _uut.Toggle();
            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void Toggle_WasOff_AudioReceived_Play()
        {
            _uut.Toggle();
            _audio.Received(1).Play();
        }

        [Test]
        public void Toggle_WasOn_CorrectOutput()
        {
            _uut.Toggle();
            _uut.Toggle();
            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void Toggle_WasOn_AudioReceived_Stop()
        {
            _uut.Toggle();
            _uut.Toggle();
            _audio.Received(1).Stop();
        }

        [Test]
        public void Stop_CorrectOutput()
        {
            _uut.Stop();
            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("stopped")));
        }
    }
}