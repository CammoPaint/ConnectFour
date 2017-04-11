using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectFour.Domain;

namespace ConnectFour.Tests
{
    //Scenario One (Yellow wins - Horizontal)
    //Scenario Two(Red wins - Vertical)
    //Scenario Three(Yellow wins diagonal)
    //Scenario Four(Draw)
    //Scenario Five(Invalid board dimensions)
    //Scenario Six(Invalid move)

    [TestClass]
    public class GameTests
    {
        /// <summary>
        /// Game board is ready to play
        /// </summary>
        [TestMethod]
        public void BoardIsReady()
        {
            // assign
            // create the board
            int row = 5;
            int col = 5;

            // act
            var game = new Game(row, col);

            // assert
            Assert.IsTrue(game.GameBoard.IsReady);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid Board Dimensions.")]
        public void CreatingZeroSizedBoard_ThrowsException()
        {
            // assign
            // create the board
            int row = 0;
            int col = 0;

            // act
            var game = new Game(row, col);
        }

        [TestMethod]
        public void PlacingAPiece_ReturnsAMarker()
        {
            // assign
            int column = 1;
            Game game = CreateGame(5,5);

            // act
            var result = game.PlaceMarker("y", column);

            // assert
            Assert.IsInstanceOfType(result, typeof(Marker));
        }

        [TestMethod]
        public void PlacingAPieceInASpecificColumn_ReturnsAMarker_WithCorrectColumnNumber()
        {
            // assign
            Game game = CreateGame(5, 5);
            var expected = new Marker { Colour = "y", Column = 3, Row = 1 };

            // act
            var result = game.PlaceMarker(expected.Colour, expected.Column);

            // assert
            Assert.AreEqual(expected.Column, result.Column);
        }

        [TestMethod]
        public void PlacingAPieceInAColumn_ReturnsAMarker_WithCorrectRowNumber()
        {
            // assign
            Game game = CreateGame(5, 5);
            var expected = new Marker { Colour = "y", Column = 1, Row = 1 };
            
            // act
            var result = game.PlaceMarker(expected.Colour, expected.Column);

            // assert
            Assert.AreEqual(expected.Row, result.Row);
        }

        [TestMethod]
        public void PlacingAPieceInAColumn_ReturnsTheCorrectMarker()
        {
            // assign
            Game game = CreateGame(5, 5);
            var expected = new Marker { Colour = "y", Column = 1, Row = 1 };

            // act
            var result = game.PlaceMarker(expected.Colour, expected.Column);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PlacingAPiecesVerticallyInAColumn_SetsTheAppropriateRowNumber()
        {
            // assign
            Game game = CreateGame(3, 2);
            int col = 1;
            var marker = new Marker();

            // act
            for (int i = 0; i < game.GameBoard.Rows; i++)
            {
                // alternate between red and yellow.
                marker = game.PlaceMarker(i % 2 == 0 ? "y" : "r", col);
            }

            // assert
            Assert.AreEqual(game.GameBoard.Rows, marker.Row);
        }

        [TestMethod]
        public void Placing4PiecesVertically_WinsTheGame()
        {
            // assign
            Game game = CreateGame(5, 1);
            int col = 1;
            var marker = new Marker();

            // act
            for (int i = 0; i < 4; i++)
            {
                marker = game.PlaceMarker("r", col);
            }

            // assert
            Assert.IsTrue(game.IsWon);
        }


        [TestMethod]
        public void Placing3PiecesVertically_DoesNotWinTheGame()
        {
            // assign
            Game game = CreateGame(5, 1);
            int col = 1;
            var marker = new Marker();

            // act
            for (int i = 0; i < 3; i++)
            {
                marker = game.PlaceMarker("r", col);
            }
            // opponent blocks streak
            game.PlaceMarker("y", col);

            game.PlaceMarker("r", col);

            // assert
            Assert.IsFalse(game.IsWon);
        }

        [TestMethod]
        public void Placing4PiecesHorizontally_WinsTheGame()
        {
            // assign
            Game game = CreateGame(1, 5);
            var marker = new Marker();

            // act
            for (int i = 1; i < 5; i++)
            {
                marker = game.PlaceMarker("r",i);
            }

            // assert
            Assert.IsTrue(game.IsWon);
        }

        [TestMethod]
        public void Placing3PiecesHorizontally_DoesNotWinTheGame()
        {
            // assign
            Game game = CreateGame(5, 5);
            var marker = new Marker();

            // act
            for (int i = 1; i < 4; i++)
            {
                marker = game.PlaceMarker("r", i);
            }
            // opponent blocks streak
            marker = game.PlaceMarker("y", 4);

            marker = game.PlaceMarker("r", 5);

            // assert
            Assert.IsFalse(game.IsWon);
        }

        [TestMethod]
        public void Placing4PiecesDiagonallyUpward_WinsTheGame()
        {
            // assign
            Game game = CreateGame(5, 5);
            var marker = new Marker();

            // act

            // first column
            marker = game.PlaceMarker("r", 1);

            // second column
            marker = game.PlaceMarker("y", 2);
            marker = game.PlaceMarker("r", 2);

            // third
            marker = game.PlaceMarker("y", 3);
            marker = game.PlaceMarker("y", 3);
            marker = game.PlaceMarker("r", 3);

            // forth
            marker = game.PlaceMarker("y", 4);
            marker = game.PlaceMarker("y", 4);
            marker = game.PlaceMarker("y", 4);
            marker = game.PlaceMarker("r", 4);

            // assert
            Assert.IsTrue(game.IsWon);
        }

        [TestMethod]
        public void Placing3PiecesDiagonallyUpward_DoesNotWinTheGame()
        {
            // assign
            Game game = CreateGame(5, 5);
            var marker = new Marker();

            // act

            // first column
            marker = game.PlaceMarker("r", 1);

            // second column
            marker = game.PlaceMarker("y", 2);
            marker = game.PlaceMarker("r", 2); 

            // third
            marker = game.PlaceMarker("y", 3);
            marker = game.PlaceMarker("y", 3);
            marker = game.PlaceMarker("y", 3); // streak interupted

            // forth
            marker = game.PlaceMarker("r", 4);
            marker = game.PlaceMarker("y", 4);
            marker = game.PlaceMarker("y", 4);
            marker = game.PlaceMarker("r", 4);

            // fifth
            marker = game.PlaceMarker("y", 5);
            marker = game.PlaceMarker("y", 5);
            marker = game.PlaceMarker("y", 5);
            marker = game.PlaceMarker("r", 5);
            marker = game.PlaceMarker("r", 5);

            // assert
            Assert.IsFalse(game.IsWon);
        }

        [TestMethod]
        public void Placing4PiecesDiagonallyDownward_WinsTheGame()
        {
            // assign
            Game game = CreateGame(5, 5);
            var marker = new Marker();

            // act
            // forth
            marker = game.PlaceMarker("r", 4);


            // second column
            marker = game.PlaceMarker("y", 2);
            marker = game.PlaceMarker("y", 2);
            marker = game.PlaceMarker("r", 2);


            // third
            marker = game.PlaceMarker("y", 3);
            marker = game.PlaceMarker("r", 3);

            // first column
            marker = game.PlaceMarker("y", 1);
            marker = game.PlaceMarker("y", 1);
            marker = game.PlaceMarker("y", 1);
            marker = game.PlaceMarker("r", 1);

            // assert
            Assert.IsTrue(game.IsWon);
        }

        [TestMethod]
        public void Placing3PiecesDiagonallyDownward_DoesNotWinTheGame()
        {
            // assign
            Game game = CreateGame(5, 5);
            var marker = new Marker();

            // act

            // first column
            marker = game.PlaceMarker("y", 1);
            marker = game.PlaceMarker("y", 1);
            marker = game.PlaceMarker("r", 1);
            marker = game.PlaceMarker("y", 1);
            marker = game.PlaceMarker("r", 1);

            // second column
            marker = game.PlaceMarker("r", 2);
            marker = game.PlaceMarker("y", 2);
            marker = game.PlaceMarker("y", 2);
            marker = game.PlaceMarker("r", 2);

            // third
            marker = game.PlaceMarker("y", 3);
            marker = game.PlaceMarker("r", 3);
            marker = game.PlaceMarker("r", 3);

            // forth
            marker = game.PlaceMarker("r", 4);
            marker = game.PlaceMarker("y", 4); // streak interupted

            // fifth
            marker = game.PlaceMarker("r", 5);

            // assert
            Assert.IsFalse(game.IsWon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This is not a valid column.")]
        public void PlacingAPieceInAnInvalidColumn_ThrowsAnException()
        {
            // assign
            Game game = CreateGame(5, 5);
            int col = 6;

            // act
            var result = game.PlaceMarker("y",col);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This is not a valid column.")]
        public void PlacingAPieceInAnNegativeColumn_ThrowsAnException()
        {
            // assign
            Game game = CreateGame(5, 5);
            int col = -1;

            // act
            var result = game.PlaceMarker("y", col);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "This column is full.")]
        public void PlacingAPieceInAFullColumn_ThrowsAnException()
        {
            // assign
            Game game = CreateGame(3, 2);
            int col = 1;

            // act - add too many markers to the column
            for (int i = 0; i < game.GameBoard.Rows+1; i++)
            {
                game.PlaceMarker("y", col);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "All columns are full.")]
        public void FillingAllColumns_ThrowsAnException()
        {
            // assign
            Game game = CreateGame(1, 1);

            // act - fill all rows and column
            for (int i = 1; i < game.GameBoard.Columns+1; i++)
            {
                for (int j = 1; j < game.GameBoard.Rows+1; j++)
                {
                    game.PlaceMarker(i % 2 == 0 ? "y" : "r", i);
                }
            }

            // add one more Marker
            game.PlaceMarker("r", 1);

        }

        private static Game CreateGame(int row, int col)
        {
            var game = new Game(row, col);
            return game;
        }
    }
}
