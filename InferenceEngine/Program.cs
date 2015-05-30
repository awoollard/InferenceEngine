using System;
using System.Linq;

namespace InferenceEngine
{
    // Program.cs: Handles console output.
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() != 2)
            {
                Console.WriteLine("Invalid arguments.");
                Console.WriteLine("Syntax: iengine method filename");
            }

            KnowledgeBase knowledgeBase = new KnowledgeBase();
            FileHandler fileHandler = new FileHandler(args[1]);
            fileHandler.FeedKnowledgeBase(knowledgeBase);
            Console.WriteLine(fileHandler.QueryKnowledgeBase(args[0], knowledgeBase));

            //give user time to read output
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}

