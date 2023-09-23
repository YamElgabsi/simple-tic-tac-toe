using System;
namespace simple_tic_tac_toe
{
    enum cellType { EMPTY, X, O };

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

        private cellType[,] board;
        private cellType turn = cellType.X;
        private bool isGameOver = false;
        

        public Game()
		{
            this.board = new cellType[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i,j] = cellType.EMPTY;
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
                    if (board[row, col] == cellType.O) charToPrint = 'O';
                    else if (board[row, col] == cellType.X) charToPrint = 'X';

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

        private bool isPointOnBoard(Point point)
        {
            return ((point.X <= 2) && (point.X >= 0) && (point.Y <= 2) && (point.Y >= 0));
        }

        private bool isEmpty(Point point)
        {
            return (board[point.X, point.Y] == cellType.EMPTY);
        }

        private bool checkRow(int x) {
            return ((board[x, 0] == board[x, 1]) && (board[x, 2] == board[x, 1]));
        }

        private bool checkCol(int y)
        {
            return ((board[0, y] == board[1, y]) && (board[2, y] == board[1, y]));
        }

        private bool checkNegetiveSlant()
        {
            return ((board[0, 0] != cellType.EMPTY) &&  (board[0, 0] == board[1, 1]) && (board[2, 2] == board[1, 1]));
        }

        private bool checkPositiveSlant()
        {
            return ((board[0, 2] != cellType.EMPTY) &&  (board[0, 2] == board[1, 1]) && (board[2, 0] == board[1, 1]));
        }



        public void playGame()
        {
            char charToPrint;
            while (!isGameOver)
            {
                charToPrint = 'X';
                if (turn == cellType.O) charToPrint = 'O';
                Console.WriteLine("\n"+charToPrint + " Turn.");
                PrintBoard();
                Console.WriteLine();

                Point point;
                while (true)
                {
                    
                    point = Point.GetPointFromUser();

                    if (isPointOnBoard(point))
                    {
                        if (isEmpty(point)) break;
                        Console.WriteLine("Invalid input. Please enter a point that not taken.");
                        continue;
                    }
                    Console.WriteLine("Invalid input. Please enter a point in the board (X & Y must to be 0 or 1 or 2).");
                }

                board[point.X, point.Y] = turn;

                if (checkRow(point.X) || checkCol(point.Y) || checkNegetiveSlant() || checkPositiveSlant())
                {
                    isGameOver = true;
                    break;
                }
                turn = 3 - turn;  
            }

            charToPrint = 'X';
            if (turn == cellType.O) charToPrint = 'O';
            Console.WriteLine(charToPrint + " Won!");

        }

        public void reset()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = cellType.EMPTY;
                }
            }
            isGameOver = false;
        }

	}
}

