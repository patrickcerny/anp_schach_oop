﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach_v1
{
    class Tower : Figure
    {
        public Tower(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {

            FigureType = FigureTypes.tower;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.tower_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.tower_white;
            }

        }
        public override List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles)
        {

            List<Tile> PossibleMoves = new List<Tile>();
            List<Tile> MovesInDirection = new List<Tile>();

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
            return PossibleMoves;


        }
    }
}
