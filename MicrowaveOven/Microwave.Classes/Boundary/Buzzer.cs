using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput myOutput;
        #nullable enable
        private IAudio _soundModule;
        #nullable disable
        private bool isOn = false;
        
        public Buzzer(IOutput output, IAudio soundModule)
        {
            myOutput = output;
            _soundModule = soundModule;
        }

        public void Toggle()
        {
            if (!isOn)
            {
                myOutput.OutputLine("Buzzer is on");
                _soundModule.Play();
                isOn = true;
            }
            else
            {
                myOutput.OutputLine("Buzzer is off");
                _soundModule.Stop();
                isOn = false;
            }
        }

        // Makes sure that buzzer is off at the end
        public void Stop()
        {
            myOutput.OutputLine("Buzzer stopped");
            _soundModule.Stop();
            isOn = false;
        }
        

    }
}