using System;
using System.Drawing.Text;
using System.Linq;

namespace Logic_Gate_Builder
{
    public class MyList<T>
    {
        private int length;
        private T[] theList;
        public MyList() {
            length = 0;
            theList = new T[length];
        }
        public void add(T item) {
            
            this.length++;
            T[] tempA = this.theList;
            this.theList = new T[length];
            for (int i = 0; i < tempA.Count(); i++) { 
                this.theList[i] = tempA[i];
            }
            this.theList[length - 1] = item;
                
        }
        public void removeAt(int index)
        {
            if (index < 0 || index >= length) {
                throw new IndexOutOfRangeException("Index must be within the bounds of the list.");
            }
            length--;
            bool ignoredDone = false;
            T[] newList = new T[length];
            for (int i = 0; i < theList.Count(); i++) {
                if (ignoredDone == false)
                {
                    if (i == index)
                    {
                        ignoredDone = true;
                    }
                    else
                    {
                        newList[i] = theList[i];
                    }
                }
                else {
                    newList[i - 1] = theList[i];
                }
            }
            theList = newList;
        }
        public void insert(int index, T item) {
            if (index < 0 || index > length)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds of the list.");
            }
            length++;
            T[] newL = new T[length];
            bool insertDone = false;
            for (int i = 0; i < newL.Count(); i++) {
                if (insertDone == false)
                {
                    if (i == index)
                    {
                        insertDone = true;
                        newL[i] = item;
                    }
                    else {
                        newL[i] = theList[i];
                    }
                }
                else {
                    newL[i] = theList[i - 1];
                }
            }
            theList = newL;
        }
        public T getItem(int index) {
            if (index < 0 || index >= length)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds of the list.");
            }
            return theList[index];
        }
        public T[] getList() {
            return this.theList;
        }
        public int getLength() {
            return theList.Length;
        }
        public bool doesContain(T item) {
            for (int i = 0; i < theList.Length; i++) {
                if (theList[i].Equals(item) == true) {
                    return true;
                }
            }
            return false;
        }
        public void setVal(int index, T val) {
            if (index < 0 || index >= length)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds of the list.");
            }
            theList[index] = val;
        }
        public void randomiseList()
        {
            Random rnd = new Random();
            T[] newL = new T[length];
            int count = 0;
            MyList<int> usedIndex = new MyList<int>();
            while (count < this.length) {
                int index = rnd.Next(0,length);
                if (usedIndex.doesContain(index) == false) { 
                    usedIndex.add(index);
                    newL[count] = theList[index];
                    count++;
                }

            }
            theList = newL;
        }
    }
}
