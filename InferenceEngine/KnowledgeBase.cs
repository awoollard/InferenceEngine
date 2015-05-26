using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class KnowledgeBase
    {
        // Bunch of variables stored privately here... ?
        public List<Term> Terms = new List<Term>();
        public List<string> Statements = new List<string>();
        public KnowledgeBase()
        {

        }

        public bool tell(String tellStatements)
        {
            // Logic for querying here
            // Maybe this.InstantiateTerms(tellStatement); goes here or something

            tellStatements = tellStatements.Replace(" ", "");


            //add the statements to the appropriate list
            string[] statementsDelimiter = { ";" };
            List<string> statementsTemp = new List<string>(tellStatements.Split((statementsDelimiter), StringSplitOptions.RemoveEmptyEntries));
            List<string> statements = statementsTemp.Distinct().ToList();

            foreach(string s in statements)
            {
                Statements.Add(s);
            }

            //add terms to the appropriate list
            string[] termDelimiters = { "=>", "&", ";"};//break the string into pieces between these things.
            List<string> namesTemp = new List<string>(tellStatements.Split((termDelimiters), StringSplitOptions.RemoveEmptyEntries));//put into a list rather than string array to allow for duplicate removal
            List<string> names = namesTemp.Distinct().ToList();//duplicate removal

            foreach(string s in names)
            {
                Terms.Add(new Term(s));
            }

            return true;
        }
        public bool query(String queryString, String method)
        {
            // Logic for querying here
            return true;
        }
    }
}
