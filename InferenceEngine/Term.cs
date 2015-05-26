using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Term
    {
        private bool entailed;
        private string name;
    
        public Term()
        {
            entailed = false;
            name = null;
        }

        public Term(string n)
        {
            name = n;
        }

        // getEntailed and setEntailed methods
        public bool Entailed
        {
            get { return entailed; }
            set { entailed = value; }
        }

        // getName and setName methods
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
