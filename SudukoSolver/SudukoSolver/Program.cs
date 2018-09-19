using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukoSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sudoku game = new Sudoku("003020600900305001001806400" + "008102900700000008006708200" + "002609500800203009005010300");
            //Sudoku game = new Sudoku("060090020100000000070001800015300002000604000800005430003400050000000009050070060");
            Sudoku game = new Sudoku("800000000003600000070090200050007000000045700000100030001000068008500010090000400");
            //Sudoku game = new Sudoku("037060000205000800006908000" + "000600024001503600650009000" + "000302700009000402000050360");
            game.Solve();
            Console.ReadKey();
        }
    }
}

