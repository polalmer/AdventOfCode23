namespace AdventOfCode23.FunctionalTicTacToe;

static class TicTacToe
{
    static char[,] CreateBoard()
    {
        char[,] board = new char[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }
        return board;
    }
    static void PrintBoard(char[,] board)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                Console.Write($" {board[row, col]} ");
                if (col < 2) Console.Write('|');
            }
            Console.WriteLine();
            if (row < 2) Console.WriteLine("---+---+---");
        }
        Console.WriteLine();
    }
    static bool IsMoveValid(char[,] board, int row, int col)
        => row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ';
    static bool MakeMove(char[,] board, int row, int col, Player player)
    {
        if (IsMoveValid(board, row, col))
        {
            board[row, col] = player == Player.X ? 'X' : 'O';
            return true;
        }
        return false;
    }
    static bool CheckWin(char[,] board, char playerChar)
    {
        for (int row = 0; row < 3; row++)
        {
            if (board[row, 0] == playerChar && board[row, 1] == playerChar && board[row, 2] == playerChar) return true;
        }
        for (int col = 0; col < 3; col++)
        {
            if (board[0, col] == playerChar && board[1, col] == playerChar && board[2, col] == playerChar) return true;
        }
        if ((board[0, 0] == playerChar && board[1, 1] == playerChar && board[2, 2] == playerChar)
           || (board[0, 2] == playerChar && board[1, 1] == playerChar && board[2, 0] == playerChar)) return true;
        return false;
    }
    static bool CheckDraw(char[,] board)
    {
        bool draw = true;
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == ' ')
                    draw = false;
            }
        }
        return draw;
    }

    static void PlayGame(char[,] board, Player currentPlayer)
    {
        PrintBoard(board);
    ReReadInput:
        Console.WriteLine($"Player {(currentPlayer == Player.X ? 'X' : 'O')}, enter your move (row and column): ");
        string? input = Console.ReadLine();
        var parts = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts?.Length != 2) goto ReReadInput;
        int row = int.Parse(parts[0]) - 1;
        int col = int.Parse(parts[1]) - 1;
        if (IsMoveValid(board, row, col))
        {
            MakeMove(board, row, col, currentPlayer);
            if (CheckWin(board, currentPlayer.ToString()[0]))
            {
                PrintBoard(board);
                Console.WriteLine($"Player {currentPlayer} wins!");
            }
            else if (CheckDraw(board))
            {
                PrintBoard(board);
                Console.WriteLine("Its a draw!");
            }
            else
            {
                PlayGame(board, currentPlayer == Player.X ? Player.O : Player.X);
            }
        }
        else
        {
            Console.WriteLine("Invalid move");
            goto ReReadInput;
        }
    }

    public static void Start()
    {
        var board = CreateBoard();
        PlayGame(board, Player.X);
    }

    enum Player
    {
        X,
        O
    }
}
