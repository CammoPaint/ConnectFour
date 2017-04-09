using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Domain
{
    public class Game
    {
        public Game(int Rows, int Columns)
        {
            GameBoard = new Board(Rows, Columns);
        }

        public Board GameBoard { get; set; }
        
        public bool IsWon { get; set; }

        public Marker PlaceMarker(string Colour, int Column)
        {
            // check that the column to place the marker is valid
            if (Column > GameBoard.BoardMarkers.GetLength(0) || Column < 1)
                throw new ArgumentException("This is not a valid column.");

            int column = Column - 1;

            var marker = new Marker { Colour = Colour, Column = Column };

            for (int i = 0; i < GameBoard.BoardMarkers.GetLength(0); i++)
            {
                // find the first empty
                if (GameBoard.BoardMarkers[i, column] == null)
                {
                    // place the marker here
                    marker.Row = i + 1;
                    GameBoard.BoardMarkers[i, column] = marker;

                    // check if this move wins the game
                    IsWon = CheckMarkerWinsGame(marker);

                    return marker;
                }
            }

            // no empty slots found in this column
            throw new ArgumentException("This column is full.");
        }
        private bool CheckMarkerWinsGame(Marker marker)
        {
            bool isFour = false;

            // Check for vertical win
            isFour = CheckVerticalMarkers(marker);

            // Check for horizontal win
            if (!isFour)
            {
                isFour = CheckHorizontalMarkers(marker);
            }

            // Check diagonal win
            if (!isFour)
            {
                isFour = CheckDiagonalMarkers(marker);
            }

            return isFour;

        }

        private bool CheckVerticalMarkers(Marker marker)
        {
            List<Marker> markers = new List<Marker>();

            int column = marker.Column - 1;

            // 4 in a row vertically
            for (int i = 0; i < GameBoard.BoardMarkers.GetLength(0); i++)
            {
                if (GameBoard.BoardMarkers[i, column] != null && GameBoard.BoardMarkers[i, column].Colour == marker.Colour)
                    markers.Add(GameBoard.BoardMarkers[i, column]);
            }

            // if there are 4 markers for this colour in the column, check they are consecutive
            if (markers.Count() == 4)
                return !markers.Select((i, j) => i.Row - j).Distinct().Skip(1).Any();

            return false;

        }

        private bool CheckHorizontalMarkers(Marker marker)
        {
            List<Marker> markers = new List<Marker>();

            // 4 in a row horizontally
            int row = marker.Row - 1;

            for (int i = 0; i < GameBoard.BoardMarkers.GetLength(1); i++)
            {
                if (GameBoard.BoardMarkers[row, i] != null && GameBoard.BoardMarkers[row, i].Colour == marker.Colour)
                    markers.Add(GameBoard.BoardMarkers[row, i]);
            }

            // if there are 4 markers for this colour in the row, check they are consecutive
            if (markers.Count() == 4)
                return !markers.OrderBy(x => x.Column).Select((i, j) => i.Column - j).Distinct().Skip(1).Any();

            return false;
        }

        private bool CheckDiagonalMarkers(Marker marker)
        {

            //check downwards diagonal markers
            int diagonallyDown = 0;

            // check the markers diagonally up from the current one
            diagonallyDown += Loop(marker.Row - 1, marker.Column - 1, marker, new int[] { 1, -1 });

            // check markers diagonally below the current one
            diagonallyDown += Loop(marker.Row - 2, marker.Column , marker, new int[] { -1 , 1 });

            if (diagonallyDown == 4)
                return true;

            //check upwards diagonal markers
            int diagonallyUp = 0;

            // start with the marker diagonally up from the current one
            diagonallyUp += Loop(marker.Row, marker.Column, marker, new int[] { 1, 1 });

            // check markers below
            diagonallyUp += Loop(marker.Row - 1, marker.Column - 1, marker, new int[] { -1, -1 });

            return diagonallyUp == 4;

        }

        private bool MarkerExists(int Row, int Column)
        {
            return Column >= 0 && Column < GameBoard.Columns && Row >= 0 && Row < GameBoard.Rows;
        }


        private int Loop(int Row, int Column, Marker Marker, int[] Delta)
        {
            int recordsFound = 0;
            while (MarkerExists(Row, Column))
            {
                if (GameBoard.BoardMarkers[Row, Column] != null && GameBoard.BoardMarkers[Row, Column].Colour == Marker.Colour)
                {
                    recordsFound++;
                }
                else
                {
                    break;
                }
                Row+= Delta[0];
                Column += Delta[1];
            }

            return recordsFound;
        }

        private bool PreviousMarker(int Row, int Column, string Colour)
        {
            if (Column > GameBoard.Columns && Row > GameBoard.Rows)
            {
                // check the next marker diagonally below
                var markerBelow = GameBoard.BoardMarkers[Row - 1, Column - 1];
                if (markerBelow != null && markerBelow.Colour == Colour)
                {
                    // matching
                    return true;
                }
            }
            return false;
        }


    }
}
