using System;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Windows;
using System.Media;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class AudioManager : ISound
    {
    private SoundPlayer? _audioSource;
    private IOutput _output;

        public AudioManager(IOutput output, SoundPlayer player = null)
        {
            _audioSource = player;
            _output = output;
        }

        public void Play()
        {
            if (_audioSource != null)
            {
                _output.OutputLine("A sound is being played!");
                _audioSource.Play();
            }
        }

        public void Stop()
        {
            if (_audioSource != null)
            {
                _output.OutputLine("Sound is stopped!");
                _audioSource.Stop();
            }
        }
        
    }
}