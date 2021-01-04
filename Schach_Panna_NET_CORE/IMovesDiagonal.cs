using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach_v1
{
    interface IMovesDiagonal
    {
        public List<Tile> GetDiagonalMoves()
        {
            return new List<Tile>();

        }

    }
}
