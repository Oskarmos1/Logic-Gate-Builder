namespace Logic_Gate_Builder
{
    public class NAND : Gate
    {
        public NAND(int NANDnum)
        {
            gateNum = NANDnum;
            gateType = "NAND";
            gateName = "NAND" + NANDnum.ToString();
            output = new OutputNode(gateName);
            input1 = new InputNode(gateName);
            input2 = new InputNode(gateName);
            numberOfInputs = 2;
            initialNumberOfInputs = 2;
        }
        protected override void calculate()
        {
            if (input1.getVal() == 1 && input2.getVal() == 1)
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

            NAND returning = new NAND(Program.mainForm.getComponentCount());
            Program.mainForm.setComponentCount(Program.mainForm.getComponentCount()+1);
            return returning;
        }
    }
}
