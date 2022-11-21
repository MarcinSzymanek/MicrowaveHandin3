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

        public PowerTube(IOutput output)
        {
            myOutput = output;
        }
        enum PowerSetting
        {
            Low = 500,
            Medium = 800,
            High = 1000,
        }

        static void Main(string[] args)
        {
            PowerSetting PowerSettingLow = PowerSetting.Low;
            Console.WriteLine(PowerSettingLow);

            PowerSetting PowerSettingMedium = PowerSetting.Medium;
            Console.WriteLine(PowerSettingMedium);

            PowerSetting PowerSettingHigh = PowerSetting.High;
            Console.WriteLine(PowerSettingHigh);


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
            if (IsOn)
            {
                throw new InvalidOperationException("PowerTube.TurnOn: is already on");
            }

            IsOn = true;
            myOutput.OutputLine($"PowerTube works with {power}");
        }
    }
}
    
  
            
            



            
      