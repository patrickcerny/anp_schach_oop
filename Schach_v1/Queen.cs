using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach_v1
{
    class Queen : Figure
    {
        public Queen(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {
            
            FigureType = FigureTypes.queen;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.queen_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.queen_white;
            }


        }


        public override List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles)
        {

            List<Tile> PossibleMoves = new List<Tile>();
            List<Tile> TilesToRemove = new List<Tile>();


            //ALLE möglichen felder, auch wenn sie belegt sind
            foreach (Tile tile in Tiles)
            {

                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"])
                {
                    PossibleMoves.Add(tile);
                }

                if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"])
                {
                    PossibleMoves.Add(tile);
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
