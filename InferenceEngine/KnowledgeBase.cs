using System;
using System.Collections.Generic;
using System.Linq;

namespace InferenceEngine
{
    // KnowledgeBase.cs: Stores terms and statements, and instantiates the required methods for FC/BC/TT.
    class KnowledgeBase
    {
        // Bunch of variables stored privately here... ?
        private List<string> Statements;
        private List<Term> Terms;
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

        private void PopulateStatements(string tellStatements)
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

        private void PopulateTerms(string tellStatements)
        {
            // Remove white-space
            tellStatements = tellStatements.Replace(" ", "");
            // Break the string into individual strings delimited by the following symbols.
            string[] termDelimiters = { "=>", "&", ";" };

            // A list is used rather than a string array to allow for duplicate removal
            foreach (
                string statement in
                    tellStatements.Split((termDelimiters), StringSplitOptions.RemoveEmptyEntries).Distinct().ToList()
            )
            {
                //duplicate protection
                Term temp = new Term(statement);
                if(!Terms.Contains(temp))
                {
                    Terms.Add(temp);
                }
            }
        }

        public string Query(string q, string method)
        {
            Term query = new Term(q);
            string returnString = null;

            if (method.ToUpper().Equals("FC"))
            {
                ForwardChain forwardChain = new ForwardChain(this.Statements, this.Terms);
                forwardChain.Query(query);
                if (query.IsEntailed())
                {
                    returnString = "YES: " + forwardChain.GetEntailedTermsString();
                }
            }
            else if (method.ToUpper().Equals("BC"))
            {
                BackwardsChain backwardsChain = new BackwardsChain(this.Statements, this.Terms);
                query.SetEntailed(backwardsChain.Query(query));
                if (query.IsEntailed())
                {
                    returnString = "YES: " + backwardsChain.GetEntailedTermsString();
                }
            }
            else if (method.ToUpper().Equals("TT"))
            {
                TruthTable truthTable = new TruthTable(this.Statements, this.Terms);
                truthTable.Query(query);
                if (query.IsEntailed())
                {
                    returnString = "YES: " + truthTable.HowManyTermsOrWhatever();
                }
            }
            else
            {
                // TODO: Verify this is the best approach
                returnString = "Unsupported method";
            }

            if (!query.IsEntailed())
                returnString = "NO";

            return returnString;
        }      
    }
}
