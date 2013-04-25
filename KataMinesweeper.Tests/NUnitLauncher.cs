using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Reflection;
using NUnit.Gui;

namespace KataMinesweeper.Tests
{
    static class NUnitLauncher
    {
        [STAThread]
        static void Main()
        {
            AppEntry.Main(new[] { Assembly.GetExecutingAssembly().Location });
        }
    }
}