using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Schach_v1
{
    class Queen : Figure
    {
        public Queen( Tile startingTile, List<Tile> Tiles) : base( startingTile, Tiles)
        {
           

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.queen_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.queen_white;
            }
        }


        public override List<Tile> GetPossibleMoves()
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
                        foreach (Tile tile in this.BoardTiles)
                        {
                            //ob gleiche X position und ob tile Y kleiner als Figure Y
                            if (tile.X == this.CurrentTile.X && tile.Y < this.CurrentTile.Y)
                            {
                                MovesInDirection.Add(tile);
                            }
                        }


                        //gehen Hand in Hand Orderby und Reverse (lambda)

                        //Sortiert die Tiles AUSTEIGEND
                        MovesInDirection.OrderBy(x => x.Y);

                        //ändert die Reiehnfolge auf ABSTEIGEND
                        MovesInDirection.Reverse();

                        // MACO: (4) Da ihr dieses foreach sogar in mehreren Klassen
                        // brauchst wäre eine Überlegung, die Methode, in die ihr das
                        // auslagert in der Oberklasse zu schreiben, damit sie geerbt 
                        // werden kann (sonst habt ihr sie ja doppelt). Da sie dann 
                        // aber alle Klassen erben (und es brauchen sie ja nicht alle)
                        // könntet ihr euch auch überlegen, noch eine Zwischenklasse
                        // zu modellieren für jene Figuren, die sich ohne Begrenzung
                        // in bestimmte Richtungen bewegen können. (5)
                        //geht jedes Tile in der richtigen Reihenfolge durch
                        foreach (Tile tile in MovesInDirection)
                        {
                            //wenn das Feld leer ist wird es hinzugefügt
                            if (tile.CurrentFigure == null)
                            {
                                PossibleMoves.Add(tile);
                            }
                            //falls nicht wird gecheckt welche FigureColor die Figur hat
                            else
                            {
                                //wenn sie die gleiche Farbe hat wird abgebrochen
                                if (tile.CurrentFigure.FigureColor == this.FigureColor)
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
                        foreach (Tile tile in this.BoardTiles)
                        {
                            //Ob auf der gleichen Y aber größere X
                            if (tile.Y == this.CurrentTile.Y && tile.X > this.CurrentTile.X)
                            {
                                MovesInDirection.Add(tile);
                            }
                        }

                        //Sortiert die Tiles AUSTEIGEND
                        MovesInDirection.OrderBy(x => x.X);

                        //geht jedes Tile in der richtigen Reihenfolge durch
                        foreach (Tile tile in MovesInDirection)
                        {
                            //wenn das Feld leer ist wird es hinzugefügt
                            if (tile.CurrentFigure == null)
                            {
                                PossibleMoves.Add(tile);
                            }
                            //falls nicht wird gecheckt welche FigureColor die Figur hat
                            else
                            {
                                //wenn sie die gleiche Farbe hat wird abgebrochen
                                if (tile.CurrentFigure.FigureColor == this.FigureColor)
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
                        foreach (Tile tile in this.BoardTiles)
                        {
                            //ob gleiche X position und ob tile Y größer als Figure Y
                            if (tile.X == this.CurrentTile.X && tile.Y > this.CurrentTile.Y)
                            {
                                MovesInDirection.Add(tile);
                            }
                        }

                        //Sortiert die Tiles AUSTEIGEND
                        MovesInDirection.OrderBy(x => x.X);

                        //geht jedes Tile in der richtigen Reihenfolge durch
                        foreach (Tile tile in MovesInDirection)
                        {
                            //wenn das Feld leer ist wird es hinzugefügt
                            if (tile.CurrentFigure == null)
                            {
                                PossibleMoves.Add(tile);
                            }
                            //falls nicht wird gecheckt welche FigureColor die Figur hat
                            else
                            {
                                //wenn sie die gleiche Farbe hat wird abgebrochen
                                if (tile.CurrentFigure.FigureColor == this.FigureColor)
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
                        foreach (Tile tile in this.BoardTiles)
                        {
                            //Ob auf der gleichen Y aber kleinere X
                            if (tile.Y == this.CurrentTile.Y && tile.X < this.CurrentTile.X)
                            {
                                MovesInDirection.Add(tile);
                            }
                        }

                        //Sortiert die Tiles AUSTEIGEND
                        MovesInDirection.OrderBy(x => x.X);

                        //ändert die Reihenfolge auf AUSTEIGEND
                        MovesInDirection.Reverse();

                        //geht jedes Tile in der richtigen Reihenfolge durch
                        foreach (Tile tile in MovesInDirection)
                        {
                            //wenn das Feld leer ist wird es hinzugefügt
                            if (tile.CurrentFigure == null)
                            {
                                PossibleMoves.Add(tile);
                            }
                            //falls nicht wird gecheckt welche FigureColor die Figur hat
                            else
                            {
                                //wenn sie die gleiche Farbe hat wird abgebrochen
                                if (tile.CurrentFigure.FigureColor == this.FigureColor)
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
            // MACO: Auch das braucht ihr in mehreren Klassen. -> in Methode aus-
            // lagern und diese in geeignete Oberklasse! (5)
            foreach (Tile tile in this.BoardTiles)

            {
                if (this.CurrentTile.X + this.CurrentTile.Y == tile.X + tile.Y)
                {
                    //nur die Tiles oben rechts und unten links
                    if (tile.Y < this.CurrentTile.Y && tile.X > this.CurrentTile.X)
                    {
                        MovesUpperRight.Add(tile);
                    }
                    //unten links
                    else if (tile.Y > this.CurrentTile.Y && tile.X < this.CurrentTile.X)
                    {
                        MovesLowerLeft.Add(tile);
                    }
                }

                //nur die Tiles oben links
                if (this.CurrentTile.Y - this.CurrentTile.X == tile.Y - tile.X)
                {
                    if (tile.Y < this.CurrentTile.Y && tile.X < this.CurrentTile.X)
                    {
                        MovesUpperLeft.Add(tile);
                    }
                    else if (tile.Y > this.CurrentTile.Y && tile.X > this.CurrentTile.X)
                    {
                        MovesLowerRight.Add(tile);
                    }
                }
            }

            //OBEN RECHTS SORTIERUNG UND ENTFERNUNG
            //Sortiert die Tiles AUSTEIGEND
            MovesUpperRight.OrderBy(x => x.X);

            //ändert die Tiles ABSTEIGEND
            MovesUpperRight.Reverse();

            //geht jedes Tile in der richtigen Reihenfolge durch
            foreach (Tile tile in MovesUpperRight)
            {
                //wenn das Feld leer ist wird es hinzugefügt
                if (tile.CurrentFigure == null)
                {
                    PossibleMoves.Add(tile);
                }
                //falls nicht wird gecheckt welche FigureColor die Figur hat
                else
                {
                    //wenn sie die gleiche Farbe hat wird abgebrochen
                    if (tile.CurrentFigure.FigureColor == this.FigureColor)
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
            MovesLowerRight.OrderBy(x => x.X);

            //geht jedes Tile in der richtigen Reihenfolge durch
            foreach (Tile tile in MovesLowerRight)
            {
                //wenn das Feld leer ist wird es hinzugefügt
                if (tile.CurrentFigure == null)
                {
                    PossibleMoves.Add(tile);
                }
                //falls nicht wird gecheckt welche FigureColor die Figur hat
                else
                {
                    //wenn sie die gleiche Farbe hat wird abgebrochen
                    if (tile.CurrentFigure.FigureColor == this.FigureColor)
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
            MovesLowerLeft.OrderBy(x => x.X);

            //geht jedes Tile in der richtigen Reihenfolge durch
            foreach (Tile tile in MovesLowerLeft)
            {
                //wenn das Feld leer ist wird es hinzugefügt
                if (tile.CurrentFigure == null)
                {
                    PossibleMoves.Add(tile);
                }
                //falls nicht wird gecheckt welche FigureColor die Figur hat
                else
                {
                    //wenn sie die gleiche Farbe hat wird abgebrochen
                    if (tile.CurrentFigure.FigureColor == this.FigureColor)
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
            MovesUpperLeft.OrderBy(x => x.X);

            //ändert die Tiles ABSTEIGEND
            MovesUpperLeft.Reverse();

            //geht jedes Tile in der richtigen Reihenfolge durch
            foreach (Tile tile in MovesUpperLeft)
            {
                //wenn das Feld leer ist wird es hinzugefügt
                if (tile.CurrentFigure == null)
                {
                    PossibleMoves.Add(tile);
                }
                //falls nicht wird gecheckt welche FigureColor die Figur hat
                else
                {
                    //wenn sie die gleiche Farbe hat wird abgebrochen
                    if (tile.CurrentFigure.FigureColor == this.FigureColor)
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
