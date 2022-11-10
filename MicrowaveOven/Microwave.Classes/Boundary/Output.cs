using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Output : IOutput
    {
        public void OutputLine(string line)
        {
            System.Console.WriteLine(line);
        }
        
    }
}