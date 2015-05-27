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
        public List<Term> Terms = new List<Term>();
        public List<string> Statements = new List<string>();
        public KnowledgeBase()
        {

        }

        public bool tell(String tellStatements)
        {
            // Logic for querying here
            // Maybe this.InstantiateTerms(tellStatement); goes here or something

            tellStatements = tellStatements.Replace(" ", "");


            //add the statements to the appropriate list
            string[] statementsDelimiter = { ";" };
            List<string> statementsTemp = new List<string>(tellStatements.Split((statementsDelimiter), StringSplitOptions.RemoveEmptyEntries));
            List<string> statements = statementsTemp.Distinct().ToList();

            foreach(string s in statements)
            {
                Statements.Add(s);
            }

            //add terms to the appropriate list
            string[] termDelimiters = { "=>", "&", ";"};//break the string into pieces between these things.
            List<string> namesTemp = new List<string>(tellStatements.Split((termDelimiters), StringSplitOptions.RemoveEmptyEntries));//put into a list rather than string array to allow for duplicate removal
            List<string> names = namesTemp.Distinct().ToList();//duplicate removal

            foreach(string s in names)
            {
                Terms.Add(new Term(s));
            }

            return true;
        }
        public bool query(String queryString, String method)
        {
            // Logic for querying here
            return true;
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
                        FetchTerm(implication[1]).Entailed = FetchTerm(implication[0]).Entailed;
                        impliedTerm = FetchTerm(implication[1]);

                        //entailment established, therefore, statement s no longer needed
                        if (FetchTerm(implication[1]).Entailed)
                        {
                            forRemoval.Add(s);
                        }

                        if(impliedTerm.Entailed)
                        {
                            q.Entailed = true;
                        }
                    }
                }

                if((q.Entailed==true)||(!forRemoval.Any()))//when q is entailed or no new implications have been made
                {
                    allChecked = true;
                }

                foreach (string s in forRemoval)//remove marked statements
                {
                    Statements.Remove(s);
                    forRemoval.Remove(s);
                }
            }
            return q.Entailed;
        }

        public void SetEntailed(string name)//finds the term with the given name and sets it's Entailed value to true
        {
            foreach (Term t in Terms)
            {
                if (t.Name == name)
                {
                    t.Entailed = true;
                }
            }
        }

        public Term FetchTerm(string name)
        {
            foreach(Term t in Terms)
            {
                if(t.Name == name)
                {
                    return t;
                }
            }
            return null;
        }
    }
}
