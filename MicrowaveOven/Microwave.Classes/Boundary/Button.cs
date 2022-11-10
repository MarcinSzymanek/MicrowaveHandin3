using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Button : IButton
    {
        public event EventHandler Pressed;

        public void Press()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
