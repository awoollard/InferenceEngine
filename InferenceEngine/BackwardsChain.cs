using System.Collections.Generic;
using System;
namespace InferenceEngine
{
    /* BackwardsChain.cs: Takes the statements and terms from knowledgebase and does operations
     * Query returns whether or not the supplied query is entailable.
     * There is a seperate function for getting the entailable terms or whatever the case may be.
     */
    class BackwardsChain : Method
    {
        private List<Term> entailRequired = new List<Term>();

        public BackwardsChain(List<string> statements, List<Term> terms)
            : base(statements, terms)
        {
            // No custom functionality in constructor; inherits Method constructor
        }

        public override bool Query(Term query)
        {

            if (FetchTerm(query.GetName()) == null)
                return false;

            List<Term> forRemoval = new List<Term>();
            List<Term> forAddition = new List<Term>();
            List<string> statementRemoval = new List<string>();
            bool checkComplete = false;
            entailRequired.Add(query);

            while(!checkComplete)
            {
                foreach(Term t in entailRequired)
                {
                    foreach(string statement in Statements)
                    {
                        // Break the string into pieces between these delimiters.
                        string[] termDelimiters = { "=>", "&" };
                        string[] implication = statement.Split((termDelimiters), StringSplitOptions.RemoveEmptyEntries);
                        int termCount = implication.Length;

                        Term rhsTerm = FetchTerm(implication[implication.Length - 1]);
                        if(termCount<2)
                        {
                            rhsTerm.SetEntailed(true);
                            rhsTerm.SetExplored(true);
                            if(!statementRemoval.Contains(statement)) statementRemoval.Add(statement);
                        }
                        else
                        {
                            // If Right-Hand-Side is term t
                            if (rhsTerm.GetName() == t.GetName())
                            {
                                rhsTerm.SetExplored(true);
                                if (!forRemoval.Contains(t)) forRemoval.Add(t);

                                // Add non-entailed terms on LHS to entailRequired
                                for (int i = 0; i < (termCount - 1); i++)
                                {
                                    // Set up parent child link
                                    FetchTerm(implication[i]).SetChild(t); 
                                    rhsTerm.AddParent(FetchTerm(implication[i]));
                                    forAddition.Add(FetchTerm(implication[i]));
                                }
                                // Expansion complete, statement no longer needed
                                statementRemoval.Add(statement);
                            }
                        }
                    }
                }

                applyEntailment();

                // When query is entailed or no new implications have been made
                if (FetchTerm(query.GetName()).IsEntailed() || (statementRemoval.Count == 0))
                {
                    checkComplete = true;
                    query = FetchTerm(query.GetName());
                }

                // Add new terms to entailRequired
                foreach (Term t in forAddition)
                {
                    entailRequired.Add(t);
                }
                forAddition.Clear();

                // Remove marked terms
                foreach (Term t in forRemoval)//issues here, doesnt remove d? seems to create a new instance of entailREquired and doesnt edit it
                {
                    entailRequired.Remove(t);
                }
                forRemoval.Clear();

                // Remove marked statements
                foreach (string statement in statementRemoval)
                {
                    Statements.Remove(statement);
                }
                statementRemoval.Clear();

            }
            return query.IsEntailed();
        }

        private void applyEntailment()
        {
            // Any term without a parent or an entailed parent becomes entailed
            foreach (Term t in Terms)
            {
                List<Term> parents = t.GetParents();
                if (parents.Count > 0)
                {
                    if (!t.IsEntailed())
                    {
                        bool entailFlag = true;

                        // If any of the parents aren't entailed, t doesnt get entailed
                        foreach (Term p in parents)
                            if (!p.IsEntailed())
                                entailFlag = false;
                        t.SetEntailed(entailFlag);
                    }
                }
            }
        }
    }
}
