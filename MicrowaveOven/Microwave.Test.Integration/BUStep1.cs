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
    public class BUStep1
    {
        private IOutput output;
        private Timer timer;
        private Display display;
        private PowerTube powerTube;
        private CookController cooker;

        private IUserInterface ui;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();

            timer = new Timer();
            display = new Display(output);
            powerTube = new PowerTube(output);

            ui = Substitute.For<IUserInterface>();

            cooker = new CookController(timer, display, powerTube, ui);
        }

        #region CookController_PowerTube

        [Test]
        public void CookController_PowerTube_TurnOn_50W()
        {
            cooker.StartCooking(50, 60);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        [Test]
        public void CookController_PowerTube_TurnOn_150W()
        {
            cooker.StartCooking(150, 60);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("150")));
        }

        [Test]
        public void CookController_PowerTube_TurnOn_700W()
        {
            cooker.StartCooking(700, 60);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("700")));
        }

        #endregion

        #region CookController_Display

        [Test]
        public void CookController_Display_Showtime()
        {
            cooker.StartCooking(50, 60);

            // Wait for the first tick to have happened
            Thread.Sleep(1050);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:59")));
        }

        #endregion

        #region CookController_Timer

        [Test]
        public void CookController_Timer_Start_3sec_LongEnough()
        {
            cooker.StartCooking(50, 3);

            Thread.Sleep(2900);  // Wait almost 3 seconds

            // Shouldn't have received it yet
            ui.DidNotReceive().CookingIsDone();
        }

        [Test]
        public void CookController_Timer_Start_3sec_ShortEnough()
        {
            cooker.StartCooking(50, 3);

            Thread.Sleep(3100);  // Wait a little more than 3 seconds

            // Should have it received by now
            ui.Received(1).CookingIsDone();
        }

        [Test]
        public void Cookcontroller_Timer_TimerTicks()
        {
            cooker.StartCooking(50, 60);

            Thread.Sleep(1050); // Wait a little longer than the first tick

            // Should have called display that writes out the remaining time
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("00:59")));
        }


        #endregion
    }
}