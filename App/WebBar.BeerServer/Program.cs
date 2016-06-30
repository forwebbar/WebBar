using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Impl.Core;

namespace WebBar.BeerServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            ServiceHelperInit.InitService(args, new BeerServerMain());
        }
    }
}
