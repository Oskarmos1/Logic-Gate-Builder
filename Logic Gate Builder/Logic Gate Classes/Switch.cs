using System;

namespace Logic_Gate_Builder
{
    public class Switch:IGate
    {
        public OutputNode output;
        private int numberOfInputs;
        private string gateName;
        private string gateType;
        private int gateNum;
        public Switch(int SWITCHnum) {
            gateNum = SWITCHnum;
            gateType = "SWITCH";
            gateName = "SWITCH" + SWITCHnum.ToString();
            output = new OutputNode(gateName);
            output.setVal(0);
            numberOfInputs = 0;
        }

        public void setOutputVal(int val) {
            output.setVal(val);
        }

        public OutputNode getOutput() { 
            
            return output;
        }

        public void triggerInputRead() { 
            output.triggerInputRead();
        }


        public void execute() {
            /*
            Console.WriteLine(gateName);
            Console.WriteLine(output.getVal());
            */
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
        public virtual MyList<string> returnGateInfo()
        {
            MyList<string> returnList = new MyList<string>();
            returnList.add(gateType);
            returnList.add(gateName);
            return returnList;
        }


        public virtual void resetGate()
        {
            //output.setVal(0);
        }

        public void setName(string newName)
        {
            gateName = newName;
        }

        public IGate exportComponent()
        {

            Switch returning = new Switch(Program.mainForm.getComponentCount());
            Program.mainForm.setComponentCount(Program.mainForm.getComponentCount()+1);
            return returning;
        }

        public int getGateNum() { 
            return gateNum;
        }
    }
}
