using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput myOutput;
        private bool isOn = false;
        
        public Buzzer(IOutput output)
        {
            myOutput = output;
        }

        public void Toggle()
        {
            if (!isOn)
            {
                myOutput.OutputLine("Buzzer is on");
                isOn = true;
            }
            else
            {
                myOutput.OutputLine("Buzzer is off");
                isOn = false;
            }
        }

        // Makes sure that buzzer is off at the end
        public void Stop()
        {
            myOutput.OutputLine("Buzzer stopped");
            isOn = false;
        }
        

    }
}