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
            SetName(name);
            parents = new List<Term>();
        }

        public Term(string name,Term child)
        {
            SetName(name);
        }

        public bool IsEntailed()
        {
            return entailed;
        }

        public void SetEntailed(bool isEntailed)
        {
            this.entailed = isEntailed;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetChild(Term t)
        {
            this.child = t;
        }

        public void AddParent(Term t)
        {
            parents.Add(t);
        }
        public List<Term> GetParents()
        {
            return parents;
        }
        public bool IsExplored()
        {
            return explored;
        }

        public void SetExplored(bool isExplored)
        {
            explored = isExplored;
        }
    }
}
