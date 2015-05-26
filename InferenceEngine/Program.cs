using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() != 2)
            {
                Console.WriteLine("Invalid arguments.");
                Console.WriteLine("Syntax: iengine method filename");
            }

            Initialiser init = new Initialiser();
            init.parseFile(args[0], args[1]);
        }
    }
}
