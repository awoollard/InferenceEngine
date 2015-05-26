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
          //hello world
        }

        void InstantiateTerms(List<string> statements)
        {
            string sTemp;
            List<string> forRemoval = new List<string>(); 

            while (statements.Any())//until all statements have been analysed
            {
                foreach (string s in statements) //examine every string in the list of statements
                {
                    sTemp = s.Replace(" ", ""); //remove spaces
                    string[] delimiters = { "=>", "&" };
                    string[] names = sTemp.Split((delimiters), StringSplitOptions.RemoveEmptyEntries);

                    foreach(string s2 in names)
                    {
                        Term Term = new Term(s2, false); //somehow name each new term after the string. might have to create a 2d list.
                    }
                }
            }
        }
    }
}
