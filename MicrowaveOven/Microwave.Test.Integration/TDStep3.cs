using System;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep3
    {
        private Door door;
        private Button powerButton;
        private Button timeButton;
        private Button startCancelButton;

        private UserInterface ui;

        private Light light;
        private Display display;
        private CookController cooker;

        private PowerTube powerTube;
        private Timer timer;

        private IOutput output;

        [SetUp]
        public void Setup()
        {
            door = new Door();
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();

            output = Substitute.For<IOutput>();

            light = new Light(output);
            display = new Display(output);
            powerTube = new PowerTube(output);
            timer = new Timer();


            cooker = new CookController(timer, display, powerTube);

            ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);
            cooker.UI = ui;
        }

        #region CookControler_PowerTube

        [Test]
        public void CookController_PowerTube_TurnOn_50W()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            // Should now be started with 50W
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50") && str.Contains("PowerTube works")));
        }

        [Test]
        public void CookController_PowerTube_TurnOn_150W()
        {
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            // Should now be started with 150W
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("150") && str.Contains("PowerTube works")));
        }

        [Test]
        public void CookController_PowerTube_TurnOn_700W()
        {
            for (int p = 50; p <= 700; p += 50)
            {
                powerButton.Press();
            }
            timeButton.Press();
            startCancelButton.Press();

            // Should now be started with 700W
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("700") && str.Contains("PowerTube works")));
        }

        [Test]
        public void CookController_PowerTube_TurnOff()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            // Now we force stopping
            startCancelButton.Press();

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }
        #endregion

        #region CookController_Display

        [Test]
        public void CookController_Display_ShowTime_0_58_RealTimer()
        {
            // Starting up with 50 W and 1 minute
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            // Timer is included, so we can wait for 2 time tick
            Thread.Sleep(2050);  // Wait for at least two tick

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("00:58")));
        }

        [Test]
        public void CookController_Display_ShowTime_0_59_RealTimer()
        {
            // Starting up with 50 W and 1 minute
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            // Timer is included, so we can wait for 1 time tick, or we can 
            Thread.Sleep(1050);  // Wait for at least one tick

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("00:59")));
        }

        [Test]
        public void CookController_Display_ShowTime_0_59_FakedTimer()
        {
            // We can stub the timer, and simulate a tick

            // This should hide the problem
            // But shows the big setup for just this
            door = new Door();
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();

            output = Substitute.For<IOutput>();

            light = new Light(output);
            display = new Display(output);
            powerTube = new PowerTube(output);
            var faketimer = Substitute.For<ITimer>();

            // Make a new cooker, with the 
            cooker = new CookController(faketimer, display, powerTube);
            // Then we must make a new UI
            ui = new UserInterface(
                powerButton, timeButton, startCancelButton,
                door, display, light, cooker);
            // And make the association
            cooker.UI = ui;

            // Set the fake timer
            faketimer.TimeRemaining.Returns(59);

            // Starting up with 50 W and 1 minute
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            faketimer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("00:59")));
        }

        #endregion

        #region CookController_Timer

        [Test]
        public void CookController_Timer_Start_1min()
        {
            // Starting up with 50 W and 1 minute
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            Thread.Sleep(60500);  // Wait for a minute

            output.Received(60).OutputLine(Arg.Is<string>(str => str.Contains("00:")));
        }

        [Test]
        public void CookController_Timer_LongEnough()
        {
            // Starting up with 50 W and 1 minute
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            Thread.Sleep(59900);  // Wait a little less than a minute

            // End of cooking is indicated by ligth being turned off
            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));

        }


        [Test]
        public void CookController_Timer_Stop()
        {
            // Starting up with 50 W and 1 minute
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            // Stop again
            startCancelButton.Press();

            // Timer should be stopped, so we shouldn't rece
            // timer ticks
            Thread.Sleep(1050);

            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("00:")));
        }

        #endregion

        #region CookController_UserInterface

        [Test]
        public void CookController_UserInterface_CookingIsDone()
        {
            // Starting up with 50 W and 1 minute
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            Thread.Sleep(60500);  // Wait for a minute

            // End of cooking is indicated by ligth being turned off
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));

        }

        #endregion

    }
}