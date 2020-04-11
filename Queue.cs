using System;
using System.Collections;

namespace QueueBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a queue 
            // Using Queue class 
            Queue myQueue = new Queue();

            // Adding elements in Queue 
            // Using Enqueue() method 
            myQueue.Enqueue("Bob");
            myQueue.Enqueue("Lacy");
            myQueue.Enqueue("Greg");
            myQueue.Enqueue("Cece");

            // Accessing the elements 
            // of myQueue Queue 
            // Using foreach loop 
            foreach (var ele in myQueue)
            {
                Console.WriteLine(ele);
            }

            Console.WriteLine("\n");
            Console.WriteLine("Total elements present in my_queue: {0}", myQueue.Count);

            // Removes the first element in the queue. (LIFO)
            myQueue.Dequeue();

            // After Dequeue method 
            Console.WriteLine("Total elements present in my_queue: {0}", myQueue.Count);

            // Remove all the elements from the queue 
            myQueue.Clear();

            // After Clear method 
            Console.WriteLine("Total elements present in my_queue: {0}",
                                                        myQueue.Count);


           
        }
    }
}
