using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class FileHandler
    {
        private KnowledgeBase knowledgeBase;
        private string fileContents;

        public FileHandler()
        {
            knowledgeBase = new KnowledgeBase();
        }

        public void ReadFile(string fileName)
        {
            fileContents = File.ReadAllText(fileName);
        }

        public void FeedKnowledgeBase()
        {
            bool nextLineIsTellStatement = false;
            foreach (
                string fileLine in
                    fileContents.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (fileLine.Equals("TELL"))
                {
                    nextLineIsTellStatement = true;
                    continue;
                }

                if (nextLineIsTellStatement)
                {
                    knowledgeBase.Tell(fileLine);
                    nextLineIsTellStatement = false;
                }
            }
        }

        public void QueryKnowledgeBase(string method)
        {
            bool nextLineIsQuery = false;
            foreach (
                string fileLine in
                    fileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (fileLine.Equals("ASK"))
                {
                    nextLineIsQuery = true;
                    continue;
                }

                if (nextLineIsQuery)
                {
                    knowledgeBase.Query(fileLine, method);
                    nextLineIsQuery = false;
                }
            }
        }
    }
}
