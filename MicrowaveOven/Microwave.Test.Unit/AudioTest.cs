using System.Media;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    public class AudioTest
    {
        private AudioManager _uutNoSound;
        private AudioManager _uutWSound;
        private IOutput _output;
        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _uutNoSound = new AudioManager(_output);
            _uutWSound = new AudioManager(_output, new SoundPlayer(sounds.shortBuzz));
        }

        [Test]
        public void PlayCalled_NoPlayer_OutputDidNotReceive()
        {
            _uutNoSound.Play();
            _output.DidNotReceiveWithAnyArgs().OutputLine(default);
        }

        [Test]
        public void PlayCalled_Player_OutputReceived_played()
        {
            _uutWSound.Play();
            _output.Received(1).OutputLine("A sound is being played!");
        }

        [Test]
        public void StopCalled_NoPlayer_OutputDidNotReceive()
        {
            _uutNoSound.Stop();
            _output.DidNotReceiveWithAnyArgs().OutputLine(default);
        }
        [Test]
        public void StopCalled_Player_OutputReceived_stopped()
        {
            _uutWSound.Stop();
            _output.Received(1).OutputLine("Sound is stopped!");
        }
    }
}