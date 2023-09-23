
using simple_tic_tac_toe;

Game ticTacToe = new Game();

while (true)
{
    ticTacToe.PlayGame();

    Console.Write("Play again? (y/else): ");
    if (Console.ReadLine().ToLower() != "y")
        break;
    ticTacToe.ResetGame();
}


