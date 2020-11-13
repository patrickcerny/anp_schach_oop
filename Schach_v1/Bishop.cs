using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach_v1
{
    class Bishop : Figure
    {
        public Bishop(Size panelSize, Tile startingTile) : base(panelSize, startingTile)
        {

            FigureType = FigureTypes.bishop;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.bishop_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.bishop_white;
            }

             

        }

        public override List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles)
        {

            List<Tile> PossibleMoves = new List<Tile>();


            List<Tile> MovesUpperRight = new List<Tile>();
            List<Tile> MovesLowerRight = new List<Tile>();
            List<Tile> MovesLowerLeft = new List<Tile>();
            List<Tile> MovesUpperLeft = new List<Tile>();

            //sorry für dean code, i hobs ned effizienter herkrigt :( es isch 2:28 und i HEUL weil der Code so hässlich isch, but u gotta do what u gotta do

            //checkt die  anzahl der möglichen züge nach oben
            foreach (Tile tile in Tiles)
            {
                if (figure.CurrentTile.Coordinates["X"] + figure.CurrentTile.Coordinates["Y"] == tile.Coordinates["X"] + tile.Coordinates["Y"])
                {
                                
                    //nur die Tiles oben rechts und unten links
                    if (tile.Coordinates["Y"] < figure.CurrentTile.Coordinates["Y"] && tile.Coordinates["X"] > figure.CurrentTile.Coordinates["X"])
                    {
                                    
                        MovesUpperRight.Add(tile);
                    }
                    //unten links
                    else if (tile.Coordinates["Y"] > figure.CurrentTile.Coordinates["Y"] && tile.Coordinates["X"] < figure.CurrentTile.Coordinates["X"])
                    {
                        MovesLowerLeft.Add(tile);
                    }
                }

                //nur die Tiles oben links
                if (figure.CurrentTile.Coordinates["Y"] - figure.CurrentTile.Coordinates["X"]  == tile.Coordinates["Y"] - tile.Coordinates["X"])
                {


                    if (tile.Coordinates["Y"] < figure.CurrentTile.Coordinates["Y"] && tile.Coordinates["X"] < figure.CurrentTile.Coordinates["X"])
                    {

                        MovesUpperLeft.Add(tile);
                    }
                    else if (tile.Coordinates["Y"] > figure.CurrentTile.Coordinates["Y"] && tile.Coordinates["X"] > figure.CurrentTile.Coordinates["X"])
                    {
                        MovesLowerRight.Add(tile);
                    }
                }



            }

            //OBEN RECHTS SORTIERUNG UND ENTFERNUNG
            //Sortiert die Tiles AUSTEIGEND
            MovesUpperRight.OrderBy(x => x.Coordinates["X"]);

            //ändert die Tiles ABSTEIGEND
            MovesUpperRight.Reverse();

            //geht jedes Tile in der richtigen Reihenfolge durch
            foreach (Tile tile in MovesUpperRight)
            {
                
                //wenn das Feld leer ist wird es hinzugefügt
                if (tile.CurrenFigure == null)
                {
                    PossibleMoves.Add(tile);
                }
                //falls nicht wird gecheckt welche FigureColor die Figur hat
                else
                {
                    //wenn sie die gleiche Farbe hat wird abgebrochen
                    if (tile.CurrenFigure.FigureColor == figure.FigureColor)
                    {
                        break;
                    }
                    //ansonsten Hinzugefügt und abgebrochen
                    else
                    {
                        PossibleMoves.Add(tile);
                        break;
                    }
                }
            }


            //UNTEN RECHTS SORTIERUNG UND ENTFERNUNG
            //Sortiert die Tiles AUSTEIGEND
            MovesLowerRight.OrderBy(x => x.Coordinates["X"]);

            

            //geht jedes Tile in der richtigen Reihenfolge durch
            foreach (Tile tile in MovesLowerRight)
            {
                
                //wenn das Feld leer ist wird es hinzugefügt
                if (tile.CurrenFigure == null)
                {
                    PossibleMoves.Add(tile);
                }
                //falls nicht wird gecheckt welche FigureColor die Figur hat
                else
                {
                    //wenn sie die gleiche Farbe hat wird abgebrochen
                    if (tile.CurrenFigure.FigureColor == figure.FigureColor)
                    {
                        break;
                    }
                    //ansonsten Hinzugefügt und abgebrochen
                    else
                    {
                        PossibleMoves.Add(tile);
                        break;
                    }
                }
            }

            //UNTEN LINKS SORTIERUNG UND ENTFERNUNG
            //Sortiert die Tiles AUSTEIGEND
            MovesLowerLeft.OrderBy(x => x.Coordinates["X"]);



            //geht jedes Tile in der richtigen Reihenfolge durch
            foreach (Tile tile in MovesLowerLeft)
            { 
                //wenn das Feld leer ist wird es hinzugefügt
                if (tile.CurrenFigure == null)
                {
                    PossibleMoves.Add(tile);
                }
                //falls nicht wird gecheckt welche FigureColor die Figur hat
                else
                {
                    //wenn sie die gleiche Farbe hat wird abgebrochen
                    if (tile.CurrenFigure.FigureColor == figure.FigureColor)
                    {
                        break;
                    }
                    //ansonsten Hinzugefügt und abgebrochen
                    else
                    {
                        PossibleMoves.Add(tile);
                        break;
                    }
                }
            }

            //OBEN LENKS SORTIERUNG UND ENTFERNUNG
            //Sortiert die Tiles AUSTEIGEND
            MovesUpperLeft.OrderBy(x => x.Coordinates["X"]);

            //ändert die Tiles ABSTEIGEND
            MovesUpperLeft.Reverse();

            //geht jedes Tile in der richtigen Reihenfolge durch
            foreach (Tile tile in MovesUpperLeft)
            {
                
                //wenn das Feld leer ist wird es hinzugefügt
                if (tile.CurrenFigure == null)
                {
                    PossibleMoves.Add(tile);
                }
                //falls nicht wird gecheckt welche FigureColor die Figur hat
                else
                {
                    //wenn sie die gleiche Farbe hat wird abgebrochen
                    if (tile.CurrenFigure.FigureColor == figure.FigureColor)
                    {
                        break;
                    }
                    //ansonsten Hinzugefügt und abgebrochen
                    else
                    {
                        PossibleMoves.Add(tile);
                        break;
                    }
                }
            }


            return PossibleMoves;
        }

    }
}
