using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        private int _MaxValue = 700;


        public PowerTube(IOutput output)
        {
        }

        public PowerTube(IOutput output, int MaxValue) : this(output)
        {
            ChangeMaxValue(MaxValue);
            myOutput = output;
        }

        public Output Output { get; }

        public void ChangeMaxValue(int MaxValue)
        {
            if (MaxValue < 300 || 1000 < MaxValue)
                throw new ArgumentOutOfRangeException("MaxValue", "MaxValue must be between 500 and 1000");
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || power > _MaxValue)
            {
                throw new ArgumentOutOfRangeException("power", $"Must be between 1 and {_MaxValue} (incl.)");
            }

            if (IsOn)
            {
                throw new InvalidOperationException("PowerTube.TurnOn: is already on");
            }

            IsOn = true;
            myOutput.OutputLine($"PowerTube works with {power}");
        }
    }
}
    
  
            
            



            
      