using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Schach_v1
{
    class Tower : Figure
    {
        public Tower( Tile startingTile, List<Tile> Tiles) : base( startingTile, Tiles)
        {
            if (FigureColor == Color.Black)
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\Tower_black.png");
            }
            else
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\Tower_white.png");
            }
        }

        public override List<Tile> GetPossibleMoves()

        {
            return this.GetStraightMoves();
        }
    }
}
