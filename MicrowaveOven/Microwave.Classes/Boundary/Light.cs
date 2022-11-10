using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Light : ILight
    {
        private IOutput myOutput;
        private bool isOn = false;
        
        public Light(IOutput output)
        {
            myOutput = output;
        }

        public void TurnOn()
        {
            if (!isOn)
            {
                myOutput.OutputLine("Light is turned on");
                isOn = true;
            }
        }

        public void TurnOff()
        {
            if (isOn)
            {
                myOutput.OutputLine("Light is turned off");
                isOn = false;
            }
        }

    }
}