﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex01_Hello
{
    class ExStarter
    {
        /// <summary>
        /// Note that the project property Application...Startup object is set to this class.
        /// </summary>
        [STAThread]
        public static int Main(string[] args)
        {
            return Abnaki.Windows.Software.Wpf.Starter.Start<MainWindow>(args);
            // App has nothing remarkable and is ignored

        }
    }
}
