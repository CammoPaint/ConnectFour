using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Domain
{
    /// <summary>
    /// Represents a single marker used in a game of ConnectFour
    /// </summary>
    public class Marker : IEquatable<Marker>
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public string Colour { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Marker);
        }

        /// <summary>
        /// Configure Equals for use in testing that the correct Marker is returned
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Marker other)
        {
            return this.Colour.Equals(other.Colour) &&
                this.Column.Equals(other.Column) &&
                this.Row.Equals(other.Row);
        }
    }
}
