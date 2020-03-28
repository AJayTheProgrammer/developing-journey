// Name: Anthony Jordan

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueenProblem
{
    class Queen
    {
        public int N;
        public int[,] board;

        public Queen(int n)
        {
            N = n;
            board = new int[N, N];
        }

        // fuction to check if a queen can be placed
        public bool isSafe(int r, int c)
        {
            int i, j; 

            for (i = 0; i < c; i++) 
                if (board[r, i] == 1)
                    return false;

            for (i = r, j = c; i >= 0 && j >= 0; i--, j--)
                if (board[i, j] == 1)
                    return false;

            for (i = r, j = c; i < N && j >= 0; i++, j--) 
                if (board[i, j] == 1)
                    return false;

            return true;
        }

        // places queens on the board
        // if the parameter c is greater than or equal to N it will return true
        // then has a for loop to iterate while utilizing the 'isSafe' method to determine if 
        // it can move to the next spot.
        public bool Solve(int c)
        {
            if (c >= N)
                return true;
            for (int i = 0; i<N;i++)
            {
                if (isSafe(i,c))
                {
                    board[i, c] = 1;
                    if (Solve(c + 1))
                        return true;
                    board[i, c] = 0;
                }
            }
            return false;
        }

        // Displays the board
        public void Display()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (board[i, j] == 1) // If a queens is placed there
                        Console.Write("Q");
                    else
                        Console.Write("*"); // if a queen is not place there
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
