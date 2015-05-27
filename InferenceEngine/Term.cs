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

        public Term(string name)
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
    }
}
