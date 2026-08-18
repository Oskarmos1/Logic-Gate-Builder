using System;

namespace Logic_Gate_Builder
{
    public class InputNode
    {
        private OutputNode previousOutput;
        private int val;
        private string ownerGate;
        public InputNode(string oG) {
            val = 0;
            previousOutput = null;
            ownerGate = oG;
        }
        public int getVal()
        {
            return val;
        }

        public void setVal(int v)
        {
            val = v;
        }

        public void readOutput() {
            if (previousOutput != null)
            {
                val = previousOutput.getVal();
            }
            else {
                throw new InvalidOperationException("Cannot read output from a disconnected input node.");
            }

        }

        public void connectToOutput(OutputNode n) {
            previousOutput = n;
        }
        public string getOwnerGate() { 
            return ownerGate;
        }

        public string getPreviousOutputGate()
        {
            if (previousOutput != null)
            {
                return previousOutput.getOwnerGate();
            }
            else {
                return null;
                //throw new InvalidOperationException("Cannot find owner of previous output from a disconnected input node.");
            }

        }

        public void breakConnection() {
            if (previousOutput != null) {
                previousOutput.oneInputBreakConnection(this);
                previousOutput = null;
            }

        }

    }
}
