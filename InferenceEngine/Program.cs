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

        List<Term> InstantiateTerms(List<string> statements)//returns a list of all terms in a list
        {
            string sTemp;
            List<string> forRemoval = new List<string>();
            List<Term> Terms = new List<Term>();

            while (statements.Any())//until all statements have been analysed
            {
                foreach (string s in statements) //examine every string in the list of statements
                {
                    sTemp = s.Replace(" ", ""); //remove spaces
                    string[] delimiters = { "=>", "&" };
                    string[] names = sTemp.Split((delimiters), StringSplitOptions.RemoveEmptyEntries);

                    foreach(string s2 in names)
                    {
                        Terms.Add(new Term(s2, false));
                    }
                }
            }
            return Terms;
        }
    }
}
