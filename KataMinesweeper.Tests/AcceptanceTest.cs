using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace KataMinesweeper.Tests
{
    [TestFixture]
    public class AcceptanceTest
    {
        [Test]
        public void Test()
        {
            var input = File.ReadAllLines("../../AcceptanceTestFiles/SampleInput.txt");
            var expectedOutput = File.ReadAllLines("../../AcceptanceTestFiles/SampleOutput.txt");
            Assert.That(Minesweeper.Sweep(input).ToList(), Is.EqualTo(expectedOutput).AsCollection);
        }
    }
}
