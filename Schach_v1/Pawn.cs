using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Schach_v1
{
    public class Pawn : Figure
    {

        public Pawn(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {

            FigureType = FigureTypes.pawn;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.pawn_black;
            }
            else 
            {
                BackgroundImage = Properties.Resources.pawn_white;
            }


            
            
        }
        public override List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles)
        {

            List<Tile> PossibleMoves = new List<Tile>();
            List<Tile> TilesToRemove = new List<Tile>();


            //ALLE möglichen felder, auch wenn sie belegt sind
            foreach (Tile tile in Tiles)
            {
                //nur wenn pawn schwarz ist kann er nach unten
                if (figure.FigureColor == Color.Black)
                {
                    //geradeaus
                    if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"])
                    {
                        if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 1)
                        {
                            PossibleMoves.Add(tile);
                        }
                        if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 2 && figure.CurrentTile.Coordinates["Y"] == 1)
                        {
                            PossibleMoves.Add(tile);
                        }
                    }

                    
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 1)
                    {
                        if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] - 1 && tile.CurrenFigure != null)
                        {
                            PossibleMoves.Add(tile);
                        }

                        if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] + 1 && tile.CurrenFigure != null)
                        {
                            PossibleMoves.Add(tile);
                        }
                    }
                    
                }
                //nur wenn pawn weiss ist kann er nach oben
                else
                {
                    if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"])
                    {
                        if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 1)
                        {
                            PossibleMoves.Add(tile);
                        }
                        if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 2 && figure.CurrentTile.Coordinates["Y"] == 6)
                        {
                            PossibleMoves.Add(tile);
                        }
                    }

                     if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 1)
                    {
                        if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] - 1 && tile.CurrenFigure != null )
                        {
                            PossibleMoves.Add(tile);
                        }

                        if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] + 1 && tile.CurrenFigure != null)
                        {
                            PossibleMoves.Add(tile);
                        }
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
