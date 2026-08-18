using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Phone.Notification.Management;
using static System.Net.Mime.MediaTypeNames;

namespace Logic_Gate_Builder.Functionality_Classes
{
    public class BinaryFunctions
    {

        public static bool isNumeric(char c) {
            if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
            {
                return true;
            }
            else {
                return false;
            }
        }
        public static int pow(int b, int e)
        {
            if (e < 0) {
                if (b == 0)
                {
                    throw new DivideByZeroException("Cannot put 0 to a negative power.");
                }
                else {
                    throw new ArgumentOutOfRangeException(nameof(e), e, "The exponent 'e' cannot be negative. This function only supports non-negative exponents.");
                }
            }

            int total = 1;
            for (int i = 0; i < e; i++)
            {
                try
                {
                    checked {
                        total = total * b;
                    }
                }
                catch (OverflowException ex) {
                    throw new OverflowException("The result exceeded the maximum value for an integer" + int.MaxValue.ToString() + ".");
                }

            }
            return total;
        }
        public static string denaryToBinary(int denary)
        {
            if (denary < 0) {
                throw new ArgumentOutOfRangeException(nameof(denary), denary, "The denary input cannot be negative. This function only supports non-negative denary values.");
            }

            if (denary > 0)
            {
                string result = "";
                int maxPower = 0;
                try
                {
                    while (pow(2, maxPower) <= denary)
                    {
                        maxPower = maxPower + 1;
                    }


                    maxPower = maxPower - 1;
                    denary = denary - pow(2, maxPower);
                    result = result + "1";
                    while (maxPower > 0)
                    {
                        maxPower--;
                        if (pow(2, maxPower) <= denary)
                        {
                            denary = denary - pow(2, maxPower);
                            result = result + "1";
                        }
                        else
                        {
                            result = result + "0";
                        }
                    }
                    return result;
                }
                catch (OverflowException ex)
                {
                    throw new ArgumentOutOfRangeException(nameof(denary), denary, "The denary input is too large to be converted into binary.");
                }
            }
            else
            {
                return "0";
            }
        }
        public static string increaseBinaryLength(string binary, int desiredLength)
        {
            if (binary == null)
            {
                throw new ArgumentNullException(nameof(binary), "The inputted binary cannot be null.");
            }
            if (desiredLength < binary.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(desiredLength), desiredLength, "The desired length cannot be less than the current length of the binary string.");
            }
            if (binary.Length == desiredLength)
            {
                return binary;
            }
            else
            {
                string modification = "";
                for (int i = 0; i < (desiredLength - binary.Length); i++)
                {
                    modification += "0";
                }
                return modification + binary;

            }

        }
        public static int binaryToDenary(string binary)
        {
            if (binary == null)
            {
                throw new ArgumentNullException(nameof(binary), "The inputted binary cannot be null.");
            }
            int total = 0;
            string reversedBinary = reverseString(binary);
            for (int i = 0; i < reversedBinary.Length; i++)
            {
                if (reversedBinary[i] == '1')
                {
                    try {
                        checked {
                            total = total + pow(2, i);
                        }
                    }
                    catch (OverflowException ex)
                    {
                        throw new ArgumentOutOfRangeException(nameof(binary), binary, "The binary input is too large to be converted into denary.");
                    }

                }
            }
            return total;
        }
        public static string reverseString(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text), "The inputted text cannot be null.");
            }
            Stack<char> reverseStack = new Stack<char>();
            string reverseString = "";
            for (int i = 0; i < text.Length; i++)
            {
                reverseStack.push(text[i]);
            }
            int initialLength = reverseStack.getLength();
            for (int i = 0; i < initialLength; i++)
            {
                reverseString = reverseString + reverseStack.pop();
            }
            return reverseString;
        }
        public static dynamic[] areDifferentBy1(string binary1, string binary2)
        {
            if (binary1 == null)
            {
                throw new ArgumentNullException(nameof(binary1), "The inputted binary cannot be null.");
            }
            if (binary2 == null)
            {
                throw new ArgumentNullException(nameof(binary2), "The inputted binary cannot be null.");
            }
            int differenceIndex = -1;
            bool areDifferentBy1 = false;
            int differences = 0;
            if (binary1.Length != binary2.Length)
            {
                throw new ArgumentException("The inputted binary strings must have the same length.");
            }
            for (int i = 0; i < binary1.Length; i++)
            {
                if (binary1[i] != binary2[i])
                {
                    differenceIndex = i;
                    differences++;
                }
            }
            if (differences == 1)
            {
                areDifferentBy1 = true;
            }
            else
            {
                differenceIndex = -1;
            }
            dynamic[] returnArr = new dynamic[2];
            returnArr[0] = areDifferentBy1;
            returnArr[1] = differenceIndex;
            return returnArr;
        }
        public static int count1s(string bin)
        {
            if (bin == null)
            {
                throw new ArgumentNullException(nameof(bin), "The inputted binary cannot be null.");
            }
            int ones = 0;
            for (int i = 0; i < bin.Length; i++)
            {
                if (bin[i] == '1')
                {
                    ones++;
                }
            }
            return ones;
        }
        public static MyList<string> splitString(string str, char c)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str), "The inputted string cannot be null.");
            }
            MyList<string> returnList = new MyList<string>();
            bool doesSeperatorExist = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c)
                {
                    doesSeperatorExist = true;
                }
            }
            if (doesSeperatorExist == false)
            {
                returnList.add(str);
                return returnList;
            }
            string runningTotal = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c)
                {
                    returnList.add(runningTotal);
                    runningTotal = "";

                }
                else
                {
                    runningTotal = runningTotal + str[i];
                }
            }
            if (runningTotal != "")
            {
                returnList.add(runningTotal);
            }
            return returnList;
        }
        public static bool areAtLeast2DecimalPlaces(string number) {
            bool dotRead = false;
            string dec = "";
            for (int i = 0; i < number.Length; i++) {
                if (dotRead == false)
                {
                    if (number[i] == '.')
                    {
                        dotRead = true;
                    }
                }
                else { 
                    dec = dec + number[i];
                }
            }
            if (dec.Length >= 2)
            {
                return true;
            }
            else {
                return false;
            }

        }
    }
}
