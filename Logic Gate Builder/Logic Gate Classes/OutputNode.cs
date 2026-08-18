namespace Logic_Gate_Builder
{
    public class OutputNode
    {
        MyList<InputNode> nextInputs;
        private int val;
        private string ownerGate;
        public OutputNode(string oG) {
            val = 0;
            nextInputs = new MyList<InputNode>();
            this.ownerGate = oG;
        }

        public int getVal() { 
            return val;
        }

        public void setVal(int v) {
            val = v;
        }

        public void connectToInput(InputNode n)
        {
            nextInputs.add(n);
        }

        public void triggerInputRead() {
            for (int i = 0; i < nextInputs.getLength(); i++) {
                InputNode nextInput = nextInputs.getItem(i);
                nextInput.readOutput();
            }
        }

        public string[] getNextInputOwnerGate()
        {
            string[] nextNames = new string[nextInputs.getLength()];
            for (int i = 0; i < nextInputs.getLength(); i++)
            {
                InputNode nextInput = nextInputs.getItem(i);
                nextNames[i] = nextInput.getOwnerGate();
            }
            return nextNames;
        }

        public string getOwnerGate() { 
            return ownerGate;
        }

        public void breakConnection()
        {
            for (int i = 0; i < nextInputs.getLength(); i++) {
                nextInputs.getItem(i).breakConnection();
            }
            while (nextInputs.getLength() > 0) {
                nextInputs.removeAt(0);
            }
        }

        public void oneInputBreakConnection(InputNode inputNode) {
            for (int i = 0; i < nextInputs.getLength(); i++) {
                if (nextInputs.getItem(i) == inputNode) {
                    nextInputs.removeAt(i);
                    return;
                }

            }
        }

        public MyList<InputNode> getInputList() {
            return nextInputs;
        }

    }
}
