using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Schach_v1
{
    public delegate void EventTypeTileClicked(Tile clickedTile);

    public class Tile : Panel
    {
        public event EventTypeTileClicked TileClicked;

        // MACO: Public Felder sind unüblich, weil man da keine genaue Kontrolle über
        // den Zugriff hat. -> besser als Eigenschaft umsetzen! (2)

        // MACO: Es gibt nur die X- und Y-Koordinate, da ist ein Dictionary dezent
        // ein Overkill (verkompliziert nur den Code). Warum nicht einfach über zwei
        // Eigenschaften umsetzen?
        //X und Y Coordinaten des Tiles
        public int X, Y;


        //die Daraufliegende Figur
        public Figure CurrentFigure = null;

        public Tile(Size tileSize, Color Color, Point Coords)
        {
            //Setzung der Hintergrundfarbe
            BackColor = Color;

            //Breite und höhe der Tiles anhand der Übergebenden Parameter
            Width = tileSize.Width;
            Height = tileSize.Height;

            // MACO: Auch hier finde ich ein int[] für zwei Koordinaten überkompliziert.
            // Viel einfacher wären doch zwei Parameter jeweils für x und y oder von
            // mir aus etwas vom Typ "Point", das beide Koordinaten beinhaltet?
            //Coordinaten des Tile's
<<<<<<< HEAD
            X = Coords.X;
            Y = Coords.Y;
            
=======
            Coordinates.Add("X", Coords[0]);
            Coordinates.Add("Y", Coords[1]);
>>>>>>> 2a02e19f3548dfaf48b0c6802bcbf83765365052
        }

        protected override void OnClick(EventArgs e)
        {
            TileClicked?.Invoke(this);
        }
    }
}
