using System;
using System.Linq;

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

            FileHandler fileHandler = new FileHandler();
            fileHandler.ReadFile(args[1]);
            fileHandler.FeedKnowledgeBase();
            fileHandler.QueryKnowledgeBase(args[0]);
        }
    }
}

