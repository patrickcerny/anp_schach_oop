using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Schach_v1
{
    class Queen : Figure
    {
        public Queen( Tile startingTile, List<Tile> Tiles) : base(startingTile, Tiles)
        {
           

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\queen_black.png");
            }
            else
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\queen_white.png");
            }
        }


        public override List<Tile> GetPossibleMoves()
        {
            List<Tile> PossibleMoves = new List<Tile>();

            PossibleMoves.AddRange(this.GetStraightMoves());
            PossibleMoves.AddRange(this.GetDiagonalMoves());
            
            return PossibleMoves;
        }
    }
}
