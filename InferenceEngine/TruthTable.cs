using System.Collections.Generic;

namespace InferenceEngine
{
    /* TruthTable.cs: Takes the statements and terms from knowledgebase and does operations
     * Query returns whether or not the supplied query is entailable.
     * There is a seperate function for getting the amount of entailable terms or whatever the case may be.
     */ 
    class TruthTable
    {
        private List<string> Statements;
        private List<Term> Terms;

        public TruthTable(List<string> statements, List<Term> terms)
        {
            this.Statements = statements;
            this.Terms = terms;
        }

        public bool Query(string query)
        {
            return true;
        }

        public int HowManyTermsOrWhatever()
        {
            return 42;
        }
    }
}
