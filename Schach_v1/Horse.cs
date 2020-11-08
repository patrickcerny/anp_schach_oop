using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach_v1
{
    class Horse : Figure
    {
        public Horse(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {

            FigureType = FigureTypes.horse;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.horse_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.horse_white;
            }


        }

        public override List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles)
        {
            Console.WriteLine(figure.CurrentTile.Coordinates["X"] + "  " + figure.CurrentTile.Coordinates["Y"]);

            List<Tile> PossibleMoves = new List<Tile>();
            List<Tile> TilesToRemove = new List<Tile>();


            foreach (Tile tile in Tiles)
            {
                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] - 2)
                {
                    Console.WriteLine(tile.Coordinates["Y"] + " " + tile.ID);
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                    
                }

                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] + 2)
                {
                    Console.WriteLine(tile.Coordinates["Y"] + " " + tile.ID);
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
                    Console.WriteLine(tile.Coordinates["Y"] + " " + tile.ID);
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 2)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 2)
                    {
                        PossibleMoves.Add(tile);
                    }

                }

                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] - 1)
                {
                    Console.WriteLine(tile.Coordinates["Y"] + " " + tile.ID);
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 2)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 2)
                    {
                        PossibleMoves.Add(tile);
                    }

                }
            }

            //welche felder entfernt werden müssen da sie nicht zugänglich sind
            foreach (Tile item in PossibleMoves)
            {

                if (item.CurrenFigure != null)
                {

                    if (item.CurrenFigure.FigureColor == figure.FigureColor)
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
