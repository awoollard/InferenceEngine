using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;

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

        // function TT-ENTAILS?(KB,α) returns true or false
        // inputs: KB, the knowledge base, a sentence in propositional logic
        // α, the query, a sentence in propositional logic
        public override bool Query(Term query)
        {
            /* symbols ← a list of the proposition symbols in KB and α
               return TT-CHECK-ALL(KB,α, symbols, { })
             */
            // ignore unioning term into statements because d already exists
            return this.checkAll(Statements, query, Statements /*, null*/);
        }

        // function TT-CHECK-ALL(KB,α, symbols, model) returns true or false
        public bool checkAll(List<string> statements, Term alpha, List<string> symbols /*, Model model*/)
        {
            /*
                if EMPTY?(symbols) then
                if PL-TRUE?(KB, model) then return PL-TRUE?(α, model)
                else return true // when KB is false, always return true
                else do
                P ← FIRST(symbols)
                rest ← REST(symbols)
                return (TT-CHECK-ALL(KB,α, rest, model ∪ {P = true})
                and
                TT-CHECK-ALL(KB,α, rest, model ∪ {P = false }))
             */
            return true;
        }

        public int HowManyTermsOrWhatever()
        {
            return 0;
        }

        public override string GetTermsOrWhatever()
        {
            // Doesn't need to be implemented in TT but should be overridden in case this method is ever called
            throw new NotImplementedException();
        }
    }
}
