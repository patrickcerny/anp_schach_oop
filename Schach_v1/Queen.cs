using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach_v1
{
    /// <summary>
    /// DIE MIT ABSTAND HÄSSLICHSTE FIGUR DIE I JE GECODED HOB I WILL NÜMM AS ISCH EINFACH A UNEHELICHES KIND 
    /// ZWISCHEN AM BISHOP UND AM TOWER ES HOT MI ZU VIELE NERVEN GEKOSTEN DIE QUEEN HERZUMKRIGA AAAAAAAAAAAAAAAH
    /// </summary>
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
            List<Tile> MovesInDirection = new List<Tile>();

            List<Tile> MovesUpperRight = new List<Tile>();
            List<Tile> MovesLowerRight = new List<Tile>();
            List<Tile> MovesLowerLeft = new List<Tile>();
            List<Tile> MovesUpperLeft = new List<Tile>();

            //geht 4 mal in alle Directions (oben unten links rechts)
            for (int i = 0; i < 4; i++)
            {

                //Cleared die Liste von dem Vorherigem Durchlauf
                MovesInDirection.Clear();
                switch (i)
                {

                    //nach oben
                    case (0):

                        //geht jedes Tile durch
                        foreach (Tile tile in Tiles)
                        {
                            //ob gleiche X position und ob tile Y kleiner als Figure Y
                            if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] && tile.Coordinates["Y"] < figure.CurrentTile.Coordinates["Y"])
                            {
                                MovesInDirection.Add(tile);
                            }

                            
                        }

                        //Sortiert die Tiles AUSTEIGEND
                        MovesInDirection.OrderBy(x => x.Coordinates["Y"]);

                        //ändert die Reiehnfolge auf ABSTEIGEND
                        MovesInDirection.Reverse();
                        //geht jedes Tile in der richtigen Reihenfolge durch
                        foreach (Tile tile in MovesInDirection)
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
                        break;

                    //nach rechts
                    case (1):
                        foreach (Tile tile in Tiles)
                        {
                            //Ob auf der gleichen Y aber größere X
                            if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] && tile.Coordinates["X"] > figure.CurrentTile.Coordinates["X"])
                            {
                                MovesInDirection.Add(tile);
                            }


                        }


                        //Sortiert die Tiles AUSTEIGEND
                        MovesInDirection.OrderBy(x => x.Coordinates["X"]);

                        

                        //geht jedes Tile in der richtigen Reihenfolge durch
                        foreach (Tile tile in MovesInDirection)
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
                        break;

                    //nach unten
                    case (2):
                        //geht jedes Tile durch
                        foreach (Tile tile in Tiles)
                        {
                            //ob gleiche X position und ob tile Y größer als Figure Y
                            if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] && tile.Coordinates["Y"] > figure.CurrentTile.Coordinates["Y"])
                            {
                                MovesInDirection.Add(tile);
                            }
                        }

                        //Sortiert die Tiles AUSTEIGEND
                        MovesInDirection.OrderBy(x => x.Coordinates["Y"]);

                        

                        //geht jedes Tile in der richtigen Reihenfolge durch
                        foreach (Tile tile in MovesInDirection)
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


                        break;

                    //nach links
                    case (3):
                        foreach (Tile tile in Tiles)
                        {
                            //Ob auf der gleichen Y aber kleinere X
                            if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] && tile.Coordinates["X"] < figure.CurrentTile.Coordinates["X"])
                            {
                                MovesInDirection.Add(tile);
                            }
                        }

                        //Sortiert die Tiles AUSTEIGEND
                        MovesInDirection.OrderBy(x => x.Coordinates["X"]);

                        //ändert die Reihenfolge auf AUSTEIGEND
                        MovesInDirection.Reverse();

                        //geht jedes Tile in der richtigen Reihenfolge durch
                        foreach (Tile tile in MovesInDirection)
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
                        break;
                }
            }
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
                if (figure.CurrentTile.Coordinates["Y"] - figure.CurrentTile.Coordinates["X"] == tile.Coordinates["Y"] - tile.Coordinates["X"])
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
