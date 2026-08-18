using System;
using System.Diagnostics;

namespace Logic_Gate_Builder
{

    public class Lamp:IGate
    {
        private int initialNumberOfInputs;
        private int numberOfInputs;
        private InputNode input;
        private string gateName;
        private string gateType;
        private int actualInputs;
        private int gateNum;
        public Lamp(int LAMPnum)
        {
            gateNum = LAMPnum;
            gateType = "LAMP";
            gateName = "LAMP" + LAMPnum.ToString();
            input = new InputNode(gateName);
            numberOfInputs = 1;
            actualInputs = 0;
            initialNumberOfInputs = 1;
        }

        public void connectToInput( OutputNode prevOutput, int n)
        {
            if (actualInputs < numberOfInputs)
            {
                input.connectToOutput(prevOutput);
                prevOutput.connectToInput(input);
                actualInputs++;
            }
            else
            {
                throw new InvalidOperationException("Lamp can only have one input connection.");
            }
        }
        public int getOutput() {
            return input.getVal();
        }

        public int getNumberOfInputs() { 
            return numberOfInputs;
        }

        public void removeInput() {
            numberOfInputs--;
        }

        public string getGateType() { 
            return gateType;
        }

        public string getName()
        {
            return gateName;
        }

        public void execute() {
            //displayOutput();
        }

        public bool allInputsUsed()
        {
            if (actualInputs == numberOfInputs)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void resetGate()
        {
            numberOfInputs = initialNumberOfInputs;
            input.setVal(0);
        }

        public void breakInput()
        {
            if (actualInputs == numberOfInputs)
            {
                input.breakConnection();
                actualInputs--;
            }
        }

        public virtual MyList<string> returnGateInfo()
        {
            MyList<string> returnList = new MyList<string>();
            returnList.add(gateType);
            returnList.add(gateName);
            returnList.add(input.getPreviousOutputGate());
            return returnList;
        }

        public void breakAllInputs()
        {
            if (actualInputs == numberOfInputs)
            {
                input.breakConnection();
                actualInputs--;
            }
        }

        public void deleteInputConnectionFromPreviousGateName(string gateName)
        {
            if (input != null)
            {
                try
                {
                    if (input.getPreviousOutputGate() == gateName)
                    {
                        input.breakConnection();
                        actualInputs--;
                    }
                }
                catch { }


            }
        }

        public void setName(string newName)
        {
            gateName = newName;
        }

        public MyList<InputNode> getInputs()
        {
            MyList<InputNode> inputs = new MyList<InputNode>();
            inputs.add(input);
            return inputs;
        }

        public IGate exportComponent()
        {

            Lamp returning = new Lamp(Program.mainForm.getComponentCount());
            Program.mainForm.setComponentCount(Program.mainForm.getComponentCount()+1);
            return returning;
        }

        public int getGateNum() {
            return gateNum;
        }
    }
}
