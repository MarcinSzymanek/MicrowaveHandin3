using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Door : IDoor
    {
        public event EventHandler Opened;
        public event EventHandler Closed;

        public void Close()
        {
            Closed?.Invoke(this, System.EventArgs.Empty);
        }

        public void Open()
        {
            Opened?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
