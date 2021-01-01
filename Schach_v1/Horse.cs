using System.Collections.Generic;
using System.Drawing;

namespace Schach_v1
{
    class Horse : Figure
    {
        public Horse(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {
            FigureType = FigureTypes.horse;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.horse_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.horse_white;
            }
        }

        public override List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles)
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
            foreach (Tile tile in Tiles)
            {
                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] - 2)
                {
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] + 2)
                {
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 1)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] + 1)
                {
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 2)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 2)
                    {
                        PossibleMoves.Add(tile);
                    }
                }

                if (tile.Coordinates["X"] == figure.CurrentTile.Coordinates["X"] - 1)
                {
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] + 2)
                    {
                        PossibleMoves.Add(tile);
                    }
                    if (tile.Coordinates["Y"] == figure.CurrentTile.Coordinates["Y"] - 2)
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
                    if (item.CurrentFigure.FigureColor == figure.FigureColor)
                    {
                        TilesToRemove.Add(item);
                    }
                }
            }

            // MACO: Warum tut ihr hier so kompliziert mit einer extra Liste der zu
            // entfernenden herum? Lasst doch einfach die obere Schleife über eine
            // Kopie von PossibleMoves laufen und dann könnt ihr die direkt in der
            // Schleife entfernen.
            // MACO: Warum wird überhaupt erst danach entfernt? Man könnte ja gleich
            // checken, ob eine entsprechende Figur drauf steht und die dann erst gar
            // nicht adden? Da müsste man nur diesen Check in eine geeignete Methode
            // auslagern.
            //entfernung der nicht möglichen moves
            foreach (Tile tileToRemove in TilesToRemove)
            {
                PossibleMoves.Remove(tileToRemove);
            }

            return PossibleMoves;
        }

        // MACO: Ihr habt in diesen Unterklassen superviel doppelten und dreifachen
        // Code. Da müsst ihr euch eine sinnvolle Gliederung überlegen und das besser
        // strukturieren, sodass der Code nur noch 1 Mal vorkommt!
    }
}
