namespace Logic_Gate_Builder
{
    public class NOT : Gate
    {
        public NOT(int NOTnum)
        {
            gateNum = NOTnum;
            gateType = "NOT";
            gateName = "NOT" + NOTnum.ToString();
            output = new OutputNode(gateName);
            input1 = new InputNode(gateName);
            input2 = null;
            numberOfInputs = 1;
            initialNumberOfInputs = 1;
        }
        protected override void calculate()
        {
            if (input1.getVal() == 1)
            {
                output.setVal(0);
            }
            else
            {
                output.setVal(1);
            }
        }

        override public void connectToInput(OutputNode prevOutput, int inputNum)
        {
            input1.connectToOutput(prevOutput);
            prevOutput.connectToInput(input1);
            actualInputs++;
        }

        override public void breakInput(int inputNum)
        {
            if (actualInputs == numberOfInputs)
            {
                input1.breakConnection();
                actualInputs--;
            }

        }

        public override MyList<string> returnGateInfo()
        {
            MyList<string> returnList = new MyList<string>();
            returnList.add(gateType);
            returnList.add(gateName);
            returnList.add(input1.getPreviousOutputGate());
            return returnList;
        }
        override public  void resetGate()
        {
            numberOfInputs = initialNumberOfInputs;
            output.setVal(0);
            input1.setVal(0);
        }

        override public  MyList<InputNode> getInputs()
        {
            MyList<InputNode> inputs = new MyList<InputNode>();
            inputs.add(input1);
            return inputs;
        }

        public override IGate exportComponent()
        {

            NOT returning = new NOT(Program.mainForm.getComponentCount());
            Program.mainForm.setComponentCount(Program.mainForm.getComponentCount()+1);
            return returning;
        }
    }
}