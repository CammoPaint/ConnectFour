using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFour.Domain;

namespace ConnectFour
{
    class Program
    {
        enum Player
        {
            Yellow = 1,
            Red = 0
        }

        static void Main(string[] args)
        {
            // start a new game based on board size

            Console.WriteLine("Welcome to ConnectFour");
            Console.WriteLine("Please enter the board dimensions (number of rows, number of columns)");
            var boardDimensions = Console.ReadLine();
            // attempt to split the board dimensions on either a space or comma
            var dimensions = boardDimensions.Split(',', ' ');

            // initialize game
            Game game = new Game(int.Parse(dimensions[0]), int.Parse(dimensions[1]));

            DisplayGameBoard(game);

            int turn = 1;
            Player player= Player.Yellow;

            while (!game.IsWon)
            {
                player = (Player)(turn % 2);
                Console.WriteLine($"{player}'s Turn: ");
                var column = Console.ReadLine();
                try
                {
                    game.PlaceMarker(player.ToString(), int.Parse(column));
                    turn++;
                    // output the results
                    DisplayGameBoard(game);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"{ex.Message} It's a draw (press eny key to exit)");
                    break;
                }
            }
            if (game.IsWon)
            {
                Console.WriteLine($"{player} WINS! (press eny key to exit)");
            }
            Console.ReadKey();
        }
        static void DisplayGameBoard(Game game)
        {
            //display the top row of the game, followed by the next and continue until all rows are displayed
            for (int i = game.GameBoard.BoardMarkers.GetLength(0)-1; i >= 0 ; i--)
            {
                for (int j = 0; j < game.GameBoard.BoardMarkers.GetLength(1); j++)
                {
                    var marker = game.GameBoard.BoardMarkers[i,j];
                    if (marker == null)
                        Console.Write("o"); // display an o when there is no marker yet
                    else
                        Console.Write(marker.Colour.Substring(0, 1).ToLower()); // display the first letter of the colour
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

    }
}
