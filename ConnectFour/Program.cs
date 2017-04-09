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
            Yellow = 0,
            Red = 1
        }
        static void Main(string[] args)
        {
            // start a new game based on board size

            Console.WriteLine("Welcome to ConnectFour");
            Console.Write("Please enter the size of the board (Row,Columns): ");
            var boardDimensions = Console.ReadLine();
            // attempt to split the board dimensions
            var dimensions = boardDimensions.Split(',');

            // initialize game
            Game game = new Game(int.Parse(dimensions[0]), int.Parse(dimensions[1]));

            Console.WriteLine($"To place your token enter a column number between 1 & {dimensions[1]}. Press any key to play.");
            Console.ReadKey();

            int turn = 1;
            Player player= Player.Yellow;

            while (!game.IsWon)
            {
                player = (Player)(turn % 2);
                Console.Write($"{player}'s Turn: ");
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
            }
            if (game.IsWon)
            {
                Console.WriteLine($"{player} has won, press eny key to exit.");
                Console.ReadKey();
            }
                

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
                        Console.Write(0);
                    else
                        Console.Write(marker.Colour.Substring(0, 1));
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }



    }
}
