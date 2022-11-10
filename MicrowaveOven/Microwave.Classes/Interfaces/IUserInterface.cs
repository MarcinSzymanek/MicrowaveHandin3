using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    public interface IUserInterface
    {
        void OnPowerPressed(object sender, EventArgs e);
        void OnTimePressed(object sender, EventArgs e);
        void OnStartCancelPressed(object sender, EventArgs e);

        void OnDoorOpened(object sender, EventArgs e);
        void OnDoorClosed(object sender, EventArgs e);

        void CookingIsDone();
    }
}
