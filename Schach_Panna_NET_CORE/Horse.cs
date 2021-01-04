using System.Collections.Generic;
using System.Drawing;

namespace Schach_v1
{
    class Horse : Figure
    {
        public Horse( Tile startingTile, List<Tile> Tiles) : base( startingTile, Tiles)
        {


            if (FigureColor == Color.Black)
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\horse_black.png");
            }
            else
            {
                BackgroundImage = Bitmap.FromFile(@"..\..\..\img\horse_white.png");
            }
        }

        public override List<Tile> GetPossibleMoves()
        {
            List<Tile> PossibleMoves = new List<Tile>();
            List<Tile> TilesToRemove = new List<Tile>();
            

            // MACO: Ich fürchte ihr tut euch mit Listen zum Organisieren der Tiles
            // keinen Gefallen. Das dauernde komplette Durchlaufen der Liste und die
            // vielen Ifs zur Feststellung, ob man jetzt gerade das richtige Tile in
            // der Hand hat, könntet ihr euch sparen, wenn ihr hier eine besser für
            // dieses Problem geeignetet Datenstruktur verwendet. Überlegung - mehr-
            // dimensionales Array (2-dimensional, Matrix), das würde Zugriff über
            // Indizes bieten, was für euch echt praktisch wäre.
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays
            // https://www.tutorialspoint.com/csharp/csharp_multi_dimensional_arrays.htm
            // Antwort: HAb ich mir mitten im Projekt auch gedacht, aber alles nochmal neu umzustrukturieren war uns zu viel Arbeit

            foreach (Tile tile in this.BoardTiles)

            {


                if (tile.X == this.CurrentTile.X - 2)
                {

                    if (tile.Y == this.CurrentTile.Y + 1)

                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Y== this.CurrentTile.Y - 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.X == this.CurrentTile.X + 2)
                {

                    if (tile.Y == this.CurrentTile.Y + 1)

                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Y == this.CurrentTile.Y - 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.X == this.CurrentTile.X + 1)
                {

                    if (tile.Y == this.CurrentTile.Y + 2)

                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Y == this.CurrentTile.Y - 2)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.X== this.CurrentTile.X - 1)
                {

                    if (tile.Y == this.CurrentTile.Y + 2)

                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Y == this.CurrentTile.Y - 2)
                    {
                        PossibleMoves.Add(tile);
                    }
                }
            }

            //welche felder entfernt werden müssen da sie nicht zugänglich sind
            foreach (Tile item in PossibleMoves)
            {
                if (item.CurrentFigure != null)
                {

                    if (item.CurrentFigure.FigureColor == this.FigureColor)

                    {
                        TilesToRemove.Add(item);
                    }
                }
            }

            foreach (Tile tileToRemove in TilesToRemove)
            {
                PossibleMoves.Remove(tileToRemove);
            }

            return PossibleMoves;
        }

    }
}
