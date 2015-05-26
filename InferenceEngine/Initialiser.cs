using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Initialiser
    {
        public Initialiser()
        {
            // Empty constructor
        }

        public void parseFile(String method, String fileName)
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase();
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            String fileLine;
            while ((fileLine = file.ReadLine()) != null)
            {
                switch (fileLine)
                {
                    case "TELL":
                        // Read the next line
                        fileLine = file.ReadLine();

                        // Feed the knowledge-base
                        knowledgeBase.tell(fileLine);
                        break;
                    case "ASK":
                        // Read the next line
                        fileLine = file.ReadLine();

                        // Query the knowledge-base
                        knowledgeBase.query(fileLine, method);
                        break;
                    default:
                        // Do nothing
                        break;
                }
            }
            file.Close();
        }
    }
}
