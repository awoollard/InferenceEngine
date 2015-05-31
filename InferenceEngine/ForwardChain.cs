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

        public override bool Query(Term query)//doesn't need to be bool, since now a term is passed it, entailed can be checked externally
        {
            if (FetchTerm(query.getName()) == null)
            {
                return false;
            }

            List<string> forRemoval = new List<string>(); //Allows marking statements for removal inside the foreach loop
                                                          //Anytime a new entailment is established, the current statement should be added to this list
            bool checkComplete = false;

            while (checkComplete == false)
            {
                foreach (string s in Statements)
                {
                    if (!s.Contains("=>") && !s.Contains("&"))//if the statement is a single term, set the term with that name to entailed
                    {
                        SetEntailed(s);
                        forRemoval.Add(s);
                    }
                    else //checks LHS terms entailed status and infer RHS entailed status. 
                    {
                        string[] termDelimiters = { "=>", "&" };//break the string into pieces between these things.
                        string[] implication = s.Split((termDelimiters),StringSplitOptions.RemoveEmptyEntries);//StringSplitOptions.RemoveEmptyEntries
                        int termCount = implication.Length;
                        int entailedTerms = 0;

                        //check if the LHS terms are entailed.
                        for (int i = 0; i < (termCount-1); i++)
                        {
                            if(FetchTerm(implication[i]).isEntailed())
                            {
                                entailedTerms++;
                            }
                        }

                        //if all the LHS terms are entailed, set the RHS to entailed
                        if(entailedTerms == (termCount-1))
                        {
                            FetchTerm(implication[termCount - 1]).setEntailed(true);
                            forRemoval.Add(s);

                            if (FetchTerm(implication[termCount - 1]).getName() == query.getName())
                            {
                                query.setEntailed(true);
                            }
                        }

                    }
                }

                if (query.isEntailed() || (forRemoval.Count==0))//when q is entailed or no new implications have been made
                {
                    checkComplete = true;
                }

                foreach (string statement in forRemoval)//remove marked statements
                {
                    Statements.Remove(statement);
                }
                forRemoval.Clear();
            }
            return query.isEntailed();
        }
    }
}
