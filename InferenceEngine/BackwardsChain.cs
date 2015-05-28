using System.Collections.Generic;

namespace InferenceEngine
{
    /* BackwardsChain.cs: Takes the statements and terms from knowledgebase and does operations
     * Query returns whether or not the supplied query is entailable.
     * There is a seperate function for getting the entailable terms or whatever the case may be.
     */ 
    class BackwardsChain
    {
        private List<string> Statements;
        private List<Term> Terms;

        public BackwardsChain(List<string> statements, List<Term> terms)
        {
            this.Statements = statements;
            this.Terms = terms;
        }

        public bool Query(Term query)
        {
            //initial ideas
            //is query entailed?
            //yes: done, no: check for statements with query on RHS
            //Add LHS terms to entailed required list
            //repeat this process for each term in entailed required list

            return true;
        }

        public string GetTermsOrWhatever()
        {
            return "a, b, c";
        }
    }
}
