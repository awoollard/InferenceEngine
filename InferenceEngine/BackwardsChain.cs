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
            //Query
            //-----
            //looks at all the terms which need entailing to reach q (starting with q), 
            //it checks for statements where term t is implied.
            //if statement is single term t, t set to entailed.
            //if not, LHS terms are linked as parents to t, t is set as child to LHS terms(to allow entailment chains later)
            //adds the LHS of that statement to entailRequired.
            //removes statement from Statements

            //Does add multiple parents for one term but doesnt support "&" yet.

            //***to add: in a different loop?: need to check entailRequired for entailed terms.
            //remove entailed terms from entailRequired and make their children entailed
            //(since at this stage only "=>" is supported, multiple parents need not be considered)

            List<Term> forRemoval = new List<Term>();
            List<Term> forAddition = new List<Term>();
            List<string> statementRemoval = new List<string>();

            entailRequired.Add(query);

            while(!query.isEntailed())//this while loop is insufficient, need some condition for when we're sure query cant be known
            {
                foreach(Term t in entailRequired)
                {
                    foreach(string statement in Statements)
                    {
                        string[] termDelimiters = { "=>", "&" };//break the string into pieces between these things.
                        string[] implication = statement.Split((termDelimiters), StringSplitOptions.RemoveEmptyEntries);//StringSplitOptions.RemoveEmptyEntries
                        int termCount = implication.Length;
                    
                        if(termCount<2)
                        {
                            t.setEntailed(true);
                        }
                        else
                        {
                            if(implication[implication.Length-1]==t.getName())//if RHS is term t
                            {
                                //Add non-entailed terms on LHS to entailRequired
                                for (int i = 0; i < (termCount - 1); i++)
                                {
                                    FetchTerm(implication[i]).setChild(t); //set up parent child link
                                    t.setParent(FetchTerm(implication[i]));

                                    if (!FetchTerm(implication[i]).isEntailed())
                                    {
                                        forAddition.Add(FetchTerm(implication[i]));
                                    }
                                    else
                                    {
                                        t.setEntailed(true);
                                        forRemoval.Add(t);
                                    }
                                }
                                statementRemoval.Add(statement);
                            }
                        }
                    }
                }

                foreach (Term t in forAddition)//remove marked statements
                {
                    entailRequired.Add(t);
                }
                forRemoval.Clear();

                foreach (Term t in forRemoval)//remove marked statements
                {
                    entailRequired.Remove(t);
                }
                forRemoval.Clear();

                foreach (string statement in statementRemoval)//remove marked statements
                {
                    Statements.Remove(statement);
                }
                statementRemoval.Clear();
            }
            return true;
        }
    }
}
