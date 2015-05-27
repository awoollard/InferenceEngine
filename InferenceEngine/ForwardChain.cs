using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    /* ForwardChain.cs: Takes the statements and terms from knowledgebase and does operations
     * Query returns whether or not the supplied query is entailable.
     * There is a seperate function for getting the entailable terms or whatever the case may be.
     */ 
    class ForwardChain
    {
        private List<string> Statements;
        private List<Term> Terms;

        public ForwardChain(List<string> statements, List<Term> terms)
        {
            this.Statements = statements;
            this.Terms = terms;
        }

        public bool Query(string query)
        {
            return true;
        }

        public string GetTermsOrWhatever()
        {
            return "a, b, c";
        }
    }
}
