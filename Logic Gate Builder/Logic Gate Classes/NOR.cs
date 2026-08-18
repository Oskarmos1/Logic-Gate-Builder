namespace Logic_Gate_Builder
{
    public class NOR : Gate
    {
        public NOR(int NORnum)
        {gateNum = NORnum;
            gateType = "NOR";
            gateName = "NOR" + NORnum.ToString();
            output = new OutputNode(gateName);
            input1 = new InputNode(gateName);
            input2 = new InputNode(gateName);
            numberOfInputs = 2;
            initialNumberOfInputs = 2;
        }
        protected override void calculate()
        {
            if (input1.getVal() == 1 || input2.getVal() == 1)
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

            NOR returning = new NOR(Program.mainForm.getComponentCount());
            Program.mainForm.setComponentCount(Program.mainForm.getComponentCount()+1);
            return returning;
        }
    }
}
