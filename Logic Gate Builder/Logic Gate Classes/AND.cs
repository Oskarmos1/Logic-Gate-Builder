namespace Logic_Gate_Builder
{
    public class AND : Gate
    {
        public AND(int ANDnum) {
            gateNum = ANDnum;
            gateType = "AND";
            gateName = "AND"+ANDnum.ToString();
            output = new OutputNode(gateName);
            input1 = new InputNode(gateName);
            input2 = new InputNode(gateName);
            numberOfInputs = 2;
            initialNumberOfInputs = 2;
        }
        protected override void calculate() {
            if (input1.getVal() == 1 && input2.getVal() == 1)
            {
                output.setVal(1);
            }
            else {
                output.setVal(0);
            }
        }

        public override IGate exportComponent()
        {

            AND returning = new AND(Program.mainForm.getComponentCount());
            Program.mainForm.setComponentCount(Program.mainForm.getComponentCount()+1);
            return returning;
        }
    }
}
