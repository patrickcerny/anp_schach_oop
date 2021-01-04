using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Schach_v1
{
    class Tower : Figure
    {
        public Tower( Tile startingTile, List<Tile> Tiles) : base( startingTile, Tiles)
        {

          

           


            if (FigureColor == Color.Black)
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\Tower_black.png");
            }
            else
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\Tower_white.png");
            }
        }




        public override List<Tile> GetPossibleMoves()

        {
            List<Tile> PossibleMoves = new List<Tile>();
            List<Tile> MovesInDirection = new List<Tile>();

            // MACO: Auch das braucht ihr in mehreren Klassen. -> in Methode aus-
            // lagern und diese in geeignete Oberklasse! (5)
            //geht vier mal in alle Directions
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

                        //Sortiert die Tiles AUSTEIGEND
                        MovesInDirection.OrderByDescending(x => x.Y);
                        

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
                        MovesInDirection.OrderBy(x => x.Y);

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
                        MovesInDirection.OrderByDescending(x => x.X);


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

            return PossibleMoves;
        }
    }
}
