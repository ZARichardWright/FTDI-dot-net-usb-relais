using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using libftdinet;

namespace RelaisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            relais myRelais = new relais();
            myRelais.init();
            myRelais.relaisTest();
            myRelais.close();
        }
    }
}
