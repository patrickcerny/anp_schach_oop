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
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\bishop_black.png");
            }
            else
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\bishop_white.png");
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
            
            PossibleMoves.AddRange(this.SortOutMoves(MovesUpperRight.OrderBy(x => x.X).ToList<Tile>()));

            //UNTEN RECHTS SORTIERUNG UND ENTFERNUNG
            
            PossibleMoves.AddRange(this.SortOutMoves(MovesLowerRight.OrderBy(x => x.X).ToList<Tile>()));

            //UNTEN LINKS SORTIERUNG UND ENTFERNUNG
            
            PossibleMoves.AddRange(this.SortOutMoves(MovesLowerLeft.OrderByDescending(x => x.X).ToList<Tile>()));


            //OBEN LENKS SORTIERUNG UND ENTFERNUNG
            
            PossibleMoves.AddRange(this.SortOutMoves(MovesUpperLeft.OrderByDescending(x => x.X).ToList<Tile>()));


           
            return PossibleMoves;
        }
    }
}
