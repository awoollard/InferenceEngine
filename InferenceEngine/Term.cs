using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Term
    {
        public bool entailed = false;
        public string name;
    
        public Term()
        {
            name = null;
        }

        public Term(string n)
        {
            name = n;
        }
    }
}
