using System.Collections.Generic;
using System.Drawing;

namespace Schach_v1
{
    class King : Figure
    {
        public King( Tile startingTile, List<Tile> Tiles) : base( startingTile, Tiles)
        {

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.king_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.king_white;
            }
        }

        public override List<Tile> GetPossibleMoves()

        {
            List<Tile> PossibleMoves = new List<Tile>();
            List<Tile> TilesToRemove = new List<Tile>();

            //ALLE möglichen felder, auch wenn sie belegt sind
            foreach (Tile tile in this.BoardTiles)
            {
                if (tile.X == this.CurrentTile.X- 1)
                {
                    if (tile.Y == this.CurrentTile.Y)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Y == this.CurrentTile.Y+ 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Y== this.CurrentTile.Y - 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.X == this.CurrentTile.X + 1)
                {
                    if (tile.Y == this.CurrentTile.Y)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Y == this.CurrentTile.Y + 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Y == this.CurrentTile.Y - 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.Y == this.CurrentTile.Y + 1)
                {
                    if (tile.X == this.CurrentTile.X)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.Y == this.CurrentTile.Y - 1)
                {
                    if (tile.X == this.CurrentTile.X)
                    {
                        PossibleMoves.Add(tile);
                    }
                }
            }

            //welche felder entfernt werden müssen da sie nicht zugänglich sind
            foreach (Tile item in PossibleMoves)
            {
                if (item.CurrentFigure != null)
                {

                    if (item.CurrentFigure.FigureColor == this.FigureColor)

                    { 
                        TilesToRemove.Add(item);
                    }
                }
            }

            //entfernung der nicht möglichen moves
            foreach (Tile tileToRemove in TilesToRemove)
            {
                PossibleMoves.Remove(tileToRemove);
            }

            return PossibleMoves;
        }
    }
}
