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
    }
}