using System;

namespace Logic_Gate_Builder
{
    public class Queue<T>
    {
        private MyList<T> theQueue;
        public Queue() {
            theQueue = new MyList<T>();
        }
        public void enQueue(T item) {
            theQueue.insert(theQueue.getLength(), item);
        }
        public T deQueue() {
            if (isEmpty() == true) {
                throw new InvalidOperationException("Cannot dequeue from an empty queue.");
            }
            T i = theQueue.getItem(0);
            theQueue.removeAt(0);
            return i;
        }
        public bool isEmpty() {
            if (theQueue.getLength() == 0)
            {
                return true;
            }
            else {
                return false;
            }
        }
        public T[] getQueue() {
            return theQueue.getList();
        }
        public int getLength() {
            return theQueue.getLength();
        }
        public bool doesContain(T item)
        {
            if (theQueue.doesContain(item) == true)
            {
                return true;
            }
            else {
                return false;
            }
            
        }
    }
}
