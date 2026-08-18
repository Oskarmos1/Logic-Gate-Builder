namespace Logic_Gate_Builder
{
    public class XOR : Gate
    {
        public XOR(int XORnum)
        {
            gateNum = XORnum;
            gateType = "XOR";
            gateName = "XOR" + XORnum.ToString();
            output = new OutputNode(gateName);
            input1 = new InputNode(gateName);
            input2 = new InputNode(gateName);
            numberOfInputs = 2;
            initialNumberOfInputs = 2;
        }
        protected override void calculate()
        {
            if ((input1.getVal() == 1 && input2.getVal() == 1) || (input1.getVal() == 0 && input2.getVal() == 0))
            {
                output.setVal(0);
            }
            else
            {
                output.setVal(1);
            }
        }

        public override IGate exportComponent()
        {

            XOR returning = new XOR(Program.mainForm.getComponentCount());
            Program.mainForm.setComponentCount(Program.mainForm.getComponentCount()+1);
            return returning;
        }
    }
}
