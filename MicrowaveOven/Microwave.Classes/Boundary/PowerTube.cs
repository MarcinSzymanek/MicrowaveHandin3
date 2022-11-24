using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection.Emit;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        private int _Maxpower;

        enum PowerTubeValue
        {
            low = 500,
            medium = 700,
            high = 1000
        }

        public PowerTube(IOutput output, int power)
        {
            
            myOutput = output;
            if (power == (int)PowerTubeValue.low || power == (int)PowerTubeValue.medium ||
                power == (int)PowerTubeValue.high)
            {
                _Maxpower = power;
                
            }
           
            else
                throw new ArgumentException("Power is not valid");

        }

        public Output Output { get; }

      

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
            if (power > _Maxpower)
                throw new ArgumentOutOfRangeException("Power is not valid");
            if (power < 1)
                throw new ArgumentOutOfRangeException("Power is not valid");
            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            IsOn = true;
            myOutput.OutputLine($"PowerTube is on with {power}");
        }

        public int GetMaxPower()
        {
            return _Maxpower;
        }
    }
}
    
  
            
            



            
      