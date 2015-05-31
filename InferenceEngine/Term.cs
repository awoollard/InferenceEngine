using System.Collections.Generic;

namespace InferenceEngine
{
    class Term
    {
        private bool entailed = false;
        private bool explored = false;
        private string _name;
        private List<Term> parents;
    
        public Term()
        {
            _name = null;
        }

        public Term(string name)
        {
            _name = name;
            parents = new List<Term>();
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
            return _name;
        }

        public void AddParent(Term t)
        {
            parents.Add(t);
        }
        public List<Term> GetParents()
        {
            return parents;
        }

        public void SetExplored(bool isExplored)
        {
            explored = isExplored;
        }
    }
}
