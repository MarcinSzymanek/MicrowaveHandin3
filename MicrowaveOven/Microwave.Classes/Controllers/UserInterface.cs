using System;
using System.Runtime.Serialization;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class UserInterface : IUserInterface
    {
        private enum States
        {
            READY, SETPOWER, SETTIME, COOKING, DOOROPEN
        }

        private States myState = States.READY;

        private ICookController myCooker;
        private ILight myLight;
        private IDisplay myDisplay;
		private ITimer buzzerTimer;
		private IBuzzer myBuzzer;
        private IPowerTube myPowerTube;

        private int powerLevel = 50;
        private int time = 1;

        public UserInterface(
            IButton powerButton,
            IButton timeButton,
            IButton startCancelButton,
            IDoor door,
            IDisplay display,
            ILight light,
            ITimer timer,
            IBuzzer buzzer,
            ICookController cooker,
            IPowerTube Power
            
            )
        {
            powerButton.Pressed += new EventHandler(OnPowerPressed);
            timeButton.Pressed += new EventHandler(OnTimePressed);
            startCancelButton.Pressed += new EventHandler(OnStartCancelPressed);

            door.Closed += new EventHandler(OnDoorClosed);
            door.Opened += new EventHandler(OnDoorOpened);

            myCooker = cooker;
            myLight = light;
            myDisplay = display;
            myBuzzer = buzzer;
            buzzerTimer = timer;
            buzzerTimer.TimerTick += new EventHandler(OnBuzzerTimerEvent);
            buzzerTimer.Expired += new EventHandler(OnBuzzerTimerExpired);

            myPowerTube = Power;
        }

        private void SetBuzzer()
        {
            myBuzzer.Toggle();
            buzzerTimer.Start(5); // turns off after 1 sec. turns on after 2. turns off after 3 etc.
        }

        private void OnBuzzerTimerEvent(object sender, EventArgs e)
        {
            myBuzzer.Toggle();
        }
        private void OnBuzzerTimerExpired(object sender, EventArgs e)
        {
            myBuzzer.Stop();
            buzzerTimer.Stop();
        }

        private void ResetValues()
        {
            powerLevel = 50;
            time = 1;
        }

        public void OnPowerPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myDisplay.ShowPower(powerLevel);
                    myState = States.SETPOWER;
                    break;
                case States.SETPOWER:
                    powerLevel = (powerLevel >= myPowerTube.GetMaxPower() ? 50 : powerLevel+50);
                    myDisplay.ShowPower(powerLevel);
                    break;
            }
        }

        public void OnTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    myDisplay.ShowTime(time, 0);
                    myState = States.SETTIME;
                    break;
                case States.SETTIME:
                    time += 1;
                    myDisplay.ShowTime(time, 0);
                    break;
                case States.COOKING:
                    myCooker.ExtendTime();
                    break;
            }
        }

        public void OnStartCancelPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    ResetValues();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
                case States.SETTIME:
                    myLight.TurnOn();
                    myCooker.StartCooking(powerLevel, time*60);
                    myState = States.COOKING;
                    break;
                case States.COOKING:
                    ResetValues();
                    myCooker.Stop();
                    myLight.TurnOff();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
            }
        }

        public void OnDoorOpened(object sender, EventArgs e)
        {
            if (buzzerTimer.TimeRemaining > 0)
            {
                myBuzzer.Stop();
                buzzerTimer.Stop();
            }
            switch (myState)
            {
                case States.READY:
                    myLight.TurnOn();
                    myState = States.DOOROPEN;
                    break;
                case States.SETPOWER:
                    ResetValues();
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.SETTIME:
                    ResetValues();
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.COOKING:
                    myCooker.Stop();
                    myDisplay.Clear();
                    ResetValues();
                    myState = States.DOOROPEN;
                    break;
            }
        }

        public void OnDoorClosed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.DOOROPEN:
                    myLight.TurnOff();
                    myState = States.READY;
                    break;
            }
        }

        public void CookingIsDone()
        {
            switch (myState)
            {
                case States.COOKING:
                    ResetValues();
                    myDisplay.Clear();
                    myLight.TurnOff();
                    SetBuzzer();
                    myState = States.READY;
                    break;
            }
        }
    }
}