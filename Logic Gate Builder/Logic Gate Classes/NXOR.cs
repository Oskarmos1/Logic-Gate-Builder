namespace Logic_Gate_Builder
{
    public class NXOR : Gate
    {
        public NXOR(int NXORnum)
        {gateNum = NXORnum;
            gateType = "NXOR";
            gateName = "NXOR" + NXORnum.ToString();
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
                output.setVal(1);
            }
            else
            {
                output.setVal(0);
            }
        }

        public override IGate exportComponent()
        {

            NXOR returning = new NXOR(Program.mainForm.getComponentCount());
            Program.mainForm.setComponentCount(Program.mainForm.getComponentCount()+1);
            return returning;
        }
    }
}
