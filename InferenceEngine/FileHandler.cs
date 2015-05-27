using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    // FileHandler.cs: Reads the input file, feeds and queries the knowledge base depending on file input.
    class FileHandler
    {
        private string fileContents;

        public FileHandler(string fileName)
        {
            fileContents = File.ReadAllText(fileName);
        }

        public void FeedKnowledgeBase(KnowledgeBase knowledgeBase)
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

        public string QueryKnowledgeBase(string method, KnowledgeBase knowledgeBase)
        {
            bool nextLineIsQuery = false;
            string returnString = null;

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
                    returnString = knowledgeBase.Query(fileLine, method);
                    nextLineIsQuery = false;
                }
            }
            return returnString;
        }
    }
}
