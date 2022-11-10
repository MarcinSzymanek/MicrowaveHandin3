using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    public interface IDisplay
    {
        void ShowTime(int minutes, int seconds);
        void ShowPower(int power);
        void Clear();
    }
}
