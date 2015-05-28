using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (term.getName().Equals(name))
                    term.setEntailed(true);
        }

        virtual public Term FetchTerm(string name)
        {
            foreach (Term term in Terms)
                if (term.getName().Equals(name))
                    return term;
            return null;
        }

        virtual public string GetTermsOrWhatever()
        {
            string returnString = " ";

            foreach (Term t in Terms)
            {
                if (t.isEntailed())
                {
                    returnString = returnString + t.getName() + ", ";
                }
            }
            return returnString;
        }
    }
}
