using System.Collections.Generic;
using System;

namespace InferenceEngine
{
    /* ForwardChain.cs: Takes the statements and terms from knowledgebase and does operations
     * Query returns whether or not the supplied query is entailable.
     * There is a seperate function for getting the entailable terms or whatever the case may be.
     */ 
    class ForwardChain : Method
    {
        public ForwardChain(List<string> statements, List<Term> terms)
            : base(statements, terms)
        {
            // No custom functionality in constructor; inherits Method constructor
        }

        // Doesn't need to be bool, since now a term is passed it, entailed can be checked externally
        public override bool Query(Term query)
        {
            if (FetchTerm(query.getName()) == null)
                return false;

            // Allows marking statements for removal inside the foreach loop
            // Anytime a new entailment is established, the current statement should be added to forRemoval list
            List<string> forRemoval = new List<string>();   
            bool checkComplete = false;

            while (checkComplete == false)
            {
                foreach (string s in Statements)
                {
                    // If the statement is a single term, set the term with that name to entailed
                    if (!s.Contains("=>") && !s.Contains("&"))
                    {
                        SetEntailed(s);
                        forRemoval.Add(s);
                    }
                    else // Checks Left-Hand-Side terms entailed status and infer Right-Hand-Side entailed status. 
                    {
                        string[] termDelimiters = { "=>", "&" };//break the string into pieces between these things.
                        string[] implication = s.Split((termDelimiters), StringSplitOptions.RemoveEmptyEntries);
                        int termCount = implication.Length;
                        int entailedTerms = 0;

                        // Check if the Left-Hand-Side terms are entailed.
                        for (int i = 0; i < (termCount - 1); i++)
                            if (FetchTerm(implication[i]).isEntailed())
                                entailedTerms++;

                        // If all the Left-Hand-Side terms are entailed, set the Right-Hand-Side to entailed
                        if(entailedTerms == (termCount-1))
                        {
                            FetchTerm(implication[termCount - 1]).setEntailed(true);
                            forRemoval.Add(s);

                            if (FetchTerm(implication[termCount - 1]).getName() == query.getName())
                                query.setEntailed(true);
                        }

                    }
                }

                // When query is entailed or no new implications have been made
                if (query.isEntailed() || (forRemoval.Count == 0))
                    checkComplete = true;

                // Remove marked statements
                foreach (string statement in forRemoval)
                    Statements.Remove(statement);
                forRemoval.Clear();
            }
            return query.isEntailed();
        }
    }
}
