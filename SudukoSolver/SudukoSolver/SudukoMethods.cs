using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Char = System.Char;

namespace SudukoSolver
{
    public class Suduko
    {
        char[,] puzzle = new Char[9, 9];

        public Suduko (string text)
        {
            int index = 0;
            
            for (int row = 0; row < puzzle.GetLength(0); row++)
            {
                Console.WriteLine("+-----------+-----------+-----------+");
                Console.Write("| ");
                for (int col = 0; col < puzzle.GetLength(1); col++)
                {
                   
                    if (text[index].Equals('0'))
                    {
                        puzzle[row, col] = '-';
                        Console.Write(puzzle[row,col] + " | ");
                    }
                    
                    else
                    {
                        puzzle[row, col] = text[index];
                        Console.Write(puzzle[row, col] + " | ");
                    }
                    index++;
                }
                Console.WriteLine();
            }
            
        }


        }

        /**
         * Returnerar true/false om talet finns i raden.
         */
        public void FindInRow(int number, int row)
        {

        }

        /**
         * Returnerar true/false om talet finns i kolumnen.
         */
        public void FindInCol(int number, int col)
        {

        }

        /**
         * Kontrollerar om sudokut är klart.
         */
        public void CheckIfComplete()
        {

        }

        /**
         * Returnerar en lista med siffror som redan finns i boxen.
         */
        public void GetNumbersInBox()
        {

        }

        /**
         * Returnerar en lista med alla tal som skulle kunna vara i positionen.
         */
        public void GetInputNumbers()
        {

        }
    }
}
