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

                foreach (string s in statements) //examine every string in the list of statements
                {
                    sTemp = s.Replace(" ", ""); //remove spaces
                    string[] delimiters = { "=>", "&" };
                    //string[] namesTemp = sTemp.Split((delimiters), StringSplitOptions.RemoveEmptyEntries);
                    List<string> namesTemp = new List<string>(sTemp.Split((delimiters), StringSplitOptions.RemoveEmptyEntries));
                    List<string> names = namesTemp.Distinct().ToList();

                    foreach(string s2 in names)
                    {
                            Terms.Add(new Term(s2));
                    }
                }
         
            return Terms;
        }
    }
}
