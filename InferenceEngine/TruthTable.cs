using System;
using System.Collections.Generic;

namespace InferenceEngine
{
    /* TruthTable.cs: Takes the statements and terms from knowledgebase and does operations
     * Query returns whether or not the supplied query is entailable.
     * There is a seperate function for getting the amount of entailable terms or whatever the case may be.
     */ 
    class TruthTable : Method
    {
        public TruthTable(List<string> statements, List<Term> terms)
            : base(statements, terms)
        {
            // No custom functionality in constructor; inherits Method constructor
        }

        public override bool Query(Term query)
        {
            return true;
        }

        public int HowManyTermsOrWhatever()
        {
            return 42;
        }

        public override string GetTermsOrWhatever()
        {
            // Doesn't need to be implemented in TT but should be overridden in case this method is ever called
            // Maybe just delete this once coding is completed and we're sure it's never called
            throw new NotImplementedException();
        }
    }
}
