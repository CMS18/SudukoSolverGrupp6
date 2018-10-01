using System;
using System.Collections.Generic;
using System.Linq;

namespace SudukoSolver
{
    public class Sudoku
    {
        public char[,] puzzle = new char[9, 9];
        public int valueTested = 0;
        public DateTime start;
        public DateTime stop;

        public Sudoku(string text)
        {
            var index = 0;
            for (var row = 0; row < puzzle.GetLength(0); row++)
            {
                Console.WriteLine("+-----------+-----------+-----------+");
                Console.Write("| ");
                for (var col = 0; col < puzzle.GetLength(1); col++)
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
            for (var rows = 0; rows <= puzzle.GetUpperBound(0); rows++)
            {
                Console.WriteLine("+-----------+-----------+-----------+");
                Console.Write("| ");
                for (var cols = 0; cols <= puzzle.GetUpperBound(1); cols++) Console.Write(puzzle[rows, cols] + " | ");
                Console.WriteLine();
            }

            Console.WriteLine("+-----------+-----------+-----------+");
            Console.WriteLine();
            //Console.WriteLine("Tested value: " + valueTested);
        }

        /*Returnerar true/false om talet finns i raden.*/
        private bool FindInRow(char number, int row, int x)
        {
            for (var col = 0; col <= puzzle.GetUpperBound(1); col++)
                if (number == puzzle[row, col] && (x != col))
                    return true;
            return false;
        }

        /* Returnerar true/false om talet finns i kolumnen.*/
        private bool FindInCol(char number, int col, int y)
        {
            for (var row = 0; row <= puzzle.GetUpperBound(0); row++)
                if (number == puzzle[row, col] && (y != row))
                    return true;

            return false;
        }

        /*Kontrollerar om sudokut är klart.*/
        private bool CheckIfComplete()
        {
            int totalRow;
            for (var row = 0; row < puzzle.GetLength(0); row++)
            {
                totalRow = 0;
                for (var col = 0; col < puzzle.GetLength(1); col++)
                    totalRow += (int) char.GetNumericValue(puzzle[row, col]);
                if (totalRow != 45) return false;
            }

            return true;
        }

        /**
         * Checks if the puzzle is valid or not.
         */
        private void ValidPuzzle()
        {
            bool validBoard = true;
            for (int rows = 0; rows <= puzzle.GetUpperBound(0); rows++)
            {
                for (int cols = 0; cols <= puzzle.GetUpperBound(1); cols++)
                {
                    if (!puzzle[rows, cols].Equals('-') && (FindInRow(puzzle[rows, cols], rows, cols) || FindInCol(puzzle[rows, cols], cols, rows)))
                    {
                        validBoard = false;
                    }
                }
            }
            if (!validBoard)
            {
                Console.WriteLine("Invalid board..");
                Console.ReadLine();
                Environment.Exit(0);
            }
            
        }

        /* Collects the numbers in the selected 3x3 area.*/
        private List<char> FindNumbers(int startY, int stopY, int startX, int stopX)
        {
            var boxContains = new List<char>();
            for (var rows = startY; rows < stopY; rows++)
            for (var cols = startX; cols < stopX; cols++)
                if (!puzzle[rows, cols].Equals('-'))
                    boxContains.Add(puzzle[rows, cols]);

            return boxContains;
        }


        /*Returnerar en lista med siffror som redan finns i boxen.*/
        private List<char> GetNumbersInBox(int y, int x)
        {
            var numbersInBox = new List<char>();
            var boxLength = 3;
            var endValue = puzzle.GetLength(0) / boxLength;
            var startY = y / boxLength;
            startY = startY * boxLength;
            var startX = x / boxLength;
            startX = startX * boxLength;
            numbersInBox = FindNumbers(startY, startY + endValue, startX, startX + endValue);
            return numbersInBox;
        }

        /*Returnerar en lista med alla tal som skulle kunna vara i positionen.*/
        private List<char> GetInputNumbers(int y, int x)
        {
            var inputNumbers = new List<char>();
            var boxContains = new List<char>();
            boxContains = GetNumbersInBox(y, x);
            for (var testValue = 1; testValue < 10; testValue++)
            {
                var tempValue = testValue.ToString();

                if (!FindInRow(tempValue[0], y, x) && !FindInCol(tempValue[0], x, y) && !boxContains.Contains(tempValue[0]))
                    inputNumbers.Add(tempValue[0]);
            }

            return inputNumbers;
        }

        /*Easy sudokusolver.*/
        public void Solve()
        {
            var inputNumbers = new List<char>();
            var puzzleNotSolved = true;
            ValidPuzzle();
            while (puzzleNotSolved)
            {
                puzzleNotSolved = false;
                for (var rows = 0; rows <= puzzle.GetUpperBound(0); rows++)
                for (var cols = 0; cols <= puzzle.GetUpperBound(1); cols++)
                {
                    inputNumbers = GetInputNumbers(rows, cols);

                    if (puzzle[rows, cols].Equals('-') && inputNumbers.Count == 1)
                    {
                        puzzle[rows, cols] = inputNumbers.ElementAt(0);
                        puzzleNotSolved = true;
                    }

                    inputNumbers.Clear();
                }
                
            }
            if (CheckIfComplete())
            {
                PrintPuzzle(puzzle);
                Console.WriteLine();
                Console.WriteLine("***********SOLUTION FOUND!***********");
                Console.ReadLine();
                Environment.Exit(0); ;
            }
            start = DateTime.Now;
            RecursionSolve();
            Console.WriteLine("No solution found.");
        }

        private bool GuessNumber(int y, int x, char[,] puzzle)
        {
            var inputNumber = new List<char>();

            for (var testValue = 9; testValue > 0; testValue--)
            {
            //for (var testValue = 1; testValue < 10; testValue++){
             
                inputNumber = GetInputNumbers(y, x);
                var tempValue = testValue.ToString();
                if (inputNumber.Contains(tempValue[0]))
                {
                    puzzle[y, x] = tempValue[0];
                    //Console.SetCursorPosition(0, 0);
                    //PrintPuzzle(puzzle);
                    valueTested++;
                    if (RecursionSolve())
                    {
                        if (CheckIfComplete())
                        {
                            PrintPuzzle(puzzle);
                            Console.WriteLine();
                            Console.WriteLine("***********SOLUTION FOUND!***********");
                            Console.WriteLine("Tested: " + valueTested + " numbers.");
                            stop = DateTime.Now;
                            TimeSpan totalTime = stop - start;
                            Console.WriteLine("Sudoku solved in: " + totalTime);
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
            for (var rows = 0; rows < puzzle.GetLength(0); rows++)
            for (var cols = 0; cols < puzzle.GetLength(1); cols++)
                if (puzzle[rows, cols].Equals('-'))
                    return GuessNumber(rows, cols, puzzle);

            return true;
        }
    }
}