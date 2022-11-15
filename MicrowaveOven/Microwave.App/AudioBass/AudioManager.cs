using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Windows;

namespace Microwave.App.AudioBass
{
    public class AudioManager
    {
        private System.Media.SoundPlayer _player;
        private readonly string ClipsFolder = "C:\\AuProjects\\Semester 4\\SoftwareTest\\MicrowaveAssignment\\MicrowaveHandin3\\MicrowaveOven\\External\\Clips\\";
        public AudioManager()
        {
            _player = new System.Media.SoundPlayer(ClipsFolder + "shortBuzz.wav");
        }

        public void PlayBuzz()
        {
            _player.Play();
        }
    }


}