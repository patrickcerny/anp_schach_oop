using System.Collections.Generic;
using System.Drawing;

namespace Schach_v1
{
    class King : Figure
    {
        public King(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {
            FigureType = FigureTypes.king;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.king_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.king_white;
            }
        }

        public override List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles)
        {
            List<Tile> PossibleMoves = new List<Tile>();
            List<Tile> TilesToRemove = new List<Tile>();

            //ALLE möglichen felder, auch wenn sie belegt sind
            foreach (Tile tile in Tiles)
            {
                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] - 1)
                {
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"])
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] + 1)
                {
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"])
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 1)
                {
                    if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"])
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 1)
                {
                    if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"])
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
                    if (item.CurrentFigure.FigureColor == figure.FigureColor)
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
