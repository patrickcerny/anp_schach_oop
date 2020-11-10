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
                    if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"])
                    {
                        //geraudeaus fahren 8ein einzelnes feld
                        if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 1 && tile.CurrenFigure == null)
                        {

                            PossibleMoves.Add(tile);
                        }

                        //geradeaus fahren wenn das 1. feld nicht belegt ist und man an der startposition ist
                        if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 2 && tile.CurrenFigure == null && figure.CurrentTile.Coordinates["Y"] == 1)
                        {
                            //findet das feld unter dem 2. möglichen zugfeld und fragt ab ob dieses null ist == true => kann besprungen werden
                            Tile tileUnderPawn = Tiles.Find(x => x.Coordinates["X"] == tile.Coordinates["X"] && x.Coordinates["Y"] - 1 == figure.CurrentTile.Coordinates["Y"]);
                            Console.WriteLine("X" + tileUnderPawn.Coordinates["X"] + ", Y" + tileUnderPawn.Coordinates["Y"]);
                            if (tileUnderPawn.CurrenFigure == null)
                            {
                                PossibleMoves.Add(tile);
                            } 
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



            //entfernung der nicht möglichen moves
            foreach (Tile tileToRemove in TilesToRemove)
            {
                PossibleMoves.Remove(tileToRemove);
            }

            return PossibleMoves;
        }
    }


}
