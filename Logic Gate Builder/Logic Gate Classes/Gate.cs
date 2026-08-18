using Logic_Gate_Builder.Logic_Gate_Classes;
using System;
using System.Diagnostics;

namespace Logic_Gate_Builder
{
    public abstract class Gate:IGate
    {
        protected OutputNode output;
        protected InputNode input1;
        protected InputNode input2;
        protected int initialNumberOfInputs;
        protected int numberOfInputs;
        protected int actualInputs = 0;
        protected string gateName;
        protected string gateType;
        protected int gateNum;
        

        abstract protected void calculate();

        public void execute()
        {
            calculate();
            triggerInputRead();
        }

        public virtual void connectToInput(OutputNode prevOutput, int inputNum)
        {
            if (inputNum == 1)
            {
                input1.connectToOutput(prevOutput);
                prevOutput.connectToInput(input1);

            }
            else if (inputNum == 2)
            {
                input2.connectToOutput(prevOutput);
                prevOutput.connectToInput(input2);
            }
                actualInputs++;
        }

        public virtual void breakInput(int inputNum) {
            if (inputNum == 1)
            {
                input1.breakConnection();

            }
            else if (inputNum == 2)
            {
                input2.breakConnection();
            }
            actualInputs--;
        }



        public OutputNode getOutput()
        {
            return output;
        }

        public void triggerInputRead()
        {
            output.triggerInputRead();
        }

        public int getNumberOfInputs()
        {
            return numberOfInputs;
        }

        public string getGateType()
        {
            return gateType;
        }

        public void removeInput()
        {
            numberOfInputs--;
        }

        public string getName()
        {
            return gateName;
        }

        public int getActualInputs() {
            return actualInputs;
        }

        public bool allInputsUsed() { 
            if (actualInputs == numberOfInputs)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual MyList<string> returnGateInfo() {
            MyList<string> returnList = new MyList<string>();
            returnList.add(gateType);
            returnList.add(gateName);
            if (input1 != null) {
                returnList.add(input1.getPreviousOutputGate());
            }
            if (input2 != null) {
                returnList.add(input2.getPreviousOutputGate());
            }

            return returnList;
        }

        public virtual void resetGate() {
            numberOfInputs = initialNumberOfInputs;
            output.setVal(0);
            if (input1 != null) {
                input1.setVal(0);
            }
            if (input2 != null) {
                input2.setVal(0);
            }

        }
        public void breakAllInputs() {
            if (input1 != null) {
                input1.breakConnection();
            }
            if (input2 != null) {
                input2.breakConnection();
            }
            actualInputs--;
        }

        public void deleteInputConnectionFromPreviousGateName(string gateName) {
            bool deleted = false;
            if (input1 != null) {
                try
                {
                    if (input1.getPreviousOutputGate() == gateName)
                    {
                        input1.breakConnection();
                        actualInputs--;
                        deleted = true;
                    }
                }
                catch { }

                
            }
            if (deleted == false) {
                if (input2 != null)
                {
                    {
                        try
                        {
                            if (input2.getPreviousOutputGate() == gateName)
                            {
                                input2.breakConnection();
                                actualInputs--;
                            }
                        }
                        catch { }

                    }
                }
            }


            

        }

        public void setName(string newName)
        {
            gateName = newName;
        }

        public virtual MyList<InputNode> getInputs() {
           MyList<InputNode> inputs = new MyList<InputNode>();
           inputs.add(input1);
           inputs.add(input2);
           return inputs;
        }

        abstract public IGate exportComponent();

        public int getGateNum() { 
            return gateNum;
        }
      
    }
}
