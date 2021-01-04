using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Schach_v1
{
    class Bishop : Figure
    {
        public Bishop( Tile startingTile, List<Tile> Tiles) : base( startingTile, Tiles)
        {
           

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\bishop_black.png");
            }
            else
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\bishop_white.png");
            }
        }

        public override List<Tile> GetPossibleMoves()
        {
            return GetDiagonalMoves();
        }
    }
}
