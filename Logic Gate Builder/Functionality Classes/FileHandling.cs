using Logic_Gate_Builder.Logic_Gate_Classes;
using Newtonsoft.Json;
using System;
//using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Logic_Gate_Builder.Functionality_Classes
{
    public class FileHandling
    {
        public static int extractNumberFromString(string str) {
            if (isNullOrWhiteSpace(str) == true)
            {
                throw new ArgumentNullException(nameof(str), "The inputted string cannot be null.");
            }
            string integerString = "";
            bool intergersBegan = false;
            for (int i = 0; i < str.Length; i++) {
                if (intergersBegan == false)
                {
                    try
                    {
                        int x = int.Parse(str[i].ToString());
                        intergersBegan = true;
                        integerString = integerString + str[i];
                    }
                    catch
                    {

                    }
                }
                else {
                    integerString = integerString + str[i];
                }

            }
            try
            {
                return int.Parse(integerString);
            }
            catch { 
                throw new ArgumentException("String must include numbers", nameof(str));
            }


        }
        public static bool isNullOrWhiteSpace(string fileName) {
            if (fileName == null)
            {
                return true;
            }
            else {
                if (fileName.Trim() == "")
                {
                    return true;
                }
                
                return false;
                
            }
        }
        public static void saveLogicCircuit(MyList<IGate> components, string fileName)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
            if (isNullOrWhiteSpace(fileName) == true) 
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            }

            MyList<IGate> sortedComponents;
            try
            {
                sortedComponents = GraphAlgorithms.khansAlgorithm(components);
            }
            catch (Exception ex) {
                throw new IOException($"Topological sort failed {ex.Message}",ex);
            }
            StreamWriter sw = new StreamWriter(fileName);
            try
            {
               
                for (int i = 0; i < sortedComponents.getLength(); i++)
                {
                    if (sortedComponents.getItem(i) == null)
                    {
                        throw new InvalidOperationException($"The component at index {i} in the sorted components list was null.");
                    }

                    MyList<string> info = sortedComponents.getItem(i).returnGateInfo();
                    if (info == null)
                    {
                        throw new InvalidOperationException($"The component '{sortedComponents.getItem(i).getName()}' at the index {i} returned invalid gate info. Cannot save circuit.");
                    }
                    if (info.getLength() < 2)
                    {
                        throw new InvalidOperationException($"The component '{sortedComponents.getItem(i).getName()}' at the index {i} returned invalid gate info. Cannot save circuit.");
                    }
                    string typeLine = "Ty:" + info.getItem(0);
                    string nameLine = "Na:" + info.getItem(1);
                    if (isNullOrWhiteSpace(info.getItem(0)))
                    {
                        throw new InvalidOperationException($"Component at index {i} has a null or empty gate type. Cannot save circuit.");
                    }
                    if (isNullOrWhiteSpace(info.getItem(1)))
                    {
                        throw new InvalidOperationException($"Component at index {i} has a null or empty name. Cannot save circuit.");
                    }
                    sw.WriteLine("NEW COMPONENT");
                    sw.WriteLine(typeLine);
                    sw.WriteLine(nameLine);
                    if (info.getItem(0) != "CUSTOM")
                    {
                        for (int j = 2; j < info.getLength(); j++)
                        {
                            string inputLine = "I" + (j - 1).ToString() + ":" + info.getItem(j);
                            sw.WriteLine(inputLine);
                        }
                    }
                    else
                    {
                        string mintermsLine = "MI:" + info.getItem(2);
                        sw.WriteLine(mintermsLine);
                        for (int j = 3; j < info.getLength(); j++)
                        {
                            string inputLine = "I" + (j - 2).ToString() + ":" + info.getItem(j);
                            sw.WriteLine(inputLine);
                        }
                    }

                }
                sw.WriteLine("NEW COMPONENT");
                sw.Close();
            }
            catch (Exception e)
            {
                throw new IOException($"An error occured when trying to save the file: {e.Message}", e);
            }
            finally {
                if (sw != null) {
                    sw.Dispose();
                }
            }
        }
        public static MyList<IGate> openLogicCircuit(string fileName)
        {
            if (isNullOrWhiteSpace(fileName) == true)
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            }
            MyList<IGate> components = new MyList<IGate>();
            StreamReader sr = null;
            MyList<string> componentInfo = new MyList<string>();
            try
            {
                sr = new StreamReader(fileName);
                int lineNumber = 0;
                while (sr.Peek() > -1)
                {
                    lineNumber++;
                    string lineRead = sr.ReadLine().Trim();
                    if (lineRead == "NEW COMPONENT")
                    {
                        if (componentInfo.getLength() != 0)
                        {
                            if (componentInfo.getLength() < 2)
                            {
                                throw new IOException($"Line {lineNumber}: Component data is incomplete.");
                            }
                            string type = componentInfo.getItem(0).ToString();
                            string name = componentInfo.getItem(1).ToString();
                            if (isNullOrWhiteSpace(type))
                            {
                                throw new IOException($"Line {lineNumber}: Component type is missing.");
                            }
                            if (isNullOrWhiteSpace(name))
                            {
                                throw new IOException($"Line {lineNumber}: Component name is missing.");
                            }

                            string minterms = "";
                            int startReadInputs = -1;
                            if (type == "CUSTOM")
                            {
                                if (componentInfo.getLength() < 3)
                                {
                                    throw new IOException($"Line {lineNumber}: CUSTOM component '{name}' is missing minterm info.");
                                }
                                minterms = componentInfo.getItem(2).ToString();
                                startReadInputs = 3;
                            }
                            else
                            {
                                startReadInputs = 2;
                            }
                            MyList<IGate> inputComponents = new MyList<IGate>();
                            for (int i = startReadInputs; i < componentInfo.getLength(); i++)
                            {
                                for (int j = 0; j < components.getLength(); j++)
                                {
                                    if (components.getItem(j).getName() == componentInfo.getItem(i))
                                    {

                                        inputComponents.add(components.getItem(j));
                                    }

                                }
                            }
                            int passingNum;
                            try
                            {
                                passingNum = extractNumberFromString(name);
                            }
                            catch (Exception e)
                            {
                                throw new IOException($"Line {lineNumber}: Component name '{name}' must end with a valid number.");

                            }
                            switch (type)
                            {
                                case "SWITCH":
                                    Switch newSwitch = new Switch(passingNum);
                                    if (inputComponents.getLength() != 0) {
                                        throw new IOException($"End of file: SWITCH '{name}' should not have inputs, but found {inputComponents.getLength()}.");
                                    }
                                    components.add(newSwitch);
                                    break;
                                case "AND":
                                    AND newAnd = new AND(passingNum);

                                    for (int j = 0; j < inputComponents.getLength(); j++)
                                    {
                                        dynamic gate = inputComponents.getItem(j);
                                        newAnd.connectToInput(gate.getOutput(), j + 1);
                                    }
                                    components.add(newAnd);
                                    break;
                                case "OR":
                                    OR newOR = new OR(passingNum);
                                    for (int j = 0; j < inputComponents.getLength(); j++)
                                    {
                                        dynamic gate = inputComponents.getItem(j);
                                        newOR.connectToInput(gate.getOutput(), j + 1);
                                    }
                                    components.add(newOR);
                                    break;

                                case "LAMP":
                                    Lamp newLamp = new Lamp(passingNum);
                                    dynamic g = inputComponents.getItem(0);
                                    newLamp.connectToInput(g.getOutput(), 0);
                                    components.add(newLamp);
                                    break;
                                case "NAND":
                                    NAND newNand = new NAND(passingNum);
                                    for (int j = 0; j < inputComponents.getLength(); j++)
                                    {
                                        dynamic gate = inputComponents.getItem(j);
                                        newNand.connectToInput(gate.getOutput(), j + 1);
                                    }
                                    components.add(newNand);
                                    break;
                                case "NOR":
                                    NOR newNor = new NOR(passingNum);
                                    for (int j = 0; j < inputComponents.getLength(); j++)
                                    {
                                        dynamic gate = inputComponents.getItem(j);
                                        newNor.connectToInput(gate.getOutput(), j + 1);
                                    }
                                    components.add(newNor);
                                    break;
                                case "NOT":
                                    NOT newNot = new NOT(passingNum);
                                    for (int j = 0; j < inputComponents.getLength(); j++)
                                    {
                                        dynamic gate = inputComponents.getItem(j);
                                        newNot.connectToInput(gate.getOutput(), j + 1);
                                    }
                                    components.add(newNot);
                                    break;
                                case "XOR":
                                    XOR newXor = new XOR(passingNum);
                                    for (int j = 0; j < inputComponents.getLength(); j++)
                                    {
                                        dynamic gate = inputComponents.getItem(j);
                                        newXor.connectToInput(gate.getOutput(), j + 1);
                                    }
                                    components.add(newXor);
                                    break;
                                case "NXOR":
                                    NXOR newNxor = new NXOR(passingNum);
                                    for (int j = 0; j < inputComponents.getLength(); j++)
                                    {
                                        dynamic gate = inputComponents.getItem(j);
                                        newNxor.connectToInput(gate.getOutput(), j + 1);
                                    }
                                    components.add(newNxor);
                                    break;
                                case "CUSTOM":
                                    MyList<string> strSplitMinterms = BinaryFunctions.splitString(minterms, ',');
                                    MyList<int> intSplitMinterms = new MyList<int>();
                                    for (int i = 0; i < strSplitMinterms.getLength(); i++)
                                    {
                                        intSplitMinterms.add(int.Parse(strSplitMinterms.getItem(i)));
                                    }
                                    MyList<MyList<string>> truthTable = LogicGateFunctions.generateTruthTableFromMinterms(intSplitMinterms, inputComponents.getLength());
                                    //displayTruthTable(truthTable);
                                    CustomGate newCustom = new CustomGate(passingNum, inputComponents.getLength(), truthTable);
                                    for (int j = 0; j < inputComponents.getLength(); j++)
                                    {
                                        dynamic gate = inputComponents.getItem(j);
                                        newCustom.connectToInput(gate.getOutput(), j + 1);
                                    }
                                    components.add(newCustom);
                                    break;
                                default:
                                    throw new InvalidDataException($"Unknown gate type '{type}' found in data.");
                            }
                        }
                        componentInfo = new MyList<string>();
                    }
                    else
                    {
                        componentInfo.add(lineRead.Substring(3));
                    }
                }
            }
            catch (Exception e){
                throw new IOException($"An error occurred while reading the file: {e.Message}", e);
            }
            

            sr.Close();
            return components;
        }
        public static void exportToJson(MyList<IGate> components, string fileName)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
            if (isNullOrWhiteSpace(fileName) == true)
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            }
            try
            {
                List<GateData> gateDatas = componentsToGateDataList(components);
                string jsonString = JsonConvert.SerializeObject(gateDatas, Formatting.Indented);
                File.WriteAllText(fileName, jsonString);
            }
            catch (Exception e)
            {
                throw new Exception($"An unexpected error occurred during export: {e.Message}", e);
            }
        }
        public static MyList<IGate> importFromJson(string fileName)
        {
            if (isNullOrWhiteSpace(fileName) == true)
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            }
            try{
                string jsonString = File.ReadAllText(fileName);
                List<GateData> gateDatas = JsonConvert.DeserializeObject<List<GateData>>(jsonString);
                return gateDataListToComponents(gateDatas);
            }
            catch (Exception e)
            {
                throw new Exception($"An unexpected error occurred during import: {e.Message}", e);
            }

        }
        public static void exportToXML(MyList<IGate> components, string fileName)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
            if (isNullOrWhiteSpace(fileName) == true)
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<GateData>));
                List<GateData> gateDatas = componentsToGateDataList(components);
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, gateDatas);
                }
            }
            catch (Exception e) {
                throw new Exception($"An unexpected error occurred during export: {e.Message}", e);
            }
        }
        public static MyList<IGate> importFromXML(string fileName)
        {
            if (isNullOrWhiteSpace(fileName) == true)
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            }
            try {
                //string jsonString = File.ReadAllText(fileName);
                XmlSerializer serializer = new XmlSerializer(typeof(List<GateData>));
                List<GateData> gateDatas;
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    gateDatas = (List<GateData>)serializer.Deserialize(fs);
                }
                return gateDataListToComponents(gateDatas);
            }
            catch (Exception e)
            {
                throw new Exception($"An unexpected error occurred during import: {e.Message}", e);
            }

        }
        public static void exportToCSV(MyList<IGate> components, string fileName)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
            if (isNullOrWhiteSpace(fileName) == true)
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            }
            string interMediateTextFileName = "intermediateCSVStorage82848729112312390273.txt";
            saveLogicCircuit(components, interMediateTextFileName);
            try
            {
                using (StreamReader sr = new StreamReader(interMediateTextFileName))
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine("Type,Name,Minterms,Inputs");
                    string line;
                    string type = "";
                    string name = "";
                    string minterms = "";
                    List<string> inputs = new List<string>();
                    while (sr.Peek() > -1)
                    {
                        line = sr.ReadLine();
                        if (line == "NEW COMPONENT")
                        {
                            if (type != "")
                            {
                                sw.WriteLine($"{type},{name},{minterms},{string.Join("|", inputs)}");
                            }

                            type = "";
                            name = "";
                            minterms = "";
                            inputs.Clear();
                        }
                        else if (line.StartsWith("Ty:"))
                        {
                            type = line.Substring(3);
                        }
                        else if (line.StartsWith("Na:"))
                        {
                            name = line.Substring(3);
                        }
                        else if (line.StartsWith("MI:"))
                        {
                            minterms = line.Substring(3);
                        }
                        else if (line.StartsWith("I"))
                        {
                            inputs.Add(line.Substring(line.IndexOf(":") + 1));
                        }
                    }
                    if (type != "")
                    {
                        sw.WriteLine($"{type},{name},{minterms},{string.Join("|", inputs)}");
                    }
                }
            }
            catch (Exception e) {
                throw new Exception($"An unexpected error occurred during CSV export: {e.Message}", e);
            }
            finally
            {
                if (File.Exists(interMediateTextFileName))
                {
                    File.Delete(interMediateTextFileName);
                }
            }


        }
        public static MyList<IGate> importFromCSV(string fileName)
        {
            if (isNullOrWhiteSpace(fileName) == true)
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            }
            List<GateData> gatesData = new List<GateData>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string headerLine = sr.ReadLine();

                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        MyList<string> parts = BinaryFunctions.splitString(line, ',');
                        string type = parts.getItem(0);
                        string name = parts.getItem(1);
                        string minterms;
                        try
                        {
                            minterms = parts.getItem(2);
                        }
                        catch (Exception e)
                        {
                            minterms = "";
                        }
                        List<string> inputs = new List<string>();

                        try
                        {
                            MyList<string> inputStrs = BinaryFunctions.splitString(parts.getItem(3), '|');
                            for (int i = 0; i < inputStrs.getLength(); i++)
                            {
                                inputs.Add(inputStrs.getItem(i));
                            }
                        }
                        catch (Exception e)
                        {
                        }
                        ;



                        GateData newGateData = new GateData();
                        newGateData.gateType = type;
                        newGateData.gateName = name;
                        newGateData.minterms = minterms;
                        newGateData.inputs = inputs;

                        gatesData.Add(newGateData);
                    }
                }
                MyList<IGate> components = gateDataListToComponents(gatesData);

                return components;
            }
            catch (Exception e) {
                throw new Exception($"An error occurred during CSV import from '{fileName}': {e.Message}", e);
            }
        }
        public static List<GateData> componentsToGateDataList(MyList<IGate> components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The components list cannot be null.");
            }
            MyList<IGate> sortedComponents = GraphAlgorithms.khansAlgorithm(components);
            List<GateData> gateDatas = new List<GateData>();
            GateData newGateData;
            try
            {
                for (int i = 0; i < sortedComponents.getLength(); i++)
                {
                    IGate gate = sortedComponents.getItem(i);
                    MyList<string> info = gate.returnGateInfo();
                    if (info == null || info.getLength() < 2)
                    {
                        throw new InvalidDataException("Component info is in an invalid format.");
                    }
                    if (info.getItem(0) != "CUSTOM")
                    {
                        List<string> previousOwners = new List<string>();
                        for (int j = 2; j < info.getLength(); j++)
                        {
                            previousOwners.Add(info.getItem(j));
                        }
                        newGateData = new GateData();
                        newGateData.gateType = info.getItem(0);
                        newGateData.gateName = info.getItem(1);
                        newGateData.minterms = "";
                        newGateData.inputs = previousOwners;
                    }
                    else
                    {
                        List<string> previousOwners = new List<string>();
                        if (info.getLength() < 3)
                        {
                            throw new InvalidDataException("Custom gate info is missing minterms.");
                        }
                        for (int j = 3; j < info.getLength(); j++)
                        {
                            previousOwners.Add(info.getItem(j));
                        }

                        newGateData = new GateData();
                        newGateData.gateType = info.getItem(0);
                        newGateData.gateName = info.getItem(1);
                        newGateData.minterms = info.getItem(2);
                        newGateData.inputs = previousOwners;
                    }
                    gateDatas.Add(newGateData);

                }
            }
            catch (Exception e) {
                throw new Exception($"An error occurred while converting components to GateData: {e.Message}", e);
            }

            return gateDatas;
        }
        public static MyList<IGate> gateDataListToComponents(List<GateData> gateDatas)
        {
            if (gateDatas == null)
            {
                throw new ArgumentNullException(nameof(gateDatas), "The gate data list cannot be null.");
            }
            MyList<IGate> components = new MyList<IGate>();
            try
            {
                foreach (GateData gateD in gateDatas)
                {

                    string type = gateD.gateType;
                    string name = gateD.gateName;
                    if (isNullOrWhiteSpace(type) ||isNullOrWhiteSpace(name))
                    {
                        throw new InvalidDataException("Gate type or name cannot be null or empty.");
                    }
                    string minterms = gateD.minterms;
                    List<string> wrongFormatI = gateD.inputs;
                    MyList<IGate> inputComponents = new MyList<IGate>();
                    for (int i = 0; i < wrongFormatI.Count; i++)
                    {
                        for (int j = 0; j < components.getLength(); j++)
                        {
                            if (components.getItem(j).getName() == wrongFormatI[i])
                            {
                                inputComponents.add(components.getItem(j));
                            }

                        }
                    }
                    int passingNum = -1;
                    try
                    {
                        passingNum = int.Parse(name[name.Length - 1].ToString());
                    }
                    catch (Exception ex) {
                        throw new FormatException($"Invalid number format in gate name '{name}'.");
                    }

                    switch (type)
                    {
                        case "SWITCH":
                            Switch newSwitch = new Switch(passingNum);
                            if (inputComponents.getLength() != 0)
                            {
                                throw new IOException($"End of file: SWITCH '{name}' should not have inputs, but found {inputComponents.getLength()}.");
                            }
                            components.add(newSwitch);
                            break;
                        case "AND":
                            AND newAnd = new AND(passingNum);

                            for (int j = 0; j < inputComponents.getLength(); j++)
                            {
                                dynamic gate = inputComponents.getItem(j);
                                newAnd.connectToInput(gate.getOutput(), j + 1);
                            }
                            components.add(newAnd);
                            break;
                        case "OR":
                            OR newOR = new OR(passingNum);
                            for (int j = 0; j < inputComponents.getLength(); j++)
                            {
                                dynamic gate = inputComponents.getItem(j);
                                newOR.connectToInput(gate.getOutput(), j + 1);
                            }
                            components.add(newOR);
                            break;

                        case "LAMP":
                            Lamp newLamp = new Lamp(passingNum);
                            dynamic g = inputComponents.getItem(0);
                            newLamp.connectToInput(g.getOutput(),0);
                            components.add(newLamp);
                            break;
                        case "NAND":
                            NAND newNand = new NAND(passingNum);
                            for (int j = 0; j < inputComponents.getLength(); j++)
                            {
                                dynamic gate = inputComponents.getItem(j);
                                newNand.connectToInput(gate.getOutput(), j + 1);
                            }
                            components.add(newNand);
                            break;
                        case "NOR":
                            NOR newNor = new NOR(passingNum);
                            for (int j = 0; j < inputComponents.getLength(); j++)
                            {
                                dynamic gate = inputComponents.getItem(j);
                                newNor.connectToInput(gate.getOutput(), j + 1);
                            }
                            components.add(newNor);
                            break;
                        case "NOT":
                            NOT newNot = new NOT(passingNum);
                            for (int j = 0; j < inputComponents.getLength(); j++)
                            {
                                dynamic gate = inputComponents.getItem(j);
                                newNot.connectToInput(gate.getOutput(), j + 1);
                            }
                            components.add(newNot);
                            break;
                        case "XOR":
                            XOR newXor = new XOR(passingNum);
                            for (int j = 0; j < inputComponents.getLength(); j++)
                            {
                                dynamic gate = inputComponents.getItem(j);
                                newXor.connectToInput(gate.getOutput(), j + 1);
                            }
                            components.add(newXor);
                            break;
                        case "NXOR":
                            NXOR newNxor = new NXOR(passingNum);
                            for (int j = 0; j < inputComponents.getLength(); j++)
                            {
                                dynamic gate = inputComponents.getItem(j);
                                newNxor.connectToInput(gate.getOutput(), j + 1);
                            }
                            components.add(newNxor);
                            break;
                        case "CUSTOM":
                            MyList<string> strSplitMinterms = BinaryFunctions.splitString(minterms, ',');
                            MyList<int> intSplitMinterms = new MyList<int>();
                            for (int i = 0; i < strSplitMinterms.getLength(); i++)
                            {
                                intSplitMinterms.add(int.Parse(strSplitMinterms.getItem(i)));
                            }
                            MyList<MyList<string>> truthTable = LogicGateFunctions.generateTruthTableFromMinterms(intSplitMinterms, inputComponents.getLength());
                            CustomGate newCustom = new CustomGate(passingNum, inputComponents.getLength(), truthTable);
                            for (int j = 0; j < inputComponents.getLength(); j++)
                            {
                                dynamic gate = inputComponents.getItem(j);
                                newCustom.connectToInput(gate.getOutput(), j + 1);
                            }
                            components.add(newCustom);
                            break;

                        default:
                            throw new InvalidDataException($"Unknown gate type '{type}' found in data.");
                    }
                }
                return components;
            }
            catch (Exception e) {
                throw new Exception($"An error occurred during component creation: {e.Message}", e);
            }

        }
        public static string getFileEnding(string fileName) {
            string ending = "";
            Stack<char> tempStack = new Stack<char>();
            for (int i = 0; i < fileName.Length; i++) { 
                tempStack.push(fileName[i]);
            }
            string reversedName = "";
            while (tempStack.isEmpty() == false) { 
                reversedName = reversedName + tempStack.pop();
            }
            int posInRev = -1;
            for (int i = 0; i < reversedName.Length; i++) {
                if (reversedName[i] == '.') {
                    posInRev = i;
                    break;
                }
            }
            if (posInRev == -1)
            {
                throw new IOException("File directory has no '.' (no file ending.) File cannot be opened.");
            }
            else {
                int actualPos = fileName.Length - 1 - posInRev;
                for (int i = 0; i < fileName.Length; i++)
                {
                    if (i >= actualPos) { 
                        ending = ending + fileName[i];
                    }
                }
                return ending;
            }
        }
       
    }
}
