using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class KnowledgeBase
    {
        // Bunch of variables stored privately here... ?
        public List<Term> Terms;
        public List<string> Statements;
        public KnowledgeBase()
        {
            Terms = new List<Term>();
            Statements = new List<string>();
        }

        public void Tell(string tellStatements)
        {
            PopulateStatements(tellStatements);
            PopulateTerms(tellStatements);
        }

        private void PopulateStatements(String tellStatements)
        {
            // Remove white-space
            tellStatements = tellStatements.Replace(" ", "");
            // Break the string into individual strings delimited by a semi-comma.
            string[] statementsDelimiter = { ";" };

            foreach (
                string statement in
                    tellStatements.Split((statementsDelimiter), StringSplitOptions.RemoveEmptyEntries).Distinct().ToList()
            )
                Statements.Add(statement);
        }

        private void PopulateTerms(String tellStatements)
        {
            // Break the string into individual strings delimited by the following symbols.
            string[] termDelimiters = { "=>", "&", ";" };

            // A list is used rather than a string array to allow for duplicate removal
            foreach (
                string statement in
                    tellStatements.Split((termDelimiters), StringSplitOptions.RemoveEmptyEntries).Distinct().ToList()
            )
                Terms.Add(new Term(statement));
        }

        public bool Query(String queryString, String method)
        {
            // Logic for querying here
            if (method.ToUpper().Equals("FC"))
            {
                // Invoke FC class here
                return true;
            }
            else if (method.ToUpper().Equals("BC"))
            {
                // Invoke BC class here
                return true;
            }
            else
            {
                // Query neither FC or BC so unsupported
                // Returning false here is probably not the best idea
                // TODO: Should raise an exception but too lazy
                return false;
            }
        }

        public bool JMethod(Term q)
        {
            List<string> forRemoval = new List<string>(); //Allows marking statements for removal inside the foreach loop
                                                          //Anytime a new entailment is established, the current statement should be added to this list
            bool allChecked = false;

            while(allChecked == false)
            {
                foreach(string s in Statements)//need to add some mechanism for terminating the method when q is entailed or cant be entailed
                {
                    if (!s.Contains("=>")&&!s.Contains("&"))//if the statement is a single term set the term with that name to entailed
                    {
                        SetEntailed(s);
                        forRemoval.Add(s);
                    }
                    else //checks LHS terms entailed status and infer RHS entailed status. 
                         //CURRENTLY ONLY WORKS WITH SINGLE TERM ON LHS (no "&")
                    {
                        string[] termDelimiters = { "=>", "&" };//break the string into pieces between these things.
                        string[] implication = s.Split((termDelimiters), StringSplitOptions.RemoveEmptyEntries);

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

                        if(impliedTerm.isEntailed())
                        {
                            q.setEntailed(true);
                        }
                    }
                }

                if(q.isEntailed() || !forRemoval.Any())//when q is entailed or no new implications have been made
                {
                    allChecked = true;
                }

                foreach (string statement in forRemoval)//remove marked statements
                {
                    Statements.Remove(statement);
                    forRemoval.Remove(statement);
                }
            }
            return q.isEntailed();
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
    }
}
