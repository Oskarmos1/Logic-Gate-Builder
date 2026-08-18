using Logic_Gate_Builder.Logic_Gate_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.Functionality_Classes
{
    public class LogicGateFunctions
    {
        public static MyList<MyList<string>> generateTruthTable(MyList<IGate> components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
           
            try
            {
                if (GraphAlgorithms.isCircuitComplete(components) == false)
                {
                    throw new Exception("Cannot generate a new truth table because the circuit is not complete.");
                }
                MyList<MyList<string>> truthTable = new MyList<MyList<string>>();
                MyList<Switch> switchList = new MyList<Switch>();
                MyList<Lamp> lampList = new MyList<Lamp>();
                int numberOfSwitches = 0;
                int numberOfLamps = 0;
                for (int i = 0; i < components.getLength(); i++)
                {
                    if (components.getItem(i) == null || FileHandling.isNullOrWhiteSpace(components.getItem(i).getGateType()))
                    {
                        throw new InvalidDataException($"Component at index {i} is null or has an invalid type.");
                    }
                    if (components.getItem(i).getGateType() == "SWITCH")
                    {
                        numberOfSwitches++;
                        switchList.add(components.getItem(i) as Switch);
                    }
                    if (components.getItem(i).getGateType() == "LAMP") {
                        numberOfLamps++;
                        lampList.add(components.getItem(i) as Lamp);
                    }
                }
                int numberOfInputs = BinaryFunctions.pow(2, numberOfSwitches);
                MyList<string> infoRow = new MyList<string>();
                if (numberOfSwitches == 0)
                {
                    throw new InvalidOperationException("Cannot generate a truth table for a circuit with no SWITCH components.");
                }
                if (numberOfLamps == 0) {
                    throw new InvalidOperationException("Cannot generate a truth table for a circuit with no LAMP components.");
                }
                for (int i = 0; i < numberOfSwitches; i++)
                {
                    infoRow.add(switchList.getItem(i).getName());
                }
                //infoRow.add("OUTPUT");
                for (int i = 0; i < numberOfLamps; i++)
                {
                    infoRow.add(lampList.getItem(i).getName());
                }
                truthTable.add(infoRow);
                for (int i = 0; i < numberOfInputs; i++)
                {
                    MyList<string> row = new MyList<string>();
                    string inputValues = BinaryFunctions.increaseBinaryLength(BinaryFunctions.denaryToBinary(i), numberOfSwitches);
                    for (int j = 0; j < switchList.getLength(); j++)
                    {
                        row.add(inputValues[j].ToString());

                        try
                        {
                            Switch s = switchList.getItem(j) as Switch;
                            s.setOutputVal(int.Parse(inputValues[j].ToString()));
                        }
                        catch (Exception e) {
                            throw new FormatException("Invalid character found in binary input values.");
                        }

                    }

                    MyList<IGate> sortedComponents = GraphAlgorithms.khansAlgorithm(components);
                    for (int j = 0; j < sortedComponents.getLength(); j++)
                    {
                        sortedComponents.getItem(j).execute();
                    }
                    int result = -1;
                    for (int j = 0; j < lampList.getLength(); j++) {
                        Lamp lamp = lampList.getItem(j);
                        result = lamp.getOutput();
                        row.add(result.ToString());
                    }
                    for (int j = 0; j < components.getLength(); j++)
                    {
                        components.getItem(j).resetGate();
                    }
                    if (result == -1) {
                        throw new Exception("An error occured during the generation of the truth table for the logic circuit. ");
                    }


                    truthTable.add(row);
                }
                return truthTable;
            }
            catch (Exception e) {
                throw new Exception($"An error occurred while generating the truth table: {e.Message}", e);
            }


        }
        //https://www.youtube.com/watch?v=40jliL3KPKY
        public static string generateUnsimplifiedBooleanExpression(MyList<IGate> components)
        {
            try
            {
                int numberOfLamps = 0;
                for (int i = 0; i < components.getLength(); i++)
                {
                    if (components.getItem(i).getGateType() == "LAMP")
                    {
                        numberOfLamps++;
                    }
                }
                if (numberOfLamps == 0)
                {
                    throw new InvalidOperationException("Cannot generate a boolean expression from a circuit with no lamps.");
                }
                else if (numberOfLamps > 1) {
                    throw new InvalidOperationException("Cannot generate a boolean expression from a circuit with more than 1 lamp.");
                }
                    MyList<MyList<string>> truthTable = generateTruthTable(components);
                if (truthTable == null || truthTable.getLength() < 2)
                {
                    throw new InvalidOperationException("Cannot generate a boolean expression from an empty or incorrect truth table.");
                }

                MyList<string> infoRow = truthTable.getItem(0);
                dynamic[] truthTableArray = truthTable.getList();
                string expression = "";
                for (int i = 1; i < truthTable.getLength(); i++)
                {
                    MyList<string> row = truthTable.getItem(i);
                    if (row.getLength() != infoRow.getLength())
                    {
                        throw new InvalidDataException($"Row {i} has an invalid number of columns.");
                    }
                    string endTerm = row.getItem(row.getLength() - 1).ToString();
                    string minTerm = "";
                    if (endTerm == "1")
                    {
                        for (int j = 0; j < row.getLength() - 1; j++)
                        {
                            if (row.getItem(j).ToString() == "0")
                            {
                                minTerm = minTerm + "·" + "¬" + infoRow.getItem(j);
                            }
                            else
                            {
                                minTerm = minTerm + "·" + infoRow.getItem(j);
                            }
                        }
                        if (!string.IsNullOrEmpty(minTerm))
                            minTerm = minTerm.Remove(0, 1);
                    }
                    if (minTerm != "")
                    {
                        expression = expression + "+" + "(" + minTerm + ")";
                    }
                }
                if (!string.IsNullOrEmpty(expression))
                    expression = expression.Remove(0, 1);
                return expression;
            }
            catch (Exception e) {
                throw new InvalidOperationException($"An error occurred while generating the boolean expression: {e.Message}", e);
            }
         
        }
        public static string generateSimplifiedBooleanExpression(MyList<IGate> components)
        {
            try
            {
                int numberOfLamps = 0;
                for (int i = 0; i < components.getLength(); i++)
                {
                    if (components.getItem(i).getGateType() == "LAMP")
                    {
                        numberOfLamps++;
                    }
                }
                if (numberOfLamps == 0)
                {
                    throw new InvalidOperationException("Cannot generate a boolean expression from a circuit with no lamps.");
                }
                else if (numberOfLamps > 1)
                {
                    throw new InvalidOperationException("Cannot generate a boolean expression from a circuit with more than 1 lamp.");
                }
                string expression = "";
                MyList<MyList<string>> truthTable = generateTruthTable(components);
                if (truthTable == null || truthTable.getLength() < 2)
                {
                    throw new InvalidOperationException("Cannot generate a boolean expression from an empty or incorrect truth table.");
                }
                MyList<string> infoRow = truthTable.getItem(0);
                MyList<string> binaryMinterms = new MyList<string>();
                MyList<dynamic[]> primeImplicants = new MyList<dynamic[]>();
                MyList<string> stringImplicants = new MyList<string>();
                MyList<MyList<dynamic[]>> tables = new MyList<MyList<dynamic[]>>();
                // CREATION OF THE FIRST TABLE
                for (int i = 1; i < truthTable.getLength(); i++)
                {
                    MyList<string> row = truthTable.getItem(i);
                    if (row == null || row.getLength() < 2)
                    {
                        throw new InvalidDataException($"Truth table row {i} is incorrect.");
                    }
                    string endTerm = row.getItem(row.getLength() - 1).ToString();
                    if (endTerm == "1")
                    {
                        string binaryMinterm = "";
                        for (int j = 0; j < row.getLength() - 1; j++)
                        {
                            binaryMinterm = binaryMinterm + row.getItem(j);
                        }
                        binaryMinterms.add(binaryMinterm);
                    }
                }
                if (binaryMinterms.getLength() == 0) {
                    return "0";
                }
                MyList<dynamic[]> firstTable = new MyList<dynamic[]>();
                for (int i = 0; i < binaryMinterms.getLength(); i++)
                {
                    int ones = BinaryFunctions.count1s(binaryMinterms.getItem(i));
                    bool groupExists = false;
                    int groupIndex = -1;
                    for (int j = 0; j < firstTable.getLength(); j++)
                    {
                        if (firstTable.getItem(j)[0] == ones)
                        {
                            groupExists = true;
                            groupIndex = j;
                        }
                    }
                    if (groupExists == true)
                    {
                        MyList<dynamic> mintermInfo = new MyList<dynamic>();
                        string mintermName = "m" + BinaryFunctions.binaryToDenary(binaryMinterms.getItem(i));
                        mintermInfo.add(mintermName);
                        mintermInfo.add(binaryMinterms.getItem(i));
                        mintermInfo.add(false);
                        firstTable.getItem(groupIndex)[1].add(mintermInfo);
                    }
                    else
                    {
                        dynamic[] newRow = new dynamic[2];
                        newRow[0] = ones;
                        MyList<MyList<dynamic>> mintermInfos = new MyList<MyList<dynamic>>();
                        MyList<dynamic> mintermInfo = new MyList<dynamic>();
                        string mintermName = "m" + BinaryFunctions.binaryToDenary(binaryMinterms.getItem(i));
                        mintermInfo.add(mintermName);
                        mintermInfo.add(binaryMinterms.getItem(i));
                        mintermInfo.add(false);
                        mintermInfos.add(mintermInfo);
                        newRow[1] = mintermInfos;
                        firstTable.add(newRow);
                    }

                }
                // END OF CREATION OF THE FIRST TABLE
                // BEGINNING OF ITERATIVE COMBINATION OF TABLES
                tables.add(firstTable);
                string binary = binaryMinterms.getItem(0);
                if (string.IsNullOrEmpty(binary))
                {
                    return "0";
                }
                int tableIterations = binary.Length - 1;
                for (int i = 1; i <= tableIterations; i++)
                {
                    MyList<dynamic[]> newTable = new MyList<dynamic[]>();
                    MyList<dynamic[]> lastTable = tables.getItem(i - 1);
                    for (int j = 0; j < lastTable.getLength() - 1; j++)
                    {
                        MyList<MyList<dynamic>> currentRowMinterms = lastTable.getItem(j)[1];
                        MyList<MyList<dynamic>> nextRowMinterms = lastTable.getItem(j + 1)[1];

                        for (int k = 0; k < currentRowMinterms.getLength(); k++)
                        {
                            MyList<dynamic> cRM = currentRowMinterms.getItem(k);

                            for (int l = 0; l < nextRowMinterms.getLength(); l++)
                            {
                                MyList<dynamic> nRM = nextRowMinterms.getItem(l);
                                string binary1 = cRM.getItem(1);
                                string binary2 = nRM.getItem(1);
                                dynamic[] res = BinaryFunctions.areDifferentBy1(binary1, binary2);
                                if (res[0] == true)
                                {
                                    string newBinary = binary1.Substring(0, res[1]) + "_" + binary1.Substring(res[1] + 1);
                                    string newMintermName = cRM.getItem(0) + nRM.getItem(0);

                                    cRM.setVal(2, true);
                                    nRM.setVal(2, true);
                                    bool groupExists = false;
                                    int groupIndex = -1;
                                    for (int m = 0; m < newTable.getLength(); m++)
                                    {
                                        if (newTable.getItem(m)[0] == lastTable.getItem(j)[0])
                                        {
                                            groupExists = true;
                                            groupIndex = j;
                                        }
                                    }
                                    if (groupExists == true)
                                    {
                                        MyList<dynamic> newMintermInfo = new MyList<dynamic>();
                                        newMintermInfo.add(newMintermName);
                                        newMintermInfo.add(newBinary);
                                        newMintermInfo.add(false);
                                        newTable.getItem(j)[1].add(newMintermInfo);
                                    }
                                    else
                                    {
                                        dynamic[] newRow = new dynamic[2];
                                        newRow[0] = lastTable.getItem(j)[0];
                                        MyList<MyList<dynamic>> mintermInfos = new MyList<MyList<dynamic>>();
                                        MyList<dynamic> mintermInfo = new MyList<dynamic>();
                                        mintermInfo.add(newMintermName);
                                        mintermInfo.add(newBinary);
                                        mintermInfo.add(false);
                                        mintermInfos.add(mintermInfo);
                                        newRow[1] = mintermInfos;
                                        newTable.add(newRow);
                                    }


                                }
                            }
                        }
                    }
                    tables.add(newTable);
                    if (i == tableIterations)
                    {
                        for (int j = 0; j < newTable.getLength(); j++)
                        {
                            MyList<MyList<dynamic>> minterms = newTable.getItem(j)[1];
                            for (int k = 0; k < minterms.getLength(); k++)
                            {
                                MyList<dynamic> minterm = minterms.getItem(k);
                                dynamic[] primeImplicantInfo = new dynamic[2];
                                primeImplicantInfo[0] = minterm.getItem(0);
                                primeImplicantInfo[1] = minterm.getItem(1);
                                if (stringImplicants.doesContain(minterm.getItem(1)) == false)
                                {

                                    primeImplicants.add(primeImplicantInfo);
                                    stringImplicants.add(minterm.getItem(1));
                                }

                            }
                        }
                    }
                    for (int j = 0; j < lastTable.getLength(); j++)
                    {
                        MyList<MyList<dynamic>> minterms = lastTable.getItem(j)[1];
                        for (int k = 0; k < minterms.getLength(); k++)
                        {
                            MyList<dynamic> minterm = minterms.getItem(k);

                            if (minterm.getItem(2) == false)
                            {
                                dynamic[] primeImplicantInfo = new dynamic[2];
                                stringImplicants.add(minterm.getItem(1));
                                primeImplicantInfo[0] = minterm.getItem(0);
                                primeImplicantInfo[1] = minterm.getItem(1);
                                primeImplicants.add(primeImplicantInfo);
                            }

                        }
                    }
                }
                //END OF ITERATIVE COMBINATION OF TABLES
                //BEGINNING OF PRODUCTION OF BOOLEAN EXPRESSION
                MyList<string> essentialPrimeImplicants = new MyList<string>();
                for (int i = 0; i < primeImplicants.getLength(); i++)
                {
                    MyList<string> notItemsMinterms = new MyList<string>();
                    for (int j = 0; j < primeImplicants.getLength(); j++)
                    {
                        if (j != i)
                        {
                            if (FileHandling.isNullOrWhiteSpace(primeImplicants.getItem(j)[0]) == false) {
                                MyList<string> minterms = BinaryFunctions.splitString(primeImplicants.getItem(j)[0], 'm');
                                for (int k = 0; k < minterms.getLength(); k++)
                                {
                                    notItemsMinterms.add(minterms.getItem(k));
                                }
                            }
                           
                        }
                    }
                    MyList<string> thisItemsMinterms = BinaryFunctions.splitString(primeImplicants.getItem(i)[0], 'm');
                    bool isEssential = false;
                    for (int j = 0; j < thisItemsMinterms.getLength(); j++)
                    {
                        if (notItemsMinterms.doesContain(thisItemsMinterms.getItem(j)) == false)
                        {
                            isEssential = true;
                        }
                    }
                    if (isEssential == true)
                    {
                        essentialPrimeImplicants.add(primeImplicants.getItem(i)[1]);
                    }
                }
                for (int i = 0; i < essentialPrimeImplicants.getLength(); i++)
                {
                    string binaryPI = essentialPrimeImplicants.getItem(i);
                    string mT = "";
                    for (int j = 0; j < binaryPI.Length; j++)
                    {
                        if (binaryPI[j] == '0')
                        {
                            mT = mT + "·" + "¬" + infoRow.getItem(j);

                        }
                        else if (binaryPI[j] == '1')
                        {
                            mT = mT + "·" + infoRow.getItem(j);
                        }
                    }
                    if (!string.IsNullOrEmpty(mT))
                        mT = mT.Remove(0, 1);
                    if (mT != "")
                    {
                        expression = expression + "+" + "(" + mT + ")";
                    }
                }
                if (!string.IsNullOrEmpty(expression))
                    expression = expression.Remove(0, 1);
                //BOOLEAN EXPRESSION MADE
                return expression;
            }
            catch (Exception e){
                throw new Exception($"An error occurred while generating the simplified boolean expression: {e.Message}", e);
            }
           

        }
        public static MyList<MyList<string>> generateTruthTableFromMinterms(MyList<int> minterms, int noOfSwitches)
        {
            try {
                if (noOfSwitches <= 0)
                {
                    throw new ArgumentException("The number of switches must be a positive integer.");
                }

                if (minterms == null)
                {
                    minterms = new MyList<int>();
                }

                MyList<MyList<string>> truthTable = new MyList<MyList<string>>();
                int numberOfInputs = BinaryFunctions.pow(2, noOfSwitches);
                for (int i = 0; i < numberOfInputs; i++)
                {
                    string rowStr = "";
                    MyList<string> row = new MyList<string>();
                    bool isOne = false;
                    for (int j = 0; j < minterms.getLength(); j++)
                    {
                        if (minterms.getItem(j) == i)
                        {
                            isOne = true;
                        }
                    }
                    if (isOne == false)
                    {
                        rowStr = BinaryFunctions.increaseBinaryLength(BinaryFunctions.denaryToBinary(i), noOfSwitches) + "0";
                    }
                    else
                    {
                        rowStr = BinaryFunctions.increaseBinaryLength(BinaryFunctions.denaryToBinary(i), noOfSwitches) + "1";
                    }
                    for (int j = 0; j < rowStr.Length; j++)
                    {
                        try
                        {
                            row.add(rowStr[j].ToString());
                        }
                        catch (Exception e) {
                            throw new InvalidDataException($"Invalid character found in binary string: {rowStr[j]}");
                        }

                    }
                    truthTable.add(row);
                }
                return truthTable;
            } catch (Exception e) {
                throw new Exception($"An error occurred while generating the truth table: {e.Message}", e);
            }
        }        
        public static void displayTruthTable(MyList<MyList<string>> truthTable)
        {
            try
            {
                dynamic[] truthTableArray = truthTable.getList();
                for (int i = 0; i < truthTableArray.Length; i++)
                {
                    MyList<string> row = truthTableArray[i];
                    for (int j = 0; j < row.getLength() - 1; j++)
                    {
                        Console.Write(row.getItem(j));
                    }
                    Console.Write("|" + row.getItem(row.getLength() - 1));
                    Console.WriteLine();
                }
                Console.ReadLine();
            }
            catch (Exception e) {
                Console.WriteLine("An error occured when displaying the truth table: {0}", e.Message);
            }

            }
        public static void displayCircuit(MyList<IGate> components) {
            for (int i = 0; i < components.getLength(); i++) { 
                IGate component = components.getItem(i);
                Debug.WriteLine("NEW COMPONENT.");
                Debug.WriteLine(component.getName());
                if (component.getGateType() == "LAMP")
                {
                    Lamp l = component as Lamp;
                    Debug.WriteLine("INPUT:" + l.getInputs().getItem(0).getPreviousOutputGate());
                }
                else if (component.getGateType() == "SWITCH")
                {
                    Switch sw = component as Switch;
                    OutputNode swOutput = sw.getOutput();
                    string[] nextOwners = swOutput.getNextInputOwnerGate();
                    for (int j = 0; j < nextOwners.Length; j++) {
                        Debug.WriteLine("OUTPUT " + (j + 1).ToString() + ":" + nextOwners[j]);
                    }
                }
                else if (component.getGateType() == "CUSTOM")
                {
                    CustomGate cg = component as CustomGate;
                    MyList<InputNode> inputs = cg.getInputs();
                    for (int j = 0; j < inputs.getLength(); j++) {
                        Debug.WriteLine("INPUT " + (j + 1) + ":" + inputs.getItem(j).getPreviousOutputGate());
                    }
                    OutputNode cgOutput = cg.getOutput();
                    string[] nextOwners = cgOutput.getNextInputOwnerGate();
                    for (int j = 0; j < nextOwners.Length; j++)
                    {
                        Debug.WriteLine("OUTPUT " + (j + 1).ToString() + ":" + nextOwners[j]);
                    }
                }
                else {
                    Gate cg = component as Gate;
                    MyList<InputNode> inputs = cg.getInputs();
                    for (int j = 0; j < inputs.getLength(); j++)
                    {
                        Debug.WriteLine("INPUT " + (j + 1) + ":" + inputs.getItem(j).getPreviousOutputGate());
                    }
                    OutputNode cgOutput = cg.getOutput();
                    string[] nextOwners = cgOutput.getNextInputOwnerGate();
                    for (int j = 0; j < nextOwners.Length; j++)
                    {
                        Debug.WriteLine("OUTPUT " + (j + 1).ToString() + ":" + nextOwners[j]);
                    }
                }
            }
        }

    }
}
