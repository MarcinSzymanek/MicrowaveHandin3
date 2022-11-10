using System;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class BUStep2
    {
        private IOutput output;

        private Timer timer;
        private Display display;
        private PowerTube powerTube;
        private CookController cooker;

        private UserInterface ui;
        private Light light;

        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;

        private IDoor door;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();

            powerButton = Substitute.For<IButton>();
            timeButton = Substitute.For<IButton>();
            startCancelButton = Substitute.For<IButton>();

            door = Substitute.For<IDoor>();

            timer = new Timer();
            display = new Display(output);
            powerTube = new PowerTube(output);

            light = new Light(output);

            cooker = new CookController(timer, display, powerTube);


            ui = new UserInterface(
                powerButton, timeButton, startCancelButton,
                door, 
                display, light, cooker);

            cooker.UI = ui;

        }

        #region UserInterface_Light

        [Test]
        public void UserInterface_Light_TurnOn()
        {
            door.Opened += Raise.Event();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void UserInterface_Light_TurnOff()
        {
            door.Opened += Raise.Event();
            door.Closed += Raise.Event();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        #endregion

        #region UserInterface_Display

        [Test]
        public void UserInterface_Display_ShowPower()
        {
            powerButton.Pressed += Raise.Event();
            powerButton.Pressed += Raise.Event();
            // Should now be 50 W
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
        }

        [Test]
        public void UserInterface_Display_ShowTime()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();

            // Should now show time 01:00
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void UserInterface_Display_Clear()
        {
            powerButton.Pressed += Raise.Event();
            startCancelButton.Pressed += Raise.Event();

            // Should cancel, and clear display
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }
        #endregion

        #region UserInterface_CookController

        [Test]
        public void UserInterface_CookController_StartCooking_50W()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            startCancelButton.Pressed += Raise.Event();

            // Should start cooking 
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube works with 50")));

        }

        [Test]
        public void UserInterface_CookController_StartCooking_150W()
        {
            powerButton.Pressed += Raise.Event();
            powerButton.Pressed += Raise.Event();
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            startCancelButton.Pressed += Raise.Event();

            // Should start cooking 
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube works with 150")));

        }

        [Test]
        public void UserInterface_CookController_StartCooking_700W()
        {
            for (int p = 50; p <= 700; p += 50)
            {
                powerButton.Pressed += Raise.Event();
            }

            timeButton.Pressed += Raise.Event();
            startCancelButton.Pressed += Raise.Event();

            // Should start cooking 
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube works with 700")));
        }

        [Test]
        public void UserInterface_CookController_StartCooking_1min()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            startCancelButton.Pressed += Raise.Event();

            // Should start cooking 
            // Wait for first time tick
            Thread.Sleep(1050);

            // Now should have updated 
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("00:59")));
        }

        [Test]
        public void UserInterface_CookController_StartCooking_2min()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            startCancelButton.Pressed += Raise.Event();

            // Should start cooking 
            // Wait for first time tick
            Thread.Sleep(1050);

            // Now should have updated 
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("01:59")));
        }

        [Test]
        public void UserInterface_CookController_CookingsIsDone()
        {
            // Checks the call back from CookController to UserInterface
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            startCancelButton.Pressed += Raise.Event();

            // Should start cooking 
            // Wait for expiration
            Thread.Sleep(60500);

            // Now should have turned off light
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));

        }

        #endregion
    }
}