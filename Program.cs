using System;
using System.Collections;

namespace StacksPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a stack 
            // Using Stack class 
            Stack myStack = new Stack();

            // Adding elements in the Stack 
            // Using Push method 
            myStack.Push("Bottom");
            myStack.Push("Middle");
            myStack.Push("Top");
            // 
            // The Pop method is used to remove the most recent element added to the stack.
            // Remove the comment slashes to see the effect of the Pop method.
            // my_stack.Pop();
         

            // Accessing the elements 
            // of my_stack Stack 
            // Using foreach loop 
            foreach (var elem in myStack)
            {
                Console.WriteLine(elem);
            }
        }
    }
}
