using System.Collections.Generic;
using System;

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

        public bool Query(Term query)//doesn't need to be bool, since now a term is passed it, entailed can be checked externally
        {

            List<string> forRemoval = new List<string>(); //Allows marking statements for removal inside the foreach loop
                                                          //Anytime a new entailment is established, the current statement should be added to this list
            bool allChecked = false;

            while (allChecked == false)
            {
                foreach (string s in Statements)//need to add some mechanism for terminating the method when q is entailed or cant be entailed
                {
                    if (!s.Contains("=>") && !s.Contains("&"))//if the statement is a single term set the term with that name to entailed
                    {
                        SetEntailed(s);
                        forRemoval.Add(s);
                    }
                    else //checks LHS terms entailed status and infer RHS entailed status. 
                    //CURRENTLY ONLY WORKS WITH SINGLE TERM ON LHS (no "&")
                    {
                        string[] termDelimiters = { "=>", "&" };//break the string into pieces between these things.
                        string[] implication = s.Split((termDelimiters),StringSplitOptions.RemoveEmptyEntries);//StringSplitOptions.RemoveEmptyEntries

                        //"FetchTerm(implication[1]).Entailed" effectively refers to the RHS of the implication
                        Term impliedTerm = new Term("rightTerm");

                        //the implied term takes the entailed value of the implying term
                        FetchTerm(implication[1]).setEntailed(FetchTerm(implication[0]).isEntailed());
                        impliedTerm = FetchTerm(implication[1]);

                        //entailment established, therefore, statement s no longer needed
                        if (FetchTerm(implication[1]).isEntailed())
                        {
                            forRemoval.Add(s);
                        }

                        if (impliedTerm.isEntailed())
                        {
                            query.setEntailed(true);
                        }
                    }
                }

                if (query.isEntailed() || (forRemoval.Count==0))//when q is entailed or no new implications have been made
                {
                    allChecked = true;
                }

                foreach (string statement in forRemoval)//remove marked statements
                {
                    Statements.Remove(statement);
                    forRemoval.Remove(statement);
                }
            }
            return query.isEntailed();
        }

        public void SetEntailed(string name)
        {
            foreach (Term term in Terms)
                if (term.getName().Equals(name))
                    term.setEntailed(true);
        }

        public Term FetchTerm(string name)
        {
            foreach (Term term in Terms)
                if (term.getName().Equals(name))
                    return term;
            return null;
        }

        public string GetTermsOrWhatever()
        {
            return "a, b, c";
        }
    }
}
