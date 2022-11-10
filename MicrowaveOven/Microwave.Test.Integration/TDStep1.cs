using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep1
    {
        private Door door;
        private Button powerButton;
        private Button timeButton;
        private Button startCancelButton;

        private UserInterface ui;

        private ILight light;
        private IDisplay display;
        private ICookController cooker;

        [SetUp]
        public void Setup()
        {
            door = new Door();
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();

            light = Substitute.For<ILight>();
            display = Substitute.For<IDisplay>();
            cooker = Substitute.For<ICookController>();

            ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);
        }

        [Test]
        public void Door_UI_DoorOpen()
        {
            door.Open();

            light.Received(1).TurnOn();
        }
        public void Door_UI_DoorClose()
        {
            door.Open();
            door.Close();

            light.Received(1).TurnOff();
        }

        [Test]
        public void PowerButton_UI_PowerPressed()
        {
            powerButton.Press();

            display.Received(1).ShowPower(50);
        }

        [Test]
        public void TimeButton_UI_TimePressed()
        {
            powerButton.Press();
            timeButton.Press();

            display.Received(1).ShowTime(1, 0);
        }


    }
}