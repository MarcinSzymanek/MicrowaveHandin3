using System;
using System.Media;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;


namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Door door = new Door();

            Output output = new Output();
            SoundPlayer player = new SoundPlayer(sounds.shortBuzz);
            AudioManager audio = new AudioManager(output, player);

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output, 700);
            

            Light light = new Light(output);

            Buzzer buzzer = new Buzzer(output, audio);

            Microwave.Classes.Boundary.Timer timer = new Timer();
            Microwave.Classes.Boundary.Timer buzzerTimer = new Timer();
            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(
                powerButton,
                timeButton, 
                startCancelButton, 
                door,
                display,
                light, 
                buzzerTimer,
                buzzer,
                cooker, 
                powerTube);

          
            
            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();
            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            // Wait for input

            System.Console.WriteLine("Press 's' to stop the program or press 'e' to extend the program with 10 seconds.");
            var cont = true;
            while (cont)
            {
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 's':
                    case 'S':
                        cont = false;
                        break;
                    case 'e':
                    case 'E':
                        timeButton.Press();
                        break;

                }
            }
        }
    }
}
