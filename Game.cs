using System;
namespace simple_tic_tac_toe
{
    enum CellType { EMPTY, X, O };

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public static Point GetPointFromUser()
        {
            int x, y;

            // Prompt the user for input and validate it
            Console.Write("Enter the X-coordinate (row): ");
            while (!int.TryParse(Console.ReadLine(), out x))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for X-coordinate.");
                Console.Write("Enter the X-coordinate (row): ");
            }

            Console.Write("Enter the Y-coordinate (column): ");
            while (!int.TryParse(Console.ReadLine(), out y))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for Y-coordinate.");
                Console.Write("Enter the Y-coordinate (column): ");
            }

            // Create and return a Point object
            return new Point(x, y);
        }

    }

    public class Game
	{

        private CellType[,] board;
        private CellType turn = CellType.X;
        private bool isGameOver = false;
        private int moves = 0;
        

        public Game()
		{
            this.board = new CellType[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i,j] = CellType.EMPTY;
                }
            }
        }

        public void PrintBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    char charToPrint = ' ';
                    if (board[row, col] == CellType.O) charToPrint = 'O';
                    else if (board[row, col] == CellType.X) charToPrint = 'X';

                    Console.Write(charToPrint);
                    if (col < 2)
                    {
                        Console.Write(" | ");
                    }
                }

                Console.WriteLine(); 
                if (row < 2)
                {
                    Console.WriteLine("---------");
                }
            }
        }

        private bool IsPointOnBoard(Point point)
        {
            return ((point.X <= 2) && (point.X >= 0) && (point.Y <= 2) && (point.Y >= 0));
        }

        private bool IsEmpty(Point point)
        {
            return (board[point.X, point.Y] == CellType.EMPTY);
        }

        private bool CheckRow(int x) {
            return ((board[x, 0] == board[x, 1]) && (board[x, 2] == board[x, 1]));
        }

        private bool CheckCol(int y)
        {
            return ((board[0, y] == board[1, y]) && (board[2, y] == board[1, y]));
        }

        private bool CheckNegetiveSlant()
        {
            return ((board[0, 0] != CellType.EMPTY) &&  (board[0, 0] == board[1, 1]) && (board[2, 2] == board[1, 1]));
        }

        private bool CheckPositiveSlant()
        {
            return ((board[0, 2] != CellType.EMPTY) &&  (board[0, 2] == board[1, 1]) && (board[2, 0] == board[1, 1]));
        }



        public void PlayGame()
        {
            char charToPrint;
            while ((!isGameOver) && moves < 9)
            {
                charToPrint = 'X';
                if (turn == CellType.O) charToPrint = 'O';
                Console.WriteLine("\n"+charToPrint + " Turn.");
                PrintBoard();
                Console.WriteLine();

                Point point;
                while (true)
                {
                    
                    point = Point.GetPointFromUser();

                    if (IsPointOnBoard(point))
                    {
                        if (IsEmpty(point)) break;
                        Console.WriteLine("Invalid input. Please enter a point that not taken.");
                        continue;
                    }
                    Console.WriteLine("Invalid input. Please enter a point in the board (X & Y must to be 0 or 1 or 2).");
                }

                board[point.X, point.Y] = turn;
                moves++;

                if (CheckRow(point.X) || CheckCol(point.Y) || CheckNegetiveSlant() || CheckPositiveSlant())
                {
                    isGameOver = true;
                    break;
                }
                turn = 3 - turn;  
            }

            if((moves == 9) && !isGameOver)
            {
                Console.WriteLine("It is a Tie!");
                return;
            }

            charToPrint = 'X';
            if (turn == CellType.O) charToPrint = 'O';
            Console.WriteLine(charToPrint + " Won!");

        }

        public void ResetGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = CellType.EMPTY;
                }
            }
            isGameOver = false;
            moves = 0;
        }

	}
}

