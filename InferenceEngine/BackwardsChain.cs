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
            //Latest parent added overwrites previous parent

            //(since at this stage only "=>" is supported, multiple parents need not be considered)
            //when we do implement multi parents we can do something like foreach (parent p in term.parents){//test for entailed}
            //problem with that is if multiple statements imply the same term, it would require all sentences to be true in order to entail

            if(FetchTerm(query.getName())==null)
            {
                return false;
            }

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
                        string[] termDelimiters = { "=>", "&" };//break the string into pieces between these things.
                        string[] implication = statement.Split((termDelimiters), StringSplitOptions.RemoveEmptyEntries);//StringSplitOptions.RemoveEmptyEntries
                        int termCount = implication.Length;

                        Term rhsTerm = FetchTerm(implication[implication.Length - 1]);
                        //forRemoval.Add(t);
                    
                        if(termCount<2)
                        {
                            rhsTerm.setEntailed(true);
                            rhsTerm.setExplored(true);
                            if(!statementRemoval.Contains(statement)) statementRemoval.Add(statement);
                        }
                        else
                        {
                            if (rhsTerm.getName() == t.getName())//if RHS is term t
                            {
                                rhsTerm.setExplored(true);
                                if (!forRemoval.Contains(t)) forRemoval.Add(t);

                                //Add non-entailed terms on LHS to entailRequired
                                for (int i = 0; i < (termCount - 1); i++)
                                {
                                    //set up parent child link
                                    FetchTerm(implication[i]).setChild(t); 
                                    rhsTerm.addParent(FetchTerm(implication[i]));
                                    forAddition.Add(FetchTerm(implication[i]));
                                }
                                statementRemoval.Add(statement);//expansion complete, statement no longer needed
                            }
                        }
                    }//end foreach statement
                }//end foreach term

                applyEntailment();

                if (FetchTerm(query.getName()).isEntailed() || (statementRemoval.Count == 0))//when q is entailed or no new implications have been made
                {
                    checkComplete = true;
                    query = FetchTerm(query.getName());
                }

                //add new terms to entailRequired
                foreach (Term t in forAddition)
                {
                    entailRequired.Add(t);
                }
                forAddition.Clear();

                //remove marked terms
                foreach (Term t in forRemoval)//issues here, doesnt remove d? seems to create a new instance of entailREquired and doesnt edit it
                {
                    entailRequired.Remove(t);
                }
                forRemoval.Clear();

                //remove marked statements
                foreach (string statement in statementRemoval)
                {
                    Statements.Remove(statement);
                }
                statementRemoval.Clear();

            }
            return query.isEntailed();
        }

        private void applyEntailment()//any term without a parent or an entailed parent becomes entailed
        {
            foreach (Term t in Terms)
            {
                List<Term> parents = t.getParents();
                if (parents.Count > 0)
                {
                    if (!t.isEntailed())
                    {
                        bool entailFlag = true;

                        //if any of the parents aren't entailed, t doesnt get entailed
                        foreach (Term p in parents)
                        {
                            if (!p.isEntailed())
                            {
                                entailFlag = false;
                            }
                        }
                        t.setEntailed(entailFlag);
                    }
                }
            }
        }
    }
}
