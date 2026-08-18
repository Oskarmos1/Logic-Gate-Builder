using Logic_Gate_Builder.UI_Classes.Command_Classes;
using System;
using System.Diagnostics;
//using System.Windows.Input;

namespace Logic_Gate_Builder
{
    public class Stack<T>
    {
        private MyList<T> theStack;
        public Stack() {
            theStack = new MyList<T>();
        }
        public void push(T item) {


            theStack.add(item);
            //Debug.WriteLine("PUSHED TO STACK");
            //debugStack();
        }
        public T pop() {


            if (isEmpty() == true)
            {
                throw new InvalidOperationException("Cannot pop from an empty stack.");
            }
            T item = theStack.getItem(theStack.getLength() - 1);
            theStack.removeAt(theStack.getLength() - 1);
            //Debug.WriteLine("POPPED FROM STACK");
            //debugStack();
            return item;
        }
        public bool isEmpty() {
            if (theStack.getLength() == 0)
            {
                return true;
            }
            else {
                return false;
            }
        }
        public T[] getStack()
        {
            return theStack.getList();
        }
        public int getLength()
        {
            return theStack.getLength();
        }

        public T peek(int index) {
            return theStack.getItem(index);
        }

        public void removeAt(int index) {
            theStack.removeAt(index);
        }

        private void debugStack()
        {
            try
            {
                Debug.WriteLine("NEW RUN");
                for (int i = 0; i < this.theStack.getLength(); i++) {
                    ICommand x = theStack.getItem(i) as ICommand;
                    Debug.WriteLine(x.debugInfo());
                    Debug.WriteLine("NEXT");
               
                }
            }
            catch { }
        }
    }
}

