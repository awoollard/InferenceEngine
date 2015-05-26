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
        public KnowledgeBase()
        {

        }

        public bool tell(String tellStatements)
        {
            // Logic for querying here
            // Maybe this.InstantiateTerms(tellStatement); goes here or something

            tellStatements = tellStatements.Replace(" ", "");

            string[] delimiters = { "=>", "&",";"};
            List<string> namesTemp = new List<string>(tellStatements.Split((delimiters), StringSplitOptions.RemoveEmptyEntries));
            List<string> names = namesTemp.Distinct().ToList();

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
