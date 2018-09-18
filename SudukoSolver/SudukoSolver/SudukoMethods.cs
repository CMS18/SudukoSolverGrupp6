using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SudukoSolver
{
    public class Sudoku
    {
        public char[,] puzzle = new char[9, 9];
        public char[,] completePuzzle = new char[9, 9];

        public Sudoku(string text)
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
                        Console.Write(puzzle[row, col] + " | ");
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
            Console.WriteLine("+-----------+-----------+-----------+");
        }

        /* Prints the selected puzzle.*/
        private void PrintPuzzle(char[,] puzzle)
        {
            for (int rows = 0; rows <= puzzle.GetUpperBound(0); rows++)
            {
                Console.WriteLine("+-----------+-----------+-----------+");
                Console.Write("| ");
                for (int cols = 0; cols <= puzzle.GetUpperBound(1); cols++)
                {
                    Console.Write(puzzle[rows, cols] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("+-----------+-----------+-----------+");
        }

        /*Returnerar true/false om talet finns i raden.*/
        private bool FindInRow(char number, int row)
        {
            for (int col = 0; col <= puzzle.GetUpperBound(1); col++)
            {
                if (number == puzzle[row, col])
                {
                    return true;
                }
            }
            return false;
        }

        /* Returnerar true/false om talet finns i kolumnen.*/
        private bool FindInCol(char number, int col)
        {
            for (int row = 0; row <= puzzle.GetUpperBound(0); row++)
            {
                if (number == puzzle[row, col])
                {
                    return true;
                }
            }

            return false;
        }
        
        /*Kontrollerar om sudokut är klart.*/
        private bool CheckIfComplete()
        {
            int totalRow;
            for (int row = 0; row < puzzle.GetLength(0); row++)
            {
                totalRow = 0;
                for (int col = 0; col < puzzle.GetLength(1); col++)
                {
                    totalRow += (int) Char.GetNumericValue(puzzle[row, col]);
                }
                if (totalRow != 45)
                {
                    return false;
                }
            }
            return true;
        }
        
        /* Collects the numbers in the selected 3x3 area.*/
        private List<char> FindNumbers(int startY, int stopY, int startX, int stopX)
        {
            List<char> boxContains = new List<char>();
            for (int rows = startY; rows < stopY; rows++)
            {
                for (int cols = startX; cols < stopX; cols++)
                {
                    if (!puzzle[rows, cols].Equals('-'))
                    {
                        boxContains.Add(puzzle[rows, cols]);
                    }
                }
            }

            return boxContains;
        }


        /*Returnerar en lista med siffror som redan finns i boxen.*/
        private List<char> GetNumbersInBox(int y, int x)
        {
            List<char> numbersInBox = new List<char>();
            int boxLength = 3;
            int endValue = puzzle.GetLength(0) / boxLength;
            int startY = y / boxLength;
            startY = startY * boxLength;
            int startX = x / boxLength;
            startX = startX * boxLength;
            numbersInBox = FindNumbers(startY, startY + endValue, startX, startX + endValue);
            return numbersInBox;
        }

        /*Returnerar en lista med alla tal som skulle kunna vara i positionen.*/
        private List<char> GetInputNumbers(int y, int x)
        {
            List<char> inputNumbers = new List<char>();
            List<char> boxContains = new List<char>();
            boxContains = GetNumbersInBox(y, x);
            for (int testValue = 1; testValue < 10; testValue++)
            {
                string tempValue = testValue.ToString();

                if (!FindInRow(tempValue[0], y) && !FindInCol(tempValue[0], x) && !boxContains.Contains(tempValue[0]))
                {
                    inputNumbers.Add(tempValue[0]);
                }
            }

            return inputNumbers;
        }

        /*Easy sudokusolver.*/
        public void Solve()
        {

            List<char> inputNumbers = new List<char>();
            bool puzzleNotSolved = true;
            while (puzzleNotSolved)
            {
                puzzleNotSolved = false;
                if (CheckIfComplete())
                {
                    PrintPuzzle(puzzle);
                    Console.WriteLine();
                    Console.WriteLine("***********SOLUTION FOUND!***********");
                    break;
                }

                for (int rows = 0; rows <= puzzle.GetUpperBound(0); rows++)
                {
                    for (int cols = 0; cols <= puzzle.GetUpperBound(1); cols++)
                    {
                        inputNumbers = GetInputNumbers(rows, cols);

                        if (puzzle[rows, cols].Equals('-') && (inputNumbers.Count == 1))
                        {
                            puzzle[rows, cols] = inputNumbers.ElementAt(0);
                            puzzleNotSolved = true;
                        }

                        inputNumbers.Clear();
                    }
                }
            }

            RecursionSolve();
        }

        private bool GuessNumber(int y, int x, char[,] puzzle)
        {
            List<char> inputNumber = new List<char>();

            for (int testValue = 1; testValue < 10; testValue++)
            {
                inputNumber = GetInputNumbers(y, x);
                string tempValue = testValue.ToString();
                if (inputNumber.Contains(tempValue[0]))
                {
                    puzzle[y, x] = tempValue[0];

                    if (RecursionSolve())
                    {
                        if (CheckIfComplete())
                        {
                            PrintPuzzle(puzzle);
                            Console.WriteLine();
                            Console.WriteLine("***********SOLUTION FOUND!***********");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }

                        return true;
                    }
                }
            }

            puzzle[y, x] = '-';
            return false;
        }
    

            /*Solves hard sudoku puzzles using mutual recursion searching for a solution using depth-first.*/
         private bool RecursionSolve()
        {
            for (int rows = 0; rows < puzzle.GetLength(0); rows++)
            {
                for (int cols = 0; cols < puzzle.GetLength(1); cols++)
                {
                    if (puzzle[rows,cols].Equals('-'))
                    {
                        return GuessNumber(rows,cols,puzzle);
                    }
                }
            }
            
            return true;
        }
    }
}
