using System.Collections.Generic;

namespace InferenceEngine
{
    abstract class Method
    {
        protected List<string> Statements;
        protected List<Term> Terms;

        protected Method(List<string> statements, List<Term> terms)
        {
            this.Statements = statements;
            this.Terms = terms;
        }

        abstract public bool Query(Term query);

        virtual public void SetEntailed(string name)
        {
            foreach (Term term in Terms)
                if (term.GetName().Equals(name))
                    term.SetEntailed(true);
        }

        virtual public Term FetchTerm(string name)
        {
            foreach (Term term in Terms)
                if (term.GetName().Equals(name))
                    return term;
            return null;
        }

        virtual public string GetEntailedTermsString()
        {
            string returnString = " ";

            foreach (Term t in Terms)
            {
                if (t.IsEntailed())
                {
                    returnString = returnString + t.GetName() + ", ";
                }
            }

            // Remove trailing comma character
            return returnString.Remove(returnString.Length - 2);
        }
    }
}
