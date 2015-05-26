using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Term
    {
        public bool entailed;
        public string name;
    
        public Term()
        {
            entailed = false;
            name = null;
        }

        public Term(string n, bool entail)
        {
            name = n;
            entailed = entail;
        }
    }
}
