using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Domain
{
    /// <summary>
    /// Represents the board for playing ConnectFour
    /// </summary>
    public class Board
    {

        public Board(int Rows, int Columns)
        {
            // check for valid size (greater than 0)
            if(Rows == 0 || Columns==0)
                throw new ArgumentException("Invalid Board Dimensions.");
            
            // store the dimensions
            this.Rows = Rows;
            this.Columns = Columns;

            // set up the board markers
            BoardMarkers = new Marker[Rows, Columns];

            IsReady = true;
        }

        public bool IsReady { get; set; }
        public Marker[,] BoardMarkers { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

    }
}
