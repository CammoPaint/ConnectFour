using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Domain
{
    public class Board
    {
        private Marker[,] boardColumns;

        public Board(int Rows, int Columns)
        {
            // check for valid size (greater than 0)
            if(Rows == 0 || Columns==0)
                throw new ArgumentException("Invalid Board Dimensions.");

            this.Rows = Rows;
            this.Columns = Columns;
            // set up the board
            boardColumns = new Marker[Rows, Columns];

            IsReady = true;
        }

        public bool IsReady { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public bool IsWon { get; set; }

        public Marker PlaceMarker(string Colour, int Column)
        {
            // check that the column to place the marker is valid
            if (Column > boardColumns.GetLength(0) || Column < 1)
                throw new ArgumentException("This is not a valid column.");

            int column = Column - 1;

            var marker = new Marker { Colour = Colour, Column = Column };

            for (int i = 0; i < boardColumns.GetLength(0); i++)
            {
                // find the first empty
                if(boardColumns[i, column] == null)
                {
                    // place the marker here
                    marker.Row = i + 1;
                    boardColumns[i, column] = marker;

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
            List<Marker> markers = new List<Marker>();
            bool isFour = false;
            int column = marker.Column - 1;

            // 4 in a row vertically
            for (int i = 0; i < boardColumns.GetLength(0); i++)
            {
                if(boardColumns[i, column] !=null && boardColumns[i, column].Colour == marker.Colour)
                    markers.Add(boardColumns[i, column]);
            }

            if(markers.Count()==4)
                isFour = !markers.Select((i, j) => i.Row - j).Distinct().Skip(1).Any();

            if(!isFour)
            {
                // 4 in a row horizontally
                int row = marker.Row - 1;

                for (int i = 0; i < boardColumns.GetLength(1); i++)
                {
                    if (boardColumns[row, i] != null && boardColumns[row, i].Colour == marker.Colour)
                        markers.Add(boardColumns[row, i]);
                }

                if (markers.Count() == 4)
                    isFour = !markers.OrderBy(x => x.Column).Select((i, j) => i.Column - j).Distinct().Skip(1).Any();
            }



            return isFour;

        }
    }
}
