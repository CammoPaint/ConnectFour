using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectFour.Domain;

namespace ConnectFour.Tests
{
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
            var board = new Board(row, col);

            // assert
            Assert.IsTrue(board.IsReady);
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
            var board = new Board(row, col);
        }

        [TestMethod]
        public void PlacingAPiece_ReturnsAMarker()
        {
            // assign
            int column = 1;
            Board board = CreateBoard(5,5);

            // act
            var result = board.PlaceMarker("y", column);

            // assert
            Assert.IsInstanceOfType(result, typeof(Marker));
        }

        [TestMethod]
        public void PlacingAPieceInASpecificColumn_ReturnsAMarker_WithCorrectColumnNumber()
        {
            // assign
            Board board = CreateBoard(5, 5);
            var expected = new Marker { Colour = "y", Column = 3, Row = 1 };

            // act
            var result = board.PlaceMarker(expected.Colour, expected.Column);

            // assert
            Assert.AreEqual(expected.Column, result.Column);
        }

        [TestMethod]
        public void PlacingAPieceInAColumn_ReturnsAMarker_WithCorrectRowNumber()
        {
            // assign
            Board board = CreateBoard(5, 5);
            var expected = new Marker { Colour = "y", Column = 1, Row = 1 };
            
            // act
            var result = board.PlaceMarker(expected.Colour, expected.Column);

            // assert
            Assert.AreEqual(expected.Row, result.Row);
        }

        [TestMethod]
        public void PlacingAPieceInAColumn_ReturnsTheCorrectMarker()
        {
            // assign
            Board board = CreateBoard(5, 5);
            var expected = new Marker { Colour = "y", Column = 1, Row = 1 };

            // act
            var result = board.PlaceMarker(expected.Colour, expected.Column);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PlacingAPiecesVerticalyInAColumn_SetsTheAppropriateRowNumber()
        {
            // assign
            Board board = CreateBoard(5, 5);
            int col = 1;
            var marker = new Marker();

            // act
            for (int i = 0; i < board.Rows; i++)
            {
                // alternate between red and yellow.
                marker = board.PlaceMarker(i % 2 == 0 ? "y" : "r", col);
            }

            // assert
            Assert.AreEqual(board.Rows, marker.Row);
        }

        [TestMethod]
        public void Placing4PiecesVertically_WinsTheGame()
        {
            // assign
            Board board = CreateBoard(5, 5);
            int col = 1;
            var marker = new Marker();

            // act
            for (int i = 0; i < 4; i++)
            {
                marker = board.PlaceMarker("r", col);
            }

            // assert
            Assert.IsTrue(board.IsWon);
        }


        [TestMethod]
        public void Placing3PiecesVertically_DoesNotWinTheGame()
        {
            // assign
            Board board = CreateBoard(5, 5);
            int col = 1;
            var marker = new Marker();

            // act
            for (int i = 0; i < 3; i++)
            {
                marker = board.PlaceMarker("r", col);
            }
            // opponent blocks streak
            board.PlaceMarker("y", col);

            board.PlaceMarker("r", col);

            // assert
            Assert.IsFalse(board.IsWon);
        }

        [TestMethod]
        public void Placing4PiecesHorizontally_WinsTheGame()
        {
            // assign
            Board board = CreateBoard(5, 5);
            var marker = new Marker();

            // act
            for (int i = 1; i < 5; i++)
            {
                marker = board.PlaceMarker("r",i);
            }

            // assert
            Assert.IsTrue(board.IsWon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This is not a valid column.")]
        public void PlacingAPieceInAnInvalidColumn_ThrowsAnException()
        {
            // assign
            Board board = CreateBoard(5, 5);
            int col = 6;

            // act
            var result = board.PlaceMarker("y",col);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This is not a valid column.")]
        public void PlacingAPieceInAnNegativeColumn_ThrowsAnException()
        {
            // assign
            Board board = CreateBoard(5, 5);
            int col = -1;

            // act
            var result = board.PlaceMarker("y", col);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This column is full.")]
        public void PlacingAPieceInAFullColumn_ThrowsAnException()
        {
            // assign
            Board board = CreateBoard(5, 5);
            int col = 1;

            // act - add too many markers to the column
            for (int i = 0; i < board.Columns+1; i++)
            {
                board.PlaceMarker("y", col);
            }
        }

        private static Board CreateBoard(int row, int col)
        {
            var board = new Board(row, col);
            return board;
        }

        //Scenario One (Yellow wins - Horizontal)
        //Scenario Two(Red wins - Vertical)
        //Scenario Three(Yellow wins diagonal)
        //Scenario Four(Draw)
        //Scenario Five(Invalid board dimensions)
        //Scenario Six(Invalid move)
    }
}
