using System.Collections.Generic;

namespace InferenceEngine
{
    class Term
    {
        private bool entailed = false;
        private bool explored = false;
        private string name;
        private Term child;
        private List<Term> parents;
    
        public Term()
        {
            name = null;
        }

        public Term(string name)
        {
            setName(name);
            parents = new List<Term>();
        }

        public Term(string name,Term child)
        {
            setName(name);
        }

        public bool isEntailed()
        {
            return entailed;
        }

        public void setEntailed(bool isEntailed)
        {
            this.entailed = isEntailed;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setChild(Term t)
        {
            this.child = t;
        }
        public Term getChild()
        {
            return child;
        }

        public void addParent(Term t)
        {
            parents.Add(t);
        }
        public List<Term> getParents()
        {
            return parents;
        }
        public bool isExplored()
        {
            return explored;
        }

        public void setExplored(bool isExplored)
        {
            explored = isExplored;
        }
    }
}
