using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Boundary;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    class OutputTest
    {
        private StringWriter str;
        private Output uut;
        
        [SetUp]
        public void Setup()
        {
            str = new StringWriter();
            Console.SetOut(str);
            uut = new Output();
        }

        [Test]
        public void OutputLineCorrect()
        {
            uut.OutputLine("Test");

            Assert.That(str.ToString().Contains("Test"));

        }
    }
}
