using Logic_Gate_Builder.Functionality_Classes;
using System;
using System.Diagnostics;

namespace Logic_Gate_Builder.Logic_Gate_Classes
{
    public class CustomGate:IGate
    {
        private OutputNode output;
        private MyList<InputNode> inputsList;
        private MyList<int> numericInputsFor1;
        private int initialNumberOfInputs;
        private int numberOfInputs;
        private int actualInputs = 0;
        private string gateName;
        private string gateType;
        private MyList<MyList<string>> truthTable;
        private int gateNum;
        public CustomGate(int CUSTOMNum, int noOfInputs, MyList<MyList<string>> tT)
        {
            gateNum = CUSTOMNum;
            truthTable = tT;
            gateType = "CUSTOM";
            gateName = "CUSTOM" + CUSTOMNum.ToString();
            output = new OutputNode(gateName);
            numericInputsFor1 = new MyList<int>();
            
            if (tT == null || tT.getLength() == 0)
            {
                throw new ArgumentException("Provided truth table cannot be null or empty.");
            }
            if (tT.getItem(0).getLength() > (noOfInputs + 1)) {
                throw new ArgumentException("Provided truth table cannot have more than 1 output");
            }
            for (int i = 0; i < tT.getLength(); i++) {
                MyList<string> row = tT.getItem(i);
                if (row == null || row.getLength() < 2)
                {
                    throw new ArgumentException($"Invalid row format in truth table at index {i}.");
                }
                if (row.getItem(row.getLength() - 1).ToString() == "1") {
                    string currentBinaryCombo = "";
                    for (int j = 0; j < row.getLength() - 1; j++)
                    {
                        currentBinaryCombo = currentBinaryCombo + row.getItem(j).ToString();
                    }
                    int numericInput = BinaryFunctions.binaryToDenary(currentBinaryCombo);
                    numericInputsFor1.add(numericInput);
                }
            }
            inputsList = new MyList<InputNode>();
            for (int i = 0; i < noOfInputs; i++) {
                inputsList.add(new InputNode(gateName));          
            }      
            numberOfInputs = noOfInputs;
            initialNumberOfInputs = noOfInputs;
            
        }

        private void calculate() {
            string inputCombination = "";
            int oVal = -1;
            for (int i = 0; i < inputsList.getLength(); i++) {
                InputNode currentInput = inputsList.getItem(i);
                inputCombination = inputCombination + currentInput.getVal();
            }
            int currentNumericInput = BinaryFunctions.binaryToDenary(inputCombination);
            bool isOne = false;
            for (int i = 0; i < numericInputsFor1.getLength(); i++) {
                if (currentNumericInput == numericInputsFor1.getItem(i)) {
                    isOne = true;
                }
            }
            if (isOne == true)
            {
                oVal = 1;
            }
            else {
                oVal = 0;
            }
            output.setVal(oVal);
        }

        public void execute()
        {
            calculate();
            triggerInputRead();
        }

        public virtual void connectToInput(OutputNode prevOutput, int inputNum)
        {
            if ((inputNum-1) < initialNumberOfInputs) { 
                inputsList.getItem(inputNum-1).connectToOutput(prevOutput);
                prevOutput.connectToInput(inputsList.getItem(inputNum-1));
            }
            actualInputs++;
        }

        public virtual void breakInput(int inputNum)
        {
            if (inputNum < initialNumberOfInputs)
            {
                inputsList.getItem(inputNum).breakConnection();
                actualInputs--;
            }
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

        public int getActualInputs()
        {
            return actualInputs;
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
            output.setVal(0);
            for (int i = 0; i < initialNumberOfInputs; i++)
            {
                inputsList.getItem(i).setVal(0);   
            }            
        }

        public virtual MyList<string> returnGateInfo()
        {
            MyList<string> returnList = new MyList<string>();
            returnList.add(gateType);
            returnList.add(gateName);
            string nIF1 = "";
            for (int i = 0; i < numericInputsFor1.getLength(); i++) {
                nIF1 = nIF1 + numericInputsFor1.getItem(i).ToString() + ",";
            }
            if (nIF1.Length > 0) {
                nIF1 = nIF1.Substring(0, nIF1.Length - 1);
            }

            returnList.add(nIF1);
            for (int i = 0; i < inputsList.getLength(); i++) { 
                InputNode inputNode = inputsList.getItem(i);
                returnList.add(inputNode.getPreviousOutputGate());
            }
            return returnList;
        }

        public void breakAllInputs()
        {
            for (int i = 0; i < inputsList.getLength(); i++) { 
                inputsList.getItem(i).breakConnection();
                actualInputs--;
            }
        }

        public void deleteInputConnectionFromPreviousGateName(string gateName)
        {
            //bool deleted = false;
            for (int i = 0; i < inputsList.getLength(); i++) {
                InputNode input = inputsList.getItem(i);
                if (input != null)
                {
                    try
                    {
                        if (input.getPreviousOutputGate() == gateName)
                        {
                            //if (deleted == false) {
                                input.breakConnection();
                                actualInputs--;
                              //  deleted = true;
                                break;
                            //}

                        }
                    }
                    catch { }

                }
            }
        }

        public void setName(string newName) { 
            gateName = newName;
        }

        public MyList<InputNode> getInputs()
        {
            return inputsList;
        }

        public IGate exportComponent() {

            CustomGate returning = new CustomGate(Program.mainForm.getComponentCount(), numberOfInputs, truthTable);
            Program.mainForm.setComponentCount(Program.mainForm.getComponentCount()+1);
            return returning;
        }

        public int getGateNum() {
            return gateNum;
        }
    }
}
