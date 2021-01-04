using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Schach_v1
{
    class Bishop : Figure
    {
        public Bishop( Tile startingTile, List<Tile> Tiles) : base( startingTile, Tiles)
        {
           

            if (FigureColor == Color.Black)
            {
                BackgroundImage =Image.FromFile(@"C:\\Users\\patri\\Desktop\\Schach_Test\\Schach_Test\\img\\king_black.png");
            }
            else
            {
                BackgroundImage = Image.FromFile(@"C:\\Users\\patri\\Desktop\\Schach_Test\\Schach_Test\\img\\king_black.png");
            }
        }

        public override List<Tile> GetPossibleMoves()
        {
            List<Tile> PossibleMoves = new List<Tile>();

            List<Tile> MovesUpperRight = new List<Tile>();
            List<Tile> MovesLowerRight = new List<Tile>();
            List<Tile> MovesLowerLeft = new List<Tile>();
            List<Tile> MovesUpperLeft = new List<Tile>();

            //sorry für dean code, i hobs ned effizienter herkrigt :( es isch 2:28 und i HEUL weil der Code so hässlich isch, but u gotta do what u gotta do

            //checkt die  anzahl der möglichen züge nach oben
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

                
                if (this.CurrentTile.Y - this.CurrentTile.X  == tile.Y - tile.X)
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
            //Sortiert die Tiles ABSTEIGEND
            MovesUpperRight.OrderByDescending(x => x.X);



            // MACO: Diese Schleife brauchst du öfters mit genau dem gleichen Inhalt. 
            // -> in Methode auslagern! (4)
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
            //Sortiert die Tiles AUFSTEIGEND
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
            MovesUpperLeft.OrderByDescending(x => x.X);






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
